using NetCoreSamples.Logging.Lib;
using NetCoreSamples.Caching.Lib.Extensions;
using Microsoft.EntityFrameworkCore;
using NetCoreSamples.Caching.Domain;
using NetCoreSamples.Caching.Application.Features.Location.Handlers;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Persistence;

namespace NetCoreSamples.Caching.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SerilogSetup.ConfigureSerilog(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // MediatR setup
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(GetLocationHandlers).Assembly);
            });

            // Configure Redis access
            builder.ConfigureRedis();

            // DbContext injection
            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<CachingSampleDbContext>(options => options.UseSqlServer(connectionString));

            // DAOs injection
            builder.Services.AddTransient<ICountryDAO, CountryDAO>();
            builder.Services.AddTransient<IUserDAO, UserDAO>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
