using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using EmpleadosEBS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosEBS.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdministradorController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //--------------------------------------------------------------------------------
        //INDEX DE ADMINISTRADOR
        //--------------------------------------------------------------------------------
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------
        public IActionResult Registrar()
        {
            return View();
        }
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //SECCION DE ADMINISTRADORPEDIDO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: AdministradorPedido
        //--------------------------------------------------------------------------------
        public IActionResult IndexPedido()
        {
            // var applicationDbContext = _context.Pedido.Include(p => p.EstadoPedido);
            // return View(await applicationDbContext.ToListAsync());

            PedidoUsuarioViewModel model = new PedidoUsuarioViewModel();
            model.Pedidos = _context.Pedido.Include(p => p.EstadoPedido).ToList();
            model.Usuarios = _userManager.Users.ToArray();

            return View(model);
        }
        //--------------------------------------------------------------------------------
        //FIN DE ADMINISTRADORPEDIDO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //SECCION ADMINISTRADORARTICULO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: AdministradorArticulo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> IndexArticulo()
        {
            var articulo = await _context.Articulo.ToListAsync();
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/CreateArticulo
        //--------------------------------------------------------------------------------
        public IActionResult CreateArticulo()
        {
            return View();
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/CreateArticulo
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticulo([Bind("ID,Denominacion" +
            ",PrecioCompra,PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")]
                Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexArticulo));
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/EditArticulo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> EditArticulo(int? id)
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
        // POST: Administrador/EditArticulo
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticulo(int id, [Bind("ID,Denominacion," +
            "PrecioCompra,PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")]
                Articulo articulo)
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
                return RedirectToAction(nameof(IndexArticulo));
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/RevisarArticulo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> RevisarArticulo(int? id)
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
        // POST: Administrador/RevisarArticulo
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevisarArticulo(int id, [Bind("ID,Denominacion" +
            ",PrecioCompra,PrecioVenta,EsInsumo,Stock,UnidadMedida,Aprobado")]
                Articulo articulo)
        {
            if (id != articulo.ID)
            {
                return NotFound();
            }

            articulo.Aprobado = true;

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
                return RedirectToAction(nameof(IndexArticulo));
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/DeleteArticulo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> DeleteArticulo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var articulo = await _context.Articulo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/DeleteArticulo
        //--------------------------------------------------------------------------------
        [HttpPost, ActionName("DeleteArticulo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArticuloConfirmed(int id)
        {
            var articulo = await _context.Articulo.FindAsync(id);
            _context.Articulo.Remove(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexArticulo));
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/DeleteArticulo
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> DescartarArticulo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var articulo = await _context.Articulo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/DeleteArticulo
        //--------------------------------------------------------------------------------
        [HttpPost, ActionName("DescartarArticulo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DescartarArticuloConfirmed(int id)
        {
            var articulo = await _context.Articulo.FindAsync(id);
            _context.Articulo.Remove(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexArticulo));
        }
        //--------------------------------------------------------------------------------
        private bool ArticuloExists(int id)
        {
            return _context.Articulo.Any(e => e.ID == id);
        }
        //--------------------------------------------------------------------------------
        //FIN ADMINISTRADORARTICULO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //SECCION ADMINISTRADORPLATO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: AdministradorPlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> IndexPlato()
        {
            return View(await _context.Plato.ToListAsync());
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/CreatePlato
        //--------------------------------------------------------------------------------
        public IActionResult CreatePlato()
        {
            return View();
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/CreatePlato
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlato([Bind("ID,Denominacion,Descripcion" +
            ",PrecioVenta,Aprobado")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexPlato));
            }
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/EditPlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> EditPlato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        //POST: Administrador/EditPlato
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlato(int id, [Bind("ID,Denominacion" +
            ",Descripcion,PrecioVenta,Aprobado")] Plato plato)
        {
            if (id != plato.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexPlato));
            }
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/EditPlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> RevisarPlato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // POST Administrador/RevisarPlato
        //--------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevisarPlato(int id, [Bind("ID,Denominacion" +
            ",Descripcion,PrecioVenta,Aprobado")] Plato plato)
        {
            if (id != plato.ID)
            {
                return NotFound();
            }

            plato.Aprobado = true;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexPlato));
            }
            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/DeletePlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> DeletePlato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/DeletePlato
        //--------------------------------------------------------------------------------
        [HttpPost, ActionName("DeletePlato")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlatoConfirmed(int id)
        {
            var plato = await _context.Plato.FindAsync(id);
            _context.Plato.Remove(plato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexPlato));
        }
        //--------------------------------------------------------------------------------
        // GET: Administrador/DescartarPlato
        //--------------------------------------------------------------------------------
        public async Task<IActionResult> DescartarPlato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }
        //--------------------------------------------------------------------------------
        // POST: Administrador/DeletePlato
        //--------------------------------------------------------------------------------
        [HttpPost, ActionName("DescartarPlato")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DescartarPlatoConfirmed(int id)
        {
            var plato = await _context.Plato.FindAsync(id);
            _context.Plato.Remove(plato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexPlato));
        }
        //--------------------------------------------------------------------------------
        private bool PlatoExists(int id)
        {
            return _context.Plato.Any(e => e.ID == id);
        }
        //--------------------------------------------------------------------------------
        //FIN ADMINISTRADORPLATO
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //SECCION ADMINISTRADORINFORMES
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        // GET: Administradorinformes
        //--------------------------------------------------------------------------------
        public IActionResult IndexInformes()
        {
            return View();
        }
    }


}