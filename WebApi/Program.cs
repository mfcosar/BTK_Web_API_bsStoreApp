using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

//Logger definition
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

//builder.Services.AddControllers();

//patch yapabilmek i�in controller'a Json handle eden mekanizma tan�mlanmas� gerekir
builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
        config.CacheProfiles.Add("5mins", new CacheProfile { Duration=300});
    })
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCsvFormatter()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(opt => 
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); // object cycle hatas�ndan kurtulmak i�in


//ActionFilter kayd� Extensions'a ta��n�r => builder.Services.AddScoped<ValidationFilterAttribute>();


//ModelState'i config.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();   //builder.Services.AddSwaggerGen();

//DBcontext i�in servis kayd�: IoC'e dbcontext Register yap�lm�� olur
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program)); //assembly run time'da dikkate al�n�r
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();
builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<IBookLinks, BookLinks>();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache(); //Requestleri saymak i�in
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureIdentity(); //S�ra �nemli: �nce Identity sonra JWT gelmeli
//builder.Services.AddAuthentication(); => Bunun yerine JWT �a�r�l�r
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "BTK Akademi v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "BTK Akademi v2");
    });
}
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseIpRateLimiting(); // Cors'un �zerinde olmas� gerek
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

