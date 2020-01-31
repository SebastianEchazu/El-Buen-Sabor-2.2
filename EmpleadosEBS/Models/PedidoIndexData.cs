using EmpleadosEBS.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Models.PedidoIndexData
{
    public class PedidoIndexData
    {
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<DetPedido> DetPedidos { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<Plato> Platos { get; set; }
        public IEnumerable<EstadoPedido> EstadoPedidos { get; set; }
        public IEnumerable<User> Usuarios { get; set; }
    }
}
