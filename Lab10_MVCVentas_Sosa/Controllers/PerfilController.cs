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
            string foto;
            ModelState.Remove("Password");

            if (model.FOTO != null)
            {
                foto = model.FOTO;
                System.IO.File.Delete(Server.MapPath("../Uploads/" + foto));
            }

            if (ModelState.IsValid)
            {
                rm = model.GuardarFoto(Foto);
            }
            rm.href = Url.Content("~/Usuario/Index");
            return Json(rm);
        }
    }
}