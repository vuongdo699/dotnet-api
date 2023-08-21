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
    public class GetChemicalTypeDropdownQuery : IRequest<List<DropdownItem<int>>>
    {
        public string Keyword { get; set; }

        class Handler : IRequestHandler<GetChemicalTypeDropdownQuery, List<DropdownItem<int>>>
        {
            private readonly IQueryDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(IQueryDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<List<DropdownItem<int>>> Handle(GetChemicalTypeDropdownQuery request, CancellationToken cancellationToken)
            {
                var query = _dbContext.Set<ChemicalType>().AsQueryable();

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(w => w.Name.Contains(request.Keyword));
                }

                var result = await query.AsNoTracking().ProjectTo<DropdownItem<int>>(_mapper.ConfigurationProvider).ToListAsync();

                return result;
            }
        }
    }
}
