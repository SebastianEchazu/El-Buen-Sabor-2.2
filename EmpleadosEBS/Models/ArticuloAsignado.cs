using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Models.PlatoIndexData
{
    public class ArticuloAsignado
    {
        public int ID { get; set; }
        public string Denominacion { get; set; }
        public bool Asignado { get; set; }
        public double Cantidad { get; set; }
        public string UnidadMedida{ get; set; }

    }
}
