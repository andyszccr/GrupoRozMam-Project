namespace CapaDatos.Entities;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int IdRol { get; set; }
    public string NombreRol { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public DateTime? FechaDesactivacion { get; set; }
    public int Telefono { get; set; }
    public string? Direccion { get; set; }
    public string TipoUsuario { get; set; } = string.Empty;
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordExpiration { get; set; }
}
