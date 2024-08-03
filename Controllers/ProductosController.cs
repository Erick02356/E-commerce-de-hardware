
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
    public class ProductosController : Controller
    {
        // Instancia de la base de datos
        private parcialEntities4 db = new parcialEntities4();

        // GET: Productos
        // Método para mostrar la lista de productos
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Marca);
            return View(producto.ToList());
        }

        // GET: Productos/Details/5
        // Método para mostrar los detalles de un producto específico
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        // Método para mostrar el formulario de creación de un nuevo producto
        public ActionResult Create()
        {
            ViewBag.MarcaId = new SelectList(db.Marca, "Id", "nombre");
            return View();
        }

        // POST: Productos/Create
        // Método para guardar un nuevo producto creado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Foto,precio,detalles,Tipo,MarcaId,descripcionCorta,Nombre")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarcaId = new SelectList(db.Marca, "Id", "nombre", producto.MarcaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        // Método para mostrar el formulario de edición de un producto existente
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarcaId = new SelectList(db.Marca, "Id", "nombre", producto.MarcaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Método para guardar los cambios realizados en la edición de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Foto,precio,detalles,Tipo,MarcaId,descripcionCorta,Nombre")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarcaId = new SelectList(db.Marca, "Id", "nombre", producto.MarcaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        // Método para mostrar el formulario de confirmación de eliminación de un producto
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        // Método para eliminar un producto
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Liberar recursos
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
