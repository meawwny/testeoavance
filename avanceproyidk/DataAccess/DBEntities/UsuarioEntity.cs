using avanceproyidk.DataAccess.DBEntities.Abstraccions;
using System.ComponentModel.DataAnnotations.Schema;

namespace avanceproyidk.DataAccess.DBEntities
{
    [Table("Usuario")]
    public class UsuarioEntity : BaseEntity
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}
