using EmpleadosEBS.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Models
{
    public class Factura
    {
        public Pedido Pedido { get; set; }
        public User Usuario { get; set; }
        public IEnumerable<DetPedido> DetPedidos { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
        public IEnumerable<Plato> Platos { get; set; }
        
    }
}
