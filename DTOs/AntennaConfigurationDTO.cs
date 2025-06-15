namespace ReadAndVerify.DTOs
{
    public class AntennaConfigurationDTO
    {
        public int PortNumber { get; set; }        // Ej: 1, 2, 3, 4
        public bool IsEnabled { get; set; }        // ¿Se usa esta antena?
        public int PowerDbm { get; set; }       // Ej: 20.0 dBm
        public int SensitivityDbm { get; set; } // Ej: -70.0 dBm
    }
}
