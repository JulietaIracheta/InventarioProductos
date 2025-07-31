using InventarioDistribuido.Data;
using InventarioDistribuido.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioDistribuido.DataAccess
{
    public class InventarioAccess
    {
        private readonly AppDbContext _context;
        private readonly ILogger<InventarioAccess> _logger;

        public InventarioAccess(AppDbContext context, ILogger<InventarioAccess> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Producto>> Productos()
        {
            try
            {
                return await _context.Productos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos.");
                throw;
            }
        }

        public async Task<Producto> Producto(int id)
        {
            try
            {
                var result = await _context.Productos
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el producto con ID {id}.");
                throw;
            }
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto.");
                throw;
            }
        }

        public async Task<Producto?> ActualizarProducto(int id, Producto producto)
        {
            try
            {
                var existente = await _context.Productos.FindAsync(id);
                if (existente == null) return null;

                existente.Nombre = producto.Nombre;
                existente.Stock = producto.Stock;
                existente.Tienda = producto.Tienda;

                await _context.SaveChangesAsync();
                return existente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el producto con ID {id}.");
                throw;
            }
        }

        public async Task<bool> EliminarProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null) return false;

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el producto con ID {id}.");
                throw;
            }
        }

        public async Task SincronizarConCentral()
        {
            try
            {
                List<Producto> inventarioCentral =
                [
                    new Producto { Id = 1, Nombre = "Mouse", Stock = 20, Tienda = "Central" },
                    new Producto { Id = 2, Nombre = "Teclado", Stock = 15, Tienda = "Central" },
                    new Producto { Id = 3, Nombre = "Monitor", Stock = 30, Tienda = "Central" },
                    new Producto { Id = 4, Nombre = "CPU", Stock = 70, Tienda = "Central" },
                    new Producto { Id = 5, Nombre = "Camara", Stock = 19, Tienda = "Central" },
                ];

                var productosLocales = await _context.Productos.ToListAsync();

                foreach (var productoCentral in inventarioCentral)
                {
                    var local = productosLocales.FirstOrDefault(p => p.Id == productoCentral.Id);

                    if (local != null)
                    {
                        local.Stock = productoCentral.Stock;
                    }
                    else
                    {
                        _context.Productos.Add(new Producto
                        {
                            Id = productoCentral.Id,
                            Nombre = productoCentral.Nombre,
                            Stock = productoCentral.Stock,
                            Tienda = productoCentral.Tienda
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al sincronizar con el inventario central.");
                throw;
            }
        }
    }
}