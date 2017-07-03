using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;


namespace qingjia_YiBan.HomePage
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
                //WebClient 发送 Post请求
                string postString = "client_id=" + appid + "&client_secret=" + secret + "&code=" + code + "&redirect_uri=" + "http://zhanglidaoyan.com";
                byte[] postData = Encoding.UTF8.GetBytes(postString);//将字符串转换为UTF-8编码
                string url = "https://openapi.yiban.cn/oauth/access_token";
                System.Net.WebClient webclient = new System.Net.WebClient();
                webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//POST 请求在头部必须添加
                byte[] responseData = webclient.UploadData(url, "POST", postData);//发起POST请求、返回byte字节
                string srcString = Encoding.UTF8.GetString(responseData);//将byte字节转换为字符串

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();//解析JSON数据
                JsonModel obj = serializer.Deserialize<JsonModel>(srcString);

                if (obj != null)
                {
                    context.Response.Redirect("qingjia_WeChat.aspx?access_token=" + obj.access_token + "&userid=" + obj.userid + "&expires=" + obj.expires + "&data=" + srcString.ToString());//获取成功后
                }
                else
                {
                    context.Response.Redirect("error.aspx");//未获取到授权
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

        public class JsonModel //获取Json数据 解析模型
        {
            public string access_token { get; set; }
            public string userid { get; set; }
            public string expires { get; set; }
        }
    }
}