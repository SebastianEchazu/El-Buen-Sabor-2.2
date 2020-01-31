using EmpleadosEBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Repositories
{
    public interface IArticuloRepository
    {
        IEnumerable<Articulo> Articulo { get; }

        Articulo GetArticuloById(int id);
    }
}