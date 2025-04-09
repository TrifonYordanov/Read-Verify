using Microsoft.EntityFrameworkCore;
using ReadAndVerify.Data;
using ReadAndVerify.Models;

namespace ReadAndVerify.Repos
{
    public interface IReaderRepository
    {
        Task<List<Reader>> GetAllAsync();
        Task<List<Location>> GetAllLocationsAsync();
        Task AddAsync(Reader reader);
    }

    public class ReaderRepository : IReaderRepository
    {
        private readonly LocalDB _db;

        public ReaderRepository(LocalDB db)
        {
            _db = db;
        }

        public async Task<List<Reader>> GetAllAsync()
        {
            return await _db.Readers
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await _db.Locations
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Reader reader)
        {
            _db.Readers.Add(reader);
            await _db.SaveChangesAsync();
        }
    }
}
