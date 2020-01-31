using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using EmpleadosEBS.Models.PedidoIndexData;
using EmpleadosEBS.Models.PlatoIndexData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace EmpleadosEBS.Controllers
{
    [Authorize(Roles = "Cajero")]
    public class CajeroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CajeroController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------
        //INDEX DE CAJERO
        //------------------------------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }
        //------------------------------------------------------------------------------
        // SECCION CAJEROBEBIDAS
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        // GET: Cajero/Bebida
        //------------------------------------------------------------------------------
        public async Task<IActionResult> IndexBebida()
        {
            return View(await _context.Articulo.Where(i => i.EsInsumo == false && i
            .Aprobado == true).ToListAsync());
        }
        //-------------------------------------------------------------------------------
        // GET: Cajero/CreateBebida
        //------------------------------------------------------------------------------
        public IActionResult CreateBebida()
        {
            return View();
        }
        //-------------------------------------------------------------------------------
        // POST: Cajero/CreateBebida
        //------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBebida([Bind("ID,Denominacion,PrecioCompra" +
            ",PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexBebida));
            }
            return View(articulo);
        }
        //-------------------------------------------------------------------------------
        // GET: Cajero/EditBebida
        //------------------------------------------------------------------------------
        public async Task<IActionResult> EditBebida(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulo.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }
        //-------------------------------------------------------------------------------
        // POST: Cajero/EditBebida
        //------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBebida(int id, [Bind("ID,Denominacion" +
            ",PrecioCompra,PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")] Articulo articulo)
        {
            if (id != articulo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexBebida));
            }
            return View(articulo);
        }
        //------------------------------------------------------------------------------
        private bool ArticuloExists(int id)
        {
            return _context.Articulo.Any(e => e.ID == id);
        }
        //------------------------------------------------------------------------------
        //FIN DE SECCION CAJEROBEBIDAS
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        // SECCION CAJEROPEDIDO
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        // GET: CajeroPedido
        //------------------------------------------------------------------------------
        public async Task<IActionResult> IndexPedido()
        {
            var viewModel = new PedidoIndexData();
            viewModel.Pedidos = await _context.Pedido
                .Include(p => p.DetPedidos)
                    .ThenInclude(p => p.Plato)
                .Include(p => p.DetPedidos)
                    .ThenInclude(a => a.Articulo)
                .Include(d => d.EstadoPedido)
                    .AsNoTracking()
                    .OrderBy(i => i.FechaHora)
                    .ToListAsync();

            return View(viewModel);
        }
        //------------------------------------------------------------------------------
        // GET: Cajero/EditPedido
        //------------------------------------------------------------------------------
        public async Task<IActionResult> EditPedido(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["EstadoPedidoID"] = new SelectList(_context.EstadoPedido, "ID",
                "Descripcion", pedido.EstadoPedidoID);
            return View(pedido);
        }
        //------------------------------------------------------------------------------
        //POST: Cajero/EditPedido
        //------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPedido(int id, [Bind("ID,NumeroPedido" +
            ",EstadoPedidoID,PorDelivery,FechaHora,PrecioVenta,UserId")] Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return NotFound();
            }
            pedido.EstadoPedidoID = 2;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexPedido));
            }
            ViewData["EstadoPedidoID"] = new SelectList(_context.EstadoPedido, "ID"
                , "Descripcion", pedido.EstadoPedidoID);
            return View(pedido);
        }
        //------------------------------------------------------------------------------
        // GET: Cajero/EntregarPedido
        //------------------------------------------------------------------------------
        public async Task<IActionResult> EntregarPedido(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["EstadoPedidoID"] = new SelectList(_context.EstadoPedido, "ID",
                "Descripcion", pedido.EstadoPedidoID);
            return View(pedido);
        }
        //------------------------------------------------------------------------------
        //POST: Cajero/EntregarPedido
        //------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EntregarPedido(int id, [Bind("ID,NumeroPedido" +
            ",EstadoPedidoID,PorDelivery,FechaHora,PrecioVenta,UserId")] Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return NotFound();
            }
            pedido.EstadoPedidoID = 4;

            var detalles = _context.DetPedido
                .Include(p => p.Pedido).Where(i => i.PedidoID == pedido.ID)
                .AsNoTracking();

            var articulos = _context.Articulo
                .Where(p => p.EsInsumo == false)
                    .ToList();

            foreach (var detalle in detalles)
            {
                foreach (var articulo in articulos)
                {
                    if (articulo.ID == detalle.ArticuloID)
                    {
                        double v = articulo.Stock - detalle.Cantidad;

                        articulo.Stock = v;

                        _context.Update(articulo);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexPedido));
            }
            ViewData["EstadoPedidoID"] = new SelectList(_context.EstadoPedido, "ID"
                , "Descripcion", pedido.EstadoPedidoID);
            return View(pedido);
        }
        //------------------------------------------------------------------------------
        // GET: Cajero/FacturaPedido
        //------------------------------------------------------------------------------
        public async Task<IActionResult> FacturaPedido(int id)
        {
           
            Factura factura = new Factura();

            await getFacturaAsync(factura, id);
            

            return View(factura);
        }
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        // GET: Cajero/Imprimir Factura
        //------------------------------------------------------------------------------
        public async Task<IActionResult> ImprimirFactura(int id)
        {
            Factura factura = new Factura();

            await getFacturaAsync(factura, id);

            return new ViewAsPdf(factura);           
        }
        //------------------------------------------------------------------------------
        private async Task getFacturaAsync(Factura factura,int id){


            factura.Pedido = await _context
                .Pedido.SingleAsync(m => m.ID == id);

            factura.Usuario = await _userManager
                .Users.SingleAsync(m => m.Id == factura.Pedido.UserId);

            factura.DetPedidos = await _context
                .DetPedido.Where(m => m.PedidoID == id).ToListAsync();

            factura.Articulos = await _context
                .Articulo.Where(m => m.EsInsumo == false).ToListAsync();

            factura.Platos = await _context.Plato.ToListAsync();

        }
        //------------------------------------------------------------------------------
        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.ID == id);
        }
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        //FIN DE SECCION CAJEROPEDIDO
        //------------------------------------------------------------------------------

    }
}