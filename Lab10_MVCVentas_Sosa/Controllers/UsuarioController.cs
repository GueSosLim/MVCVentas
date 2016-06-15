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
    public class UsuarioController : Controller
    {
        private USUARIO usuario = new USUARIO();//Instancia la clase usuario
        // GET: Usuario
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.Buscar(criterio));
            }
        }

        //public ActionResult Ver(string id)
        //{
        //    return View(usuario.Obtener(id));
        //}

        //public ActionResult AgregarEditar(string id = "")
        //{
        //    return View(
        //        id != "" ? new USUARIO() //para generar una nueva categoría
        //                : usuario.Obtener(id) //retorna un id de una categoría existente
        //        );
        //}

        public ActionResult Ver(int id)
        {
            return View(usuario.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new USUARIO() //para generar una nueva categoría
                        : usuario.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(USUARIO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Usuario/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/Usuario/AgregarEditar.cshtml", model);
            }
        }

        //public ActionResult Eliminar(string id)
        //{
        //    usuario.IDUSUARIO = id;
        //    usuario.Eliminar();
        //    return Redirect("~/Usuario/Index"); //devuelve el index
        //}

        public ActionResult Eliminar(int id)
        {
            usuario.IDUSUARIO = id;
            usuario.Eliminar();
            return Redirect("~/Usuario/Index"); //devuelve el index
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                    criterio == null || criterio == "" ? usuario.Listar()//devuelve la lista completa
                    : usuario.Buscar(criterio)//devuelve la lista en base a la búsqueda
                    );
        }
    }
}