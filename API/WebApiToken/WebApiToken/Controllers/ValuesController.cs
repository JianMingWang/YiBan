using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiToken.Common;
using WebApiToken.Models;

namespace WebApiToken.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 根据用户id获取用户信息的API接口
        /// </summary>
        /// <param name="merchant">商户编号，不同商户提供不同的token</param>
        /// <param name="sign">业务参数MD5加密获得,参数&token再MD5编码</param>
        /// <param name="version">接口版本号，本例1.0.0</param>
        /// /* <param name="timestamp">时间戳（本例不考虑）</param> */
        /// <param name="jdata">参数包(实体类)json</param>
        /// <returns></returns>
        [HttpPost]//以Post方式调用本API
        [AuthorizeFilter]//过滤器验证，本例核心代码
        //public ApiBaseResult GetUserInfoByUserID(string merchant, string sign, string version,string timestamp,[FromBody]JObject jdata)
        public ApiBaseResult GetUserInfoByUserID(string merchant, string sign, string version, [FromBody]JObject jdata)
        {
            //url:  http://localhost:8372/api/Values/GetUserInfoByUserID?merchant=baidu&version=1.0.0&sign=1b767e499f91606f80380394f994c3
            //post: {"id":"1"}
            //输出：{"result": "success","messages": null,"fieldErrors": null,"errors": null,"data":{"id": 1,"Name": "Tom","Age": 18,"Gender": "male","Nationality": "USA"}}
            ApiBaseResult Result = new ApiBaseResult();
            try
            {
                if (null == jdata)
                {
                    throw new ArgumentException("表单数据jdata参数为空");
                }
                var data = jdata.ToObject<UserModel>();//json转实体
                if (data.id == 0)
                {
                    throw new ArgumentException("表单数据id参数为空");
                }
                Result.result = ApiResult.Success.ToString().ToLower();
                Result.data = GetUserInfoByUserID(data.id);

                return Result;
            }
            catch (Exception ex)
            {
                Result.result = ApiResult.Failure.ToString().ToLower();
                Result.messages = ex.Source + ex.Message + ex.StackTrace;
                return Result;
            }
        }
        /// <summary>
        /// 获取用户数据，此处应放于BLL逻辑层处理
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private UserModel GetUserInfoByUserID(int userid)
        {
            UserModel user = AllUser().FirstOrDefault(d => d.id == userid);

            return user;
        }
        /// <summary>
        /// 模拟数据，实际应用中此处应从DAL层中取出数据库数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<UserModel> AllUser()
        {
            List<UserModel> user = new List<UserModel>();

            UserModel user1 = new UserModel
            {
                id = 1,
                Name = "Tom",
                Age = 18,
                Gender = "male",
                Nationality = "USA"
            };
            user.Add(user1);

            UserModel user2 = new UserModel
            {
                id = 2,
                Name = "Marry",
                Age = 16,
                Gender = "female",
                Nationality = "ES"
            };
            user.Add(user2);

            UserModel user3 = new UserModel
            {
                id = 3,
                Name = "Ada",
                Age = 17,
                Gender = "female",
                Nationality = "CHN"
            };
            user.Add(user3);

            return user;
        }
    }
}