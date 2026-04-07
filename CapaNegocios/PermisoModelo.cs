using System.ComponentModel.DataAnnotations;

namespace CapaNegocios;

public class PermisoModelo
{
    public int IdPermiso { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public DateTime? FechaDesactivacion { get; set; }
}
