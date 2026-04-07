using System.ComponentModel.DataAnnotations;

namespace CapaNegocios;

public class RolModelo
{
    public int IdRol { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(200)]
    public string Descripcion { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;

    [Display(Name = "Creado")]
    public DateTime? FechaCreacion { get; set; }

    [Display(Name = "Modificado")]
    public DateTime? FechaModificacion { get; set; }

    [Display(Name = "Desactivado")]
    public DateTime? FechaDesactivacion { get; set; }

    [Display(Name = "Permisos Asignados")]
    public List<int> PermisosSeleccionados { get; set; } = new();
}
