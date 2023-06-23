using APITIENDA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using APITIENDA.Models.ViewModels;

namespace APITIENDA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly DbtiendaContext _context;

        public ProductoController(DbtiendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListaTodos")]
        public IActionResult ListaTodos()
        {

            try
            {

                List<ProductoVM> lista = new List<ProductoVM>();
                var datos = _context.Productos.Include(p => p.IdCategoriaNavigation);

                foreach (var dato in datos)
                {
                    ProductoVM productovm = new ProductoVM()
                    {
                        producto = new Producto()
                        {
                            IdProducto = dato.IdProducto,
                            Nombre = dato.Nombre,
                            Descripcion = dato.Descripcion,
                            Precio = dato.Precio,
                            Stock = dato.Stock,
                            FechaRegistro = dato.FechaRegistro,
                            IdCategoria = dato.IdCategoria
                        },
                        categoriadescripcion = dato.IdCategoriaNavigation.Descripcion
                    };
                    lista.Add(productovm);
                };


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "0k", listaa = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }


        [HttpGet]
        [Route("ListaConStock")]
        public IActionResult ListaConStock()
        {

            try
            {

                List<ProductoVM> lista = new List<ProductoVM>();
                var datos = _context.Productos.Include(p => p.IdCategoriaNavigation).Where(a => a.Stock > 0);

                foreach (var dato in datos)
                {
                    ProductoVM productovm = new ProductoVM()
                    {
                        producto = new Producto()
                        {
                            IdProducto = dato.IdProducto,
                            Nombre = dato.Nombre,
                            Descripcion = dato.Descripcion,
                            Precio = dato.Precio,
                            FechaRegistro = dato.FechaRegistro,
                            Stock = dato.Stock,
                            IdCategoria = dato.IdCategoria
                        },
                        categoriadescripcion = dato.IdCategoriaNavigation.Descripcion
                    };
                    lista.Add(productovm);
                };


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "0k", listaa = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }

        [HttpGet]
        [Route("ObtenerIdProducto/{IdProducto:int}")]
        public IActionResult ObtenerIdProducto(int IdProducto)
        {

            try
            {

                List<ProductoVM> lista = new List<ProductoVM>();
                Producto producto = new Producto();
                var datos = _context.Productos.Include(p => p.IdCategoriaNavigation).Where(a => a.IdProducto == IdProducto);

                foreach (var dato in datos)
                {
                    ProductoVM productovm = new ProductoVM()
                    {
                        producto = new Producto()
                        {
                            IdProducto = dato.IdProducto,
                            Nombre = dato.Nombre,
                            Descripcion = dato.Descripcion,
                            Precio = dato.Precio,
                            FechaRegistro = dato.FechaRegistro,
                            Stock = dato.Stock,
                            IdCategoria = dato.IdCategoria
                        },
                        categoriadescripcion = dato.IdCategoriaNavigation.Descripcion
                    };
                    lista.Add(productovm);
                };


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "0k", listaa = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }



        }



        [HttpPost]
        [Route("CrearProducto")]
        public IActionResult CrearProducto([FromBody] ProductoM productoM)
        {
            

            try
            {
                var properties = productoM.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(productoM);

                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Datos incompletos" });
                    }
                }

               
                Producto producto = new Producto();
                producto.IdProducto = productoM.IdProducto;
                producto.IdCategoria = productoM.IdCategoria;
                producto.Nombre = productoM.Nombre;
                producto.Descripcion = productoM.Descripcion;
                producto.Precio = productoM.Precio;
                producto.Stock = productoM.Stock;
                producto.FechaRegistro = productoM.FechaRegistro;

                _context.Productos.Add(producto);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Agregado" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error" });
            }
        }

        [HttpPut]
        [Route("EditarProducto")]
        public IActionResult EditarProducto([FromBody] ProductoM objeto)
        {
            try
            {

                var properties = objeto.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(objeto);

                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Datos incompletos" });
                    }
                }

                // Verificar si ya existe un registro con el mismo valor en el campo que no se puede repetir
                //var existeRegistro = _context.Productos.Any(p => p.CodigoBarra == objeto.CodigoBarra);
                //if (existeRegistro)
                //{
                //    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Ya existe un registro con el mismo valor en el campo no repetido." });
                //}

                Producto producto = new Producto()
                {
                    IdProducto = objeto.IdProducto,
                    IdCategoria = objeto.IdCategoria,
                    Nombre = objeto.Nombre,
                    Descripcion = objeto.Descripcion,
                    Precio = objeto.Precio,
                    Stock = objeto.Stock

                };


                _context.Productos.Update(producto);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error" });
            }
           

          
        }

        [HttpDelete]
        [Route("EliminarProducto{idproducto:int}")]
        public IActionResult EliminarProducto(int idproducto)
        {
            try
            {
                var producto = _context.Productos.Find(idproducto);

                _context.Productos.Remove(producto);

                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
