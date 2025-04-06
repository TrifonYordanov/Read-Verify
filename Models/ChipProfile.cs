namespace ReadAndVerify.Models
{
    public class ChipProfile
    {
        // Descripción del chip
        public string Name { get; set; } = string.Empty;
        public string TidMask { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;

        // Reglas de operación
        public bool SupportsUserMemory { get; set; }
        public bool LocksAreShared { get; set; }

        public bool CanVerifyLock(string lockName)
        {
            if (LocksAreShared && lockName == "User")
                return false;

            return true;
        }
    }
}
