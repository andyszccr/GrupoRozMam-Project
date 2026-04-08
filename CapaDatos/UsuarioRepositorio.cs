using CapaDatos.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos;

public class UsuarioRepositorio
{
    private static Usuario MapearFila(SqlDataReader rd) => new()
    {
        IdUsuario = rd.GetInt32(rd.GetOrdinal("IdUsuario")),
        NombreUsuario = rd.GetString(rd.GetOrdinal("NombreUsuario")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        Apellidos = rd.GetString(rd.GetOrdinal("Apellidos")),
        Correo = rd.GetString(rd.GetOrdinal("Correo")),
        IdRol = rd.GetInt32(rd.GetOrdinal("IdRol")),
        NombreRol = rd.GetString(rd.GetOrdinal("NombreRol")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        FechaDesactivacion = rd.IsDBNull(rd.GetOrdinal("FechaDesactivacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaDesactivacion")),
        Telefono = rd.GetInt32(rd.GetOrdinal("Telefono")),
        Direccion = rd.IsDBNull(rd.GetOrdinal("Direccion")) ? null : rd.GetString(rd.GetOrdinal("Direccion")),
        TipoUsuario = rd.GetString(rd.GetOrdinal("TipoUsuario")),
        PasswordHash = rd.HasColumn("PasswordHash") && !rd.IsDBNull(rd.GetOrdinal("PasswordHash")) ? rd.GetString(rd.GetOrdinal("PasswordHash")) : string.Empty
    };

    public List<Usuario> Listar()
    {
        var lista = new List<Usuario>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Usuarios_Listar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Usuario? ObtenerPorId(int idUsuario)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Usuarios_ObtenerPorId", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Usuario u)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Usuarios_Insertar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
        cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
        cmd.Parameters.AddWithValue("@Apellidos", u.Apellidos);
        cmd.Parameters.AddWithValue("@Correo", u.Correo);
        cmd.Parameters.AddWithValue("@PasswordHash", u.PasswordHash);
        cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
        cmd.Parameters.AddWithValue("@Activo", u.Activo);
        cmd.Parameters.AddWithValue("@Telefono", u.Telefono);
        cmd.Parameters.AddWithValue("@Direccion", (object?)u.Direccion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TipoUsuario", u.TipoUsuario);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Actualizar(Usuario u)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Usuarios_Actualizar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdUsuario", u.IdUsuario);
        cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
        cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
        cmd.Parameters.AddWithValue("@Apellidos", u.Apellidos);
        cmd.Parameters.AddWithValue("@Correo", u.Correo);
        cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
        cmd.Parameters.AddWithValue("@Activo", u.Activo);
        cmd.Parameters.AddWithValue("@Telefono", u.Telefono);
        cmd.Parameters.AddWithValue("@Direccion", (object?)u.Direccion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TipoUsuario", u.TipoUsuario);
        cmd.ExecuteNonQuery();
        return u.IdUsuario;
    }

    public bool Desactivar(int idUsuario)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Usuarios_Desactivar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
        return cmd.ExecuteNonQuery() > 0;
    }
}

public static class SqlDataReaderExtensions
{
    public static bool HasColumn(this SqlDataReader dr, string columnName)
    {
        for (int i = 0; i < dr.FieldCount; i++)
        {
            if (dr.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
