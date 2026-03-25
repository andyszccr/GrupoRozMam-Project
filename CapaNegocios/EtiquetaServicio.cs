using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class EtiquetaServicio
{
    private readonly EtiquetaRepositorio _repositorio = new();

    public IReadOnlyList<EtiquetaModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public EtiquetaModelo? ObtenerPorId(int id) =>
        _repositorio.ObtenerPorId(id) is { } e ? MapearAModelo(e) : null;

    public int Crear(EtiquetaModelo modelo)
    {
        var entidad = MapearAEntidad(modelo);
        return _repositorio.Insertar(entidad);
    }

    public bool Actualizar(EtiquetaModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    public bool Desactivar(int idEtiqueta) => _repositorio.Desactivar(idEtiqueta);

    private static EtiquetaModelo MapearAModelo(Etiqueta e) => new()
    {
        IdEtiqueta = e.IdEtiqueta,
        Nombre = e.Nombre,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        FechaModificacion = e.FechaModificacion
    };

    private static Etiqueta MapearAEntidad(EtiquetaModelo m) => new()
    {
        IdEtiqueta = m.IdEtiqueta,
        Nombre = m.Nombre.Trim(),
        Activo = m.Activo
    };
}
