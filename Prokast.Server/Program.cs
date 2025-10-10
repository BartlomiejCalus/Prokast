using Microsoft.EntityFrameworkCore;
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
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

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
builder.Services.AddScoped<ISeeder, ProductSeeder>();
builder.Services.AddScoped<ISeeder, WarehouseSeeder>();
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
    var seeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
    seeder.SeedDB();
}

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{

}*/
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
