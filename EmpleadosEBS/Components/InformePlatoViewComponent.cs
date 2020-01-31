using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using EmpleadosEBS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Components
{
    public class InformePlatoViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public InformePlatoViewComponent(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<Plato> platosConsulta = new List<Plato>();

            var pedidos = await _context.Pedido
                .Include(d => d.DetPedidos)
                    .Where(i => i.EstadoPedidoID == 4).ToListAsync();

            var platos = await _context.Plato.ToListAsync();

            foreach (var pedido in pedidos)
            {
                foreach (var detalle in pedido.DetPedidos)
                {
                    foreach (var plato in platos)
                    {
                        if (detalle.PlatoID == plato.ID)
                        {
                            platosConsulta.Add(plato);
                        }
                    }
                }
            }
            return View(platosConsulta);
        }
    }
}
