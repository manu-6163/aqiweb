using ABCDCRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace ABCDCRUD.Data
{
    public class AseguradoData
    {
        public List<Asegurado> Listar()
        {

            var oLista = new List<Asegurado>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new Asegurado()
                        {
                            Identificacion = Convert.ToInt32(dr["identificacion"]),
                            Nombre1 = dr["nombre1"].ToString(),
                            Nombre2 = dr["nombre2"].ToString(),
                            Apellido1 = dr["apellido1"].ToString(),
                            Apellido2 = dr["apellido2"].ToString(),
                            Celular = dr["celular"].ToString(),
                            Email = dr["email"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]),
                            ValorSeguro = Convert.ToDecimal(dr["valor_seguro"])
                        });

                    }
                }
            }

            return oLista;
        }

        public Asegurado Obtener(int Identificacion)
        {

            var oAsegurado = new Asegurado();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("identificacion", Identificacion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oAsegurado.Identificacion = Convert.ToInt32(dr["identificacion"]);
                        oAsegurado.Nombre1 = dr["nombre1"].ToString();
                        oAsegurado.Nombre2 = dr["nombre2"].ToString();
                        oAsegurado.Apellido1 = dr["apellido1"].ToString();
                        oAsegurado.Apellido2 = dr["apellido2"].ToString();
                        oAsegurado.Celular = dr["celular"].ToString();
                        oAsegurado.Email = dr["email"].ToString();
                        oAsegurado.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
                        oAsegurado.ValorSeguro = Convert.ToDecimal(dr["valor_seguro"]);
                    }
                }
            }

            return oAsegurado;
        }

        public bool Guardar(Asegurado oAsegurado)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Insertar", conexion);
                    cmd.Parameters.AddWithValue("identificacion", oAsegurado.Identificacion);
                    cmd.Parameters.AddWithValue("nombre1", oAsegurado.Nombre1);
                    cmd.Parameters.AddWithValue("nombre2", (oAsegurado.Nombre2 == null) ? DBNull.Value : oAsegurado.Nombre2);
                    cmd.Parameters.AddWithValue("apellido1", oAsegurado.Apellido1);
                    cmd.Parameters.AddWithValue("apellido2", oAsegurado.Apellido2);
                    cmd.Parameters.AddWithValue("celular", oAsegurado.Celular.Substring(1).Replace(" ", ""));
                    cmd.Parameters.AddWithValue("email", oAsegurado.Email);
                    cmd.Parameters.AddWithValue("fechaNac", oAsegurado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("valorSeguro", oAsegurado.ValorSeguro);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }



            return rpta;
        }


        public bool Editar(Asegurado oAsegurado)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("identificacion", oAsegurado.Identificacion);
                    cmd.Parameters.AddWithValue("nombre1", oAsegurado.Nombre1);
                    cmd.Parameters.AddWithValue("nombre2", (oAsegurado.Nombre2 == null) ? DBNull.Value : oAsegurado.Nombre2);
                    cmd.Parameters.AddWithValue("apellido1", oAsegurado.Apellido1);
                    cmd.Parameters.AddWithValue("apellido2", oAsegurado.Apellido2);
                    cmd.Parameters.AddWithValue("celular", oAsegurado.Celular.Substring(1).Replace(" ",""));
                    cmd.Parameters.AddWithValue("email", oAsegurado.Email);
                    cmd.Parameters.AddWithValue("fechaNac", oAsegurado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("valorSeguro", oAsegurado.ValorSeguro);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Eliminar(int Identificacion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("identificacion", Identificacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Importar(SqlParameter dato)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ImportAsegurado", conexion);
                    cmd.Parameters.Add(dato);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
    }   

}
