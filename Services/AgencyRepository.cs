using AtlantisPortals.API.DBContexts;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Helpers;
using AtlantisPortals.API.Models;
using AtlantisPortals.API.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public class AgencyRepository : IAgencyRepository, IDisposable
    {
        private readonly DataContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public AgencyRepository(DataContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<PagedList<Agency>> GetAgencies(AgenciesParam agenciesParameters)
        {
            if (agenciesParameters == null)
            {
                throw new ArgumentNullException(nameof(agenciesParameters));
            }

            var collection = _context.Agencies as IQueryable<Agency>;

            if (!string.IsNullOrWhiteSpace(agenciesParameters.Ministry))
            {
                var ministry = agenciesParameters.Ministry.Trim();
                collection = collection.Where(a => a.Ministry.Contains(ministry));
            }

            if (!string.IsNullOrWhiteSpace(agenciesParameters.SearchQuery))
            {

                var searchQuery = agenciesParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Ministry.Contains(searchQuery)
                    || a.Name.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(agenciesParameters.OrderBy))
            {
                // get property mapping dictionary
                var agencyPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<AgencyDto, Agency>();

                collection = collection.ApplySort(agenciesParameters.OrderBy,
                    agencyPropertyMappingDictionary);
            }

            return await PagedList<Agency>.CreateAsync(collection, agenciesParameters.PageNumber, agenciesParameters.PageSize);

        }

        public async Task<Agency> GetAgency(int id)
        {
            var agency = await _context.Agencies.FirstOrDefaultAsync(e => e.Id == id);

            return agency;
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
