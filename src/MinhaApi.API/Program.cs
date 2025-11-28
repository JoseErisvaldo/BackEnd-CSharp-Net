using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MinhaApi.Application.Interfaces;
using MinhaApi.Application.Services;
using MinhaApi.Data;
using MinhaApi.Infrastructure.Mappings;
using MinhaApi.Infrastructure.Repositories.Implementations;
using MinhaApi.Infrastructure.Repositories.Interfaces;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var host = Environment.GetEnvironmentVariable("MYSQL_HOST");
var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
var user = Environment.GetEnvironmentVariable("MYSQL_USER");
var pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

string connectionString;

if (!string.IsNullOrWhiteSpace(host))
{
    connectionString = $"Server={host};Port={port};Database={db};User={user};Password={pass};SslMode=Preferred;AllowPublicKeyRetrieval=True;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string DefaultConnection não encontrada.");
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddAutoMapper(typeof(EstablishmentProfile));
builder.Services.AddScoped<IAuthService, AuthService>();

var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET não configurado.");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER não configurado.");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE não configurado.");

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

using (var scope = app.Services.CreateScope())
{
    var dbCtx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbCtx.Database.Migrate();
    }
    catch
    {
    }
}

var railwayPort = Environment.GetEnvironmentVariable("PORT") ?? "5012";
app.Urls.Add($"http://0.0.0.0:{railwayPort}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();