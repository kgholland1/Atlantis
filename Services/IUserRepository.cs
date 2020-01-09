using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public interface IUserRepository
    {
        void Delete<T>(T entity) where T : class;
        Task<User> GetUser(int userID);
        Task<List<UserForListDto>> AgencyUsers(int agencyId);
        Task<List<UserForListDto>> AdminUsers();
        Task<bool> Save();
    }
}
