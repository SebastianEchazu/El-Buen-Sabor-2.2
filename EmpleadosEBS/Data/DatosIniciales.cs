using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using EmpleadosEBS.Models;
using Microsoft.AspNetCore.Identity;

namespace EmpleadosEBS.Data
{
    public static class DatosIniciales
    {

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            

            //comprobacion de datos en la tabla EstadoPedido----------------------------------------------------------------
            if (context.EstadoPedido.Any())
            {
                return;//si no hay datos no retorna nada
            }
            var estados = new EstadoPedido[]
            {
                new EstadoPedido{Denominacion = 1, Descripcion = "Solicitado"},
                new EstadoPedido{Denominacion = 2, Descripcion = "Aceptado"},
                new EstadoPedido{Denominacion = 3, Descripcion = "Cocinado"},
                new EstadoPedido{Denominacion = 4, Descripcion = "Entregado"},
             };
            foreach (EstadoPedido p in estados)
            {
                context.EstadoPedido.Add(p);
            }
            context.SaveChanges();

            //comprobacion de datos en la tabla Plato-----------------------------------------------------------------------
            if (context.Plato.Any())
            {
                return;//si no hay datos no retorna nada
            }
            var platos = new Plato[]
            {
                new Plato{
                    Denominacion = "Lomo Completo",
                    Descripcion = "Sandwich de Lomo Completo con lechuga y tomate",
                    Imagen = "/img/Plato/lomo.jpg",
                    PrecioVenta = 100,
                    Aprobado = true
                },
                new Plato{
                    Denominacion = "Lomo Simple",
                    Descripcion = "Sandwich de Lomo sin vegetales",
                    Imagen = "/img/Plato/lomo2.jpg",
                    PrecioVenta = 80,
                    Aprobado = true
                },
                new Plato{
                    Denominacion = "Hamburguesa",
                    Descripcion = "Sandwich de Hamburguesa sin vegetales",
                    Imagen = "/img/Plato/hamburguesa.jpg",
                    PrecioVenta = 80,
                    Aprobado = true
                },
                new Plato{ Denominacion = "Pancho",
                    Descripcion = "Pancho Simple con aderezos",
                    Imagen = "/img/Plato/pancho.jpg",
                    PrecioVenta = 50, Aprobado = true
                },
                new Plato{ Denominacion = "Pizza",
                    Descripcion = "Pizza Simple con musarella",
                    Imagen = "/img/Plato/pizza.jpg",
                    PrecioVenta = 90, Aprobado = true
                }
            };
            foreach (Plato p in platos)
            {
                context.Plato.Add(p);
            }
            context.SaveChanges();

