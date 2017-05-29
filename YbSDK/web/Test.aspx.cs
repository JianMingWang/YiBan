using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YbSDK.Api;
using YbSDK.Model;

namespace web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["access_token"] != null)
            {
                testValue.Text = "授权成功，access_token为：" + Request["access_token"].ToString();
                data.Text = Request["data"].ToString();
            }
            else
            {
                testValue.Text = "授权失败";
            }
        }
    }
}