using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cuentas_x_Cobrar.Models
{
    public class Cuentas_x_CobrarContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Cuentas_x_CobrarContext() : base("name=Cuentas_x_CobrarContext")
        {
        }

        public System.Data.Entity.DbSet<CuentasPorCobrar.Domain.Clientes> Clientes { get; set; }
        public System.Data.Entity.DbSet<CuentasPorCobrar.Domain.TipoDocumentos> TipoDocumentos { get; set; }
        public System.Data.Entity.DbSet<CuentasPorCobrar.Domain.Asientos> Asientos { get; set; }
        public System.Data.Entity.DbSet<CuentasPorCobrar.Domain.CuentasContable> CuentasContable { get; set; }
        public System.Data.Entity.DbSet<CuentasPorCobrar.Domain.Transacciones> Transacciones { get; set; }
    }
}
