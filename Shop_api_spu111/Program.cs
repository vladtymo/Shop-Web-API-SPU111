using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Repositories;
using BusinessLogic.Services;
using Core.Helpers;
using Core.Interfaces;
using Core.Services;
using DataAccess.Data;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop_api_spu111.Middlewares;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("AzureDb")!;

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// configure dependencies
builder.Services.AddDbContext<ShopSPUDbContext>(opts => opts.UseSqlServer(connStr));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ShopSPUDbContext>()
                .AddDefaultTokenProviders();

// Configure services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure JWT token
var jwtOpts = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOpts.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
   var db = scope.ServiceProvider.GetRequiredService<ShopSPUDbContext>();
   db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
