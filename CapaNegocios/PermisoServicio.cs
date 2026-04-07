using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class PermisoServicio
{
    private readonly PermisoRepositorio _repositorio = new();

    public IReadOnlyList<PermisoModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public PermisoModelo? ObtenerPorId(int id) =>
        _repositorio.ObtenerPorId(id) is { } e ? MapearAModelo(e) : null;

    public int Crear(PermisoModelo modelo) =>
        _repositorio.Insertar(MapearAEntidad(modelo));

    public bool Actualizar(PermisoModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    public bool Desactivar(int idPermiso) => _repositorio.Desactivar(idPermiso);

    private static PermisoModelo MapearAModelo(Permiso e) => new()
    {
        IdPermiso = e.IdPermiso,
        Nombre = e.Nombre,
        Descripcion = e.Descripcion,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        FechaModificacion = e.FechaModificacion,
        FechaDesactivacion = e.FechaDesactivacion
    };

    private static Permiso MapearAEntidad(PermisoModelo m) => new()
    {
        IdPermiso = m.IdPermiso,
        Nombre = m.Nombre.Trim(),
        Descripcion = m.Descripcion.Trim(),
        Activo = m.Activo
    };
}
