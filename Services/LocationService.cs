using AutoMapper;
using ReadAndVerify.DTOs;
using ReadAndVerify.Repository;

namespace ReadAndVerify.Services
{
    public interface ILocationService
    {       
        Task<IEnumerable<LocationDTO>> GetAllLocationsAsync();
        Task<LocationDTO> AddLocationAsyn(LocationDTO locationDTO);
        Task<bool> UpdateLocationAsync(int id, LocationDTO location);
        Task<bool> DeleteLocationAsync(int id);
        


    }
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllLocationsAsync();
            return _mapper.Map<IEnumerable<LocationDTO>>(locations);
        }
        public async Task<LocationDTO> AddLocationAsyn(LocationDTO locationDTO)
        {
            var location = _mapper.Map<Models.Location>(locationDTO);
            var createdLocation = await _locationRepository.CreateLocationAsync(location);
            return _mapper.Map<LocationDTO>(createdLocation);
        }
        public async Task<bool> UpdateLocationAsync(int id, LocationDTO locationDto)
        {
            var existing = await _locationRepository.GetLocationByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = locationDto.Name;

            return await _locationRepository.UpdateLocationAsync(id,existing);
        }
        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null)
                return false;

            return await _locationRepository.DeleteLocationAsync(id);
        }

    }


}

