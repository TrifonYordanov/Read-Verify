using ReadAndVerify.Models;

namespace ReadAndVerify.Services
{
    public class ChipProfileService
    {
        private readonly List<ChipProfile> profiles = new()
    {
        new ChipProfile
        {
            Name = "Monza R6P",
            TidMask = "E2801160",
            Manufacturer = "Impinj",
            SupportsUserMemory = true,
            LocksAreShared = false
        },
        new ChipProfile
        {
            Name = "Monza M730",
            TidMask = "E280B12B",
            Manufacturer = "Impinj",
            SupportsUserMemory = false,
            LocksAreShared = true
        },
        new ChipProfile
        {
            Name = "Ucode 8",
            TidMask = "E2806894",
            Manufacturer = "NXP",
            SupportsUserMemory = true,
            LocksAreShared = false
        }
    };

        public ChipProfile? GetProfileByTid(string tid)
        {
            return profiles.FirstOrDefault(p => tid.StartsWith(p.TidMask));
        }
    }
}
