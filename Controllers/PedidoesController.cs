using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCtemplate.Models;

namespace MVCtemplate.Controllers
{
    public class PedidoesController : Controller
    {
        private parcialEntities4 db = new parcialEntities4();

        // GET: Pedidoes
        // Método para mostrar los pedidos del cliente actual en estado "EnCarrito"
        public ActionResult Index()
        {
            var idCliente = GetCurrentUserId();
            var pedidos = db.Pedido
                .Include(p => p.Producto)
                .Where(p => p.IdCliente == idCliente && p.Estado == "EnCarrito")
                .ToList();

            return View(pedidos);
        }

        // POST: AñadirProductoAlCarrito
        // Método para añadir un producto al carrito del cliente actual
        [HttpPost]
        public ActionResult AñadirProductoAlCarrito(int idProducto)
        {
            var pedido = new Pedido
            {
                IdProducto = idProducto,
                IdCliente = GetCurrentUserId(),
                FechaPedido = DateTime.Now,
                Estado = "EnCarrito"
            };

            db.Pedido.Add(pedido);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: CompletarPedido
        // Método para completar los pedidos del cliente actual
        [HttpPost]
        public ActionResult CompletarPedido()
        {
            var idCliente = GetCurrentUserId();
            var pedidos = db.Pedido
                .Where(p => p.IdCliente == idCliente && p.Estado == "EnCarrito")
                .ToList();

            foreach (var pedido in pedidos)
            {
                pedido.Estado = "Completado";
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Método para obtener el ID del cliente actual
        private int GetCurrentUserId()
        {
            // Placeholder para el ID del usuario actual
            return 1; // Reemplaza esto con la lógica real para obtener el ID del usuario actual
        }

        // GET: Pedidoes/Delete/5
        // Método para mostrar el formulario de confirmación de eliminación de un pedido
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        // Método para eliminar un pedido
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedido.Find(id);
            db.Pedido.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Otros métodos (Details, Create, Edit) permanecen sin cambios.
    }
}
