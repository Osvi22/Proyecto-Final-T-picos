using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto.BL;
using Proyecto.DA;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson; 


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonaRepository, Proyecto.DA.PersonaRepository>();
builder.Services.AddScoped<IAdministradorDePersonas, Proyecto.BL.AdministradorDePersonas>();
builder.Services.AddScoped<IVehiculoRepository, Proyecto.DA.VehiculoRepository>();
builder.Services.AddScoped<IAdministracionDeVehiculo, Proyecto.BL.AdministracionDeVehiculo>();


var app = builder.Build();

// HABILITAR SWAGGER EN PRODUCCIÓN (AZURE)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoFinal API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
