namespace CapaDatos.Entities;

public class Etiqueta
{
    public int IdEtiqueta { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public bool Activo { get; set; }
}
