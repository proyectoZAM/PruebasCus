using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;
using ProductosApi.Models;

namespace ProductosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoFuncion : ControllerBase
    {
        private readonly ProductosContext _context;

        public ProductoFuncion(ProductosContext context)
        {
            _context = context;
        }

        // 1. GET /api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Captura()
        {
            return await _context.Productos.ToListAsync();
        }

        // 2. Obtiene Producto
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Captura(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return producto;
        }

        // 3. Guarda Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> Agrega(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Captura), new { id = producto.Id }, producto);
        }

        // 4. Actualiza Producto
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualiza(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. Elimina Producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Elimina(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}