using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Models;
using EmpleadosEBS.Repositories;
using EmpleadosEBS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosEBS.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IArticuloRepository _articuloRepository;
        private readonly IPlatoRepository _platoRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IArticuloRepository articuloRepository, IPlatoRepository platoRepository, ShoppingCart shoppingCart)
        {
            _articuloRepository = articuloRepository;
            _platoRepository = platoRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public RedirectToActionResult AddToShoppingCart(int id)
        {
            var selectedArticulo = _articuloRepository.Articulo.FirstOrDefault(p => p.ID == id);
            if(selectedArticulo != null)
            {
                _shoppingCart.AddToCart(selectedArticulo, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult AddToShoppingCartPlato(int id)
        {
            var selectedPlato = _platoRepository.Plato.FirstOrDefault(p => p.ID == id);
            if (selectedPlato != null)
            {
                _shoppingCart.AddToCartPlato(selectedPlato, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            var selectedArticulo = _articuloRepository.Articulo.FirstOrDefault(p => p.ID == id);
            if (selectedArticulo != null)
            {
                _shoppingCart.RemoveFromCart(selectedArticulo);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCartPlato(int id)
        {
            var selectedPlato = _platoRepository.Plato.FirstOrDefault(p => p.ID == id);
            if (selectedPlato != null)
            {
                _shoppingCart.RemoveFromCartPlato(selectedPlato);
            }
            return RedirectToAction("Index");
        }
    }
}