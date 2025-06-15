using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadAndVerify.Models
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public bool IsOnline { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastSeenAt { get; set; } // Última conexión o "ping"

        public string FirmwareVersion { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        // Relación con Location
        public int LocationId { get; set; }
        public Location Location { get; set; }

        // Lecturas realizadas por este lector
        public ICollection<RfidTag> TagsRead { get; set; } = new List<RfidTag>();
    }
}
