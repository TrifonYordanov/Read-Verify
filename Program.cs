using Microsoft.EntityFrameworkCore;
using ReadAndVerify.Components;
using ReadAndVerify.Data;
using ReadAndVerify.Repos;

var builder = WebApplication.CreateBuilder(args);

// Carga de conexión antes de construir app
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// ✅ Registra la base de datos ANTES de Build
builder.Services.AddDbContext<LocalDB>(options => options.UseSqlServer(connectionString));

// Registra el repositorio de Readers
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();


// Otros servicios de Blazor
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// ✅ SOLO AHORA se construye la app
var app = builder.Build();

// Middleware y configuración del entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
