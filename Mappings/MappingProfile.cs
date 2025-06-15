using AutoMapper;
using ReadAndVerify.Models;
using ReadAndVerify.DTOs;

namespace ReadAndVerify.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Reader, ReaderDTO>().ReverseMap();
            CreateMap<ReaderConfiguration, ReaderConfigurationDTO>().ReverseMap();
            CreateMap<AntennaConfiguration, AntennaConfigurationDTO>().ReverseMap();
        }
    }
}
