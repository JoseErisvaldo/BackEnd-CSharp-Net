using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using AutoMapper;
using MinhaApi.Services;
using MinhaApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DotNetEnv;

// 🔐 Carrega variáveis do .env
Env.Load();

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

if (!string.IsNullOrWhiteSpace(host))
{
    connectionString =
        $"Server={host};Port={port};Database={db};User={user};Password={pass};SslMode=None;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Connection string DefaultConnection não encontrada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// ---------------------------------------------------------
// 🧱 2) Services, Repos, AutoMapper
// ---------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentsRepository>();
builder.Services.AddScoped<IEstablishmentsService, EstablishmentsService>();
builder.Services.AddScoped<AuthService>();



// ---------------------------------------------------------
// 🔐 3) JWT 100% seguro via ENV
// ---------------------------------------------------------
var secret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

builder.Services.AddAuthorization();


// ---------------------------------------------------------
// 📚 4) Swagger com JWT
// ---------------------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinhaApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();


// ---------------------------------------------------------
// 🗄️ 5) Auto Migrate
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
// 🌐 6) Railway PORT binding
// ---------------------------------------------------------
var railwayPort = Environment.GetEnvironmentVariable("PORT") ?? "5012";
app.Urls.Add($"http://0.0.0.0:{railwayPort}");


// ---------------------------------------------------------
// 🚀 7) Pipeline
// ---------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
