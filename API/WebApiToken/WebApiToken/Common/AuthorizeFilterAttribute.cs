using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiToken.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    //AttributeTargets.Class  代表 Authorize特性应放在类前面
    //AttributeTargets.Method 代表 Authorize特性应放在方法前面
    //AllowMultiple 代表特性是否可以被重复放置多次   true表示可以重复放置
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        private static Dictionary<HttpMethod, Method> authorizeMethod;

        #region 构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AuthorizeFilterAttribute()
        {
            if (authorizeMethod == null)
            {
                authorizeMethod = new Dictionary<HttpMethod, Method>
                {
                    {HttpMethod.Get, new HttpGetMethod()},
                    {HttpMethod.Post, new HttpPostMethod()}
                };
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 执行方法前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var method = actionContext.Request.Method;
            Method oper = authorizeMethod[method];
            ApiBaseResult result = oper.Authorize(actionContext);
            if (result.result.Equals(ApiResult.Success.ToString().ToLower()))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = result.ToResponseMessageJson();
            }
        }
        #endregion
    }

    public abstract class Method
    {
        #region 属性
        protected static readonly string Version = ConfigUtil.GetAppSetValue("Version");//版本号
        #endregion

        #region 抽象方法
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="actionContext"></param>
        public abstract ApiBaseResult Authorize(HttpActionContext actionContext);
        #endregion

        #region 保护方法
        /// <summary>
        /// 判断版本号
        /// </summary>
        /// <param name="version"></param>
        /// <param name="result"></param>
        protected void CheckVersion(string version, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            if (string.IsNullOrEmpty(version))
            {
                result.messages = "传入参数不正确,版本号不能为空";// new List<string> { "传入参数不正确,版本号不能为空" };
                return;
            }
            string currentVersion = Version.Trim().ToLower();
            version = version.Trim().ToLower();
            if (!currentVersion.Equals(version))
            {
                result.messages = "传入参数不正确,版本号不正确";// new List<string> { "传入参数不正确,版本号不正确" };
                return;
            }
            result.result = ApiResult.Success.ToString().ToLower();
        }

        /// <summary>
        /// 判断商户号
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="result"></param>
        protected void CheckMerchant(string merchant, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            if (string.IsNullOrEmpty(merchant))
            {
                result.messages = "传入参数不正确,商户号不能为空";// new List<string> { "传入参数不正确,商户号不能为空" };
                return;
            }
            result.result = ApiResult.Success.ToString().ToLower();
        }

        /// <summary>
        /// 判断时间戳
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="result"></param>
        protected void CheckTimestamp(string timestamp, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            if (string.IsNullOrEmpty(timestamp))
            {
                result.messages = "传入参数不正确,timestamp不能为空";// new List<string> { "传入参数不正确,商户号不能为空" };
                return;
            }
            DateTime dateTime = DateTime.MaxValue;
            try
            {
                dateTime = DateTime.ParseExact(timestamp, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                result.messages = "传入参数不正确,timestamp格式不正确";// new List<string> { "传入参数不正确,商户号不能为空" };
                return;
            }
            var mintues = (DateTime.Now - dateTime).TotalMinutes;
            if (mintues > 5)
            {
                result.messages = "该请求超时，请求被拒绝";// new List<string> { "传入参数不正确,商户号不能为空" };
                return;
            }
            result.result = ApiResult.Success.ToString().ToLower();
        }
        #endregion
    }

    /// <summary>
    /// Get方法验证
    /// </summary>
    public class HttpGetMethod : Method
    {
        #region 属性
        private static readonly string SignField = ConfigUtil.GetAppSetValue("GetSignField");//必须包含字段
        #endregion

        #region 公有方法
        public override ApiBaseResult Authorize(HttpActionContext actionContext)
        {
            var result = new ApiBaseResult();
            result.errors = ((int)ErrorCode.Arguments).ToString();
            CheckActionArguments(actionContext.ActionArguments, ref result);
            return result;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private void CheckActionArguments(Dictionary<string, object> args, ref ApiBaseResult result)
        {
            if (args == null || !args.Keys.Any())
            {
                result.messages = "传入参数不正确,不能为空";// new List<string> {"传入参数不正确,不能为空"};
                return;
            }

            string[] feilds = SignField.Split(',');
            bool isExists = feilds.All(args.ContainsKey);
            if (!isExists)
            {
                result.messages = "传入参数不正确,必须包含商户号、版本号";// new List<string> { "传入参数不正确,必须包含商户号、版本号" };
                return;
            }
            //判断版本号
            CheckVersion(Convert.ToString(args["version"]), ref result);
            if (result.result.Equals(ApiResult.Failure.ToString().ToLower()))
            {
                return;
            }
            //判断商户
            CheckMerchant(Convert.ToString(args["merchant"]), ref result);
            if (result.result.Equals(ApiResult.Success.ToString().ToLower()))
            {
                result.errors = null;
                result.messages = null;
            }
        }
        #endregion
    }

    /// <summary>
    /// Post方法验证
    /// </summary>
    public class HttpPostMethod : Method
    {
        #region 属性
        private static readonly string SignField = ConfigUtil.GetAppSetValue("PostSignField");//必须包含字段

        private static readonly System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        #endregion

        #region 方法
        public override ApiBaseResult Authorize(HttpActionContext actionContext)
        {
            var result = new ApiBaseResult();
            result.errors = ((int)ErrorCode.Arguments).ToString();
            CheckActionArguments(actionContext.ActionArguments, ref result);
            if (result.result.Equals(ApiResult.Success.ToString().ToLower()))
            {
                result.errors = null;
                result.messages = null;
            }
            return result;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private void CheckActionArguments(Dictionary<string, object> args, ref ApiBaseResult result)
        {
            if (args == null || !args.Keys.Any())
            {
                result.messages = "传入参数不正确,不能为空";// new List<string> { "传入参数不正确,不能为空" }; ;
                return;
            }

            string[] feilds = SignField.Split(',');
            bool isExists = feilds.All(args.ContainsKey);
            if (!isExists)
            {
                result.messages = "传入参数不正确,必须包含系统编码、加密编号";// new List<string> { "传入参数不正确,必须包含系统编码、加密编号" };
                return;
            }
            //必须时间戳（防刷）
            //CheckTimestamp(Convert.ToString(args["timestamp"]), ref result);
            //if (result.result.Equals(ApiResult.Failure.ToString().ToLower()))
            //{
            //    return;
            //}
            //必须有供应商
            CheckSourceCode(Convert.ToString(args["merchant"]), ref result);
            if (result.result.Equals(ApiResult.Failure.ToString().ToLower()))
            {
                return;
            }
            //必须有参数
            Checkjdata(Convert.ToString(args["jdata"]), ref result);
            if (result.result.Equals(ApiResult.Failure.ToString().ToLower()))
            {
                return;
            }
            //CheckAuthCode(Convert.ToString(args["sign"]), Convert.ToString(args["merchant"]), Convert.ToString(args["timestamp"]), args["jdata"], ref result);
            CheckAuthCode(Convert.ToString(args["sign"]), Convert.ToString(args["merchant"]), args["jdata"], ref result);
            if (result.result.Equals(ApiResult.Success.ToString().ToLower()))
            {
                result.errors = null;
                result.messages = null;
            }
        }

        /// <summary>
        /// 验证系统编码
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="result"></param>
        private void CheckSourceCode(string sourceCode, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            if (string.IsNullOrEmpty(sourceCode))
            {
                result.messages = "传入参数不正确加,系统编码不能为空";// new List<string> { "传入参数不正确加,系统编码不能为空" };
                return;
            }
            result.result = ApiResult.Success.ToString().ToLower();
        }

        /// <summary>
        /// 验证表单参数
        /// </summary>
        /// <param name="jdata"></param>
        /// <param name="result"></param>
        private void Checkjdata(object jdata, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            if (jdata == null)
            {
                result.messages = "表单参数为空";// new List<string> { "表单参数为空" };
                return;
            }
            result.result = ApiResult.Success.ToString().ToLower();
        }

        /// <summary>
        /// 验证加密编号
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="merchant"></param>
        /// <param name="jdata"></param>
        /// <param name="result">返回解密后字符</param>
        //private void CheckAuthCode(string sign, string merchant, string timestamp, object jdata, ref ApiBaseResult result)
        private void CheckAuthCode(string sign, string merchant, object jdata, ref ApiBaseResult result)
        {
            result.result = ApiResult.Failure.ToString().ToLower();
            try
            {
                var keyvalue = Convert.ToString(objCache.Get(sign));
                if (!string.IsNullOrWhiteSpace(keyvalue))
                {
                    result.messages = "此访问有风险，访问被拒绝";// new List<string> { "加密字符串不能为空" };
                    return;
                }
            }
            catch (Exception ex)
            {

            }

            if (string.IsNullOrEmpty(sign))
            {
                result.messages = "加密字符串不能为空";// new List<string> { "加密字符串不能为空" };
                return;
            }
            try
            {
                JObject dataValue = jdata as JObject;
                if (dataValue == null)
                {
                    result.messages = "表单参数格式不正确";// new List<string> { "表单参数格式不正确" };
                    return;
                }
                var key = ConfigUtil.GetAppSetValue(merchant);
                //var dataValueStr = dataValue.ToString().Replace("\r\n", "").Replace(" ", "") + "&" + timestamp + "&" + key;
                var dataValueStr = dataValue.ToString().Replace("\r\n", "").Replace(" ", "") + "&" + key;
                //var data = DESEncryptor.Encrypt16(dataValueStr, key);
                var data = MD5Encryptor.Encrypt(dataValueStr);
                sign = HttpUtility.UrlDecode(sign);//参数包含中文请UrlEecode加密，这边来UrlDecode解码
                if (sign != data)
                {
                    result.messages = "权限不足";// new List<string> { "权限不足" };
                    return;
                }
            }
            catch (Exception ex)
            {
                result.result = ApiResult.Failure.ToString().ToLower();
                result.messages = "加密问题,错误原因：" + ex.Message;// new List<string> { "加密问题,错误原因：" + ex.Message };
                return;
            }

            objCache.Insert(sign, 1, null, System.DateTime.Now.AddMinutes(5), TimeSpan.Zero);
            result.result = ApiResult.Success.ToString().ToLower();
        }
        #endregion
    }

    /// <summary>
    /// 序列化
    /// </summary>
    public static class HttpResponse
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage ToResponseMessageJson(this Object obj)
        {
            String str;
            if (obj == null)
            {
                HttpResponseMessage preresult = new HttpResponseMessage { Content = new StringContent("", Encoding.GetEncoding("UTF-8"), "application/json") };
                return preresult;
            }
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj);

            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
    }


}