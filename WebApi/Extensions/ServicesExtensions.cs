using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
            services.AddScoped<ValidateMediaTypeAttribute>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            //Her hangi bir server API'ye request gönderebilir, herhangibir header'ı kullanabilir,"X-Pagination" header'ı da kullanılabilir
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination");
                });
            });
        }

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter is not null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+json");
                }

                var xmlOutputFormatter = config
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+xml");
                }
            });
        }
    }
}
