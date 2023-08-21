using ApplicationCore.Command.Features.Chemicals.Commands;
using ApplicationCore.Domain.Shared.Exceptions;
using ApplicationCoreQuery.DataModel.Chemicals;
using AutoFixture.Xunit2;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Tests.Chemicals
{
    [Collection(nameof(ApplicationCollection))]
    public class AddChemicalTests
    {
        private readonly WebApiFixture _fixture;
        public AddChemicalTests(WebApiFixture fixture) => _fixture = fixture;

        [Theory, AutoData]
        public async void AddChemical_Success(AddChemicalCommand command)
        {
            // Arrange
            var types = _fixture.ChemicalTypes[0];
            command.ChemicalTypeId = types.Id;

            // Act
            var expectResult = await _fixture.SendMediatorAsync(FakePrincipals.Admin, command);

            // Assert create succesfully
            Assert.NotEqual(expectResult, Guid.Empty);

            // Assert create correctly information
            var actualChemical = await _fixture.QueryDbAsync(
                        async context => await context.Set<Chemical>()
                                                      .Where(p => p.Id == expectResult)
                                                      .FirstOrDefaultAsync());

            Assert.Equal(actualChemical?.Name, command.Name);
            Assert.Equal(actualChemical?.PreHarvestIntervalInDays, command.PreHarvestIntervalInDays);
            Assert.Equal(actualChemical?.ActiveIngredient, command.ActiveIngredient);
        }

        [Theory, AutoData]
        public async void AddChemical_NotFoundChemicalType_ThrowEx(AddChemicalCommand command)
        {
            //Arrange
            var expectedMessage = $"Chemical type {command.ChemicalTypeId} is not found";

            // Act
            async Task expectResult() => await _fixture.SendMediatorAsync(FakePrincipals.Admin, command);

            // Assert
            var result =  await Assert.ThrowsAsync<BusinessException>(expectResult);
            Assert.Equal(result.Message, expectedMessage);

        }
    }
}
