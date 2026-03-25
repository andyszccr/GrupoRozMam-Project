using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CapaDatos;

public class ConexionDB
{
    private const string NombreCadenaConexion = "DefaultConnection";

    /// <summary>
    /// Ubica appsettings.json: primero en CapaDatos\ (cuando la App copia el archivo al bin)
    /// y si no existe, en la carpeta base (compilación directa de CapaDatos).
    /// </summary>
    public static string ResolverRutaAppsettings()
    {
        var baseDir = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        var enSubcarpeta = Path.Combine(baseDir, "CapaDatos", "appsettings.json");
        if (File.Exists(enSubcarpeta))
            return enSubcarpeta;

        var mismoNivel = Path.Combine(baseDir, "appsettings.json");
        if (File.Exists(mismoNivel))
            return mismoNivel;

        throw new FileNotFoundException(
            $"No se encontró appsettings.json. Rutas probadas: {enSubcarpeta} y {mismoNivel}.");
    }

    public static IConfiguration CargarConfiguracion()
    {
        var ruta = ResolverRutaAppsettings();
        return new ConfigurationBuilder()
            .AddJsonFile(ruta, optional: false, reloadOnChange: false)
            .Build();
    }

    /// <summary>
    /// Comprueba que la cadena en ConnectionStrings:DefaultConnection sea válida y que SQL Server acepte la conexión.
    /// </summary>
    /// <returns>Éxito y un mensaje descriptivo (error de red, login, base inexistente, etc.).</returns>
    public static (bool Exito, string Mensaje) VerificarConexion()
    {
        try
        {
            var config = CargarConfiguracion();
            var cadena = config.GetConnectionString(NombreCadenaConexion);
            if (string.IsNullOrWhiteSpace(cadena))
            {
                return (false,
                    $"La cadena '{NombreCadenaConexion}' no está definida o está vacía en appsettings.json.");
            }

            using var conexion = new SqlConnection(cadena);
            conexion.Open();
            using var comando = new SqlCommand("SELECT 1", conexion);
            comando.ExecuteScalar();
            return (true, "Conexión correcta a SQL Server.");
        }
        catch (Exception ex)
        {
            return (false, $"No se pudo conectar: {ex.Message}");
        }
    }
}
