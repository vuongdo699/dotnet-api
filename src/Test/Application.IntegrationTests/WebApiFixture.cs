using ApplicationCore.Common.Shared;
using ApplicationCore.Domain.Entities.ChemincalAggregate;
using ApplicationCore.Query.Interfaces;
using Autofac;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Respawn;
using Xunit;
using static ApplicationCore.Common.Shared.ApplicationContext;

namespace Application.IntegrationTests
{
    public class WebApiFixture : IAsyncLifetime
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Checkpoint _checkpoint;

        public List<ChemicalType> ChemicalTypes { get; set; } = new();

        public WebApiFixture()
        {
            _factory = new WebApiFactory();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory",
                },
            };
        }

        public async Task InitializeAsync()
        {
            // reset test db to initial state
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

            await PrepareTestDataAsync();
        }

        private async Task PrepareTestDataAsync()
        {
            //Chemical type
            var chemicalTypes = new List<ChemicalType>() {
                new ChemicalType("Chemical Type 1"),
                new ChemicalType("Chemical Type 2")
            };

            foreach (var type in chemicalTypes)
            {
                await InsertDbAsync(FakePrincipals.Admin, type);
                ChemicalTypes.Add(type);
            }  
        }

        public Task DisposeAsync()
        {
            _factory.Dispose();
            return Task.CompletedTask;
        }

        public Task InsertDbAsync<T>(UserPrincipal principal, params T[] entities) where T : class
        {
            return ExecuteDbContextAsync((db, applicationContext) =>
            {
                AttachTestData(db);

                applicationContext.Principal = principal;

                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        private void AttachTestData(DatabaseContext dbContext)
        {
            ChemicalTypes.ForEach(x => dbContext.Attach(x));
        }

        public Task<T> QueryDbAsync<T>(Func<IQueryDbContext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetRequiredService<IQueryDbContext>()));

        public Task ExecuteDbContextAsync(Func<DatabaseContext, ApplicationContext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetRequiredService<DatabaseContext>(), sp.GetRequiredService<ApplicationContext>()));

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();

            await action(scope.ServiceProvider);
        }

        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();

            var result = await action(scope.ServiceProvider);

            return result;
        }

        public Task<TResponse> SendMediatorAsync<TResponse>(UserPrincipal principal, IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var applicationContext = sp.GetRequiredService<ApplicationContext>();
                applicationContext.Principal = principal;

                var mediator = sp.GetRequiredService<IMediator>();

                return mediator.Send(request);
            });
        }
    }

    class WebApiFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureContainer<ContainerBuilder>(b => { /* test overrides here */ });
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                configBuilder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
            });
        }
    }
}
