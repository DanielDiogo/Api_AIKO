using Api_AIKO.Data;
using Api_AIKO.DatabaseInitialization;
using Api_AIKO.Interfaces;
using Api_AIKO.Models.JwtConfiguration;
using Api_AIKO.Repositories;
using Api_AIKO.services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api_AIKO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database


            var connectionString = Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILineRepository, LineRepository>();
            services.AddScoped<IStopRepository, StopRepository>();
            services.AddScoped<IVehiclePositionRepository, VehiclePositionRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IAuthenticationBusinessLogics, AuthenticationBusinessLogics>();

            // Configurações do JWT

            var jwtConfiguration = Configuration.GetSection("Jwt").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(Configuration.GetSection("Jwt"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.Issuer,
                        ValidAudience = jwtConfiguration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Add Razor Pages
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                DbInitializer.Initialize(services);
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "apiRoute",
                    pattern: "api/{controller}/{action}/{id?}"
                );
            });
        }
    }
}
