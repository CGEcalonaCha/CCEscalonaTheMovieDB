using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estadistica
    {
        public decimal Total { get; set; }
        public decimal Sur = 0;
        public decimal Oriente { get; set; }
        public decimal Poniete { get; set; }
        public decimal Norte { get; set; }
        public List<object> Estadisticas { get; set; }
    }
}
