using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosEBS.Components
{
    public class InformeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InformeController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public ActionResult InformeIngreso(DateTime inicio,DateTime final)
        {
            


            return ViewComponent("InformeIngreso", new {fechaInicio = inicio, fechaFinal = final });
                       
        }

        
        
    }
}