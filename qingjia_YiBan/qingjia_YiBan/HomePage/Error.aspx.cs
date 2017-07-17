using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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