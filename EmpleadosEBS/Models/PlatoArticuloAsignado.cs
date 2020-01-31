using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Models.PlatoIndexData
{
    public class PlatoArticuloAsignado
    {
        public int ID { get; set; }
        public string Denominacion { get; set; }
        public bool PlatoAsignado { get; set; }
        public bool ArticuloAsignado { get; set; }
    }
}
