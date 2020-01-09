using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;

namespace AtlantisPortals.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserLoginSuccessfulDto>();
            CreateMap<User, UserDto>();
            CreateMap<AdminRegisterDto, User>();
            CreateMap<ClientRegisterDto, User>();
        }

    }
}
