//using APITIENDA.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace APITIENDA.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PedidoController : ControllerBase
//    {


//        private readonly DbtiendaContext _context;

//        public PedidoController(DbtiendaContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {
//            List<Producto> lista = new List<Producto>();


//            try
//            {
//                var datos = _context.Productos.Include(p => p.IdCategoriaNavigation);

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "0k", listaa = datos });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }
//        }

//    }
//}
