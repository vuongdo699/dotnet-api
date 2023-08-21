using ApplicationCore.Domain.Events;
using ApplicationCore.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Entities.ChemincalAggregate
{
    public class Chemical : Entity, IAggregateRoot, IConcurrencyCheck, IAuditable, ISoftDeletable
    {
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        public string PreHarvestIntervalInDays { get; set; }
        public int TypeId { get; set; }
        protected Chemical()
        {

        }

        public Chemical(
            string name,
            int typeId,
            string preHarvestIntervalInDays,
            string activeIngredient)
        {
            Name = name;
            ActiveIngredient = activeIngredient;
            PreHarvestIntervalInDays = preHarvestIntervalInDays;
            ActiveIngredient = activeIngredient;
            TypeId = typeId;

            //TODO: Example for domain event 
            AddDomainEvent(new ChemicalAddedEvent(this, "Constructor"));
        }

        public byte[] RowVersion { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationBy { get; set; }
    }
}
