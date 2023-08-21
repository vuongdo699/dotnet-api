using ApplicationCore.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Entities.ChemincalAggregate
{
    public class ChemicalType : SimpleEntity<int>
    {
        public string Name { get; set; }

        protected ChemicalType()
        {

        }

        public ChemicalType(string name)
        {
            Name = name;
        }
    }
}
