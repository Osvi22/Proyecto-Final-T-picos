using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto.BL;
using Proyecto.DA;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Json config
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios y servicios
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IAdministradorDePersonas, AdministradorDePersonas>();

builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IAdministracionDeVehiculo, AdministracionDeVehiculo>();

var app = builder.Build();

// Swagger habilitado en producción
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoFinal API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