            //comprobacion de datos en la tabla articulo--------------------------------------------------------------------
            if (context.Articulo.Any())
            {
                return;//si hay datos no retorna nada
            }
            var articulos = new Articulo[]
                {
                //Lista de Articulos Insumo---------------------------------------------------------------------
                //los ingredientes van en minusculas
                //insumos para Sandwich de Lomo-----------------------------------------------------------------
                new Articulo{Denominacion = "pan lomo",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20,UnidadMedida = "bollo" ,Aprobado = true},
                new Articulo{Denominacion = "lomo",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 ,UnidadMedida = "bife",Aprobado = true},               
                //insumo para Hamburguesa-----------------------------------------------------------------------
                new Articulo{Denominacion = "pan hamburguesa",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20,UnidadMedida = "bollo" ,Aprobado = true},
                new Articulo{Denominacion = "hamburguesa",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 ,UnidadMedida = "Medallon",Aprobado = true},
                //insumos para Panchos---------------------------------------------------------------------------
                new Articulo{Denominacion = "pan pancho",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20,UnidadMedida = "bollo" ,Aprobado = true},
                new Articulo{Denominacion = "salchicha",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 ,UnidadMedida = "una",Aprobado = true},
                //insumos para sandwichs varios-----------------------------------------------------------------
                new Articulo{Denominacion = "lechuga",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20,UnidadMedida = "hojas" ,Aprobado = true},
                new Articulo{Denominacion = "tomate",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 , UnidadMedida ="rodajas",Aprobado = true},
                //insumos para pizza----------------------------------------------------------------------------
                new Articulo{Denominacion = "prepizza",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 , UnidadMedida ="una",Aprobado = true},
                new Articulo{Denominacion = "salsa",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 , UnidadMedida ="lts",Aprobado = true},
                new Articulo{Denominacion = "queso musarella",PrecioCompra = 10,PrecioVenta = 0, EsInsumo = true ,
                    Stock = 20 , UnidadMedida ="gramos",Aprobado = true},
                //articulos bebidas va la primera con mayusculas
                //Lista de articulos Bebidas--------------------------------------------------------------------
                new Articulo{Denominacion = "Coca Zero",PrecioCompra = 10,PrecioVenta = 20, EsInsumo = false ,
                    Stock = 10 , UnidadMedida ="botella",Aprobado = true},
                new Articulo{Denominacion = "Coca Comun",PrecioCompra = 10,PrecioVenta = 20, EsInsumo = false ,
                    Stock = 10 , UnidadMedida ="Botella",Aprobado = true},
                new Articulo{Denominacion = "Cerveza",PrecioCompra = 20,PrecioVenta = 40, EsInsumo = false ,
                    Stock = 10 , UnidadMedida ="Botella",Aprobado = true},
                new Articulo{Denominacion = "Cerveza Negra",PrecioCompra = 20,PrecioVenta = 40, EsInsumo = false ,
                    Stock = 10 , UnidadMedida ="Botella",Aprobado = true},
                 new Articulo{Denominacion = "Cerveza Roja",PrecioCompra = 20,PrecioVenta = 40, EsInsumo = false ,
                    Stock = 10 , UnidadMedida ="Botella",Aprobado = true}
                };
            foreach (Articulo a in articulos)
            {
                context.Articulo.Add(a);
            }
            context.SaveChanges();

