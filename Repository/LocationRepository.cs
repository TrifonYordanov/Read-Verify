using ReadAndVerify.Models;
using ReadAndVerify.Data;
using Microsoft.EntityFrameworkCore;


namespace ReadAndVerify.Repository

{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location location);
        Task<bool> UpdateLocationAsync(int id, Location location);
        Task<bool> DeleteLocationAsync(int id);
    }
    public class LocationRepository : ILocationRepository
    {
        private readonly LocalDB  _ctx;

        public LocationRepository(LocalDB ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _ctx.Locations.ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            return await _ctx.Locations.FindAsync(id);
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            _ctx.Locations.Add(location);
            await _ctx.SaveChangesAsync();
            return location;
        }

        public async Task<bool> UpdateLocationAsync(int id, Location location)
        {
            if (id != location.Id)
            {
                return false;
            }
            _ctx.Entry(location).State = EntityState.Modified;
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LocationExists(id))
                {
                    return false;
                }
                throw;
            }
            return true;
        }

        public async Task<bool> LocationExists(int id)
        {
            return await _ctx.Locations.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _ctx.Locations.FindAsync(id);
            if (location == null)
            {
                return false;
            }
            _ctx.Locations.Remove(location);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
