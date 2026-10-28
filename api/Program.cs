    using LumeeraCare.Data;
using LumeeraCare.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? builder.Configuration.GetConnectionString("LumeeraCare");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No connection string configured. Please set ConnectionStrings:DefaultConnection or LumeeraCare.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("Angular", policy =>
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();

// Register services

builder.Services.AddScoped<ICustomerService, CustomerService>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AngularLifecycleDemo API",
        Version = "v1",
        Description = "Updated API - includes new endpoints"
    });

    options.CustomSchemaIds(t => t.FullName);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AngularLifecycleDemo API v1");
    });
}

app.UseCors("Angular");

app.MapControllers();

app.Run();
