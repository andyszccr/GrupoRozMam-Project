namespace CapaDatos.Entities;

public class Permiso
{
    public int IdPermiso { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public DateTime? FechaDesactivacion { get; set; }
    public bool Activo { get; set; }
}
