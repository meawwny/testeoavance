using avanceproyidk.DataAccess.DBEntities.Abstraccions;
using System.ComponentModel.DataAnnotations.Schema;

namespace avanceproyidk.DataAccess.DBEntities
{
    [Table("Producto")]
    public class ProductoEntity : BaseEntity
    {
        public string nombreproducto { get; set; }
        public string marca { get; set; }
        public double precio { get; set; }
        public double descuento { get; set; }
        public double preciofinal { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
    }
}
