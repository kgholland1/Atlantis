using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;

namespace AtlantisPortals.API.Profiles
{
    public class AgencyProfile : Profile
    {
        public AgencyProfile()
        {
            CreateMap<Agency, AgencyDto>();
            CreateMap<AgencyForCreationDto, Agency>();
            CreateMap<AgencyForUpdateDto, Agency>();
        }
    }
}
