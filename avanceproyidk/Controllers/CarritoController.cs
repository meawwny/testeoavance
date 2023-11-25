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
        public JsonResult ProductoTemporal(ProductosTemporalesModel param)
        {
            // product info
            var productoXid = _context.Producto.Where(c => c.Id == param.Id && c.Status == true).SingleOrDefault();
            param.nombreproducto = productoXid.nombreproducto;
            param.precio = productoXid.preciofinal;
            param.imagen = productoXid.imagen;


            // product list
            List<ProductosTemporalesModel> temporals = null;

            if(HttpContext.Session.GetList<ProductosTemporalesModel>("temporal") == null)
            {
                temporals = new List<ProductosTemporalesModel>();
            }
            else
            {
                temporals = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");
            }
            temporals.Add(param);
            HttpContext.Session.Set<List<ProductosTemporalesModel>>("temporal", temporals);
            return new JsonResult(new { a = 1 });

        }

        public IActionResult ConfirmarCarrito()
        {
            var model = new ConfirmarCarritoModel();
            model.Temporal = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");
            return View(model);
        }


        [HttpPost]
        public IActionResult GuardarCarrito(ConfirmarCarritoModel modelSave)
        {
            modelSave.Temporal = (List<ProductosTemporalesModel>)HttpContext.Session.GetList<ProductosTemporalesModel>("temporal");


            // guardar user
            var userReg = _context.Usuario.Add(new UsuarioEntity()
            {
                dni = modelSave.dni,
                nombre = modelSave.nombre,
                apellido = modelSave.apellido,
                usuario = modelSave.usuario,
                contrasena = modelSave.contrasena,
                Status = true,
            });


            // guardar orden
            var ordenReg = _context.Orden.Add(new OrdenEntity()
            {
                numerotarjeta = modelSave.numerotarjeta,
                fechaexpiracion = modelSave.fechaexpiracion,
                cvv = modelSave.cvv,
                correo = modelSave.correo,
                fechaOrden = DateTime.Now,
                Usuario = userReg.Entity,
                Status = true
            });

            // guardar productos
            foreach(var item in modelSave.Temporal)
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























        public IActionResult Index()
        {
            return View();
        }
    }
}
