using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class RolServicio
{
    private readonly RolRepositorio _repositorio = new();

    public IReadOnlyList<RolModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public RolModelo? ObtenerPorId(int id)
    {
        var entidad = _repositorio.ObtenerPorId(id);
        if (entidad is null) return null;
        
        var modelo = MapearAModelo(entidad);
        modelo.PermisosSeleccionados = _repositorio.ObtenerPermisosPorRol(id);
        return modelo;
    }

    public int Crear(RolModelo modelo)
    {
        int nuevoId = _repositorio.Insertar(MapearAEntidad(modelo));
        _repositorio.GuardarPermisos(nuevoId, modelo.PermisosSeleccionados);
        return nuevoId;
    }

    public int Actualizar(RolModelo modelo)
    {
        int idActualizado = _repositorio.Actualizar(MapearAEntidad(modelo));
        _repositorio.GuardarPermisos(idActualizado, modelo.PermisosSeleccionados);
        return idActualizado;
    }

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
