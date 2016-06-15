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
    public class PedidoController : Controller
    {
        private PEDIDO pedido = new PEDIDO();//Instancia la clase pedido
        // GET: Pedido
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(pedido.Listar());
            }
            else
            {
                return View(pedido.Buscar(criterio));
            }
        }

        public ActionResult Ver(int id)
        {
            return View(pedido.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new PEDIDO() //para generar una nueva categoría
                        : pedido.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(PEDIDO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Pedido/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/Pedido/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            pedido.IDPEDIDO = id;
            pedido.Eliminar();
            return Redirect("~/Pedido/Index"); //devuelve el index
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                    criterio == null || criterio == "" ? pedido.Listar()//devuelve la lista completa
                    : pedido.Buscar(criterio)//devuelve la lista en base a la búsqueda
                    );
        }
    }
}