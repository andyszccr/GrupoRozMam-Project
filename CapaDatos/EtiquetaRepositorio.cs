using CapaDatos.Entities;
using Microsoft.Data.SqlClient;

namespace CapaDatos;

public class EtiquetaRepositorio
{
    private static Etiqueta MapearFila(SqlDataReader rd) => new()
    {
        IdEtiqueta = rd.GetInt32(rd.GetOrdinal("IdEtiqueta")),
        Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
        FechaCreacion = rd.IsDBNull(rd.GetOrdinal("FechaCreacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaCreacion")),
        FechaModificacion = rd.IsDBNull(rd.GetOrdinal("FechaModificacion")) ? null : rd.GetDateTime(rd.GetOrdinal("FechaModificacion")),
        Activo = rd.GetBoolean(rd.GetOrdinal("Activo"))
    };

    public List<Etiqueta> Listar()
    {
        const string sql = """
            SELECT IdEtiqueta, Nombre, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Etiquetas
            ORDER BY Nombre;
            """;

        var lista = new List<Etiqueta>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public Etiqueta? ObtenerPorId(int idEtiqueta)
    {
        const string sql = """
            SELECT IdEtiqueta, Nombre, FechaCreacion, FechaModificacion, Activo
            FROM dbo.Etiquetas
            WHERE IdEtiqueta = @Id;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Id", idEtiqueta);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(Etiqueta e)
    {
        const string sql = """
            INSERT INTO dbo.Etiquetas (Nombre, FechaCreacion, FechaModificacion, Activo)
            OUTPUT INSERTED.IdEtiqueta
            VALUES (@Nombre, SYSDATETIME(), NULL, @Activo);
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@Activo", e.Activo);
        var resultado = cmd.ExecuteScalar();
        return Convert.ToInt32(resultado);
    }

    public bool Actualizar(Etiqueta e)
    {
        const string sql = """
            UPDATE dbo.Etiquetas
            SET Nombre = @Nombre,
                FechaModificacion = SYSDATETIME(),
                Activo = @Activo
            WHERE IdEtiqueta = @IdEtiqueta;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdEtiqueta", e.IdEtiqueta);
        cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
        cmd.Parameters.AddWithValue("@Activo", e.Activo);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Desactivar(int idEtiqueta)
    {
        const string sql = """
            UPDATE dbo.Etiquetas
            SET Activo = 0,
                FechaModificacion = SYSDATETIME()
            WHERE IdEtiqueta = @IdEtiqueta;
            """;

        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@IdEtiqueta", idEtiqueta);
        return cmd.ExecuteNonQuery() > 0;
    }
}
