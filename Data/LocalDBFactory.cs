using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReadAndVerify.Data
{
    public class LocalDBFactory : IDesignTimeDbContextFactory<LocalDB>
    {
        public LocalDB CreateDbContext(string[] args)
        {
            // Leer configuración desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // importante
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<LocalDB>();
            optionsBuilder.UseSqlServer(connectionString);

            return new LocalDB(optionsBuilder.Options);
        }
    }
}
