using CapaDatos;
using Microsoft.Data.SqlClient;

namespace CapaNegocios
{
    public class Class1
    {
        public static SqlConnection ObtenerConexion() => ConexionDB.CrearConexion();
    }
}
