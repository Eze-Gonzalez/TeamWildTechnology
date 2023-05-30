using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using ServicioEmail;
using System.Configuration;
using Helpers;
using System.Web.Services.Description;

namespace AppWeb
{
    public partial class Contact : System.Web.UI.Page
    {
        private string titulo, mensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usaurio"] != null)
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    txtEmail.Text = usuario.Email;
                    txtUsuario.Text = usuario.UserName;
                    txtUsuario.Enabled = false;
                }
                if (IsPostBack && Session["contacto"] != null)
                {

                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validar.campoEmail(txtEmail.Text) && !string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtMensaje.Text))
                {
                    string usuario = txtUsuario.Text + " envió un mensaje.";
                    Email servicio = new Email();
                    servicio.armarEmail("team.wild.la@gmail.com", usuario, Helper.cargarCuerpo(usuario, txtEmail.Text, txtMensaje.Text), txtEmail.Text);
                    servicio.enviarEmail();
                    ajxAlerta.Show();
                }
                else if (!Validar.campoEmail(txtEmail.Text))
                {
                    titulo = "Email vacío o no válido";
                    mensaje = "El email ingresado en el campo Email, esta vacío o no tiene un email válido, por favor revise el campo email e intente nuevamente.";
                    cargarAlerta(titulo, mensaje, false);
                }
                else if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    titulo = "Nombre incompleto";
                    mensaje = "El campo nombre esta incompleto, por facor ingrese un nombre para continuar.";
                    cargarAlerta(titulo, mensaje, false);
                }
                else if (string.IsNullOrEmpty(txtMensaje.Text))
                {
                    titulo = "Mensaje vacío";
                    mensaje = "El campo mensaje está vacío, para enviarnos un mensaje, por favor, complete el campo de mensaje.";
                    cargarAlerta(titulo, mensaje, false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void aceptarAlerta_Click(object sender, EventArgs e)
        {
            try
            {
                ajxAlerta.Hide();
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
    }
}