using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api_AIKO.DatabaseInitialization;
using Api_AIKO.Models;

namespace Api_AIKO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            string parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;

            if (!string.IsNullOrEmpty(parentDirectory))
            {
                // Combinar o caminho do diretório pai com a pasta "Banco"
                string dataDirectory = Path.Combine(parentDirectory, "Banco");

                // Definir o diretório de dados
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
            }

        }

        public DbSet<Line> Lines { get; set; } = default!;
        public DbSet<Stop> stops { get; set; } = default!;
        public DbSet<Vehicle> vehicles { get; set; } = default!;
        public DbSet<VehiclePosition> vehiclesPositions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
