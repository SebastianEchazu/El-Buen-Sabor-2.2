using EmpleadosEBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.ViewModels
{
    public class PlatoArticuloViewModel
    {
        public IEnumerable<Plato> Platos { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
    }
}
