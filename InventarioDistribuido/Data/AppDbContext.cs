using InventarioDistribuido.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioDistribuido.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos => Set<Producto>();
    }
}
