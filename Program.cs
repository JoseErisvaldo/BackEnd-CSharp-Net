using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using AutoMapper;
using MinhaApi.Services;
using MinhaApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// 🔌 1) Connection string (Railway → Local fallback)
// ---------------------------------------------------------
var host = Environment.GetEnvironmentVariable("MYSQL_HOST");
var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
var user = Environment.GetEnvironmentVariable("MYSQL_USER");
var pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

string connectionString;

if (!string.IsNullOrEmpty(host))
{
    // ✔ Railway
    connectionString =
        $"Server={host};Port={port};Database={db};User={user};Password={pass};SslMode=None;";
}
else
{
    // ✔ Local (via appsettings.json)
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// ---------------------------------------------------------
// 🧱 2) Services, Repos, AutoMapper, Swagger
// ---------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();


// ---------------------------------------------------------
// 🗄️ 3) Auto Migrate
// ---------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dbCtx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbCtx.Database.Migrate();
        Console.WriteLine("✔ Migrations applied successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Migration error: " + ex.Message);
    }
}


// ---------------------------------------------------------
// 🌐 4) Required for Railway (PORT binding)
// ---------------------------------------------------------
var railwayPort = Environment.GetEnvironmentVariable("PORT") ?? "5012";
app.Urls.Add($"http://0.0.0.0:{railwayPort}");


// ---------------------------------------------------------
// 🚀 5) Pipeline
// ---------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
