using System.ComponentModel.DataAnnotations;

namespace CapaNegocios;

public class MensajeContactoModelo
{
    public int IdMensaje { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(150)]
    public string NombreRemitente { get; set; } = string.Empty;

    [Display(Name = "Correo")]
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Correo inválido.")]
    [StringLength(150)]
    public string CorreoRemitente { get; set; } = string.Empty;

    [Display(Name = "Teléfono")]
    [StringLength(20)]
    public string? TelefonoRemitente { get; set; }

    [StringLength(200)]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "El mensaje es obligatorio.")]
    public string Mensaje { get; set; } = string.Empty;

    [Display(Name = "Fecha de envío")]
    public DateTime FechaEnvio { get; set; }

    [Display(Name = "Atendido")]
    public bool Estado { get; set; }

    public int? IdUsuario { get; set; }
    public string? NombreUsuarioGestiona { get; set; }

    [Display(Name = "Notas internas")]
    public string? NotasInternas { get; set; }
}
