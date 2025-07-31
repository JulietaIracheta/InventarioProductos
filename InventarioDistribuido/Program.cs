using InventarioDistribuido.Data;
using InventarioDistribuido.DataAccess;
using InventarioDistribuido.Models;
using InventarioDistribuido.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InventarioDB"));

builder.Services.AddScoped<InventarioService>();
builder.Services.AddScoped<InventarioAccess>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Productos.AddRange(
        new Producto { Nombre = "Mouse", Stock = 10, Tienda = "Tienda A" },
        new Producto { Nombre = "Teclado", Stock = 5, Tienda = "Tienda B" },
        new Producto { Nombre = "Monitor", Stock = 3, Tienda = "Tienda B" },
        new Producto { Nombre = "CPU", Stock = 8, Tienda = "Tienda C" }
    );
    db.SaveChanges();
}

app.Run();
