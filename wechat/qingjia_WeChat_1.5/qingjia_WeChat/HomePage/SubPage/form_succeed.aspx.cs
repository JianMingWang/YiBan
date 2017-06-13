using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using qingjia_WeChat.HomePage.Class;

namespace qingjia_WeChat.SubPage
{
    public partial class form_succeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string LV_NUM = Request.QueryString["LV_NUM"].ToString();
            if (!LeaveList.Delete_leaveList(" ID= " + LV_NUM ))
            {
                Response.Redirect("form_notaudited.aspx");
            }
        }

    }
}