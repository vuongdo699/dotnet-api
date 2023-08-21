using ApplicationCore.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCoreQuery.DataModel.Chemicals
{
    public class Chemical : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        public string PreHarvestIntervalInDays { get; set; }
        public int TypeId { get; set; }
        public ChemicalType Type { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
    }
}
