using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CapaDatos;

public class ConexionDB
{
    private const string NombreCadena = "DefaultConnection";

    private static string ResolverRutaAppsettings()
    {
        var baseDir = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        var enCapaDatos = Path.Combine(baseDir, "CapaDatos", "appsettings.json");
        if (File.Exists(enCapaDatos))
            return enCapaDatos;

        var mismoNivel = Path.Combine(baseDir, "appsettings.json");
        if (File.Exists(mismoNivel))
            return mismoNivel;

        throw new FileNotFoundException(
            $"No se encontró appsettings.json. Rutas probadas: {enCapaDatos} y {mismoNivel}.");
    }

    private static IConfiguration CargarConfiguracion()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(ResolverRutaAppsettings(), optional: false, reloadOnChange: false)
            .Build();
    }

    public static string ObtenerCadenaConexion()
    {
        var cadena = CargarConfiguracion().GetConnectionString(NombreCadena);
        if (string.IsNullOrWhiteSpace(cadena))
            throw new InvalidOperationException(
                $"Defina ConnectionStrings:{NombreCadena} en appsettings.json.");
        return cadena;
    }

    public static SqlConnection CrearConexion() => new(ObtenerCadenaConexion());
}
