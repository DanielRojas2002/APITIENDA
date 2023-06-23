namespace APITIENDA.Models.ViewModels
{
    public class CrearCuentaVM
    {

        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string? APaterno { get; set; }

        public string? AMaterno { get; set; }

        public string Correo { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

    }
}
