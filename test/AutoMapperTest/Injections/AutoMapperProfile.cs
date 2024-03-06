using AutoMapper;
using AutoMapperTest.Models;
using Contact_zoo_at_home.Core.Entities.Pets;

namespace AutoMapperTest.Injections
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pet, SimplePetDTO>();
            CreateMap<Pet, ComplexPetDTO>()
             .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName));
        }
    }
}
