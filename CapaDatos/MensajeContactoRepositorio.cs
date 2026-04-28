using CapaDatos.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos;

public class MensajeContactoRepositorio
{
    private static MensajeContacto MapearFila(SqlDataReader rd) => new()
    {
        IdMensaje = rd.GetInt32(rd.GetOrdinal("IdMensaje")),
        NombreRemitente = rd.GetString(rd.GetOrdinal("NombreRemitente")),
        CorreoRemitente = rd.GetString(rd.GetOrdinal("CorreoRemitente")),
        TelefonoRemitente = rd.IsDBNull(rd.GetOrdinal("TelefonoRemitente")) ? null : rd.GetString(rd.GetOrdinal("TelefonoRemitente")),
        Asunto = rd.IsDBNull(rd.GetOrdinal("Asunto")) ? null : rd.GetString(rd.GetOrdinal("Asunto")),
        Mensaje = rd.GetString(rd.GetOrdinal("Mensaje")),
        FechaEnvio = rd.GetDateTime(rd.GetOrdinal("FechaEnvio")),
        Estado = rd.GetBoolean(rd.GetOrdinal("Estado")),
        IdUsuario = rd.IsDBNull(rd.GetOrdinal("IdUsuario")) ? null : rd.GetInt32(rd.GetOrdinal("IdUsuario")),
        NombreUsuarioGestiona = rd.IsDBNull(rd.GetOrdinal("NombreUsuarioGestiona")) ? null : rd.GetString(rd.GetOrdinal("NombreUsuarioGestiona")),
        NotasInternas = rd.IsDBNull(rd.GetOrdinal("NotasInternas")) ? null : rd.GetString(rd.GetOrdinal("NotasInternas"))
    };

    public List<MensajeContacto> Listar()
    {
        var lista = new List<MensajeContacto>();
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Mensajes_Contacto_Listar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(MapearFila(rd));
        return lista;
    }

    public MensajeContacto? ObtenerPorId(int idMensaje)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Mensajes_Contacto_ObtenerPorId", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdMensaje", idMensaje);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? MapearFila(rd) : null;
    }

    public int Insertar(MensajeContacto mensaje)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Mensajes_Contacto_Insertar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@NombreRemitente", mensaje.NombreRemitente);
        cmd.Parameters.AddWithValue("@CorreoRemitente", mensaje.CorreoRemitente);
        cmd.Parameters.AddWithValue("@TelefonoRemitente", (object?)mensaje.TelefonoRemitente ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Asunto", (object?)mensaje.Asunto ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Mensaje", mensaje.Mensaje);
        cmd.Parameters.AddWithValue("@Estado", mensaje.Estado);
        cmd.Parameters.AddWithValue("@IdUsuario", (object?)mensaje.IdUsuario ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NotasInternas", (object?)mensaje.NotasInternas ?? DBNull.Value);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Actualizar(MensajeContacto mensaje)
    {
        using var cn = ConexionDB.CrearConexion();
        cn.Open();
        using var cmd = new SqlCommand("usp_Mensajes_Contacto_Actualizar", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdMensaje", mensaje.IdMensaje);
        cmd.Parameters.AddWithValue("@NombreRemitente", mensaje.NombreRemitente);
        cmd.Parameters.AddWithValue("@CorreoRemitente", mensaje.CorreoRemitente);
        cmd.Parameters.AddWithValue("@TelefonoRemitente", (object?)mensaje.TelefonoRemitente ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Asunto", (object?)mensaje.Asunto ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Mensaje", mensaje.Mensaje);
        cmd.Parameters.AddWithValue("@Estado", mensaje.Estado);
        cmd.Parameters.AddWithValue("@IdUsuario", (object?)mensaje.IdUsuario ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NotasInternas", (object?)mensaje.NotasInternas ?? DBNull.Value);
        return cmd.ExecuteNonQuery() > 0;
    }
}
