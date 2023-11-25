using avanceproyidk.DataAccess.DBEntities.Abstraccions;
using System.ComponentModel.DataAnnotations.Schema;

namespace avanceproyidk.DataAccess.DBEntities
{

    [Table("OrdenDetalle")]
    public class OrdenDetalleEntity : BaseEntity
    {

        public double preciofinal { get; set; }
        public virtual ProductoEntity Producto { get; set; }
        public virtual OrdenEntity Orden { get; set; }
    }
}
