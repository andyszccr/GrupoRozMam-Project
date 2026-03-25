using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class CategoriaServicio
{
    private readonly CategoriaRepositorio _repositorio = new();

    public IReadOnlyList<CategoriaModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public CategoriaModelo? ObtenerPorId(int id) =>
        _repositorio.ObtenerPorId(id) is { } e ? MapearAModelo(e) : null;

    public int Crear(CategoriaModelo modelo)
    {
        var entidad = MapearAEntidad(modelo);
        return _repositorio.Insertar(entidad);
    }

    public bool Actualizar(CategoriaModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    public bool Desactivar(int idCategoria) => _repositorio.Desactivar(idCategoria);

    private static CategoriaModelo MapearAModelo(Categoria e) => new()
    {
        IdCategoria = e.IdCategoria,
        Nombre = e.Nombre,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        FechaModificacion = e.FechaModificacion
    };

    private static Categoria MapearAEntidad(CategoriaModelo m) => new()
    {
        IdCategoria = m.IdCategoria,
        Nombre = m.Nombre.Trim(),
        Activo = m.Activo
    };
}
