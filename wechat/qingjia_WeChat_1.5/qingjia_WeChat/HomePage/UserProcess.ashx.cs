using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_WeChat.HomePage
{
    /// <summary>
    /// UserProcess 的摘要说明
    /// </summary>
    public class UserProcess : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var appid = "wx7c4da8ce4965fe78";//管理学院
            var secret = "a137fd495599209338701b65fb895a40";//管理学院

            // context.Session["pageCate"] = pageCate;
            var code = context.Request.QueryString["code"];
            var state = context.Request.QueryString["state"];

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fqingjiawechat.imawesome.net&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", appid);
                context.Response.Redirect(url);
            }

            else
            {
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
                var data = client.DownloadString(url);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var obj = serializer.Deserialize<Dictionary<string, string>>(data);
                string accessToken;
                if (!obj.TryGetValue("access_token", out accessToken))
                    return;

                var opentid = obj["openid"];

                if (opentid != null && opentid != "")
                {
                    context.Response.Redirect("./SubPage/bind.aspx?openId=" + opentid);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}