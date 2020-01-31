using EmpleadosEBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EmpleadosEBS.Data;

namespace EmpleadosEBS.ViewModels
{
    public class PedidoUsuarioViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<User> Usuarios { get; set; }

    }
}
