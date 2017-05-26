using YbSDK.Api;
using YbSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            testValue.Text = "";
            OauthApi oauth = new OauthApi();
            AccessToken at = oauth.GetAccessToken("1");
        }
    }
}