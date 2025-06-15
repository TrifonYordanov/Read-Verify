using System.Collections.Generic;

namespace ReadAndVerify.Models
{
    public class ReaderConfiguration
    {
        public List<AntennaConfiguration> Antennas { get; set; } = new();
    }
}
