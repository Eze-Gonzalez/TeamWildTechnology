using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Error"] != null && Session["ErrorCode"] != null)
                {
                    Title = Session["ErrorCode"].ToString();
                    lblErrorCode.Text = Session["ErrorCode"].ToString();
                    lblError.Text = Session["Error"].ToString();
                }
                else
                    Response.Redirect("Default.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}