using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace ReadAndVerify.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }


        //Lista de Readers en esta ubicación
        public ICollection<Reader> Readers { get; set; } = new List<Reader>();
    }
}
