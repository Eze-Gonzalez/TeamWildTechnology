using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using ServicioEmail;

namespace AppWeb
{
    public partial class Edit : System.Web.UI.Page
    {
        private string codigo, titulo, mensaje;
        private bool status;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["edit"] == null)
                    Response.Redirect("Profile.aspx", false);
                else
                {
                    int edit = int.Parse(Request.QueryString["edit"]);
                    switch (edit)
                    {
                        case 1:
                            lblEdit.Text = "Email";
                            break;
                        case 2:
                            lblEdit.Text = "Nombre de usuario";
                            text.InnerHtml = "GUARDAR";
                            break;
                        case 3:
                            lblEdit.Text = "Contraseña";
                            text.InnerHtml = "GUARDAR";
                            break;
                        case 4:
                            lblEdit.Text = "Datos personales";
                            text.InnerHtml = "GUARDAR";
                            break;
                        case 5:
                            lblEdit.Text = "Verificación en dos pasos";
                            if (Session["usuario"] != null && !((Usuario)Session["usuario"]).TwoFactor)
                            {
                                chkOnOff.Checked = true;
                                lblOff.Visible = true;
                                text.InnerHtml = "GUARDAR";
                            }
                            break;
                        default:
                            Session["ErrorCode"] = "Hubo un problema al cargar la página";
                            Session["Error"] = "El índice de edición solicitado no existe.";
                            Response.Redirect("Error.aspx", false);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                int opcion = int.Parse(Request.QueryString["edit"]);
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                DatosUsuario datos = new DatosUsuario();
                switch (opcion)
                {
                    case 1:
                        txtEmail.Text = txtEmail.Text.ToLower();
                        txtNuevoEmail.Text = txtNuevoEmail.Text.ToLower();
                        if (Validar.campoEmail(txtEmail.Text))
                        {
                            if (txtEmail.Text == ((Usuario)Session["usuario"]).Email)
                            {
                                if (Validar.campoEmail(txtNuevoEmail.Text))
                                {
                                    if (!Validar.usuarioRegistrado(txtNuevoEmail.Text))
                                    {
                                        DatosValidacionEmail.eliminarCodigo(txtNuevoEmail.Text);
                                        codigo = Helper.generarCodigo();
                                        DatosValidacionEmail.cargarCodigo(txtNuevoEmail.Text, codigo);
                                        Email servicio = new Email();
                                        servicio.armarEmail(txtNuevoEmail.Text, "Verificación de email", Helper.cargarCuerpo(txtNuevoEmail.Text, codigo), "validaciones@teamwildtechnology.com");
                                        servicio.enviarEmail();
                                        ajxValidacion.Show();
                                    }
                                    else
                                    {
                                        titulo = "Email registrado";
                                        mensaje = "El email ingresado ya se encuentra registrado, para editar el email, debe ingresar uno nuevo.";
                                        status = false;
                                    }
                                }
                                else
                                {
                                    titulo = "Email no válido";
                                    mensaje = "El email ingresado en el campo Nuevo Email no es válido o esta vacío, por favor ingrese un email válido";
                                    status = false;
                                }
                            }
                            else
                            {
                                titulo = "Email incorrecto";
                                mensaje = "El email ingresado en el campo Email, no es su email actual, por favor, para continuar, debe introducir su email actual";
                                status = false;
                            }
                        }
                        else
                        {
                            titulo = "Email no válido";
                            mensaje = "El email ingresado en el campo Email no es válido o esta vacío, por favor ingrese un email válido";
                            status = false;
                        }
                        break;
                    case 2:
                        if (Validar.regexUsuario(txtUserActual.Text))
                        {
                            if (txtUserActual.Text == ((Usuario)Session["usuario"]).UserName)
                            {
                                if (Validar.regexUsuario(txtUserNuevo.Text))
                                {
                                    if (!Validar.usuarioRegistrado(txtUserNuevo.Text))
                                    {
                                        usuario.UserName = txtUserNuevo.Text;
                                        datos.actualizarDatos(usuario);
                                        titulo = "Nombre de usuario actualizado";
                                        mensaje = "El nombre de usuario ha sido actualizado exitosamente!";
                                        status = true;
                                    }
                                    else
                                    {
                                        titulo = "Usuario existente";
                                        mensaje = "El nombre de usuario ingresado, ya se encuentra registrado, por favor, ingrese otro nombre de usuario";
                                        status = false;
                                    }
                                }
                                else
                                {
                                    titulo = "Usuario no válido";
                                    mensaje = "El usuario ingresado en el campo Nuevo Nombre de Usuario, no es válido o está vacío, por favor, ingrese un usuario válido";
                                    status = false;
                                }
                            }
                            else
                            {
                                titulo = "Usuario incorrecto";
                                mensaje = "El usuario ingresado no es su nombre de usuario actual, para continuar, por favor, ingrese su nombre de usuario actual";
                                status = false;
                            }
                        }
                        else
                        {
                            titulo = "Usuario no válido";
                            mensaje = "El usuario ingresado en el campo Nombre de Usuario, no es válido o está vacío, por favor, ingrese un usuario válido";
                            status = false;
                        }
                        break;
                    case 3:
                        if (Validar.campoPass(txtPassActual.Text))
                        {
                            if (txtPassActual.Text == ((Usuario)Session["usuario"]).Pass)
                            {
                                if (Validar.campoPass(txtPass.Text))
                                {
                                    if (txtPass.Text == txtRepetir.Text)
                                    {
                                        usuario.Pass = txtPass.Text;
                                        titulo = "Contraseña actualizada";
                                        mensaje = "La contraseña ha sido actualizada exitosamente!";
                                        status = true;
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        break;
                    case 4: //datos personales
                        break;
                    case 5: //verificacion 2 pasos
                        if (chkEmail.Checked)
                        {
                            Email servicio = new Email();
                            DatosValidacionEmail.eliminarCodigo(((Usuario)Session["usuario"]).Email);
                            string codigo = Helper.generarCodigo();
                            DatosValidacionEmail.cargarCodigo(((Usuario)Session["usuario"]).Email, codigo);
                            ajxValidacion.Show();
                        }
                        if (chkApp.Checked)
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar.codigo(codigo, txtNuevoEmail.Text))
                {
                    lblErrorVerificar.Visible = false;
                    Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                    usuario.Email = txtNuevoEmail.Text;
                    DatosUsuario datos = new DatosUsuario();
                    datos.actualizarDatos(usuario);
                    if (Request.QueryString["edit"] == "1")
                    {
                        titulo = "Email actualizado";
                        mensaje = "El email ha sido actualizado exitosamente!";
                    }
                    else
                    {
                        titulo = "Verificación actualizada";
                        mensaje = "La verificacion de dos pasos mediante el email registrado, ha sido actualizada correctamente";
                    }
                    status = true;
                    crearAlerta(titulo, mensaje, status);
                }
                else
                {
                    lblErrorVerificar.Text = "El código ingresado es incorrecto, por favor, ingrese el código enviado al email ingresado.";
                    lblErrorVerificar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void chkOnOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnOff.Checked)
            {
                lblOff.Visible = true;
                lblOff.CssClass += " show";
                lblOn.Visible = false;
                if (lblOn.CssClass.Contains("show"))
                    lblOn.CssClass = "lblOn";
                text.InnerHtml = "SIGUIENTE";
            }
            else
            {
                lblOff.Visible = false;
                lblOn.Visible = true;
                lblOn.CssClass += " show";
                if (lblOff.CssClass.Contains("show"))
                    lblOff.CssClass = "lblOff";
                text.InnerHtml = "GUARDAR";
            }
        }

        protected void btnCacelarValidacion_Click(object sender, EventArgs e)
        {
            try
            {
                ajxValidacion.Hide();
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        private void crearAlerta(string titulo, string mensaje, bool status)
        {
            try
            {
                string script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}