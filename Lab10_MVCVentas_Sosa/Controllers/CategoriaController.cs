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
    public class CategoriaController : Controller
    {
        private CATEGORIA categoria = new CATEGORIA();//Instancia la clase categoría
        // GET: Categoria
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(categoria.Listar());
            }
            else
            {
                return View(categoria.Buscar(criterio));
            }
        }
        
        public ActionResult Ver(int id)
        {
            return View(categoria.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new CATEGORIA() //para generar una nueva categoría
                        : categoria.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(CATEGORIA model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Categoria/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/Categoria/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            categoria.IDCATEGORIA = id;
            categoria.Eliminar();
            return Redirect("~/Categoria/Index"); //devuelve el index
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                    criterio == null || criterio == "" ? categoria.Listar()//devuelve la lista completa
                    : categoria.Buscar(criterio)//devuelve la lista en base a la búsqueda
                    );
        }
    }
}