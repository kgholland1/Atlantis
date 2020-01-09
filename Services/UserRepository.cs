using AtlantisPortals.API.DBContexts;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int userID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == userID);

            return user;
        }
        public async Task<List<UserForListDto>> AgencyUsers(int agencyId)
        {
            var userList = await (from user in _context.Users
                                  where user.AgencyId == agencyId && user.IsClient == true
                                  orderby user.UserName
                                  select new UserForListDto
                                  {
                                      Id = user.Id,
                                      FullName = user.FullName,
                                      Email = user.Email,
                                      PhoneNumber = user.PhoneNumber,
                                      LastActive = user.LastActive,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();

            return userList;
        }

        public async Task<List<UserForListDto>> AdminUsers()
        {
            var userList = await (from user in _context.Users
                                  where user.AgencyId == 0 && user.IsClient == false
                                  orderby user.UserName
                                  select new UserForListDto
                                  {
                                      Id = user.Id,
                                      FullName = user.FullName,
                                      Email = user.Email,
                                      PhoneNumber = user.PhoneNumber,
                                      LastActive = user.LastActive,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();

            return userList;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
