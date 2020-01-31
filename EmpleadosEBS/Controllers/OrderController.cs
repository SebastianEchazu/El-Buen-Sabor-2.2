using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Models;
using EmpleadosEBS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using EmpleadosEBS.Data;

namespace EmpleadosEBS.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, UserManager<User> userManager, ApplicationDbContext context )
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            pedido.UserId = userId;

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if(pedido.PorDelivery)
            {
                pedido.PrecioVenta = _shoppingCart.GetShoppingCartTotal();
            } else
            {
                pedido.PrecioVenta = _shoppingCart.GetShoppingCartTotal() * 0.90;
            }
            pedido.FechaHora = DateTime.Now;
            pedido.EstadoPedidoID = 1;
            //agrege numero de pedido que parsea la fecha cuando se hace el pedido
            DateTime numeropedido = DateTime.Now;
            pedido.NumeroPedido = Int32.Parse(numeropedido.ToString("yyyyMMdd"));

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Tu carrito de compras esta vacio por favor agrega algo");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(pedido);
                _shoppingCart.ClearCart();
 
                return RedirectToAction("CheckoutComplete");
            }

            return View(pedido);
        }

        public IActionResult CheckoutComplete()
        {
            Random rnd = new Random();
            int espera = rnd.Next(15, 45);

            ViewBag.CheckoutCompleteMessage = "Gracias por su orden";
            ViewBag.TiempoEspera = espera + " minutos";
            return View();
        }
    }
}