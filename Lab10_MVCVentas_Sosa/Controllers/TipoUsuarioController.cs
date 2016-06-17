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
    public class TipoUsuarioController : Controller
    {
        private TIPO_USUARIO tipo_usuario = new TIPO_USUARIO();//Instancia la clase tipo usuario
        // GET: TipoUsuario
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(tipo_usuario.Listar());
            }
            else
            {
                return View(tipo_usuario.Buscar(criterio));
            }
        }

        public JsonResult CargarTipoUsuario(AnexGRID grid)
        {
            return Json(tipo_usuario.ListarTipoGrilla(grid));
        }

        //public JsonResult Listar(Model.AnexGRID agrid)
        //{
        //    return Json(empleado.Listar(agrid), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Ver(int id)
        {
            return View(tipo_usuario.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new TIPO_USUARIO() //para generar una nueva categoría
                        : tipo_usuario.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(TIPO_USUARIO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/TipoUsuario/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/TipoUsuario/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            tipo_usuario.IDTIPOUSUARIO = id;
            tipo_usuario.Eliminar();
            return Redirect("~/TipoUsuario/Index"); //devuelve el index
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                    criterio == null || criterio == "" ? tipo_usuario.Listar()//devuelve la lista completa
                    : tipo_usuario.Buscar(criterio)//devuelve la lista en base a la búsqueda
                    );
        }
    }
}