using AutoMapper;
using ReadAndVerify.DTOs;
using ReadAndVerify.Models;
using ReadAndVerify.Repository;

namespace ReadAndVerify.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repo;
        private readonly IMapper _mapper;

        public ReaderService(IReaderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ReaderDTO>> GetAllAsync()
        {
            var readers = await _repo.GetAllAsync();
            return _mapper.Map<List<ReaderDTO>>(readers);
        }

        public async Task<ReaderDTO?> GetByIdAsync(int id)
        {
            var reader = await _repo.GetByIdAsync(id);
            return reader == null ? null : _mapper.Map<ReaderDTO>(reader);
        }

        public async Task CreateAsync(ReaderDTO readerDto)
        {
            var entity = _mapper.Map<Reader>(readerDto);

            // Buscar la Location existente por nombre
            var existingLocations = await _repo.GetAllLocationsAsync();
            var matchedLocation = existingLocations.FirstOrDefault(l => l.Name == readerDto.LocationName);

            if (matchedLocation is not null)
            {
                entity.LocationId = matchedLocation.Id;
                entity.Location = null; // evitar duplicación por error
            }

            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        public async Task UpdateAsync(ReaderDTO readerDto)
        {
            var existing = await _repo.GetByIdAsync(readerDto.Id);
            if (existing is null) return;

            existing.Name = readerDto.Name;
            existing.IpAddress = readerDto.IpAddress;            
            existing.Location = new Models.Location { Name = readerDto.LocationName };

            await _repo.UpdateAsync(existing);
        }

    }
}
