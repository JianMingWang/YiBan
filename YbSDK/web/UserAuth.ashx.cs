using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YbSDK.Model;
using YbSDK.Api;
using RestSharp;

namespace web
{
    /// <summary>
    /// UserAuth 的摘要说明
    /// </summary>
    public class UserAuth : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var appid = "d75b391ef6abcbfa";
            var secret = "cd05598f3486167f53a38da21c125e04";

            var code = context.Request.QueryString["code"];//code授权码
            var state = context.Request.QueryString["state"];//state防止拦截攻击

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://openapi.yiban.cn/oauth/authorize?client_id={0}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com&state=STATE", appid);
                context.Response.Redirect(url);
            }
            else
            {
                //get请求  报错
                //var client = new System.Net.WebClient();
                //client.Encoding = System.Text.Encoding.UTF8;

                //var url = string.Format("https://openapi.yiban.cn/oauth/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com", appid, secret, code);
                //var data = client.DownloadString(url);

                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //JsonModel obj = serializer.Deserialize<JsonModel>(data);

                //Post请求

                if (obj != null)
                {
                    context.Response.Redirect("Test.aspx?access_token=" + obj.access_token + "&userid=" + obj.userid + "&expires=" + obj.expires + "&data=" + data.ToString());
                }
                else
                {
                    context.Response.Redirect("Test.aspx");
                }

                //if (!obj.TryGetValue("access_token", out accessToken))
                //    return;

                //if (obj != null)
                //{
                //    context.Response.Redirect("Test.aspx?access_token=" + obj["access_token"] + "&userid=" + obj["userid"] + "&expires=" + obj["expires"] + "&data=" + data.ToString());
                //}
                //else
                //{
                //    context.Response.Redirect("Test.aspx");
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

    public class JsonModel
    {
        public string access_token { get; set; }
        public string userid { get; set; }
        public string expires { get; set; }
    }
}