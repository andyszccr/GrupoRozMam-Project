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
        const string sql = """
            SELECT IdRol, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
            FROM dbo.Roles
            ORDER BY Nombre;
            """;

        var lista = new List<Rol>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Rol? ObtenerPorId(int idRol)
    {
        const string sql = """
            SELECT IdRol, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
            FROM dbo.Roles
            WHERE IdRol = @Id;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Id", idRol);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Rol r)
    {
        const string sql = """
            INSERT INTO dbo.Roles (Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo)
            OUTPUT INSERTED.IdRol
            VALUES (@Nombre, @Descripcion, SYSDATETIME(), NULL, NULL, @Activo);
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", r.Activo);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Actualizar(Rol r)
    {
        const string sql = """
            UPDATE dbo.Roles
            SET Nombre = @Nombre,
                Descripcion = @Descripcion,
                FechaModificacion = SYSDATETIME(),
                Activo = @Activo,
                FechaDesactivacion = CASE
                    WHEN @Activo = 1 THEN NULL
                    ELSE ISNULL(FechaDesactivacion, SYSDATETIME())
                END
            WHERE IdRol = @IdRol;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdRol", r.IdRol);
        cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", r.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", r.Activo);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Desactivar(int idRol)
    {
        const string sql = """
            UPDATE dbo.Roles
            SET Activo = 0,
                FechaModificacion = SYSDATETIME(),
                FechaDesactivacion = SYSDATETIME()
            WHERE IdRol = @IdRol;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdRol", idRol);
        return cmd.ExecuteNonQuery() > 0;
    }
}
