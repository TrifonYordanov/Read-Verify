using Microsoft.EntityFrameworkCore;
using ReadAndVerify.Models;

namespace ReadAndVerify.Data
{
    public class LocalDB : DbContext
    {

        public LocalDB(DbContextOptions<LocalDB> options) : base(options)
        {
        }
        public DbSet<RfidTag> RfidTags { get; set; } = null!;
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Tag -> Reader (muchos tags por cada reader)
            modelBuilder.Entity<RfidTag>()
                .HasOne(t => t.Reader)            // un tag tiene un reader
                .WithMany(r => r.TagsRead)        // un reader tiene muchos tags
                .HasForeignKey(t => t.ReaderId);  // clave foránea en el tag

            // Relación Reader -> Location (muchos readers por cada ubicación)
            modelBuilder.Entity<Reader>()
                .HasOne(r => r.Location)             // un reader tiene una location
                .WithMany(l => l.Readers)            // una location tiene muchos readers
                .HasForeignKey(r => r.LocationId);   // clave foránea en el reader
        }
    }
}
