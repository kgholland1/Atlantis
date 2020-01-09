using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public interface IUserInfoService
    {
        string UserId { get; set; }
        string Username { get; set; }
    }
}
