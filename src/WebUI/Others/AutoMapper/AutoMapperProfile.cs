using AutoMapper;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using WebUI.Models.User;

namespace WebUI.Others.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //CreateMap<BaseUser, UserProfileDTO>()
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactPhone))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ContactEmail))
            //    .ReverseMap();

            CreateMap<CustomerUser, UserProfileDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactPhone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ContactEmail))
                .ReverseMap();

            CreateMap<IndividualPetOwner, IndividualPetOwnerUserProfileDTO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactPhone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ContactEmail))
                .ReverseMap();
        }
    }
}
