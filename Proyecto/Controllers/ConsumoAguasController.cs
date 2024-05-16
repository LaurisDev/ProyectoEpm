using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto;
using ProyectoEPM;

namespace ProyectoEPM.Controllers
{
    public class ConsumoAguasController : Controller
    {
        private EPMEntities db = new EPMEntities();

        // GET: ConsumoAguas
        public ActionResult Index()
        {
            var consumoAgua = db.ConsumoAgua.Include(c => c.Clientes);
            return View(consumoAgua.ToList());
        }

        // GET: ConsumoAguas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumoAgua consumoAgua = db.ConsumoAgua.FirstOrDefault(c => c.Cedula == id);
            if (consumoAgua == null)
            {
                return HttpNotFound();
            }
            return View(consumoAgua);
        }

        // GET: ConsumoAguas/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Cedula");
            return View();
        }

        // POST: ConsumoAguas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ConsumoAgua,Cedula,PromedioConsumoAgua,ConsumoActualAgua,PeriodoConsumo")] ConsumoAgua consumoAgua)
        {
            if (ModelState.IsValid)
            {
                db.ConsumoAgua.Add(consumoAgua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoAgua.Cedula);
            return View(consumoAgua);
        }

        // GET: ConsumoAguas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Buscar el registro de ConsumoAgua por Cedula
            ConsumoAgua consumoAgua = db.ConsumoAgua.FirstOrDefault(c => c.Cedula == id);
            if (consumoAgua == null)
            {
                return HttpNotFound();
            }
            // Pasar la lista de Clientes para generar el SelectList en la vista
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoAgua.Clientes.Cedula);
            return View(consumoAgua);
        }


        // POST: ConsumoAguas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ConsumoAgua,Cedula,PromedioConsumoAgua,ConsumoActualAgua,PeriodoConsumo")] ConsumoAgua consumoAgua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumoAgua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoAgua.Cedula);
            return View(consumoAgua);
        }

        // GET: ConsumoAguas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumoAgua consumoAgua = db.ConsumoAgua.FirstOrDefault(c => c.Cedula == id);
            if (consumoAgua == null)
            {
                return HttpNotFound();
            }
            return View(consumoAgua);
        }

        // POST: ConsumoAguas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsumoAgua consumoAgua = db.ConsumoAgua.FirstOrDefault(c => c.Cedula == id);
            db.ConsumoAgua.Remove(consumoAgua);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
