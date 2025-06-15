using Microsoft.EntityFrameworkCore;
using ReadAndVerify.Data;
using ReadAndVerify.Models;

namespace ReadAndVerify.Repository
{
    public interface IReaderRepository
    {
        Task<List<Reader>> GetAllAsync();
        Task<List<Location>> GetAllLocationsAsync();
        Task AddAsync(Reader reader);
        Task<Reader?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(Reader reader);


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
        public async Task<Reader?> GetByIdAsync(int id)
        {
            return await _db.Readers
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task DeleteAsync(int id)
        {
            var reader = await _db.Readers.FindAsync(id);
            if (reader != null)
            {
                _db.Readers.Remove(reader);
                await _db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Reader reader)
        {
            var existing = await _db.Readers
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.Id == reader.Id);

            if (existing is null)
                throw new InvalidOperationException("Reader not found");

            // Mapea propiedades actualizables
            existing.Name = reader.Name;
            existing.IpAddress = reader.IpAddress;
            existing.LocationId = reader.LocationId;
            existing.IsOnline = reader.IsOnline; // ← AÑADIDO

            await _db.SaveChangesAsync();
        }




    }
}
