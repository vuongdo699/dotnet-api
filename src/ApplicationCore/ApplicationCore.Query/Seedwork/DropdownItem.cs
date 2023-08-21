using ApplicationCoreQuery.DataModel.Chemicals;
using AutoMapper;

namespace ApplicationCore.Query.Seedwork
{
    public class DropdownItem<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    class DropdownItemMappingProfile : Profile
    {
        public DropdownItemMappingProfile()
        {
            CreateMap<ChemicalType, DropdownItem<int>>();
        }
    }
}
