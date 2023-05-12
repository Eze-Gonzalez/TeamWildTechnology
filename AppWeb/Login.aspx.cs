using Datos;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using Helpers;
using ServicioEmail;

namespace AppWeb
{
    public partial class Login : System.Web.UI.Page
    {
        public bool Status { get; set; }
        public bool StatusF { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("autocomplete", "off");
            txtEmailForget.Attributes.Add("autocomplete", "off");
            txtNombre.Attributes.Add("autocomplete", "off");
            txtApellido.Attributes.Add("autocomplete", "off");
            txtFecha.Attributes.Add("autocomplete", "off");
            txtPassForget.Attributes.Add("autocomplete", "off");
            try
            {
                int cont = Session["cont"] != null ? (int)Session["cont"] : 0;
                if (!IsPostBack && cont != 4)
                    panelUsuario.Visible = false;
                if (panelUsuario.Visible)
                    Status = true;
                if (cont == 4 && Request.QueryString["forget"] == null)
                {
                    Session["ErrorCode"] = "Intentos excedidos";
                    Session["Error"] = "Se excedió en los intentos de inicio de sesión, si no recuerda su contraseña, por favor, cámbiela.";
                    Response.Redirect("Error.aspx");
                }
                else if (Request.QueryString["forget"] != null)
                    lnkForget_Click(sender, e);
                if (Validar.sesion(Session["usuarios"]))
                {
                    Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                    Session.Add("Error", "Actualmente hay una sesión iniciada, para ingresar con otra cuenta, debe cerrar sesión y luego iniciar sesión nuevamente");
                    Response.Redirect("Error.aspx", false);
                }
                if (panelPassF.Visible)
                    StatusF = true;
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Validar.regexUsuario(txtEmail.Text) || Validar.campoEmail(txtEmail.Text))
                {
                    if (Validar.usuarioRegistrado(txtEmail.Text))
                    {
                        DatosUsuario datos = new DatosUsuario();
                        string nombre = datos.traerDatos(txtEmail.Text);
                        Status = true;
                        panelEmail.Visible = false;
                        panelUsuario.Visible = true;
                        lblPerfil.Text = nombre;
                    }
                    else
                    {
                        panelEmail.Visible = true;
                        panelUsuario.Visible = false;
                        cargarAlerta("Email no registrado", "El email ingresado no se encuentra registrado, si desea, puede registrarse, haciendo click en el boton Registrarse", false);
                    }
                }
                else
                {
                    panelEmail.Visible = true;
                    panelUsuario.Visible = false;
                    cargarAlerta("Campo incompleto", "El campo debe ser completado con un email o nombre de usuario.", false);
                }

            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;
                txtEmail.Text = txtEmail.Text.ToLower();
                int cont;
                if (Session["cont"] == null)
                {
                    cont = 0;
                }
                else
                {
                    cont = (int)Session["cont"];
                }
                Usuario usuario = new Usuario();
                usuario.Email = txtEmail.Text;
                usuario.UserName = txtEmail.Text;
                usuario.Pass = txtPass.Text;
                if (!string.IsNullOrEmpty(txtPass.Text) && txtPass.Text != " " && !(txtPass.Text.Length > 20 || txtPass.Text.Length < 6))
                {
                    if (Validar.inicioSesion(usuario) && cont < 4)
                    {
                        Session["cont"] = null;
                        Session.Add("usuario", usuario);
                        Response.Redirect("Default.aspx");
                    }
                    else if (cont <= 2)
                    {
                        Status = true;
                        cont++;
                        mensaje = cont < 3 ? "Email o Contraseña incorrectos, intente nuevamente. Le quedan " + (4 - cont) + " intentos." : "Email o Contraseña incorrectos, intente nuevamente. Le queda 1 intento.";
                        cargarAlerta("Credenciales incorrectas", mensaje, false);
                        Session["cont"] = cont;
                    }
                    else
                    {
                        cont++;
                        Session["cont"] = cont;
                        Session["ErrorCode"] = "Intentos excedidos";
                        Session["Error"] = "Se excedió en los intentos de inicio de sesión, si no recuerda su contraseña, por favor, cámbiela.";
                        Response.Redirect("Error.aspx");
                    }
                }
                else
                {
                    mensaje = "La contraseña no es válida, ingrese su contraseña.";
                    cargarAlerta("Contrasñea no válida", mensaje, false);
                }
            }
            catch (ThreadAbortException) { }
            catch (SqlException ex)
            {
                Session.Add("ErrorCode", "Hubo un error al conectar con la base de datos.");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void cargarAlerta(string titulo, string mensaje, bool status)
        {
            try
            {
                string script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkOtro_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmail.Text = string.Empty;
                Status = false;
                panelUsuario.Visible = false;
                panelEmail.Visible = true;
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkForget_Click(object sender, EventArgs e)
        {
            try
            {
                panelLogin.Visible = false;
                panelForget.Visible = true;
                panelDatos.Visible = true;
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
                int cont = Session["cont"] != null ? (int)Session["cont"] : 0;
                if (cont < 4)
                {
                    panelEmail.Visible = true;
                    txtEmail.Text = string.Empty;
                    panelLogin.Visible = true;
                    panelForget.Visible = false;
                }
                else
                {
                    Session["ErrorCode"] = "Intentos excedidos";
                    Session["Error"] = "Usted excedió los intentos fallidos para iniciar sesión, si no recuerda su contraseña, por favor, cámbiela.";
                    Response.Redirect("Error.aspx");
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnSiguienteF_Click(object sender, EventArgs e)
        {
            string titulo, mensaje, script;
            try
            {
                txtEmailForget.Text = txtEmailForget.Text.ToLower();
                DatosUsuario datos = new DatosUsuario();
                if (Validar.campoEmail(txtEmailForget.Text))
                {
                    if (Validar.usuarioRegistrado(txtEmailForget.Text))
                    {
                        if (datos.buscarUsuario(txtEmailForget.Text, txtNombre.Text, txtApellido.Text, txtFecha.Text))
                        {
                            string codigo = Helper.generarCodigo();
                            DatosValidacionEmail.eliminarCodigo(txtEmail.Text);
                            DatosValidacionEmail.cargarCodigo(txtEmail.Text, codigo);
                            Email servicio = new Email();
                            servicio.armarEmail(txtEmailForget.Text, "Código de validación de email", Helper.cargarCuerpo(txtEmailForget.Text, codigo), "info@teamwildtechnology.com");
                            servicio.enviarEmail();
                            ajaxValidacion.Show();
                        }
                        else
                        {
                            titulo = "Datos Incorrectos";
                            mensaje = "Los datos ingresados no corresponden a la cuenta solicitada, por favor, revise los datos e intente nuevamente.";
                            script = string.Format("crearAlerta({0},'{1}','{2}');", false.ToString().ToLower(), titulo, mensaje);
                            ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                        }
                    }
                    else
                    {
                        titulo = "Email no registrado";
                        mensaje = "El email ingresado no se encuentra registrado, si lo desea puede hacerlo, haciendo click en el boton Registrarse.";
                        script = string.Format("crearAlerta({0},'{1}','{2}');", false.ToString().ToLower(), titulo, mensaje);
                        ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                    }
                }
                else
                {
                    titulo = "Email no válido";
                    mensaje = "El email ingresado no es válido, por favor, ingrese un email válido para continuar.";
                    script = string.Format("crearAlerta({0},'{1}','{2}');", false.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            string titulo, mensaje, script;
            bool status;
            try
            {
                if (Validar.campoPass(txtPassForget.Text))
                {
                    if (!Validar.passActual(txtPassForget.Text, txtEmailForget.Text))
                    {
                        if (txtPassForget.Text == txtRepetirForget.Text)
                        {
                            DatosUsuario datos = new DatosUsuario();
                            datos.passForget(txtPassForget.Text, txtEmailForget.Text);
                            titulo = "Contraseña Cambiada";
                            mensaje = "La contraseña fue cambiada exitosamente.";
                            status = true;
                            if (Request.QueryString["forget"] != null || Session["cont"] != null)
                                Session["cont"] = null;
                        }
                        else
                        {
                            titulo = "Las contraseñas no coinciden";
                            mensaje = "Las contraseñas ingresadas no coinciden, por favor revise los campos e intente nuevamente.";
                            status = false;
                            StatusF = true;
                            panelDatos.Visible = false;
                            panelPassF.Visible = true;
                        }
                    }
                    else
                    {
                        titulo = "Contraseña existente";
                        mensaje = "La contraseña ingresada esta actualmente en uso, puede cambiarla o de lo contrario, inicie sesión con esa contraseña.";
                        status = false;
                    }
                }
                else
                {
                    titulo = "Contraseña no válida";
                    mensaje = "La contraseña ingresada no es válida, por favor introduzca una contraseña de entre 6 a 20 digitos, con al menos un número, una mayúscula, una minúscula y un símbolo.";
                    status = false;
                }
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

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar.campo(txtVerificar.Text))
                {
                    if (Validar.codigo(txtVerificar.Text, txtEmail.Text))
                    {
                        panelPassF.Visible = true;
                        panelDatos.Visible = false;
                        StatusF = true;
                        ajaxValidacion.Hide();
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

        protected void btnCacelarValidacion_Click(object sender, EventArgs e)
        {
            try
            {
                ajaxValidacion.Hide();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}