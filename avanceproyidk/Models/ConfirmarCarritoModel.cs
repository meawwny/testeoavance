using System;
using System.Collections.Generic;

namespace avanceproyidk.Models
{
    public class ConfirmarCarritoModel
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }


        public string numerotarjeta { get; set; }
        public string fechaexpiracion { get; set; }
        public string cvv { get; set; }
        public string correo { get; set; }


        public List<ProductosTemporalesModel> Temporal { get; set; }

        public ConfirmarCarritoModel()
        {
            Temporal = new List<ProductosTemporalesModel>();
        }

    }
}
