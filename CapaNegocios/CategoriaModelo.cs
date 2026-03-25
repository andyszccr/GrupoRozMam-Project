using System.ComponentModel.DataAnnotations;

namespace CapaNegocios;

public class CategoriaModelo
{
    public int IdCategoria { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;

    [Display(Name = "Creado")]
    public DateTime? FechaCreacion { get; set; }

    [Display(Name = "Modificado")]
    public DateTime? FechaModificacion { get; set; }
}
