using System.ComponentModel.DataAnnotations;

namespace CapaNegocios;

public class UsuarioModelo
{
    public int IdUsuario { get; set; }

    [Display(Name = "Nombre de Usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(150)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [StringLength(150)]
    public string Apellidos { get; set; } = string.Empty;

    [Display(Name = "Correo Electrónico")]
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Correo inválido")]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mínimo 6 caracteres")]
    public string? Password { get; set; }

    [Display(Name = "Rol")]
    [Required(ErrorMessage = "El rol es obligatorio")]
    public int IdRol { get; set; }

    [Display(Name = "Rol")]
    public string? NombreRol { get; set; }

    public bool Activo { get; set; } = true;

    [Display(Name = "Creado")]
    public DateTime? FechaCreacion { get; set; }

    [Display(Name = "Teléfono")]
    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public int Telefono { get; set; }

    [Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Display(Name = "Tipo de Usuario")]
    [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
    public string TipoUsuario { get; set; } = "Cliente";
}
