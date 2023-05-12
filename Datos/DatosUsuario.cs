using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using ModeloDominio;

namespace Datos
{
    public class DatosUsuario
    {
        public int nuevoUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("Registro");
                //@email varchar(100),
                //@pass varchar(8),
                //@nombre varchar(50),
                //@apellido varchar(50),
                //@fecha datetime
                datos.parametros("@email", usuario.Email);
                datos.parametros("@username", usuario.UserName);
                datos.parametros("@pass", usuario.Pass);
                datos.parametros("@nombre", usuario.Nombre);
                datos.parametros("@apellido", usuario.Apellido);
                datos.parametros("@fecha", usuario.Fecha);
                return datos.ejecutarScalar();
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
        public string traerDatos(string user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string nombre = "";
                datos.consultaEmbebida("select UserName from Users where Email = @usuario or UserName = @usuario");
                datos.parametros("@usuario", user);
                datos.lectura();
                if (datos.Lector.Read())
                    nombre = (string)datos.Lector["UserName"];
                return nombre;
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

        public bool buscarUsuario(string email, string nombre, string apellido, string fecha)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("Forget");
                datos.parametros("@email", email);
                datos.parametros("@nombre", nombre);
                datos.parametros("@apellido", apellido);
                datos.parametros("@username", email);
                datos.parametros("@fecha", DateTime.Parse(fecha));
                datos.lectura();
                if (datos.Lector.Read())
                    return true;
                return false;
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

        public void passForget(string pass, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("forgetPass");
                datos.parametros("@pass", pass);
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

        public void eliminarCuenta(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("delete from Usuarios where id = @id");
                datos.parametros("@id", id);
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

        public void actualizarDatos(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("ActualizarDatos");
                //@nombre varchar(50),
                //@apellido varchar(50),
                //@email varchar(100),
                //@pass varchar(50),
                //@idImagen int,
                //@id int
                datos.parametros("@nombre", usuario.Nombre);
                datos.parametros("@apellido", usuario.Apellido);
                datos.parametros("@email", usuario.Email);
                datos.parametros("@pass", usuario.Pass);
                datos.parametros("@id", usuario.Id);
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
