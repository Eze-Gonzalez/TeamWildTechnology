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
        public Usuario traerUsuario()
        {
				AccesoDatos datos = new AccesoDatos();
			try
			{
				Usuario usuario = new Usuario();
				datos.consultaSP("tarerUsuario");
				datos.lectura();
				if (datos.Lector.Read())
				{
					usuario.Id = (int)datos.Lector["Id"];
					usuario.UserName = (string)datos.Lector["UserName"];
					usuario.Nombre = (string)datos.Lector["Name"];
					usuario.Apellido = (string)datos.Lector["LastName"];
					usuario.Fecha = (DateTime)datos.Lector["BirthDate"];
					usuario.Email = (string)datos.Lector["Email"];
					usuario.Pass = (string)datos.Lector["Pass"];
					usuario.TwoFactor = (bool)datos.Lector["TwoFactor"];
					usuario.TwoFactorType = (TwoFactorType)datos.Lector["TwoFactorType"];
					usuario.Premium = (bool)datos.Lector["Premium"];
				}
				return usuario;
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
		public int registro(Usuario usuario)
		{
				AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.consultaSP("Registro");
				datos.parametros("UserName", usuario.UserName);
				datos.parametros("Name", usuario.Nombre);
				datos.parametros("LastName", usuario.Apellido);
				datos.parametros("BirthDate", usuario.Fecha);
				datos.parametros("Email", usuario.Email);
				datos.parametros("Pass", usuario.Pass);
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
		public void eliminarUsuario(int Id)
		{
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.consultaEmbebida("Delete Users where Id = @Id");
				datos.parametros("@Id", Id);
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
