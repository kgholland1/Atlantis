using AtlantisPortals.API.DBContexts;
using AtlantisPortals.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Services
{
    public class SystemRepository : ISystemRepository
    {
        private readonly DataContext _context;

        public SystemRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<List<Ministry>> GetMinistryLookups()
        {
            return await _context.Ministries.ToListAsync();

        }
        public async Task<List<ReceiptType>> GetReceiptTypeLookups()
        {
            return await _context.ReceiptTypes.ToListAsync();
        }

        public async Task<ReceiptType> GetReceiptType(int id)
        {
            var receiptType = await _context.ReceiptTypes.FirstOrDefaultAsync(e => e.Id == id);

            return receiptType;
        }
    }
}
