using ApplicationCoreQuery.DataModel.Chemicals;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Query.Chemicals.Models
{
    public class ChemicalVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ChemicalType { get; set; }
        public string PreHarvestIntervalInDays { get; set; }
        public string ActiveIngredient { get; set; }
        public string CreationDate { get; set; }
        public string modificationDate { get; set; }
        public string DeletionDate { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Chemical, ChemicalVm>()
                   .ForMember(x => x.ChemicalType, opt => opt.MapFrom(x => x.Type.Name));
            }
        }
    }
}
