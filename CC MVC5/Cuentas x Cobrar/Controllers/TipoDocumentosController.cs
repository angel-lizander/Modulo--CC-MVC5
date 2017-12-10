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
    public class TipoDocumentosController : Controller
    {
        //private Cuentas_x_CobrarContext db = new Cuentas_x_CobrarContext();
        private CuentasPorCobrarContextDataContext db = new CuentasPorCobrarContextDataContext();


        // GET: Cuentas
        public ActionResult Index()
        {
            LoadDropDownsData();
            return View(db.TipoDocumentos.ToList());
        }

        // GET: Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumentos tipoDocumento = db.TipoDocumentos.FirstOrDefault(c => c.IDDocumentos == id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            LoadDropDownsData();
            return View();
        }

        void LoadDropDownsData()
        {
            var cuentasContables = db.CuentasContable.ToList().Select(r => new SelectListItem
            {
                Value = $"{r.IDCuentasContables}",
                Text = $"{r.CuentasContables}"
            });

            //Fill DropDownList CuentasContable
            ViewBag.CuentasContable = cuentasContables;
        }

        // POST: Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDocumentos,Descripcion,Cuentacontable,Estado")] TipoDocumentos tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                db.TipoDocumentos.InsertOnSubmit(tipoDocumento);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDocumento);
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumentos tipoDocumento = db.TipoDocumentos.FirstOrDefault(c => c.IDDocumentos == id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // POST: Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDocumentos,Descripcion,Cuentacontable,Estado")] TipoDocumentos cuentas)
        {
            if (ModelState.IsValid)
            {
                var model = db.TipoDocumentos.FirstOrDefault(c => c.IDDocumentos == cuentas.IDDocumentos);
                model.Descripcion = cuentas.Descripcion;
                model.Cuentacontable = cuentas.Cuentacontable;
                model.Estado = cuentas.Estado;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(cuentas);
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TipoDocumentos cuentas = db.TipoDocumentos.FirstOrDefault(c => c.IDDocumentos == id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDocumentos cuentas = db.TipoDocumentos.FirstOrDefault(c => c.IDDocumentos == id);
            db.TipoDocumentos.DeleteOnSubmit(cuentas);
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
