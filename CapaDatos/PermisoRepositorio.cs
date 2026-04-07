using CapaDatos.Entities;
using Microsoft.Data.SqlClient;

namespace CapaDatos;

public class PermisoRepositorio
{
    private static Permiso MapearFila(SqlDataReader rd) => new()
    {
        IdPermiso = rd.GetInt32(rd.GetOrdinal("IdPermiso")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        Descripcion = rd.GetString(rd.GetOrdinal("Descripcion")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        FechaDesactivacion = rd.IsDBNull(rd.GetOrdinal("FechaDesactivacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaDesactivacion")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo"))
    };

    public List<Permiso> Listar()
    {
        var lista = new List<Permiso>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Permisos_Listar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Permiso? ObtenerPorId(int idPermiso)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Permisos_ObtenerPorId", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Id", idPermiso);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Permiso p)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Permisos_Insertar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", p.Activo);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Actualizar(Permiso p)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Permisos_Actualizar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdPermiso", p.IdPermiso);
        cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", p.Activo);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Desactivar(int idPermiso)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Permisos_Desactivar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdPermiso", idPermiso);
        return cmd.ExecuteNonQuery() > 0;
    }
}
