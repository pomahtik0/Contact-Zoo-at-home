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
            CreateMap<CustomerUser, UserProfileDTO>();
            CreateMap<IndividualPetOwner, IndividualPetOwnerUserProfileDTO>();
        }
    }
}
