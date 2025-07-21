using Serilog;
using Serilog.Debugging;

namespace EzraTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Services.AddMyServices();

            builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // Angular app's origin
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            try 
            { 
                Log.Information("Starting application");

                var app = builder.Build();
                app.UseHttpsRedirection();
                app.UseSerilogRequestLogging();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information("Executing migrations ...");
                app.MigrateDatabase();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.Information("Application is shutting down");
                Log.CloseAndFlush();
            }
        }
    }
}
