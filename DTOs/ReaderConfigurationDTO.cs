namespace ReadAndVerify.DTOs
{
    public class ReaderConfigurationDTO
    {
        public bool IncludeTid { get; set; }
        public List<AntennaConfigurationDTO> Antennas { get; set; } = new();
    }
}
