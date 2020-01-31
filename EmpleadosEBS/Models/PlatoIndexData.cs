using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Models.PlatoIndexData
{
    public class PlatoIndexData
    {
        public IEnumerable<Plato> Platos { get; set; }
        public IEnumerable<Receta> Recetas { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
    }
}
