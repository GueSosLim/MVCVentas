using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Lab10_MVCVentas_Sosa.Filters;

namespace Lab10_MVCVentas_Sosa.Controllers
{
    [Autenticado]
    public class PerfilController : Controller
    {
        private USUARIO usuario = new USUARIO();
        private TIPO_USUARIO tipo = new TIPO_USUARIO();
        // GET: Perfil
        public ActionResult Index()
        {
            ViewBag.tipo = tipo.ListarTipoUsuario();
            return View(usuario.ObtenerLogin(SessionHelper.GetUser()));
            //return View();
        }

        public JsonResult Guardar(USUARIO model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                rm = model.GuardarFoto(Foto);
            }
            return Json(rm);
        }
    }
}