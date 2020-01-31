using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosEBS.Repositories
{
    public class PlatoRepository : IPlatoRepository
    {
        private readonly ApplicationDbContext _context;

        public PlatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Plato> Plato => _context.Plato;

        public Plato GetPlatoById(int id) => _context.Plato.FirstOrDefault(p => p.ID == id);
       
    }
}
