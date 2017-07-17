using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;
using System.Security.Cryptography;
using System.IO;
using System.Web.Script.Serialization;


namespace qingjia_YiBan.HomePage
{
    /// <summary>
    /// UserAuth 的摘要说明
    /// </summary>
    public class UserAuth : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var appid = "28a9bb3bd738ffdb";
            var secret = "415e7189ec176d377fffc3678b732079";

            var verify_request = context.Request.QueryString["verify_request"];

            if (string.IsNullOrEmpty(verify_request))
            {
                var url = string.Format("https://openapi.yiban.cn/oauth/authorize?client_id={0}&redirect_uri=http%3a%2f%2ff.yiban.cn/iapp131261&display=html", appid);
                context.Response.Redirect(url);
                return;
            }
            else
            {
                string encryptStr = verify_request.ToString();
                string Key = secret;
                string IV = appid;

                VisitOauth YBresult = null;
                string decryptStr = Decrypt(encryptStr, Key, IV);
                if (decryptStr.Contains("\"visit_oauth\":false"))
                {
                    // result = new VisitOauth() { IsAuthorized = false };
                    YBresult = new VisitOauth();
                    //YiBan 授权失败 跳转到绑定页面
                    context.Response.Redirect("Error.aspx?errorMessage=" + "获取授权失败，请重新打开轻应用！");
                }
                else
                {
                    decryptStr = decryptStr.Substring(0, decryptStr.LastIndexOf('}') + 1);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    YBresult = jss.Deserialize<VisitOauth>(decryptStr);
                    YBresult.IsAuthorized = true;
                    string YiBanID = YBresult.visit_user.userid;
                    //获取到用户ID  AccessToken
                    Client<qingjia_AccessToken> client = new Client<qingjia_AccessToken>();
                    ApiResult<qingjia_AccessToken> qingjia_result = client.GetRequest("YiBanID=" + YiBanID, "/api/oauth/access_token");

                    if (qingjia_result.result == "success" || qingjia_result.data != null)
                    {
                        //获取授权
                        context.Response.Redirect("qingjia_WeChat.aspx?access_token=" + qingjia_result.data.access_token);
                    }
                    else
                    {
                        //qingjia 授权失败 跳转到绑定页面
                        context.Response.Redirect("./SubPage/bind.aspx?YiBanID=" + YiBanID);
                    }
                }
            }

            #region 网站接入接口调用写法
            //var appid = "28a9bb3bd738ffdb";
            //var secret = "415e7189ec176d377fffc3678b732079";

            //var code = context.Request.QueryString["code"];//code授权码
            //var state = context.Request.QueryString["state"];//state防止拦截攻击

            //if (string.IsNullOrEmpty(code))
            //{
            //    var url = string.Format("https://openapi.yiban.cn/oauth/authorize?client_id={0}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com&state=STATE", appid);
            //    context.Response.Redirect(url);
            //    return;
            //}
            //else
            //{
            //    //WebClient 发送 Post请求
            //    string postString = "client_id=" + appid + "&client_secret=" + secret + "&code=" + code + "&redirect_uri=" + "http://zhanglidaoyan.com";
            //    byte[] postData = Encoding.UTF8.GetBytes(postString);//将字符串转换为UTF-8编码
            //    string url = "https://openapi.yiban.cn/oauth/access_token";
            //    System.Net.WebClient webclient = new System.Net.WebClient();
            //    webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//POST 请求在头部必须添加
            //    byte[] responseData = webclient.UploadData(url, "POST", postData);//发起POST请求、返回byte字节
            //    string srcString = Encoding.UTF8.GetString(responseData);//将byte字节转换为字符串

            //    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();//解析JSON数据
            //    JsonModel obj = serializer.Deserialize<JsonModel>(srcString);

            //    if (obj != null)
            //    {
            //        //获取到用户ID  AccessToken
            //        Client<qingjia_AccessToken> client = new Client<qingjia_AccessToken>();
            //        ApiResult<qingjia_AccessToken> result = client.GetRequest("YiBanID=" + obj.userid, "/api/oauth/access_token");

            //        if (result.result == "success" || result.data != null)
            //        {
            //            //获取授权
            //            context.Response.Redirect("qingjia_WeChat.aspx?access_token=" + result.data.access_token);
            //        }
            //        else
            //        {
            //            //qingjia 授权失败 跳转到绑定页面
            //            context.Response.Redirect("./SubPage/bind.aspx?YiBanID=" + obj.userid);
            //        }
            //    }
            //    else
            //    {
            //        //未获取到易班授权授权 跳转到错误页面
            //        context.Response.Redirect("Error.aspx");
            //    }
            //}
            #endregion
        }


        //AES-128-CBC 解密
        private string Decrypt(string encryptStr, string Key, string IV)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = strToToHexByte(encryptStr);
            Rijndael aes = Rijndael.Create();
            aes.Key = bKey;
            aes.IV = bIV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.KeySize = bIV.Length == 16 ? 128 : 256;//16字符长度appID应用采用AES-128-CBC对称加密算法；原32字符长度appID应用依旧采用AES-256-CBC对称加密算法。
            aes.BlockSize = bIV.Length == 16 ? 128 : 256;
            var decrypt = "";
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            aes.Clear();
            return decrypt;
        }

        private byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class JsonModel //网站接入、返回数据模型
    {
        public string access_token { get; set; }
        public string userid { get; set; }
        public string expires { get; set; }
    }

    public class VisitOauth //轻应用接入、返回数据模型
    {
        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsAuthorized { get; set; }
        public string visit_time { get; set; }
        public Visit_User visit_user { get; set; }
        public Visit_Oauth visit_oauth { get; set; }
        public class Visit_User
        {
            public string userid { get; set; }
            public string username { get; set; }
            public string usernick { get; set; }
            public string usersex { get; set; }
        }
        public class Visit_Oauth
        {
            public string access_token { get; set; }
            public string token_expires { get; set; }
        }
    }
}