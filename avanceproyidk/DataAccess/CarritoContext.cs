using avanceproyidk.DataAccess.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace avanceproyidk.DataAccess
{
    public class CarritoContext : DbContext
    {
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<OrdenEntity> Orden { get; set; }
        public DbSet<ProductoEntity> Producto { get; set; }
        public DbSet<OrdenDetalleEntity> OrdenDetalle { get; set; }

        public CarritoContext(DbContextOptions<CarritoContext> option) : base(option)
        {
        }
    }
}
