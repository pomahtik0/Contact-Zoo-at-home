﻿using AutoMapper;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System.Globalization;
using WebUI.Models.Pet;
using WebUI.Models.User;
using WebUI.Models.User.Settings;

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

            AllowNullCollections = true;
            CreateMap<Pet, ShowPetDTO>()
                .ForMember(dest =>dest.Species, opt=>opt.MapFrom(src=>$"{src.Species} {src.SubSpecies}"));

            CreateMap<ExtraPetOption, ExtraPetOptionsDTO>();

            CreateMap<Pet, CreateOrRedactPetModel>()
                .ForMember(dest => dest.PetOptions, opt => opt.MapFrom<IList<ExtraPetOption>>(src => src.PetOptions))
                .ReverseMap();
        }
    }
}
