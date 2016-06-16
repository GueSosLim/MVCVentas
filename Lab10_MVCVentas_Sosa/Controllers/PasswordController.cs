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
    public class PasswordController : Controller
    {
        private USUARIO usuario = new USUARIO();
        // GET: Password
        public ActionResult Index()
        {
            return View(usuario.ObtenerLogin(SessionHelper.GetUser()));
            //return View();
        }

        public JsonResult Guardar(USUARIO model, HttpPostedFileBase Pass)
        {
            var rm = new ResponseModel();

            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                rm = model.GuardarPassword(Pass);
            }
            return Json(rm);
        }
    }
}