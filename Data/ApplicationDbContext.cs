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
            EnsureDataDirectoryExists();
        }

        private void EnsureDataDirectoryExists()
        {
            // Obter o caminho do diretório "Banco"
            string dataDirectory = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName, "Banco");

            // Verificar se o diretório existe, e criar se não existir
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            // Definir o diretório de dados para o diretório "Banco"
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
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
