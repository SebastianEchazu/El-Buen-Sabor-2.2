using EmpleadosEBS.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpleadosEBS.Models
{
    //------------------------------------------------------------------------------------
    public class Articulo
    {
        public int ID { get; set; }
        [Required]
        public string Denominacion { get; set; }
        [DisplayName("Precio de Compra")]
        public double PrecioCompra { get; set; }
        [DisplayName("Precio de Venta")]
        public double PrecioVenta { get; set; }
        [DisplayName("Es Insumo")]
        public Boolean EsInsumo { get; set; }
        [DisplayName("Stock")]
        public double Stock { get; set; }
        [DisplayName("Unidad")]
        public string UnidadMedida { get; set; }
        [DisplayName("Aprobado")]
        public Boolean Aprobado { get; set; }
        /// <summary>
        /// relacion con las clases Recetas y detpedidos:
        /// </summary>
        //relacion con receta de uno a muchos
        public ICollection<Receta> Recetas { get; set; }
        //relacion con detpedido de uno a muchos
        public ICollection<DetPedido> DetPedidos { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class Receta
    {
        public int ID { get; set; }
        [DisplayName("Articulo")]
        public int ArticuloID { get; set; }
        [DisplayName("Plato")]
        public int PlatoID { get; set; }
        [Required]
        public double Cantidad { get; set; }
        //relacion con articulo de muchos a uno
        public Articulo Articulo { get; set; }
        //relacion con plato de muchos a uno
        public virtual Plato Plato { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class Plato
    {
        public int ID { get; set; }
        [DisplayName("Nombre del Plato")]
        [Required]
        public string Denominacion { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [ScaffoldColumn(false)]
        public string Imagen { get; set; }
        [DisplayName("Precio de Venta")]
        public double PrecioVenta { get; set; }
        [DisplayName("Aprobado")]
        public Boolean Aprobado { get; set; }
        /// <summary>
        /// relacion con la clase Recetas y DetPedidos
        /// </summary>
        //relacion con receta de uno a muchos
        public ICollection<Receta> Recetas { get; set; }
        //relacion con detpedido de uno a muchos
        public ICollection<DetPedido> DetPedidos { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class DetPedido
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }
        [DisplayName("Plato")]
        public int? PlatoID { get; set; }
        [DisplayName("Articulo")]
        public int? ArticuloID { get; set; }
        [DisplayName("Pedido")]
        public int? PedidoID { get; set; }
        [DisplayName("Precio Articulo")]
        public double PrecioArticulo { get; set; }
        [DisplayName("Precio Plato")]
        public double PrecioPlato { get; set; }
        //relacion con receta muchos a uno 
        public Plato Plato { get; set; }
        //relacion con Articulo muchos a uno
        public virtual Articulo Articulo { get; set; }
        //relacion con pedido muchos a uno
        public virtual Pedido Pedido { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class EstadoPedido
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Denominacion")]
        public int Denominacion { get; set; }
        [Required]
        [DisplayName("Descripcion")]
        public String Descripcion { get; set; }
        /// <summary>
        /// Relacion Con la clase Pedido
        /// </summary>
        //Relaciones con pedido uno a muchos
        public ICollection<Pedido> Pedido { get; set; }
    }
    //------------------------------------------------------------------------------------
    public class Pedido
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Numero de Pedido")]
        public int NumeroPedido { get; set; }
        [Required]
        [DisplayName("Estado del Pedido")]
        public int EstadoPedidoID { get; set; }
        [Required]
        [DisplayName("Delivery")]
        public Boolean PorDelivery { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha y hora")]
        public DateTime FechaHora { get; set; }
        [DisplayName("Precio Total")]
        public double PrecioVenta { get; set; }
        [DisplayName("Facturado")]
        public Boolean Facturado { get; set; }
        /// <summary>
        /// Relacion con la clase Det pedido u Estado de pedido
        /// </summary>
        //relacione con detalle pedido de uno a muchos
        public ICollection<DetPedido> DetPedidos { get; set; }
        //relacion con EstadoPEdido de muchos a uno
        public EstadoPedido EstadoPedido { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
    //------------------------------------------------------------------------------------
}