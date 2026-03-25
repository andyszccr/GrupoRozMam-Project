using CapaDatos.Entities;
using Microsoft.Data.SqlClient;

namespace CapaDatos;

public class MarcaRepositorio
{
    private static Marca MapearFila(SqlDataReader rd) => new()
    {
        IdMarca = rd.GetInt32(rd.GetOrdinal("IdMarca")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        Descripcion = rd.GetString(rd.GetOrdinal("Descripcion")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo"))
    };

    public List<Marca> Listar()
    {
        const string sql = """
            SELECT IdMarca, Nombre, Descripcion, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Marcas
            ORDER BY Nombre;
            """;

        var lista = new List<Marca>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Marca? ObtenerPorId(int idMarca)
    {
        const string sql = """
            SELECT IdMarca, Nombre, Descripcion, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Marcas
            WHERE IdMarca = @Id;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Id", idMarca);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Marca m)
    {
        const string sql = """
            INSERT INTO dbo.Marcas (Nombre, Descripcion, FechaCreacion, FechaModificacion, Activo)
            OUTPUT INSERTED.IdMarca
            VALUES (@Nombre, @Descripcion, SYSDATETIME(), NULL, @Activo);
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", m.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", m.Activo);
        var resultado = cmd.ExecuteScalar();
        return Convert.ToInt32(resultado);
    }

    public bool Actualizar(Marca m)
    {
        const string sql = """
            UPDATE dbo.Marcas
            SET Nombre = @Nombre,
                Descripcion = @Descripcion,
                FechaModificacion = SYSDATETIME(),
                Activo = @Activo
            WHERE IdMarca = @IdMarca;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdMarca", m.IdMarca);
        cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", m.Descripcion);
        cmd.Parameters.AddWithValue("@Activo", m.Activo);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Desactivar(int idMarca)
    {
        const string sql = """
            UPDATE dbo.Marcas
            SET Activo = 0,
                FechaModificacion = SYSDATETIME()
            WHERE IdMarca = @IdMarca;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdMarca", idMarca);
        return cmd.ExecuteNonQuery() > 0;
    }
}
