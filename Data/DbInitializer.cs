using Api_AIKO.Data;
using Api_AIKO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api_AIKO.DatabaseInitialization
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Aplica todas as migrações pendentes
                context.Database.Migrate();

                // Verifica se já existem dados nas tabelas
                if (context.Lines.Any() || context.stops.Any() || context.vehicles.Any() || context.vehiclesPositions.Any())
                {
                    return;
                }

                var lines = new List<Line>
                {
                    new Line { Name = "Linha 1 - Zona Norte" },
                    new Line { Name = "Linha 2 - Zona Sul" }
                };
                context.Lines.AddRange(lines);
                context.SaveChanges();


                var stops = new List<Stop>
                {
                    // Paradas da Linha 1 - Zona Norte
                    new Stop { Name = "Estação Tietê", Latitude = -23.5257, Longitude = -46.6153 },
                    new Stop { Name = "Av. Cruzeiro do Sul", Latitude = -23.5231, Longitude = -46.6257 },
                    new Stop { Name = "Av. Braz Leme", Latitude = -23.5192, Longitude = -46.6265 },
                    new Stop { Name = "Terminal Casa Verde", Latitude = -23.5105, Longitude = -46.6476 },
                    new Stop { Name = "Estação Barra Funda", Latitude = -23.5270, Longitude = -46.6553 },

                    // Paradas da Linha 2 - Zona Sul
                    new Stop { Name = "Terminal Santo Amaro", Latitude = -23.6483, Longitude = -46.7012 },
                    new Stop { Name = "Av. Adolfo Pinheiro", Latitude = -23.6494, Longitude = -46.6987 },
                    new Stop { Name = "Av. Roque Petroni Jr.", Latitude = -23.6188, Longitude = -46.6976 },
                    new Stop { Name = "Terminal João Dias", Latitude = -23.6480, Longitude = -46.7342 },
                    new Stop { Name = "Estação Santo Amaro", Latitude = -23.6484, Longitude = -46.7009 }
                };
                context.stops.AddRange(stops);
                context.SaveChanges();


                var vehicles = new List<Vehicle>
                {
                    new Vehicle { Name = "Ônibus 1", Model = "Comil Svelto", LineId = 1 },
                    new Vehicle { Name = "Ônibus 2", Model = "Marcopolo Torino", LineId = 2 }
                };
                context.vehicles.AddRange(vehicles);
                context.SaveChanges();

                var vehiclePositions = new List<VehiclePosition>
                {
                    new VehiclePosition { Latitude = -23.5257, Longitude = -46.6153, VehicleId = 1 },
                    new VehiclePosition { Latitude = -23.6483, Longitude = -46.7012, VehicleId = 2 }
                    // Adicione mais posições conforme necessário
                };
                context.vehiclesPositions.AddRange(vehiclePositions);
                context.SaveChanges();
            }
        }

    }
}
