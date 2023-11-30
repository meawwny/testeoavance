using AutoMapper;
using avanceproyidk.DataAccess;
using avanceproyidk.Models;
using avanceproyidk.DataAccess.DBEntities;
using Microsoft.AspNetCore.Mvc;
using avanceproyidk.CustomExtensions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace avanceproyidk.Controllers
{

    public class CarritoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CarritoContext _context;

        
        public CarritoController(CarritoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult CatalogoProducto()
        {
            var productListDB = _context.Producto.Where(c => c.Status == true).ToList();
            var list = _mapper.Map<List<ProductosCatalogosModel>>(productListDB);
            return View(list);
        }


        public IActionResult DetalleProducto(int id)
        {
            var productXid = _context.Producto.Where(c => c.Id == id && c.Status == true).SingleOrDefault();
            var vistaproducto = _mapper.Map<ProductosCatalogosModel>(productXid);
            return View(vistaproducto);
        }


        [HttpPost]
        public JsonResult AddProductoTemporal(ProductosTemporalesModel param)
        {
            // product info
            var productoXid = _context.Producto.Where(c => c.Id == param.Id && c.Status == true).SingleOrDefault();
            param.nombreproducto = productoXid.nombreproducto;
            param.precio = productoXid.preciofinal;
            param.imagen = productoXid.imagen;


            // product list
            List<ProductosTemporalesModel> temporalproducts = null;

            if(HttpContext.Session.GetList<ProductosTemporalesModel>("temporal") == null)
            {
                temporalproducts = new List<ProductosTemporalesModel>();
            }
            else
            {
                temporalproducts = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");
            }
            temporalproducts.Add(param);
            HttpContext.Session.Set<List<ProductosTemporalesModel>>("temporal", temporalproducts);
            return new JsonResult(new { a = 1 });

        }

        public IActionResult ConfirmarCarrito()
        {
            var model = new ConfirmarCarritoModel();
            model.TemporalProducts = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");
            return View(model);
        }

        
        [HttpPost]
        public IActionResult GuardarCarrito(ConfirmarCarritoModel modelToSave)
        {
            modelToSave.TemporalProducts = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");

            // guardar user
            var userReg = _context.Usuario.Add(new UsuarioEntity()
            {
                dni = modelToSave.dni,
                nombre = modelToSave.nombre,
                apellido = modelToSave.apellido,
                usuario = modelToSave.usuario,
                contrasena = modelToSave.contrasena,
                Status = true,
            });


            // guardar orden
            var ordenReg = _context.Orden.Add(new OrdenEntity()
            {
                numerotarjeta = modelToSave.numerotarjeta,
                fechaexpiracion = modelToSave.fechaexpiracion,
                cvv = modelToSave.cvv,
                correo = modelToSave.correo,
                fechaOrden = DateTime.Now,
                Usuario = userReg.Entity,
                Status = true
            });

            // guardar productos
            foreach(var item in modelToSave.TemporalProducts)
            {
                _context.OrdenDetalle.Add(new OrdenDetalleEntity()
                {
                    Status = true,
                    preciofinal = item.precio,
                    Orden = ordenReg.Entity,
                    Producto = _context.Producto.Where(c => c.Id == item.Id).FirstOrDefault(),
                });
            }
                
            _context.SaveChanges();

            return RedirectToAction("MsgOrdenExitosa");
        }

        public IActionResult MsgOrdenExitosa()
        {
            return View();
        }
    }
}
