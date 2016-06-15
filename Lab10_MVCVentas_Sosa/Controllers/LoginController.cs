using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Lab10_MVCVentas_Sosa.Filters;

namespace Lab10_MVCVentas_Sosa.Controllers
{
    public class LoginController : Controller
    {
        private USUARIO usuario = new USUARIO();
        // GET: Login
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult Acceder(string Email, string Password)
        //{
        //    var rm = usuario.Acceder(Email, Password);

        //    if (rm.response)
        //    {
        //        rm.href = Url.Content("~/Home/");
        //    }
        //    return Json(rm);
        //}

        public ActionResult Acceder(string Email, string Password)
        {
            var rm = usuario.Acceder(Email, Password);

            if (rm.response)
            {
                rm.href = Url.Content("~/Home/");
            }
            return Redirect("~/");
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }

        
    }
}