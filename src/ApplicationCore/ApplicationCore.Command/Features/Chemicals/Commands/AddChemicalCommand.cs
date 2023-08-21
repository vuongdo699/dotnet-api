using ApplicationCore.Domain.Entities.ChemincalAggregate;
using ApplicationCore.Domain.SeedWork;
using ApplicationCore.Domain.Shared.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Command.Features.Chemicals.Commands
{
    public class AddChemicalCommand : IRequest<Guid>
    {
        public int ChemicalTypeId { get; set; }
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        public string PreHarvestIntervalInDays { get; set; }

        class Handler : IRequestHandler<AddChemicalCommand, Guid>
        {
            private readonly IChemicalRepository _chemicalRepo;
            private readonly ISimpleRepository<ChemicalType, int> _chemicalTypeRepo;

            public Handler(IChemicalRepository chemicalRepo, ISimpleRepository<ChemicalType, int> chemicalTypeRepo)
            {
                _chemicalRepo = chemicalRepo;
                _chemicalTypeRepo = chemicalTypeRepo;
            }

            public async Task<Guid> Handle(AddChemicalCommand request, CancellationToken cancellationToken)
            {
                var chemicalType = await _chemicalTypeRepo.GetByIdAsync(request.ChemicalTypeId);

                if (chemicalType == null)
                {
                    throw new BusinessException($"Chemical type {request.ChemicalTypeId} is not found");
                }

                Chemical chemical = new Chemical(request.Name, chemicalType.Id, request.PreHarvestIntervalInDays, request.ActiveIngredient);
                _chemicalRepo.Add(chemical);
                await _chemicalRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

                return chemical.Id;
            }
        }

        private class Validator : AbstractValidator<AddChemicalCommand>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.Name).NotEmpty();
                RuleFor(cmd => cmd.ChemicalTypeId).NotEmpty();
            }
        }
    }
}
