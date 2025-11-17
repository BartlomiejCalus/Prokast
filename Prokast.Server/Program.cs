using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prokast.Server;
using Prokast.Server.Entities;
using Prokast.Server.Filters;
using Prokast.Server.Models;
using Prokast.Server.Seeders;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Scalar.AspNetCore;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<ExcludeEntitiesFilter>();
});

builder.Services.AddDbContext<ProkastServerDbContext>(opt=>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        ));
}
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"]!)),
            ValidateIssuerSigningKey = true
        };
    });



builder.Services.Configure<AzureBlobStorageSettings>(
    builder.Configuration.GetSection("AzureBlobStorage"));


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IParamsService, ParamsService>();
builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddScoped<IPricesService, PricesService>();
builder.Services.AddScoped<IOthersService, OthersService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAdditionalDescriptionService, AdditionalDescriptionService>();
builder.Services.AddScoped<IAdditionalNameService, AdditionalNameService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IStoredProductService, StoredProductService>();
builder.Services.AddScoped<IMailingService, MailingService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBlobPhotoStorageService,BlobPhotoStorageService>();
builder.Services.AddScoped<ISeeder, RegionSeeder>();
builder.Services.AddScoped<ISeeder, DictionaryParamSeeder>();
builder.Services.AddScoped<ISeeder, RoleSeeder>();
builder.Services.AddScoped<ISeeder, ClientSeeder>();
builder.Services.AddScoped<ISeeder, ProductSeeder>();
builder.Services.AddScoped<ISeeder, WarehouseSeeder>();
builder.Services.AddScoped<ISeeder, StoredProductSeeder>();
builder.Services.AddScoped<ISeeder, OrderSeeder>();
builder.Services.AddScoped<MainSeeder>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddProkastOpenAPI();

/*builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Ignoruje cykliczne referencje zamiast serializować w nieskończoność
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    // Zwiększa maksymalną głębokość na wszelki wypadek
    options.SerializerOptions.MaxDepth = 256;
});*/

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var dbContext = scope.ServiceProvider.GetRequiredService<ProkastServerDbContext>();
    await dbContext.Database.MigrateAsync();

    var seeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
    seeder.SeedDB();
}

var env = app.Environment;

if (env.IsEnvironment("Test"))
{
    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new
                {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    description = e.Value.Description
                })
            });
            await context.Response.WriteAsync(json);
        }
    }).RequireAuthorization();  
}

app.MapOpenApi().CacheOutput();
app.MapScalarApiReference("/scalar/prokast");

//app.UseSwagger();
//app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program { }