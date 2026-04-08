using CapaDatos;
using CapaDatos.Entities;

namespace CapaNegocios;

public class UsuarioServicio
{
    private readonly UsuarioRepositorio _repositorio = new();

    public List<UsuarioModelo> Listar() =>
        _repositorio.Listar().ConvertAll(MapearAModelo);

    public UsuarioModelo? ObtenerPorId(int id)
    {
        var entidad = _repositorio.ObtenerPorId(id);
        return entidad is null ? null : MapearAModelo(entidad);
    }

    public int Crear(UsuarioModelo modelo)
    {
        var entidad = MapearAEntidad(modelo);
        // Aquí podrías agregar lógica para encriptar la contraseña (BCrypt, etc)
        entidad.PasswordHash = modelo.Password ?? "123456"; 
        return _repositorio.Insertar(entidad);
    }

    public int Actualizar(UsuarioModelo modelo)
    {
        return _repositorio.Actualizar(MapearAEntidad(modelo));
    }

    public bool Desactivar(int id) => _repositorio.Desactivar(id);

    private static UsuarioModelo MapearAModelo(Usuario e) => new()
    {
        IdUsuario = e.IdUsuario,
        NombreUsuario = e.NombreUsuario,
        Nombre = e.Nombre,
        Apellidos = e.Apellidos,
        Correo = e.Correo,
        IdRol = e.IdRol,
        NombreRol = e.NombreRol,
        Activo = e.Activo,
        FechaCreacion = e.FechaCreacion,
        Telefono = e.Telefono,
        Direccion = e.Direccion,
        TipoUsuario = e.TipoUsuario
    };

    private static Usuario MapearAEntidad(UsuarioModelo m) => new()
    {
        IdUsuario = m.IdUsuario,
        NombreUsuario = m.NombreUsuario.Trim(),
        Nombre = m.Nombre.Trim(),
        Apellidos = m.Apellidos.Trim(),
        Correo = m.Correo.Trim(),
        IdRol = m.IdRol,
        Activo = m.Activo,
        Telefono = m.Telefono,
        Direccion = m.Direccion?.Trim(),
        TipoUsuario = m.TipoUsuario
    };
}
