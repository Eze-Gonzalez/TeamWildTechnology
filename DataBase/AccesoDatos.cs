using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DataBase
{
    public class AccesoDatos
    {
        private SqlCommand comando;
        private SqlConnection conexion;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["TWT"]);
                comando = new SqlCommand();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void consultaSP(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = consulta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void consultaEmbebida(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void lectura()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ejecutar()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int ejecutarScalar()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                return (int)comando.ExecuteScalar();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void parametros(string nombre, object valor)
        {
            try
            {
                comando.Parameters.AddWithValue(nombre, valor);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void cerrarConexion()
        {
            try
            {
                if(lector != null)
                    lector.Close();
                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