            //comprueba si no hay recetas cargadas--------------------------------------------------------------------------
            if (context.Receta.Any())
            {
                return;//si no hay datos no retorna nada
            }
            var recetas = new Receta[]
            {
                //Platos Primera letra en mayusculas y ingredientes en minusculas
                //receta de Sandwich de lomo Completo------------------------------------------------------------------
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p=> p.Denominacion == "Lomo Completo").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "pan lomo").ID
                },
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Lomo Completo").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "lomo").ID
                },
                new Receta{
                    Cantidad = 2,
                    PlatoID = platos.Single(p => p.Denominacion == "Lomo Completo").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "lechuga").ID
                },
                new Receta{
                    Cantidad = 2,
                    PlatoID = platos.Single(p => p.Denominacion == "Lomo Completo").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "tomate").ID
                }
                ,               
                //receta de Sandwich de Lomo simple--------------------------------------------------------------------
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Lomo Simple").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "pan lomo").ID
                },
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Lomo Simple").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "lomo").ID
                }
                ,
                //receta de Hamburgesa---------------------------------------------------------------------------------
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Hamburguesa").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "pan hamburguesa").ID
                },
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Hamburguesa").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "hamburguesa").ID
                }
                ,               
                //receta de Pancho-------------------------------------------------------------------------------------
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Pancho").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "pan pancho").ID
                },
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Pancho").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "salchicha").ID
                }
                ,
                //receta de Pizza--------------------------------------------------------------------------------------
                new Receta{
                    Cantidad = 1,
                    PlatoID = platos.Single(p => p.Denominacion == "Pizza").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "prepizza").ID
                },
                new Receta{
                    Cantidad = 0.2,
                    PlatoID = platos.Single(p => p.Denominacion == "Pizza").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "salsa").ID
                },
                new Receta{
                    Cantidad = 0.5,
                    PlatoID = platos.Single(p => p.Denominacion == "Pizza").ID,
                    ArticuloID = articulos.Single(a => a.Denominacion == "queso musarella").ID
                },
            };
            foreach (Receta r in recetas)
            {
                context.Receta.Add(r);
            }
            context.SaveChanges();

            //comprueba si existen Pedidos---------------------------------------------------------------------------------
            if (context.Pedido.Any())
            {
                return;//Si no hay pedidos No retorna nada
            }
            var pedidos = new Pedido[]
                {
                    //pedidos En la cola del negocio--------------------------------------------------------------
                    new Pedido{
                        NumeroPedido = 1001,
                        FechaHora = DateTime.Parse("19/8/2019 09:00:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Solicitado").ID,
                        PorDelivery = true,
                        PrecioVenta = 100,
                        Facturado = false,
                      
                       
                        
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1002,
                        FechaHora = DateTime.Parse("19/8/2019 09:30:00 AM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Solicitado").ID,
                        PorDelivery = false,
                        PrecioVenta = 200,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1003,
                        FechaHora = DateTime.Parse("19/8/2019 10:00:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Solicitado").ID,
                        PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1004,
                        FechaHora = DateTime.Parse("19/8/2019 10:30:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Aceptado").ID,
                        PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1005,
                        FechaHora = DateTime.Parse("19/8/2019 11:00:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Aceptado").ID,
                        PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1006,
                        FechaHora = DateTime.Parse("19/8/2019 11:30:00 AM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Aceptado").ID,
                        PorDelivery = false,
                        PrecioVenta = 100,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1007,
                        FechaHora = DateTime.Parse("19/8/2019 01:00:00 PM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Cocinado").ID,
                        PorDelivery = true,
                        PrecioVenta = 200,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1008,
                        FechaHora = DateTime.Parse("19/8/2019 01:30:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Cocinado").ID,
                        PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    new Pedido{
                        NumeroPedido = 1009,
                        FechaHora = DateTime.Parse("19/8/2019 02:00:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    new Pedido{NumeroPedido = 1010,
                        FechaHora = DateTime.Parse("19/8/2019 02:30:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = false
                    }
                    ,
                    //---------------------------------------------------------------------------------------------
                    //pedidos ya pagados---------------------------------------------------------------------------
                    new Pedido{NumeroPedido = 1011,
                        FechaHora = DateTime.Parse("20/8/2019 09:00:00 AM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID
                        ,PorDelivery = true ,
                        PrecioVenta = 100,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1012,
                        FechaHora = DateTime.Parse("20/8/2019 09:30:00 AM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = false,
                        PrecioVenta = 200,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1013,
                        FechaHora = DateTime.Parse("21/8/2019 10:00:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1014,
                        FechaHora = DateTime.Parse("21/8/2019 10:30:00 AM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1015,
                        FechaHora = DateTime.Parse("22/8/2019 11:00:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1016,
                        FechaHora = DateTime.Parse("22/8/2019 11:30:00 AM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = false ,
                        PrecioVenta = 100,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1017,
                        FechaHora = DateTime.Parse("23/8/2019 01:00:00 PM" ),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = true,
                        PrecioVenta = 200,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1018,
                        FechaHora = DateTime.Parse("23/8/2019 01:30:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID,
                        PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1019,
                        FechaHora = DateTime.Parse("24/8/2019 02:00:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID
                        ,PorDelivery = true,
                        PrecioVenta = 150,
                        Facturado = true
                    }
                    ,
                    new Pedido{NumeroPedido = 1020,
                        FechaHora = DateTime.Parse("24/8/2019 02:30:00 PM"),
                        EstadoPedidoID = estados.Single(p => p.Descripcion == "Entregado").ID
                        ,PorDelivery = false,
                        PrecioVenta = 150,
                        Facturado = true
                    }

                };

            foreach (Pedido p in pedidos)
            {
                context.Pedido.Add(p);
            }
            context.SaveChanges();


            //comprueba si hay detalles de Pedidos--------------------------------------------------------------------------
            if (context.DetPedido.Any())
            {
                return;//Sino hay datos no retorna nada

            }
            var detalles = new DetPedido[]
                {
                    //pedido 1001--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1001).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Comun").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Comun").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido{
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1001).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Completo").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Completo").PrecioVenta,
                        PrecioArticulo = 0

                    }
                    ,
                    //pedido 1002--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1002).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Zero").ID ,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Zero").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1002).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Simple").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Simple").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1003--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1003).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1003).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Hamburguesa").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Hamburguesa").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1004--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1004).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Negra").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Negra").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1004).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Pancho").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pancho").PrecioVenta,
                        PrecioArticulo = 0
                    },
                    //pedido 1005--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1005).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Roja").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Roja").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1005).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Pizza").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pizza").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                     //pedido 1006--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1006).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Comun").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Comun").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1006).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Completo").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Completo").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1007--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1007).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Zero").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Zero").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1007).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Simple").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Simple").PrecioVenta,
                        PrecioArticulo = 0
                    },
                    //pedido 1008--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1008).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1008).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Hamburguesa").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Hamburguesa").PrecioVenta,
                        PrecioArticulo = 0
                    },
                    //pedido 1009--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1009).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Negra").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Negra").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1009).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Pancho").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pancho").PrecioVenta,
                        PrecioArticulo = 0
                    },
                    //pedido 1010--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1010).ID,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Roja").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Roja").PrecioVenta,
                        PrecioPlato = 0
                    },
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1010).ID,
                        PlatoID = platos.Single(a => a.Denominacion == "Pizza").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pizza").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //-------------------------------------------------------------------------------------
                    //pedidos ya pagados-------------------------------------------------------------------
                    //pedido 1011--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1011).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Comun").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Comun").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1011).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Completo").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Completo").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1012--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1012).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Zero").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Zero").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1012).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Simple").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Simple").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1013--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1013).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido{Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1013).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Hamburguesa").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Hamburguesa").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1014--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1014).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Negra").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Negra").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1014).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Pancho").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pancho").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1015--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1015).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Roja").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Roja").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1015).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Pizza").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pizza").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                     //pedido 1016--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1016).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Comun").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Comun").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1016).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Completo").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Completo").PrecioVenta,
                        PrecioArticulo = 0

                    }
                    ,
                    //pedido 1017--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1017).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Coca Zero").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Coca Zero").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1017).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Lomo Simple").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Lomo Simple").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1018--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1018).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1018).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Hamburguesa").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Hamburguesa").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1019--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 1 ,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1019).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Negra").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Negra").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 1,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1019).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Pancho").ID,
                         PrecioPlato = platos.Single(a=> a.Denominacion == "Pancho").PrecioVenta,
                        PrecioArticulo = 0
                    }
                    ,
                    //pedido 1020--------------------------------------------------------------------------
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1020).ID ,
                        ArticuloID = articulos.Single(a => a.Denominacion == "Cerveza Negra").ID,
                        PrecioArticulo = articulos.Single(a => a.Denominacion == "Cerveza Negra").PrecioVenta,
                        PrecioPlato = 0
                    }
                    ,
                    new DetPedido
                    {
                        Cantidad = 2,
                        PedidoID = pedidos.Single(p => p.NumeroPedido == 1020).ID ,
                        PlatoID = platos.Single(a => a.Denominacion == "Pizza").ID,
                        PrecioPlato = platos.Single(a=> a.Denominacion == "Pizza").PrecioVenta,
                        PrecioArticulo = 0
                    }

                };
            foreach (DetPedido p in detalles)
            {
                context.DetPedido.Add(p);
            }
            context.SaveChanges();


        }
    }
}
