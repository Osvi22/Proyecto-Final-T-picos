using Proyecto.DA;
using Microsoft.OpenApi.Models;
using Proyecto.SI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DBContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Proyecto.BL.IPersonaRepository,Proyecto.DA.PersonaRepository>();
builder.Services.AddScoped<Proyecto.BL.IAdministradorDePersonas, Proyecto.BL.AdministradorDePersonas>();
builder.Services.AddScoped<Proyecto.BL.IVehiculoRepository, Proyecto.DA.VehiculoRepository>();
builder.Services.AddScoped<Proyecto.BL.IAdministracionDeVehiculo, Proyecto.BL.AdministracionDeVehiculo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
