using System;

namespace qingjia_YiBan.HomePage
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["errorMessage"] != null)
            {
                ErrorMessage.Text = Request.QueryString["errorMessage"].ToString();
                ErrorMessage.Visible = true;
            }
        }
    }
}