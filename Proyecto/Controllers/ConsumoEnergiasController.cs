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
    public class ConsumoEnergiasController : Controller
    {
        private EPMEntities db = new EPMEntities();

        // GET: ConsumoEnergias
        public ActionResult Index()
        {
            var consumoEnergia = db.ConsumoEnergia.Include(c => c.Clientes);
            return View(consumoEnergia.ToList());
        }

        // GET: ConsumoEnergias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumoEnergia consumoEnergia = db.ConsumoEnergia.FirstOrDefault(c => c.Cedula == id);
            if (consumoEnergia == null)
            {
                return HttpNotFound();
            }
            return View(consumoEnergia);
        }

        // GET: ConsumoEnergias/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Cedula");
            return View();
        }

        // POST: ConsumoEnergias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ConsumoEnergia,Cedula,MetaAhorroEnergia,ConsumoActualEnergia,PeriodoConsumo")] ConsumoEnergia consumoEnergia)
        {
            if (ModelState.IsValid)
            {
                db.ConsumoEnergia.Add(consumoEnergia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoEnergia.Cedula);
            return View(consumoEnergia);
        }

        // GET: ConsumoEnergias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumoEnergia consumoEnergia = db.ConsumoEnergia.FirstOrDefault(c => c.Cedula == id);
            if (consumoEnergia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoEnergia.Cedula);
            return View(consumoEnergia);
        }

        // POST: ConsumoEnergias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ConsumoEnergia,Cedula,MetaAhorroEnergia,ConsumoActualEnergia,PeriodoConsumo")] ConsumoEnergia consumoEnergia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumoEnergia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.Clientes, "Cedula", "Nombre", consumoEnergia.Cedula);
            return View(consumoEnergia);
        }

        // GET: ConsumoEnergias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumoEnergia consumoEnergia = db.ConsumoEnergia.FirstOrDefault(c => c.Cedula == id);
            if (consumoEnergia == null)
            {
                return HttpNotFound();
            }
            return View(consumoEnergia);
        }

        // POST: ConsumoEnergias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsumoEnergia consumoEnergia = db.ConsumoEnergia.FirstOrDefault(c => c.Cedula == id);
            db.ConsumoEnergia.Remove(consumoEnergia);
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