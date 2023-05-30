using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDominio;
using DataBase;
using Datos;
using Validaciones;

namespace Helpers
{
    public class Helper
    {
        public static string nombre(Usuario usuario)
        {
            try
            {
                string nombre = "";
                TextInfo Ti = new CultureInfo("es-MX", false).TextInfo;
                if (usuario != null)
                {
                    if (string.IsNullOrEmpty(usuario.Nombre))
                    {
                        nombre = usuario.Email;
                        string[] lbl;
                        lbl = nombre.Split('@');
                        nombre = Ti.ToTitleCase(lbl[0]);
                    }
                    else
                        nombre = Ti.ToTitleCase(usuario.Nombre);
                }
                return nombre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void codificar(ref string email, ref string pass)
        {
            try
            {
                int longitud;
                string subSPrincipal = email.Substring(0, 3);
                string subSecundario = email.Substring(2);
                longitud = subSecundario.Length;
                string auxsubSecundario = "";
                for (int i = 1; i < subSecundario.Length; i++)
                {
                    auxsubSecundario += "•";
                }
                email = subSPrincipal + auxsubSecundario;
                longitud = pass.Length;
                pass = "";
                for (int i = 0; i < longitud; i++)
                {
                    pass += "•";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string generarCodigo()
        {
            try
            {
                var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var arreglo = new char[8];
                Random random = new Random();
                for (int i = 0; i < arreglo.Length; i++)
                {
                    arreglo[i] = caracteres[random.Next(caracteres.Length)];
                }
                return new string(arreglo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string cargarCuerpo(string email, string codigo)
        {
            try
            {
                return "<table align=\"center\" style=\"background: #000; width: 70%; height: auto; color: white; text-align: center; font-family: Segoe UI, Arial, Google Sans; border-radius: 10px\">\r\n\t<tr>\r\n\t\t<td>\r\n\t\t\t<div style=\"font-size: 25px; border-bottom: 1px solid white; color: white; padding:10px;\">Hola " + email + "</div>\r\n\t\t\t<p style=\"color: white; padding:10px; font-size: 19px;\">Recientemente, solicitaste un codigo de verificación en la página \"pagina\", por lo cual estamos enviando este email para verificar que sea correcto</p>\r\n\t\t\t<div>\r\n\t\t\t\t<p style=\"font-size: 23px; font-weight:500; color: black; background: #fff; width: 25%; border-radius: 20px; margin-left: 38%\">" + codigo + "</p>\r\n\t\t\t</div>\r\n\t\t\t<table style=\"width:100%; height: auto; color: white; display:block\"><tr><td style=\"padding:10px;\">Si no solicitaste este código puedes ignorar el mensaje</td></tr></table>\r\n\t\t</td>\r\n\t</tr>\r\n</table>";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string cargarCuerpo(string titulo, string email, string mensaje)
        {
            try
            {
                return "<style>\r\n  @import url('https://fonts.googleapis.com/css2?family=Kanit:wght@100;200;500;600&display=swap');\r\n  *{\r\n    font-family: Kanit;\r\n    letter-spacing: 2px;\r\n    color: #fff !important;\r\n  }\r\n   a{\r\n    color: #fff}\r\n</style>\r\n<div style=\"background: #000000d1; height: 500px; width: 50%; color: #fff !important; padding: 20px 0px;\">\r\n  <h2 style=\"text-align:center\">" + titulo + "</h2>\r\n  <p style=\"margin-left: 20px; margin-bottom: 40px;\">Email: " + email + "</p>\r\n  <pre style=\"padding: 20px;\">" + mensaje + "</pre>\r\n</div>";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool cargarDatos(Usuario usuario, string nombre, string apellido, string fecha, ref string errorNombre, ref string errorApellido, ref string errorFecha, ref string titulo, ref string mensaje, ref bool errorNombreVisible, ref bool errorApellidoVisible, ref bool errorFechaVisible)
        {
            DatosUsuario datos = new DatosUsuario();
            bool status = false;
            try
            {
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(fecha.ToString()) && (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(DateTime.Parse(fecha).ToString("yyyy"))) >= 18)
                {
                    status = true;
                    titulo = "Registro exitoso";
                    mensaje = "Su registro se completó exitosamente. Gracias por registrarse!";
                    usuario.Nombre = nombre;
                    usuario.Apellido = apellido;
                    usuario.Fecha = DateTime.Parse(fecha);
                    usuario.Id = datos.nuevoUsuario(usuario);
                }

                if (!status)
                {
                    if (string.IsNullOrEmpty(nombre))
                    {
                        errorNombre = "Debe rellenar este campo.";
                        errorNombreVisible = true;
                        status = false;
                    }
                    if (string.IsNullOrEmpty(apellido))
                    {
                        errorApellido = "Debe rellenar este campo.";
                        errorApellidoVisible = true;
                        status = false;
                    }
                    if (string.IsNullOrEmpty(fecha.ToString()))
                    {
                        errorFecha = "Debe rellenar este campo.";
                        errorFechaVisible = true;
                        status = false;
                    }
                    else if ((int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(DateTime.Parse(fecha).ToString("yyyy"))) < 18)
                    {
                        errorFecha = "Debe ser mayor de edad para continuar";
                        errorFechaVisible = true;
                        status = false;
                    }
                    titulo = "Campos con errores";
                    mensaje = "Hay campos con errores o incompletos, por favor, revise el formulario e intente nuevamente.";
                }
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string cargarImagen(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("imagen");
                datos.parametros("@id", Id);
                datos.lectura();
                if (datos.Lector.Read() && !(datos.Lector["Imagen"] is DBNull))
                    return (string)datos.Lector["Imagen"];
                return "https://i.imgur.com/yzczBvI.png";
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
