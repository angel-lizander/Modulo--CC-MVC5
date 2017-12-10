using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CuentasPorCobrar.Domain;

namespace Cuentas_x_Cobrar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var _dbContext = new CuentasPorCobrarContextDataContext();
            //var customers = _dbContext.Clientes.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}