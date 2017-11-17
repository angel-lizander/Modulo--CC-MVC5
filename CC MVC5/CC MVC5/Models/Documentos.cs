using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC_MVC5.Models
{
    public class Documentos
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Cuentacontable { get; set; }
        public bool Estado { get; set; }
    }
}