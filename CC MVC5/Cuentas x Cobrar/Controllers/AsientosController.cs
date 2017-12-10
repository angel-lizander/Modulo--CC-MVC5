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
using Cuentas_x_Cobrar.Helpers;
using Cuentas_x_Cobrar.Enums;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cuentas_x_Cobrar.Controllers
{
    public class AsientosController : Controller
    {
        private CuentasPorCobrarContextDataContext db = new CuentasPorCobrarContextDataContext();

        public List<Asientos> ResultandoAsientos
        {
            get
            {
                return (List<Asientos>)(Session["_TmpResultandoAsientos"] ?? (this.ResultandoAsientos = new List<Asientos>()));
            }
            set
            {
                Session["_TmpResultandoAsientos"] = value;
            }
        }

        // GET: Asientos
        public ActionResult Index()
        {
            LoadDropDownsData();
            return View(db.Asientos.ToList());
        }

        // GET: Asientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.FirstOrDefault(c => c.IDAsientos == id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            return View(asientos);
        }

        // GET: Asientos/Create
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

            var clientes = db.Clientes.ToList().Select(r => new SelectListItem
            {
                Value = $"{r.IDClientes}",
                Text = r.Nombre
            });

            var tipoMovimientos = Extensions.ToSelectList<TipoMovimiento>();

            //Fill DropDownLists
            ViewBag.CuentasContable = cuentasContables;
            ViewBag.Clientes = clientes;
            ViewBag.TipoMovimientos = tipoMovimientos;
        }

        // POST: Asientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAsientos,Descripcion,IDClientes,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado")] Asientos asientos)
        {
            if (ModelState.IsValid)
            {
                db.Asientos.InsertOnSubmit(asientos);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(asientos);
        }

        // GET: Asientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.FirstOrDefault(c => c.IDAsientos == id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            return View(asientos);
        }

        // POST: Asientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAsientos,Descripcion,IDClientes,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado")] Asientos asientos)
        {
            if (ModelState.IsValid)
            {
                var model = db.Asientos.FirstOrDefault(c => c.IDAsientos == asientos.IDAsientos);
                model.Descripcion = asientos.Descripcion;
                model.IDClientes = asientos.IDClientes;
                model.Cuenta = asientos.Cuenta;
                model.TipoMovimiento = asientos.TipoMovimiento;
                model.FechaAsiento = asientos.FechaAsiento;
                model.MontoAsiento = asientos.MontoAsiento;
                model.Estado = asientos.Estado;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(asientos);
        }

        // GET: Asientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.FirstOrDefault(c => c.IDAsientos == id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            return View(asientos);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asientos asientos = db.Asientos.FirstOrDefault(c => c.IDAsientos == id);
            db.Asientos.DeleteOnSubmit(asientos);
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

        public ActionResult ConsultaAsiento()
        {
            LoadDropDownsData();
            return View(new List<Asientos>());
        }

        [HttpPost]
        public ActionResult ConsultaAsiento(DateTime FechaDesde, DateTime FechaHasta)
        {
            LoadDropDownsData();

            if (FechaDesde == default(DateTime) && FechaHasta == default(DateTime))
            {
                return View(ResultandoAsientos);
            }

            ResultandoAsientos = db.Asientos.Where(r => r.FechaAsiento >= FechaDesde && r.FechaAsiento <= FechaHasta).ToList();

            return View(ResultandoAsientos);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ProcesarResultadoAsientos()
        {
            var status = HttpStatusCode.OK;

            try
            {
                foreach (var item in ResultandoAsientos)
                {
                    await CallApi(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: \n {ex.Message}");
                status = HttpStatusCode.InternalServerError;
            }
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public async Task CallApi(Asientos asiento)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://accountingsystem.azurewebsites.net");
                var entryModel = new EntryModelRequest
                {
                    Description = asiento.Descripcion,
                    AuxiliarId = 5,
                    CurreyncyType = "USD",
                    EntryAmount = Convert.ToDouble(asiento.MontoAsiento),
                    AccountId = asiento.Cuenta ?? 1,
                    MovementType = asiento.TipoMovimiento,
                    Status = "ACTIVO"
                };
                var jsonString = JsonConvert.SerializeObject(entryModel);
                var content = new StringContent(jsonString, Encoding.Default, "application/json");
                var response = await client.PostAsync("/api/Entries", content);

                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Response String: \n {responseString}");
            }
        }
    }
}
