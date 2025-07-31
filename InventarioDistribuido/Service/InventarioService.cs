using InventarioDistribuido.DataAccess;
using InventarioDistribuido.Models;

namespace InventarioDistribuido.Service
{
    public class InventarioService
    {
        public InventarioAccess _inventarioAccess { get; set; }

        public InventarioService(InventarioAccess inventarioAccess)
        {
            _inventarioAccess = inventarioAccess;
        }

        public async Task<List<Producto>> Productos()
        {
            var productos = await _inventarioAccess.Productos();
            return productos;
        }

        public async Task<Producto> Producto(int id)
        {
            var productos = await _inventarioAccess.Producto(id);
            return productos;
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            return await _inventarioAccess.CrearProducto(producto);
        }

        public async Task<Producto?> ActualizarProducto(int id, Producto producto)
        {
            return await _inventarioAccess.ActualizarProducto(id, producto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            return await _inventarioAccess.EliminarProducto(id);
        }

        public async Task Sincronizar()
        {
            await _inventarioAccess.SincronizarConCentral();
        }

    }
}
