using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Helpers;
using AtlantisPortals.API.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public interface IAgencyRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> Save();

        Task<PagedList<Agency>> GetAgencies(AgenciesParam agenciesParameters);
        Task<Agency> GetAgency(int id);
    }
}
