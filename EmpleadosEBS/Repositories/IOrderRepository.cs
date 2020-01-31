using EmpleadosEBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosEBS.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Pedido pedido);
    }
}