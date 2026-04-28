using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class MensajeContactoServicio
{
    private readonly MensajeContactoRepositorio _repositorio = new();

    public IReadOnlyList<MensajeContactoModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public MensajeContactoModelo? ObtenerPorId(int idMensaje) =>
        _repositorio.ObtenerPorId(idMensaje) is { } e ? MapearAModelo(e) : null;

    public int Crear(MensajeContactoModelo modelo)
    {
        var entidad = MapearAEntidad(modelo);
        entidad.Estado = false;
        entidad.IdUsuario = null;
        entidad.NotasInternas = null;
        return _repositorio.Insertar(entidad);
    }

    public bool Actualizar(MensajeContactoModelo modelo) =>
        _repositorio.Actualizar(MapearAEntidad(modelo));

    private static MensajeContactoModelo MapearAModelo(MensajeContacto e) => new()
    {
        IdMensaje = e.IdMensaje,
        NombreRemitente = e.NombreRemitente,
        CorreoRemitente = e.CorreoRemitente,
        TelefonoRemitente = e.TelefonoRemitente,
        Asunto = e.Asunto,
        Mensaje = e.Mensaje,
        FechaEnvio = e.FechaEnvio,
        Estado = e.Estado,
        IdUsuario = e.IdUsuario,
        NombreUsuarioGestiona = e.NombreUsuarioGestiona,
        NotasInternas = e.NotasInternas
    };

    private static MensajeContacto MapearAEntidad(MensajeContactoModelo m) => new()
    {
        IdMensaje = m.IdMensaje,
        NombreRemitente = m.NombreRemitente.Trim(),
        CorreoRemitente = m.CorreoRemitente.Trim(),
        TelefonoRemitente = m.TelefonoRemitente?.Trim(),
        Asunto = m.Asunto?.Trim(),
        Mensaje = m.Mensaje.Trim(),
        Estado = m.Estado,
        IdUsuario = m.IdUsuario,
        NotasInternas = m.NotasInternas?.Trim()
    };
}
