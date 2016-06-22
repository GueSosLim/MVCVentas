using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab10_MVCVentas_Sosa.Filters;

namespace Lab10_MVCVentas_Sosa.Controllers
{
    [Autenticado]
    public class ProductoController : Controller
    {
        private PRODUCTO producto = new PRODUCTO();//Instancia la clase producto
        private CATEGORIA tipo = new CATEGORIA();
        // GET: Producto
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(producto.Listar());
            }
            else
            {
                return View(producto.Buscar(criterio));
            }
        }

        public ActionResult Consulta()
        {
                return View(producto.Listar());
        }

        public ActionResult Ver(int id)
        {
            return View(producto.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new PRODUCTO() //para generar una nueva categoría
                        : producto.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(PRODUCTO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Producto/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/Producto/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            producto.IDPRODUCTO = id;
            producto.Eliminar();
            return Redirect("~/Producto/Index"); //devuelve el index
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                    criterio == null || criterio == "" ? producto.Listar()//devuelve la lista completa
                    : producto.Buscar(criterio)//devuelve la lista en base a la búsqueda
                    );
        }

        public ActionResult BuscarCategoria(int criterio = 0)
        {
            ViewBag.Combo = tipo.Listar();
            return View(producto.BuscarCategoria(criterio));
        }
    }
}