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
    public class CuentasContablesController : Controller
    {
        private CuentasPorCobrarContextDataContext db = new CuentasPorCobrarContextDataContext();

        // GET: CuentasContables
        public ActionResult Index()
        {
            return View(db.CuentasContable.ToList());
        }

        // GET: CuentasContables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasContable cuentasContable = db.CuentasContable.FirstOrDefault(c => c.IDCuentasContables == id);
            if (cuentasContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentasContable);
        }

        // GET: CuentasContables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentasContables/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCuentasContables,CuentasContables")] CuentasContable cuentasContable)
        {
            if (ModelState.IsValid)
            {
                db.CuentasContable.InsertOnSubmit(cuentasContable);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(cuentasContable);
        }

        // GET: CuentasContables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasContable cuentasContable = db.CuentasContable.FirstOrDefault(c => c.IDCuentasContables == id);
            if (cuentasContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentasContable);
        }

        // POST: CuentasContables/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCuentasContables,CuentasContables")] CuentasContable cuentasContable)
        {
            if (ModelState.IsValid)
            {
                var model = db.CuentasContable.FirstOrDefault(c => c.IDCuentasContables == cuentasContable.IDCuentasContables);
                model.CuentasContables = cuentasContable.CuentasContables;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(cuentasContable);
        }

        // GET: CuentasContables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasContable cuentasContable = db.CuentasContable.FirstOrDefault(c => c.IDCuentasContables == id);
            if (cuentasContable == null)
            {
                return HttpNotFound();
            }
            return View(cuentasContable);
        }

        // POST: CuentasContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentasContable cuentasContable = db.CuentasContable.FirstOrDefault(c => c.IDCuentasContables == id);
            db.CuentasContable.DeleteOnSubmit(cuentasContable);
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
