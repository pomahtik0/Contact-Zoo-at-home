using AutoMapper;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Dto;

namespace Contact_zoo_at_home.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;

            CreateMap<CustomerUser, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, IndividualOwnerSpecialSettingsDto>()
                .ReverseMap();

            CreateMap<Pet, DisplayPetsDto>()
                .ForMember( dest => dest.Breed, opt => opt.MapFrom(src => src.Breed.Name))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name));

            CreateMap<Pet, CreateRedactPetDto>()
                .ReverseMap();
            //CreateMap<CustomerUser, UserProfileDTO>()
            //    //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactPhone))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ReverseMap();

            //CreateMap<IndividualOwner, IndividualPetOwnerUserProfileDTO>()
            //    //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactPhone))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //    .ReverseMap();

            //AllowNullCollections = true;
            //CreateMap<Pet, ShowPetDTO>()
            //    //.ForMember(dest =>dest.Species, opt=>opt.MapFrom(src=>$"{src.Species} {src.SubSpecies}"));
            //    ;

            //CreateMap<Pet, SimplePetCardDTO>()
            //    //.ForMember(dest => dest.Species, opt => opt.MapFrom(src => $"{src.Species} {src.SubSpecies}"))
            //    //.ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{(src.Owner is Company ? "Company" : "Individual Owner")} \"{src.Owner.FullName}\""));
            //    ;

            //CreateMap<Pet, ComplexPetCardDTO>()
            //    .IncludeBase<Pet, SimplePetCardDTO>()
            //    .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner.Id))
            //    .ForMember(dest => dest.PetOptions, opt => opt.MapFrom(src => src.PetOptions));

            //CreateMap<Pet, PetCardForCartDTO>()
            //    .ForMember(dest => dest.PetId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner.Id))
            //    //.ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName));
            //    ;

            //CreateMap<ExtraPetOption, ExtraPetOptionsDTO>()
            //    .ReverseMap();

            //CreateMap<Pet, CreateOrRedactPetModel>()
            //    .ForMember(dest => dest.PetOptions, opt => opt.MapFrom(src => src.PetOptions))
            //    .ReverseMap();
        }
    }
}
