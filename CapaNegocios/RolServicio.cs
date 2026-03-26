using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class RolServicio
{
    private readonly RolRepositorio _repositorio = new();

    public IReadOnlyList<RolModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public RolModelo? ObtenerPorId(int id) =>
        _repositorio.ObtenerPorId(id) is { } e ? MapearAModelo(e) : null;

    public int Crear(RolModelo modelo) =>
        _repositorio.Insertar(MapearAEntidad(modelo));

    public bool Actualizar(RolModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    public bool Desactivar(int idRol) => _repositorio.Desactivar(idRol);

    private static RolModelo MapearAModelo(Rol e) => new()
    {
        IdRol = e.IdRol,
        Nombre = e.Nombre,
        Descripcion = e.Descripcion,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        FechaModificacion = e.FechaModificacion,
        FechaDesactivacion = e.FechaDesactivacion
    };

    private static Rol MapearAEntidad(RolModelo m) => new()
    {
        IdRol = m.IdRol,
        Nombre = m.Nombre.Trim(),
        Descripcion = m.Descripcion.Trim(),
        Activo = m.Activo
    };
}
