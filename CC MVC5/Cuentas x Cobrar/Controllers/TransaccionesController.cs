using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuentasPorCobrar.Domain;
using Cuentas_x_Cobrar.Models;

namespace Cuentas_x_Cobrar.Controllers
{
    public class TransaccionesController : Controller
    {
        private CuentasPorCobrarContextDataContext db = new CuentasPorCobrarContextDataContext();

        // GET: Transacciones
        public ActionResult Index()
        {
            return View(db.Transacciones.ToList());
        }

        // GET: Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.FirstOrDefault(c => c.IDTransacciones == id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // GET: Transacciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transacciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTransacciones,TipoMovimiento,IDTipoDocumento,IDCliente,Fecha,Monto")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Transacciones.InsertOnSubmit(transacciones);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(transacciones);
        }

        // GET: Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.FirstOrDefault(c => c.IDTransacciones == id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // POST: Transacciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTransacciones,TipoMovimiento,IDTipoDocumento,IDCliente,Fecha,Monto")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                var model = db.Transacciones.FirstOrDefault(c => c.IDTransacciones == transacciones.IDTransacciones);
                model.TipoMovimiento = transacciones.TipoMovimiento;
                model.IDTipoDocumento = transacciones.IDTipoDocumento;
                model.IDCliente = transacciones.IDCliente;
                model.Fecha = transacciones.Fecha;
                model.Monto = transacciones.Monto;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(transacciones);
        }

        // GET: Transacciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.FirstOrDefault(c => c.IDTransacciones == id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transacciones transacciones = db.Transacciones.FirstOrDefault(c => c.IDTransacciones == id);
            db.Transacciones.DeleteOnSubmit(transacciones);
            db.SubmitChanges();
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
