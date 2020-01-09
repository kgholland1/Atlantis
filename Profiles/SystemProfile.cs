using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;

namespace AtlantisPortals.API.Profiles
{
    public class SystemProfile : Profile
    {
        public SystemProfile()
        {
            CreateMap<Ministry, MinistryDto>();
            CreateMap<ReceiptType, ReceiptTypeDto>();
        }

    }
}
