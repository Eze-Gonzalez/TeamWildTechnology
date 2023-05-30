using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Edit.aspx?edit=1", false);
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Edit.aspx?edit=2", false);
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkPass_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Edit.aspx?edit=3", false);
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkDatos_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Edit.aspx?edit=4", false);
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkTwoFactor_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Edit.aspx?edit=5", false);
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