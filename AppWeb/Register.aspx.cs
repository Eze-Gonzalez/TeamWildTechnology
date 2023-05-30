using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using ServicioEmail;

namespace AppWeb
{
    public partial class Register : System.Web.UI.Page
    {
        private string titulo;
        private string mensaje;
        private string script;
        public bool Status;
        public bool StatusV;
        public bool StatusP;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("autocomplete", "off");
            txtPass.Attributes.Add("autocomplete", "off");
            txtRepetir.Attributes.Add("autocomplete", "off");
            txtNombre.Attributes.Add("autocomplete", "off");
            txtApellido.Attributes.Add("autocomplete", "off");
            txtFecha.Attributes.Add("autocomplete", "off");
            try
            {
                if (Validar.sesion(Session["usuarios"]))
                {
                    Session.Add("ErrorCode", "Ya hay una sesión activa");
                    Session.Add("Error", "Actualmente hay una sesión activa, si desea registrar una nueva cuenta, primera dobe cerrar sesión para luego registrar una nueva cuenta");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void crearScript(string titulo, string mensaje, bool status)
        {
            try
            {
                script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtPass.Visible)
                {
                    if (Validar.campoEmail(txtEmail.Text))
                    {
                        if (Validar.usuarioRegistrado(txtEmail.Text))
                        {
                            Status = false;
                            titulo = "Email ya registrado";
                            mensaje = "El email ingresado ya se encuentra registrado, si desea iniciar sesión con ese email, haga click en Iniciar Sesión";
                            crearScript(titulo, mensaje, false);
                        }
                        else
                        {
                            Status = true;
                            string codigo = Helper.generarCodigo();
                            DatosValidacionEmail.eliminarCodigo(txtEmail.Text);
                            DatosValidacionEmail.cargarCodigo(txtEmail.Text, codigo);
                            Email servicio = new Email();
                            servicio.armarEmail(txtEmail.Text, "Código de validación de email", Helper.cargarCuerpo(txtEmail.Text, codigo), "validaciones@claves.com");
                            servicio.enviarEmail();
                            ajxValidacion.Show();
                        }
                    }
                    else
                    {
                        titulo = "Email no válido";
                        mensaje = "El email no tiene un formato válido o esta vacío, debe ingresar un email válido para continuar.";
                        crearScript(titulo, mensaje, false);
                    }
                }
                else
                {
                    if (Validar.campoPass(txtPass.Text))
                    {
                        if (txtRepetir.Text == txtPass.Text)
                        {
                            if (Validar.regexUsuario(txtUsuario.Text))
                            {
                                Usuario usuario = new Usuario();
                                usuario.Email = txtEmail.Text;
                                usuario.UserName = txtUsuario.Text;
                                usuario.Pass = txtPass.Text;
                                Session["usuario"] = usuario;
                                Status = true;
                                StatusV = true;
                                StatusP = true;
                            }
                            else
                            {
                                Status = true;
                                StatusV = true;
                                titulo = "Nombre de usuario no válido";
                                mensaje = "El nombre de usuario no es válido, debe ingresar un usuario de 6 a 20 dígitos, sin espacios ni símbolos.";
                                crearScript(titulo, mensaje, false);
                            }
                        }
                        else
                        {
                            Status = true;
                            StatusV = true;
                            titulo = "Las contraseñas no coinciden";
                            mensaje = "Las contraseñas ingresadas no coinciden, intente nuevamente";
                            crearScript(titulo, mensaje, false);
                        }
                    }
                    else
                    {
                        Status = true;
                        StatusV = true;
                        titulo = "Contraseña inválida";
                        mensaje = "La contraseña ingresada no es válida o está vacía, ingrese una contraseña de 6 a 20 dígitos, con al menos una mayúscula, una minúscula, un símbolo y un número";
                        crearScript(titulo, mensaje, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                script = string.Format("enableButton({0});", false.ToString().ToLower());
                ScriptManager.RegisterStartupScript(this, GetType(), "enableButton", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            finally
            {
                ajxValidacion.Hide();
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar.campo(txtVerificar.Text))
                {
                    if (Validar.codigo(txtVerificar.Text, txtEmail.Text))
                    {
                        StatusV = true;
                        txtPass.Visible = true;
                        txtRepetir.Visible = true;
                        txtEmail.Enabled = false;
                        lblErrorVerificar.Visible = false;
                        ajxValidacion.Hide();
                    }
                    else
                    {
                        lblErrorVerificar.Text = "El codigo ingresado no coincide con el enviado a su email, intente nuevamente.";
                        lblErrorVerificar.Visible = true;
                    }
                }
                else
                {
                    lblErrorVerificar.Text = "El codigo ingresado no es valido, no debe contener espacios y debe tener 8 dígitos, intente nuevametne";
                    lblErrorVerificar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                StatusV = true;
                StatusP = true;
                Status = true;
                string errorNombre = "";
                string errorApellido = "";
                string errorFecha = "";
                bool errorNombreVisible = false;
                bool errorApellidoVisible = false;
                bool errorFechaVisible = false;
                bool status = Helper.cargarDatos(usuario, txtNombre.Text, txtApellido.Text, txtFecha.Text, ref errorNombre, ref errorApellido, ref errorFecha, ref titulo, ref mensaje, ref errorNombreVisible, ref errorApellidoVisible, ref errorFechaVisible);
                lblErrorNombre.Visible = errorNombreVisible;
                lblErrorNombre.Text = errorNombre;
                lblErrorApellido.Visible = errorApellidoVisible;
                lblErrorApellido.Text = errorApellido;
                lblErrorFecha.Visible = errorFechaVisible;
                lblErrorFecha.Text = errorFecha;
                script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                if (status)
                {
                    Session["usuario"] = usuario;
                    Response.Redirect("Default.aspx");
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception)
            {

                throw;
            }
        }
    }
}