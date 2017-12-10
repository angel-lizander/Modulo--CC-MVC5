using System.Web;
using System.Web.Mvc;

namespace Cuentas_x_Cobrar
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
