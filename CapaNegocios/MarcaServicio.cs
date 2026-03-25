using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class MarcaServicio
{
    private readonly MarcaRepositorio _repositorio = new();

    public IReadOnlyList<MarcaModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public MarcaModelo? ObtenerPorId(int id) =>
        _repositorio.ObtenerPorId(id) is { } e ? MapearAModelo(e) : null;

    public int Crear(MarcaModelo modelo)
    {
        var entidad = MapearAEntidad(modelo);
        return _repositorio.Insertar(entidad);
    }

    public bool Actualizar(MarcaModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    public bool Desactivar(int idMarca) => _repositorio.Desactivar(idMarca);

    private static MarcaModelo MapearAModelo(Marca e) => new()
    {
        IdMarca = e.IdMarca,
        Nombre = e.Nombre,
        Descripcion = e.Descripcion,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        FechaModificacion = e.FechaModificacion
    };

    private static Marca MapearAEntidad(MarcaModelo m) => new()
    {
        IdMarca = m.IdMarca,
        Nombre = m.Nombre.Trim(),
        Descripcion = m.Descripcion.Trim(),
        Activo = m.Activo
    };
}
