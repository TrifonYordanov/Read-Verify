namespace ReadAndVerify.Models
{
    public class AntennaConfiguration
    {
        public int PortNumber { get; set; }        // Antena 1, 2, 3, 4...
        public bool IsEnabled { get; set; }        // ¿Está activada?
        public double PowerDbm { get; set; }       // Potencia de transmisión
        public double SensitivityDbm { get; set; } // Sensibilidad de recepción
    }
}
