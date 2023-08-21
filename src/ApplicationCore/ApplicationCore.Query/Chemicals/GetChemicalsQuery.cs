using ApplicationCore.Query.Chemicals.Models;
using ApplicationCore.Query.Interfaces;
using ApplicationCore.Query.Seedwork;
using ApplicationCoreQuery.DataModel.Chemicals;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Query.Chemicals
{
    public class GetChemicalsQuery : IRequest<PaginatedResult<ChemicalVm>>
    {
        public QueryOptions QueryOptions { get; set; }

        class Handler : IRequestHandler<GetChemicalsQuery, PaginatedResult<ChemicalVm>>
        {
            private readonly IQueryDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(IQueryDbContext dbContext,
                IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<PaginatedResult<ChemicalVm>> Handle(GetChemicalsQuery request, CancellationToken cancellationToken)
            {
                var query = _dbContext.Set<Chemical>().AsNoTracking().ProjectTo<ChemicalVm>(_mapper.ConfigurationProvider);

                return await PaginatedResult<ChemicalVm>.CreateAsync(query, request.QueryOptions);
            }
        }
    }
}
