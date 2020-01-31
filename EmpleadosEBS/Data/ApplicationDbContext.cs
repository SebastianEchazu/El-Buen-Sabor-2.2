using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosEBS.Data
{
    //Utiliza todos los tipos de identidad incorporados
    // Utiliza `string` como tipo de clave
    public class ApplicationDbContext : IdentityDbContext<User>


    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

        }
        public DbSet<EmpleadosEBS.Models.Articulo> Articulo { get; set; }
        public DbSet<EmpleadosEBS.Models.Receta> Receta { get; set; }
        public DbSet<EmpleadosEBS.Models.Plato> Plato { get; set; }
        public DbSet<EmpleadosEBS.Models.DetPedido> DetPedido { get; set; }
        public DbSet<EmpleadosEBS.Models.Pedido> Pedido { get; set; }
        public DbSet<EmpleadosEBS.Models.EstadoPedido> EstadoPedido { get; set; }
        public DbSet<EmpleadosEBS.Models.ShoppingCartItem> ShoppingCartItems { get; set; }


    }
}
