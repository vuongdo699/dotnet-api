using ApplicationCore.Domain.Entities.ChemincalAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Events
{
    public class ChemicalAddedEvent : BaseDomainEvent
    {
        public Chemical Chemical { get; }

        public ChemicalAddedEvent(Chemical chemical, string source) : base(source)
        {
            Chemical = chemical;
        }
    }
}
