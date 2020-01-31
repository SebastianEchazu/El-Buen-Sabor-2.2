using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using EmpleadosEBS.Models.PedidoIndexData;
using EmpleadosEBS.Models.PlatoIndexData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmpleadosEBS.Controllers
{
    [Authorize(Roles = "Cocinero")]
    public class CocineroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CocineroController(ApplicationDbContext context)
        {
            _context = context;
        }
        //--------------------------------------------------------------------------------
        //INDEX DE COCINERO
        //--------------------------------------------------------------------------------
        public IActionResult Index()
        {

            return View();
        }
        //--------------------------------------------------------------------------------
        //SECCION DE COCINEROINSUMO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: Cocinero
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> IndexInsumo()
        {
            return View(await _context.Articulo.Where(i => i.EsInsumo == true).
                Where(i => i.Aprobado == true).ToListAsync());
        }
        //--------------------------------------------------------------------------------
        // GET: Cocinero/EditInsumo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> EditInsumo(int? id)
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
        //--------------------------------------------------------------------------------
        // POST: Cocinero/EditInsumo
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInsumo(int id, [Bind("ID,Denominacion,PrecioCompra," +
            "PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")] Articulo articulo)
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
                return RedirectToAction(nameof(IndexInsumo));
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // GET: Cocinero/CreateInsumo
        //--------------------------------------------------------------------------------
        public IActionResult CreateInsumo()
        {
            return View();
        }
        //--------------------------------------------------------------------------------
        // POST:Cocinero/CreateInsumo
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInsumo([Bind("ID,Denominacion,PrecioCompra,PrecioVenta," +
            "EsInsumo,Stock,UnidadMedida,Aprobado")] Articulo articulo)
        {
            articulo.Aprobado = false;
            articulo.PrecioVenta = 0;
            articulo.EsInsumo = true;

            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexInsumo));
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        private bool ArticuloExists(int id)
        {
            return _context.Articulo.Any(e => e.ID == id);
        }
        //--------------------------------------------------------------------------------
        //FIN DE COCINEROINSUMO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //SECCION COCINEROPEDIDO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: CocineroPedido
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> IndexPedido()
        {

            var viewModel = new PedidoIndexData();
            viewModel.Pedidos = await _context.Pedido
                .Include(p => p.DetPedidos)
                    .ThenInclude(p => p.Plato)
                .Include(d => d.EstadoPedido)
                    .AsNoTracking()
                    .OrderBy(i => i.FechaHora)
                    .ToListAsync();

            return View(viewModel);
        }
        //--------------------------------------------------------------------------------
        // GET: Cocinero/EditPedido
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> EditPedido(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(d => d.DetPedidos)
                    .ThenInclude(p => p.Plato)
                    .ThenInclude(r => r.Recetas)
                     .AsNoTracking()
                     .FirstOrDefaultAsync(m => m.ID == id);

            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);


        }
        //--------------------------------------------------------------------------------
        //POST: Cocinero/EditPedido
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPedido(int id, [Bind("ID,NumeroPedido" +
            ",EstadoPedidoID,PorDelivery,FechaHora,PrecioVenta,UserId")] Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return NotFound();
            }

            pedido.EstadoPedidoID = 3;

            var detalles = _context.DetPedido
                .Include(p => p.Pedido).Where(i => i.PedidoID == pedido.ID)
                .AsNoTracking();

            var platos = _context.Plato
                .Include(p => p.DetPedidos)
                .AsNoTracking();

            foreach (var detalle in detalles)
            {
                foreach (var plato in platos)
                {
                    if (detalle.PlatoID == plato.ID)
                    {
                        DescuentoArticulosPlato(plato, detalle.Cantidad);
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
            ViewData["EstadoPedidoID"] = new SelectList(_context.EstadoPedido, "ID",
                "Descripcion", pedido.EstadoPedidoID);
            return View(pedido);
        }
        //--------------------------------------------------------------------
        private void DescuentoArticulosPlato(Plato plato,int cantidad)
        {
            var articulos = _context.Articulo
                .Include(receta => receta.Recetas)
                    .Where(p => p.EsInsumo == true)
                    .ToList();

            var recetas = _context.Receta
                .Include(p => p.Plato)
                .Where(p => p.PlatoID == plato.ID)
                .ToList();
            
            foreach (var receta in recetas)
            {
                foreach (var articulo in articulos)
                {
                    if (articulo.ID == receta.ArticuloID)
                    {
                        double v = articulo.Stock - (receta.Cantidad*cantidad);

                        articulo.Stock = v;

                        _context.Update(articulo);
                    }
                }
            }

        }
        //--------------------------------------------------------------------------------
        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.ID == id);
        }
        //--------------------------------------------------------------------------------
        //FIN DE COCINEROPEDIDO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // SECCION COCINEROPLATO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: Cocinero/Plato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> IndexPlato()
        {
            var viewModel = new PlatoIndexData();
            viewModel.Platos = await _context.Plato
                .Include(r => r.Recetas)
                    .ThenInclude(a => a.Articulo)
                    .AsNoTracking()
                    .OrderBy(i => i.Denominacion)
                    .ToListAsync();

            return View(viewModel);
        }
        //--------------------------------------------------------------------------------
        // GET: Cocinero/CreatePlato
        //--------------------------------------------------------------------------------
        public IActionResult CreatePlato()
        {
            var plato = new Plato();
            plato.Recetas = new List<Receta>();
            ArticulosPlatoData(plato);
            return View();
        }
        //--------------------------------------------------------------------------------
        // POST: Cocinero/CreatePlato
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlato([Bind("ID, Denominacion, Descripcion" +
            ", Imagen, PrecioVenta, Aprobado")]Plato plato, List<ArticuloAsignado> articulosSeleccionados)
        {
            if (articulosSeleccionados != null)
            {
                plato.Recetas = new List<Receta>();

                foreach (var articulo in articulosSeleccionados)
                {
                    var articuloToAdd = new Receta
                    {
                        PlatoID = plato.ID,
                        ArticuloID = _context.Articulo.Where(w => w.Denominacion == articulo.Denominacion).Select(s => s.ID).FirstOrDefault(),
                        Cantidad = articulo.Cantidad
                    };

                    plato.Recetas.Add(articuloToAdd);

                }
            }
            plato.Aprobado = true;

            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexPlato));

            }

            ArticulosPlatoData(plato);
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // GET: Cocinero/EditPlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> EditPlato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .Include(i => i.Recetas)
                .ThenInclude(i => i.Articulo)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plato == null)
            {
                return NotFound();
            }
            ArticulosPlatoData(plato);
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        private void ArticulosPlatoData(Plato plato)
        {
            var allArticulos = _context.Articulo
                .Include(r => r.Recetas)
                .ThenInclude(p => p.Plato)
                .Where(p => p.EsInsumo == true);

            var PlatosArticulos = new HashSet<int>(plato.Recetas.Select(c => c.ID));

            var ArticuloReceta = _context.Receta.Select(s => s.ID);

            var viewModel = new List<ArticuloAsignado>();

            foreach (var articulo in allArticulos)
            {
                viewModel.Add(new ArticuloAsignado
                {
                    ID = articulo.ID,
                    Denominacion = articulo.Denominacion,
                    Asignado = PlatosArticulos.Contains(articulo.ID),
                    Cantidad = articulo.Recetas.Where(w => w.ArticuloID == articulo.ID).Select(s => s.Cantidad).First(),
                    UnidadMedida = articulo.UnidadMedida

                });
            }
            ViewData["Articulos"] = viewModel;
        }
        //--------------------------------------------------------------------------------
        // POST: Cocinero/EditPlato
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlato(int? id, string[] articulosSeleccionados)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoToUpdate = await _context.Plato
                    .Include(i => i.Recetas)
                    .ThenInclude(i => i.Articulo)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Plato>(
                platoToUpdate,
                "",
                i => i.Denominacion, i => i.PrecioVenta, i => i.Recetas))
            {

                UpdateArticulosPlato(articulosSeleccionados, platoToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "No se pueden guardar los cambios" +
                        " Inténtalo de nuevo, y si el problema persiste,ve al " +
                        "administrador de tu sistema" + ex.ToString());
                }
                return RedirectToAction(nameof(IndexPlato));
            }
            UpdateArticulosPlato(articulosSeleccionados, platoToUpdate);
            ArticulosPlatoData(platoToUpdate);
            return View(platoToUpdate);
        }
        //--------------------------------------------------------------------------------
        private void UpdateArticulosPlato(string[] articuloSeleccionados
            , Plato platoToUpdate)
        {
            if (articuloSeleccionados == null)
            {
                platoToUpdate.Recetas = new List<Receta>();
                return;
            }

            var articulosSeleccionadosHS = new HashSet<string>(articuloSeleccionados);
            var articulosPlato = new HashSet<int>
                (platoToUpdate.Recetas.Select(c => c.Articulo.ID));
            foreach (var articulo in _context.Articulo)
            {
                if (articulosSeleccionadosHS.Contains(articulo.ID.ToString()))
                {
                    if (!articulosPlato.Contains(articulo.ID))
                    {
                        platoToUpdate.Recetas.Add(new Receta
                        {
                            PlatoID = platoToUpdate.ID
                            ,
                            ArticuloID = articulo.ID,
                            Cantidad = 1
                        });
                    }
                }
                else
                {
                    if (articulosPlato.Contains(articulo.ID))
                    {
                        Receta articuloToRemove = platoToUpdate.Recetas
                            .FirstOrDefault(i => i.ID == articulo.ID);
                        _context.Remove(articuloToRemove);
                    }
                }
            }
            //--------------------------------------------------------------------------------
            //FIN DE COCINEROPLATO
            //--------------------------------------------------------------------------------
        }
    }



}