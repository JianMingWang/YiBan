using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YbSDK.Api;
using YbSDK.Model;
using System.Text;
using System.IO;

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
                Session["Access_Token"] = Request["access_token"].ToString();
            }
            else
            {
                testValue.Text = "授权失败";
            }
        }

        protected void userInfoButton_Click(object sender, EventArgs e)
        {
            string userInfoData = GetUserInfo("https://openapi.yiban.cn/user/me");
            string srcString = userInfoData.ToString();
            srcString += "  ";

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            UserInfo obj = serializer.Deserialize<UserInfo>(srcString);
            srcString += "账号ID：" + obj.yb_userid;
            srcString += "姓名：" + obj.yb_username;
            srcString += "学校姓名：" + obj.yb_schoolname;

            userInfo.Text = srcString;
        }

        protected string GetUserInfo(string url)
        {
            //WebClient 发送 Post请求
            //string access_token = Session["Access_Token"].ToString();
            string access_token = "baec2ddea348da3d6bb0db5998646b98a314e3dd";

            UserApi userAPI = new UserApi(access_token);
            UserMe meInfo = userAPI.GetMe();

            return null;
        }
    }

    public class UserInfo
    {
        public string yb_userid { get; set; }
        public string yb_username { get; set; }
        public string yb_usernick { get; set; }
        public string yb_sex { get; set; }
        public string yb_money { get; set; }
        public string yb_exp { get; set; }
        public string yb_userhead { get; set; }
        public string yb_schoolid { get; set; }
        public string yb_schoolname { get; set; }
        public string yb_regtime { get; set; }
    }
}