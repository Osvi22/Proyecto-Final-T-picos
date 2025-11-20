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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();