using EmpleadosEBS.Data;
using EmpleadosEBS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Hubs
{
    public class InformeHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        
        public InformeHub(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //-------------------------------------------------------------------------

        public async Task EnviarInformeIngresos(DateTime inicio, DateTime final)
        {

            double resultado = await GetIngresoAsync(inicio, final);
            await Clients.All.SendAsync("RecibirInformeIngresos", resultado);
        }
        private Task<double> GetIngresoAsync(DateTime fechaInicio, DateTime fechaFinal)
        {
            double ingreso;
            var ingresos = _context.Pedido.Where(d => d.FechaHora > fechaInicio && d.FechaHora < fechaFinal)
                                            .Select(p => p.PrecioVenta).ToList();
            ingreso = ingresos.Sum();
            return Task.FromResult(ingreso);
        }
        //-------------------------------------------------------------------------
        public async Task EnviarInformePedidosTiempo(DateTime inicio, DateTime final)
        {
            double resultado = await GetPedidosAsync(inicio, final);
            await Clients.All.SendAsync("RecibirInformePedidosTiempo", resultado);
        }
        private Task<double> GetPedidosAsync(DateTime fechaInicio, DateTime fechaFinal)
        {
            double pedido;
            var pedidos = _context.Pedido.Where(d => d.FechaHora > fechaInicio && d.FechaHora < fechaFinal)
                                            .Select(p => p.PrecioVenta).ToList();
            pedido = pedidos.Count();
            return Task.FromResult(pedido);
        }
        //-------------------------------------------------------------------------
        public async Task EnviarInformePedidosCliente(string nombre, DateTime inicio, DateTime final)
        {

            double resultado = await GetPedidosNombreAsync(nombre, inicio, final);

            await Clients.All.SendAsync("RecibirInformePedidosCliente", resultado);
        }

        private Task<double> GetPedidosNombreAsync(string nombre, DateTime fechaInicio, DateTime fechaFinal)
        {
            double pedido;

            var usuario = _userManager.Users.Single(d => d.UserName == nombre).Id;

            var pedidos = _context.Pedido.Where(d => d.FechaHora > fechaInicio && d.FechaHora < fechaFinal && d.UserId == usuario)
                                            .Select(p => p.PrecioVenta).ToList();
            pedido = pedidos.Count();
            return Task.FromResult(pedido);
        }
        //-------------------------------------------------------------------------
        public async Task EnviarInformeRegistroClientes(DateTime inicio, DateTime final)
        {

            double resultado = await GetRegistroClientesAsync(inicio, final);

            await Clients.All.SendAsync("RecibirInformeRegistroClientes", resultado);
        }
        private Task<double> GetRegistroClientesAsync(DateTime fechaInicio, DateTime fechaFinal)
        {
            double usuario;

            var clientes = _userManager.GetUsersInRoleAsync("cliente").Result;

            var usuarios = clientes.Where(d => d.Registro > fechaInicio &&
            d.Registro < fechaFinal).Select(d => d.Id).ToList();

            usuario = usuarios.Count();

            return Task.FromResult(usuario);
        }
    }
}
