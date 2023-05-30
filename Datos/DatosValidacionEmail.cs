using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class DatosValidacionEmail
    {
        public static void cargarCodigo(string email, string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Insert into Validacion (Codigo, EmailUsuario, FechaCreacion) values (@codigo, @email, @fecha)");
                datos.parametros("@codigo", codigo);
                datos.parametros("@email", email);
                datos.parametros("@fecha", DateTime.Now);
                datos.ejecutar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public static void eliminarCodigo(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Delete Validacion where EmailUsuario = @email");
                datos.parametros("@email", email);
                datos.ejecutar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public static void eliminarCodigosCaducados()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("DELETE FROM Validacion WHERE FechaCreacion < DATEADD(minute, -15, GETDATE())");
                datos.ejecutar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
