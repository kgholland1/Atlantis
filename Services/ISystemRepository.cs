using AtlantisPortals.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public interface ISystemRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<List<Ministry>> GetMinistryLookups();
        Task<List<ReceiptType>> GetReceiptTypeLookups();
        Task<ReceiptType> GetReceiptType(int id);
    }
}
