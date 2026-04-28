namespace CapaDatos.Entities;

public class MensajeContacto
{
    public int IdMensaje { get; set; }
    public string NombreRemitente { get; set; } = string.Empty;
    public string CorreoRemitente { get; set; } = string.Empty;
    public string? TelefonoRemitente { get; set; }
    public string? Asunto { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public DateTime FechaEnvio { get; set; }
    public bool Estado { get; set; }
    public int? IdUsuario { get; set; }
    public string? NombreUsuarioGestiona { get; set; }
    public string? NotasInternas { get; set; }
}
