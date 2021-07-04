using System.Data.SqlClient;
using WebServiceBase.Sistema;

namespace WebServiceBase.Modelo
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexionBD_OVSAP()
        {
            string Cadena = "DATA source=" + configInicial.SERVER_BD + ";Initial Catalog=" + configInicial.NOMBRE_BD + ";User Id=" + configInicial.USER_BD + ";Password=" + configInicial.PASS_BD;
            SqlConnection Conn = new SqlConnection(Cadena);
            Conn.Open();
            return Conn;
        }
        public static SqlConnection ObtenerConexionBD_OV()
        {
            string Cadena = "DATA source=" + configInicial.SERVER_OV + ";Initial Catalog=" + configInicial.BD_OV + ";User Id=" + configInicial.USER_OV + ";Password=" + configInicial.PASS_OV;
            SqlConnection Conn = new SqlConnection(Cadena);
            Conn.Open();
            return Conn;
        }
    }
}
