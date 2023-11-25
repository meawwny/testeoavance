using avanceproyidk.DataAccess.DBEntities.Abstraccions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace avanceproyidk.DataAccess.DBEntities
{
    [Table("Orden")]
    public class OrdenEntity : BaseEntity
    {
        public DateTime fechaOrden { get; set; }
        public string numerotarjeta { get; set; }
        public string fechaexpiracion { get; set; }
        public string cvv { get; set; }
        public string correo { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }

    }
}
