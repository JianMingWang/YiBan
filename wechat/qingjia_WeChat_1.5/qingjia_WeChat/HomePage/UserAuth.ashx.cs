using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_WeChat.HomePage
{
    /// <summary>
    /// UserAuth 的摘要说明
    /// </summary>
    public class UserAuth : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var appid = "wxd66dc84bc3f5e793";//学工广场
            var secret = "9e20112513fdff5b69251ee70c690d5f";//学工广场

            // context.Session["pageCate"] = pageCate;

            var code = context.Request.QueryString["code"];
            var state = context.Request.QueryString["state"];

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fwww.zhanglidaoyan.com&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", appid);
                context.Response.Redirect(url);
            }


            else
            {
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
 
                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
                var data = client.DownloadString(url);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
				
				//反序列化  将json转换成键值对
                var obj = serializer.Deserialize<Dictionary<string, string>>(data);
				
				//通过关键字来获取值
                string accessToken;
                if (!obj.TryGetValue("access_token", out accessToken))
                    return;

                var opentid = obj["openid"];

                if (opentid != null && opentid != "")
                {
                    context.Response.Redirect("./SubPage/bind.aspx?openId=" + opentid);
                }
                //if (state.Equals("1"))
                //{
                //    context.Response.Redirect("qingjia_WeChat.aspx?openId=" + opentid);
                //}
                //else if (state.Equals("0"))
                //{
                //    context.Response.Redirect("qingjia_WeChat.aspx?openId=" + opentid);
                //}
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