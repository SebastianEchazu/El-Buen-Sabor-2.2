using EmpleadosEBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Repositories
{
    public interface IPlatoRepository
    {
        IEnumerable<Plato> Plato { get; }

        Plato GetPlatoById(int id);
    }
}