using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class LoginViewModel
{
    [Display(Name = "Usuario o correo")]
    [Required(ErrorMessage = "El usuario o correo es obligatorio.")]
    public string UsuarioOCorreo { get; set; } = string.Empty;

    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = string.Empty;
}
