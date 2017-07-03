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
            string srcString = "";

            //if (SendMessage("8230951", "易班消息接口测试！"))
            //{
            //    srcString += "消息发送成功！   ";
            //}
            //else
            //{
            //    srcString += "消息发送失败！   ";
            //}

            UserMe userInfoData = GetUserInfo("https://openapi.yiban.cn/user/me");


            srcString += "账号ID：" + userInfoData.info.yb_userid;
            srcString += "姓名：" + userInfoData.info.yb_username;
            srcString += "学校姓名：" + userInfoData.info.yb_schoolname;

            userInfo.Text = srcString;
        }

        protected bool SendMessage(string to_yb_uid, string content)
        {
            //WebClient 发送 Post请求
            //string access_token = Session["Access_Token"].ToString();//Access_Token存储在Session中
            string access_token = "c4af6ff158a3d4fdf7e1a309a70efda7a430511f";
            MsgApi msgAPI = new MsgApi(access_token);

            return msgAPI.SendMsg(to_yb_uid, content);
        }

        protected UserMe GetUserInfo(string url)
        {
            //WebClient 发送 Post请求
            //string access_token = Session["Access_Token"].ToString();//Access_Token存储在Session中
            string access_token = "c4af6ff158a3d4fdf7e1a309a70efda7a430511f";

            UserApi userAPI = new UserApi(access_token);
            UserMe meInfo = userAPI.GetMe();

            return meInfo;
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