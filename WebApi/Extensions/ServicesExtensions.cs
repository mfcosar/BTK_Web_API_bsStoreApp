using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
         //hangi tipi genişletmek istiyorsak onu this anahtar kelimesiyle vermek gerekiyor
            => services.AddDbContext<RepositoryContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            //ActionFilter kaydını Extensions'a taşınır
            services.AddScoped<ValidationFilterAttribute>(); //with every HTTP request, we get a new instance.
            services.AddSingleton<LogFilterAttribute>(); //Sadece 1 tane üretilse yeterli
        }
    }
}
