using System.Data.SqlClient;

namespace ABCDCRUD.Data
{
    public class Conexion
    {
        private String cadenaSQL = string.Empty;

        public Conexion()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:conexion").Value;
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
