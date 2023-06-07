using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Complejo { get; set; }
        public string Direccion { get; set; }
        public decimal Ventas { get; set; }
        public int latitud { get; set; }
        public int longitud { get; set; }
        public Zona Zona { get; set; }
        public Estadistica EstadisticaCine { get; set; }
        public List<object> Cines { get; set; }
    }
}
