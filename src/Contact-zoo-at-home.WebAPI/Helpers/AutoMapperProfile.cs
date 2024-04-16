﻿using AutoMapper;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Dto.Contracts;
using Contact_zoo_at_home.Shared.Dto.Notifications;
using Contact_zoo_at_home.Shared.Dto.Pet;
using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;

            CreateMap<PetSpecies, PetSpeciesDto>()
                .ReverseMap();

            CreateMap<StandartUser, LinkedUserDto>()
                .ReverseMap();

            CreateMap<PetComment, PetCommentsDto>()
                .ReverseMap();

            CreateMap<CustomerUser, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, StandartUserSettingsDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage.Image))
                .ReverseMap();

            CreateMap<IndividualOwner, IndividualOwnerSpecialSettingsDto>()
                .ReverseMap();

            CreateMap<Pet, DisplayPetDto>()
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault().Image));

            CreateMap<Pet, DisplayPetShortDto>()
                .IncludeBase<Pet, DisplayPetDto>();

            CreateMap<Pet, DisplayPetFullDto>()
                .IncludeBase<Pet, DisplayPetShortDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<ExtraPetOptionsDTO, ExtraPetOption>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OptionName))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.OptionValue))
                .ReverseMap();

            CreateMap<IndividualOwner, IndividualOwnerPublicProfileDto>();

            CreateMap<CustomerUser, CustomerPublicProfileDto>();

            CreateMap<Pet, CreateRedactPetDto>()
                .ReverseMap();

            CreateMap<CreateStandartContractDto, StandartContract>();

            CreateMap<InnerNotification, NotificationDto>()
                .ForMember(dest => dest.NotificationTargetId, opt => opt.MapFrom(src => src.NotificationTarget.Id));

            CreateMap<InnerRatingNotification, RatingNotificationDto>()
                .IncludeBase<InnerNotification, NotificationDto>()
                .ForMember(dest => dest.RateTargetPetId, opt => opt.MapFrom(src => src.RateTargetPet.Id))
                .ForMember(dest => dest.RateTargetUserId, opt => opt.MapFrom(src => src.RateTargetUser.Id));
                
        }
    }
}
