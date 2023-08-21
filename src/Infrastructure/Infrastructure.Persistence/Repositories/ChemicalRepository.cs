using ApplicationCore.Domain.Entities.ChemincalAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ChemicalRepository : Repository<Chemical>, IChemicalRepository
    {
        public ChemicalRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
