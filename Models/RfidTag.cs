using System.ComponentModel.DataAnnotations;

namespace ReadAndVerify.Models
{
    public class RfidTag
    {
        [Key]
        public int Id { get; set; }
        public string EPC { get; set; } = string.Empty;
        public string TID { get; set; } = string.Empty;
        public ChipProfile ChipType { get; set; }
        public int RssiMin { get; set; }
        public int RssiMax { get; set; }
        public int RssiCurrent { get; set; }

        public LockStatus? EpcLock { get; set; }
        public LockStatus? AccessLock { get; set; }
        public LockStatus? KillLock { get; set; }
        public LockStatus? UserLock { get; set; }

        public DateTime ReadTimestamp { get; set; } = DateTime.Now;
        public int ReaderId { get; set; }
        public Reader Reader { get; set; } = null!; // Relación con Reader



    }
    public enum LockStatus
    {
        Unlocked = 0,
        Locked = 1,
        PermaLocked = 2,
    }
}
