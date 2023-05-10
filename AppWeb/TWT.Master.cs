using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class TWT : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblCopy.Text = "Team Wild Technology® " + DateTime.Now.ToString("yyyy");
                if(Page is Login || Page is Register)
                    menuButton.Disabled = true;
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página.";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                if (!(Page is Default))
                    Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "Hubo un problema al cargar la página.";
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}