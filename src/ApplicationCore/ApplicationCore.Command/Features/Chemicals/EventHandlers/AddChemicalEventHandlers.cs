using ApplicationCore.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Command.Features.Chemicals.EventHandlers
{
    public class AddChemicalEventHandlers : INotificationHandler<ChemicalAddedEvent>
    {
        
        public AddChemicalEventHandlers()
        {
          
        }

        public async Task Handle(ChemicalAddedEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}
