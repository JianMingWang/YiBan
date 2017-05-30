using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YbSDK.Model;
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

            var code = context.Request.QueryString["code"];
            var state = context.Request.QueryString["state"];

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://openapi.yiban.cn/oauth/authorize?client_id={0}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com&state=STATE", appid);
                context.Response.Redirect(url);
            }
            else
            {
                IRestClient restClient = new RestClient("https://openapi.yiban.cn");
                RestRequest request;
                request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.Resource = "oauth/access_token";
                request.RequestFormat = DataFormat.Json;

                //添加参数
                request.AddParameter("client_id", appid, ParameterType.QueryString);
                request.AddParameter("client_secret", secret, ParameterType.QueryString);
                request.AddParameter("code", code.ToString(), ParameterType.QueryString);
                request.AddParameter("redirect_uri", "http://zhanglidaoyan.com", ParameterType.QueryString);

                //获得response
                IRestResponse response = null;
                response = restClient.Execute(request);
                //var result = Deserialize<AccessToken>(response.Content);

                //var client = new System.Net.WebClient();
                //client.Encoding = System.Text.Encoding.UTF8;

                //var url = string.Format("https://openapi.yiban.cn/oauth/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com", appid, secret, code);
                //var data = client.DownloadString(url,"POST",);//转换为字符串格式

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //反序列化  将json转换成键值对
                var obj = serializer.Deserialize<AccessToken>(response.Content);

                //string access_token;
                //if (!obj.TryGetValue("access_token", out access_token))
                //    return;

                //string userid;
                //if (!obj.TryGetValue("userid", out userid))
                //    return;

                //string expires;
                //if (!obj.TryGetValue("expires", out expires))
                //    return;

                if (obj != null)
                {
                    context.Response.Redirect("Test.aspx?access_token=" + obj.access_token + "&userid=" + obj.userid + "&expires=" + obj.expires + "&data=" + response.Content.ToString());
                }
                else
                {
                    context.Response.Redirect("Test.aspx");
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