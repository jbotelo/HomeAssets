using HomeAssets.Infraestructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HomeAssets.Infraestructure.Extensions
{
    public static class RegisterDatabaseContextExtensions
    {
        public static IServiceCollection RegisterPostgresDatabaseContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
        public static IServiceCollection RegisterSqlServerDatabaseContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("HomeAssets"));
            });
        }
    }
}