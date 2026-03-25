using CapaDatos.Entities;
using Microsoft.Data.SqlClient;

namespace CapaDatos;

public class CategoriaRepositorio
{
    private static Categoria MapearFila(SqlDataReader rd) => new()
    {
        IdCategoria = rd.GetInt32(rd.GetOrdinal("IdCategoria")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo"))
    };

    public List<Categoria> Listar()
    {
        const string sql = """
            SELECT IdCategoria, Nombre, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Categorias
            ORDER BY Nombre;
            """;

        var lista = new List<Categoria>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Categoria? ObtenerPorId(int idCategoria)
    {
        const string sql = """
            SELECT IdCategoria, Nombre, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Categorias
            WHERE IdCategoria = @Id;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Id", idCategoria);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Categoria c)
    {
        const string sql = """
            INSERT INTO dbo.Categorias (Nombre, FechaCreacion, FechaModificacion, Activo)
            OUTPUT INSERTED.IdCategoria
            VALUES (@Nombre, SYSDATETIME(), NULL, @Activo);
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
        cmd.Parameters.AddWithValue("@Activo", c.Activo);
        var resultado = cmd.ExecuteScalar();
        return Convert.ToInt32(resultado);
    }

    public bool Actualizar(Categoria c)
    {
        const string sql = """
            UPDATE dbo.Categorias
            SET Nombre = @Nombre,
                FechaModificacion = SYSDATETIME(),
                Activo = @Activo
            WHERE IdCategoria = @IdCategoria;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdCategoria", c.IdCategoria);
        cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
        cmd.Parameters.AddWithValue("@Activo", c.Activo);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Desactivar(int idCategoria)
    {
        const string sql = """
            UPDATE dbo.Categorias
            SET Activo = 0,
                FechaModificacion = SYSDATETIME()
            WHERE IdCategoria = @IdCategoria;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
        return cmd.ExecuteNonQuery() > 0;
    }
}
