using AutoMapper;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Dto;

namespace Contact_zoo_at_home.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;

            CreateMap<BaseUser, LinkedUserDto>();

            CreateMap<PetComment, PetCommentsDto>();

            CreateMap<CustomerUser, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, IndividualOwnerSpecialSettingsDto>()
                .ReverseMap();

            CreateMap<Pet, DisplayPetsDto>()
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault().Image));

            CreateMap<Pet, ShortDisplayPetDto>()
                .IncludeBase<Pet, DisplayPetsDto>();

            CreateMap<Pet, FullDisplayPetDto>()
                .IncludeBase<Pet, ShortDisplayPetDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<ExtraPetOptionsDTO, ExtraPetOption>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OptionName))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.OptionValue))
                .ReverseMap();

            CreateMap<IndividualOwner, IndividualOwnerPublicProfileDto>();

            CreateMap<CustomerUser, CustomerPublicProfileDto>();

            CreateMap<Pet, CreateRedactPetDto>()
                .ReverseMap();

        }
    }
}
