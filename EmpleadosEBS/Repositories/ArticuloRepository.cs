using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosEBS.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticuloRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Articulo> Articulo => _context.Articulo;

        public Articulo GetArticuloById(int id) => _context.Articulo.FirstOrDefault(p => p.ID == id);
    }
}
