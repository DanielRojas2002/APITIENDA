using APITIENDA.Models.ViewModels;
using APITIENDA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;

namespace APITIENDA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbtiendaContext _context;

        public UsuarioController(DbtiendaContext context)
        {

            _context = context;
        }

        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration, DbtiendaContext context)
        {

            _configuration = configuration;
            _context = context;
        }



        [HttpPost]
        [Route("CrearCuenta")]
        public IActionResult CrearCuenta(CrearCuentaVM modelo)
        {
            try
            {
                var existingUser = _context.Usuarios.Where(p => p.Correo == modelo.Correo).FirstOrDefault();


                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "El correo electrónico ya está registrado.");
                    return BadRequest(ModelState);
                }

                // Verificar la fortaleza de la contraseña
                bool isPasswordValid = IsPasswordValid(modelo.Contrasena);
                if (!isPasswordValid)
                {
                    ModelState.AddModelError(string.Empty, "La contraseña no cumple con los requisitos de seguridad.");
                    return BadRequest(ModelState);
                }

                var user = new Usuario
                {
                    IdRol = modelo.IdRol,
                    Nombre = modelo.Nombre,
                    Correo = modelo.Correo,
                    Contrasena = modelo.Contrasena,
                    FechaRegistro = DateTime.Now
                };

                if (modelo.APaterno != null)
                {
                    user.APaterno = modelo.APaterno;
                }

                if (modelo.AMaterno != null)
                {
                    user.AMaterno = modelo.AMaterno;
                }


                _context.Usuarios.Add(user);
                _context.SaveChanges();


                var usuariocreado = _context.Usuarios.Where(p => p.Correo == user.Correo && p.Contrasena == user.Contrasena);


                foreach (var dato in usuariocreado)
                {
                    var usuariocreadoo = new Usuario
                    {
                        IdUsuario = dato.IdUsuario,
                        Correo = dato.Correo,
                        Contrasena = dato.Contrasena

                    };

                    var token = GenerateJwtToken(usuariocreadoo.IdUsuario);
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "0k", token = token });
                }

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }







        }

        bool IsPasswordValid(string password)
        {
            // Verificar la longitud mínima de la contraseña
            if (password.Length < 8)
            {
                return false;
            }

            // Verificar si la contraseña contiene al menos un número
            if (!Regex.IsMatch(password, @"\d+"))
            {
                return false;
            }

            // Verificar si la contraseña contiene al menos una letra minúscula
            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return false;
            }

            // Verificar si la contraseña contiene al menos una letra mayúscula
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return false;
            }



            // La contraseña cumple con todos los requisitos de fortaleza
            return true;
        }



        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginVM modelo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(modelo.correo, modelo.contraseña,true, lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            var user = await _userManager.FindByEmailAsync(modelo.correo);
        //            var token = GenerateJwtToken(user);
        //            return Ok(new { token });
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    }

        //    return BadRequest(ModelState);
        //}

        //[HttpGet("test")]
        //[Authorize]
        //public IActionResult Test()
        //{
        //    return Ok("Authorized");
        //}

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Nd2Yv4Le53sQ"); // Reemplaza con tu propia clave secreta
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }),
                Expires = DateTime.UtcNow.AddDays(7), // Define la fecha de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
