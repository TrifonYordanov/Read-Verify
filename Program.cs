using Microsoft.EntityFrameworkCore;
using ReadAndVerify.Components;
using ReadAndVerify.Data;
using ReadAndVerify.Mappings;
using ReadAndVerify.Repository;
using ReadAndVerify.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexion a Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<LocalDB>(options => options.UseSqlServer(connectionString));

// Registra servicios de AutoMapper y repositorios
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddSingleton<IReaderConnectionService, ReaderConnectionService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddSingleton<ReaderSdkService>();



//Agrega servicios de Razor Components
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configura middlewares y entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Mapea el componente principal
app.MapRazorComponents<AppShell>().AddInteractiveServerRenderMode();

app.Run();
