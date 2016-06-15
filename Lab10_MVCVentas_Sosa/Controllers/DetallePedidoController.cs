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
    public class DetallePedidoController : Controller
    {
        private DETALLE_PEDIDO detalle_pedido = new DETALLE_PEDIDO();//Instancia la clase detalle pedido
        // GET: DetallePedido
        public ActionResult Index()
        {
            return View(detalle_pedido.Listar());
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new DETALLE_PEDIDO() //para generar una nueva categoría
                        : detalle_pedido.Obtener(id) //retorna un id de una categoría existente
                );
        }

        public ActionResult Guardar(DETALLE_PEDIDO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/DetallePedido/Index"); //devuelve el index
            }
            else
            {
                return View("~/Views/DetallePedido/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            detalle_pedido.IDPEDIDO = id;
            detalle_pedido.Eliminar();
            return Redirect("~/DetallePedido/Index"); //devuelve el index
        }
    }
}