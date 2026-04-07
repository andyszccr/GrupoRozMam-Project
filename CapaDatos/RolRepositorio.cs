using CapaDatos.Entities;
using Microsoft.Data.SqlClient;

namespace CapaDatos;

public class RolRepositorio
{
    private static Rol MapearFila(SqlDataReader rd) => new()
    {
        IdRol = rd.GetInt32(rd.GetOrdinal("IdRol")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        Descripcion = rd.GetString(rd.GetOrdinal("Descripcion")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        FechaDesactivacion = rd.IsDBNull(rd.GetOrdinal("FechaDesactivacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaDesactivacion")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo"))
    };

    public List<Rol> Listar()
    {
        var lista = new List<Rol>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Roles_Listar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Rol? ObtenerPorId(int idRol)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Roles_ObtenerPorId", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Id", idRol);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Rol r)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Roles_Insertar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", r.Activo);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Actualizar(Rol r)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Roles_Actualizar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdRol", r.IdRol);
        cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", r.Activo);
        cmd.ExecuteNonQuery();
        return r.IdRol;
    }

    public bool Desactivar(int idRol)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_Roles_Desactivar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdRol", idRol);
        return cmd.ExecuteNonQuery() > 0;
    }

    public List<int> ObtenerPermisosPorRol(int idRol)
    {
        var lista = new List<int>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_RolPermiso_ObtenerPorRol", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdRol", idRol);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            lista.Add(rd.GetInt32(rd.GetOrdinal("IdPermiso")));
        }
        return lista;
    }

    public void GuardarPermisos(int idRol, List<int> permisosIds)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("dbo.usp_RolPermiso_Guardar", cn)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdRol", idRol);
        cmd.Parameters.AddWithValue("@PermisosIds", permisosIds != null && permisosIds.Count > 0 ? string.Join(",", permisosIds) : "");
        cmd.ExecuteNonQuery();
    }
}
