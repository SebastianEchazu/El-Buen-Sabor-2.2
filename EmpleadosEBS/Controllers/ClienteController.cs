using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosEBS.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult Index()
        {

            PlatoArticuloViewModel model = new PlatoArticuloViewModel();
            model.Platos = _context.Plato.ToArray();
            model.Articulos = _context.Articulo.ToArray();

            return View(model);
        }
    }
}