using System.Linq;
using AutoMapper;
using trialApps.API.DTOs;
using trialApps.API.Models;

namespace trialApps.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {


            CreateMap<User, ListUsersDTO>()
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(data => data.DoB.CalculateAge());
            })
            .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(source => source.Photos.FirstOrDefault(photo => photo.isMain).Url));
            CreateMap<User, DetailUserDTO>()
                .IncludeBase<User, ListUsersDTO>();
            CreateMap<Photo, DetailsUserPhotoDTO>();
            CreateMap<UserForUpdateDTO, User>();

            CreateMap<Photo, PhotoReturnDTO>()
                .IncludeBase<Photo, DetailsUserPhotoDTO>();
                
            CreateMap<PhotoCreationDTO, Photo>();
            CreateMap<UserForRegisterDTO, User>();
        }
    }
}