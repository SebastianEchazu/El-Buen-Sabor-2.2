using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosEBS.Data;
using EmpleadosEBS.Models;

namespace EmpleadosEBS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Pedido pedido)
        {
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                if (item.Articulo != null)
                {
                    DetPedido detallePedido = new DetPedido()
                    {
                        Cantidad = item.Cantidad,
                        ArticuloID = item.Articulo.ID,
                        PedidoID = pedido.ID,
                        Pedido = pedido,
                        PrecioArticulo = item.Articulo.PrecioVenta
                    };
                    _context.DetPedido.Add(detallePedido);
                }

                if (item.Plato != null)
                {
                    DetPedido detallePedido = new DetPedido()
                    {
                        Cantidad = item.Cantidad,
                        PlatoID = item.Plato.ID,
                        PedidoID = pedido.ID,
                        Pedido = pedido,
                        PrecioPlato = item.Plato.PrecioVenta
                    };
                    _context.DetPedido.Add(detallePedido);
                }
                
            }

            _context.Pedido.Add(pedido);
            _context.SaveChanges();
        }
    }
}
