using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiToken.Common;

namespace WebApiToken.Controllers
{
    public class MerchantController : ApiController
    {
        /// <summary>
        /// 加密jdata(仅用于生成：参数&token后的MD5密文，复制此密文作为sign参数传递)
        /// </summary>
        /// <param name="merchant">供应商code</param>
        /// <param name="jdata">参数集合</param>
        /// <returns></returns>
        [HttpPost]
        public ApiBaseResult Post(string merchant, [FromBody]JObject jdata)
        {
            //url:   http://localhost:8372/api/Merchant/?merchant=baidu
            //post: {"id":"1"}
            //输出：{"result": "success","messages": null,"fieldErrors": null,"errors": null,"data": "1b767e499f91606f80380394f994c3"}
            ApiBaseResult Result = new ApiBaseResult();
            try
            {
                if (jdata == null || string.IsNullOrEmpty(merchant))//检查传入参数规范
                {
                    Result.result = ApiResult.Failure.ToString().ToLower();
                    return Result;
                }
                var token = ConfigUtil.GetAppSetValue(merchant);//从web.config中获取供应商的token
                JObject dataValue = jdata as JObject;
                var dataValueStr = dataValue.ToString().Replace("\r\n", "").Replace(" ", "") + "&" + token;//将参数+token拼接起来再加密(DES，MD5都行)
                var data = MD5Encryptor.Encrypt(dataValueStr);
                Result.result = ApiResult.Success.ToString().ToLower();
                Result.data = data;

                return Result;
            }
            catch (Exception ex)
            {
                Result.result = ApiResult.Failure.ToString().ToLower();
                Result.messages = ex.Source + ex.Message + ex.StackTrace;
                return Result;
            }
        }
    }
}
