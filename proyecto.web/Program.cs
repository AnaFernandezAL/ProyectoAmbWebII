using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using proyecto.Application.Profiles;
using proyecto.Application.Services.Implementations;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Repository.Implementations;
using proyecto.Infraestructure.Repository.Interfaces;
using proyecto.Web.Middleware;
using Serilog;
using Serilog.Events;
using System.Text;

// =======================
// Configurar Serilog
// =======================
// Crear carpeta Logs automáticamente (evita errores si no existe)
Directory.CreateDirectory("Logs");

// Configuración Serilog
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(@"Logs\Info-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
        .WriteTo.File(@"Logs\Warning-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .CreateLogger();

// Paso obligatorio ANTES de crear builder
Log.Logger = logger;

var builder = WebApplication.CreateBuilder(args);

// Integrar Serilog al host
builder.Host.UseSerilog(Log.Logger);

// =======================
// Configurar Dependency Injection
// =======================

// *** Repositories
builder.Services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
builder.Services.AddTransient<IRepositorySubasta, RepositorySubasta>();
builder.Services.AddTransient<IRepositoryCarta, RepositoryCarta>();
builder.Services.AddTransient<IRepositoryPujas, RepositoryPujas>();

// *** Services
builder.Services.AddTransient<IServiceUsuario, ServiceUsuario>();
builder.Services.AddTransient<IServiceSubasta, ServiceSubasta>();
builder.Services.AddTransient<IServiceCarta, ServiceCarta>();
builder.Services.AddTransient<IServicePujas, ServicePujas>();


// =======================
// Configurar AutoMapper
// =======================
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<SubastaProfile>();
    config.AddProfile<EstadoSubastaProfile>();
    config.AddProfile<EstadoUsuarioProfile>();
    config.AddProfile<RolProfile>();
    config.AddProfile<CategoriaProfile>();
    config.AddProfile<CartaProfile>();
    config.AddProfile<ImagenCartaProfile>();
    config.AddProfile<EstadoCartaProfile>();
    config.AddProfile<CartaCategoriaProfile>();
});

// =======================
// Configurar SQL Server DbContext
// =======================
var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'SqlServerDataBase' en appsettings.json / appsettings.Development.json.");
}

builder.Services.AddDbContext<SubastasPokemonDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// =======================
// Configuración MVC
// =======================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Middleware personalizado
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Activar soporte a la solicitud de registro con Serilog
app.UseSerilogRequestLogging();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Activar protección antifalsificación
app.UseAntiforgery();

app.Run();