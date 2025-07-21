using Microsoft.EntityFrameworkCore;
using ProductosApi.Models;

namespace ProductosApi.Data
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> opcion) : base(opcion) {}

        public DbSet<Producto> Productos { get; set; }
    }
}