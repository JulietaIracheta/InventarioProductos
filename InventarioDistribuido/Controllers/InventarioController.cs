using InventarioDistribuido.Data;
using InventarioDistribuido.Models;
using InventarioDistribuido.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventarioDistribuido.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioService _inventarioService;

        public InventarioController(AppDbContext context, InventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet("productos")]
        public async Task<ActionResult<List<Producto>>> Productos()
        {
            try
            {
                var productos = await _inventarioService.Productos();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("producto/{id}")]
        public async Task<ActionResult<Producto>> Producto(int id)
        {
            try
            {
                var productos = await _inventarioService.Producto(id);

                if (productos == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("productos")]
        public async Task<ActionResult<Producto>> CrearProducto([FromBody] Producto nuevoProducto)
        {
            try
            {
                var creado = await _inventarioService.CrearProducto(nuevoProducto);
                return CreatedAtAction(nameof(Producto), new { id = creado.Id }, creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el producto: {ex.Message}");
            }
        }

        [HttpPut("producto/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto productoEditado)
        {
            try
            {
                var actualizado = await _inventarioService.ActualizarProducto(id, productoEditado);

                if (actualizado == null)
                {
                    return NotFound($"Producto con ID {id} no fue encontrado.");
                }

                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpDelete("producto/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var eliminado = await _inventarioService.EliminarProducto(id);

                if (!eliminado)
                {
                    return NotFound($"Producto con ID {id} no fue encontrado.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el producto: {ex.Message}");
            }
        }

        [HttpPost("productos/sync")]
        public async Task<IActionResult> SincronizarInventario()
        {
            try
            {
                await _inventarioService.Sincronizar();
                return Ok("Sincronización completada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en la sincronización: {ex.Message}");
            }
        }

    }
}
