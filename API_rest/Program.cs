using API_rest.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Variable para la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Registrar servicios
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddTransient<ClienteService>();

// Registrar AutoMapper correctamente
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
