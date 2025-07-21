using FullStackTest.Api.Mappings;
using FullStackTest.Api.Migrations;
using FullStackTest.Api.Repositories;
using FullStackTest.Api.Services;
using FluentMigrator.Runner;
using FluentValidation;
using Microsoft.Data.Sqlite;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Data;

namespace EzraTest
{
    public static class ServiceExtensions
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddAutoMapper(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddTransient<IDbConnection>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DefaultConnection") ?? "Data Source=data/app.db";
                return new SqliteConnection(connectionString);
            });

            services.AddTransient<IMyTaskService, MyTaskService>();
            services.AddTransient<IMyTaskRepository, MyTaskRepository>();

           var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
           var connectionStringForMigration = config.GetConnectionString("DefaultConnection") ?? "Data Source=data/app.db";

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionStringForMigration)
                    .ScanIn(typeof(InitialCreate).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddValidatorsFromAssemblyContaining<CreateTaskRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateTaskRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateDown(0);
                runner.MigrateUp();
            }
        }
    }

}
