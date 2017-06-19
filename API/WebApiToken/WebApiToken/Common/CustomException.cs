using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApiToken.Common
{
    public class CustomException
    {

    }
    /// <summary>
    /// API返回结果
    /// </summary>
    public enum ApiResult
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1
    }
    /// <summary>
    /// 错误代码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 权限问题
        /// </summary>
        Authority = 1000,
        /// <summary>
        /// 数据问题
        /// </summary>
        Data = 2000,
        /// <summary>
        /// 参数问题
        /// </summary>
        Arguments = 2001,
        /// <summary>
        /// 业务问题
        /// </summary>
        Business = 3000,
        /// <summary>
        /// 未知问题
        /// </summary>
        Unknown = 4000
    }
    /// <summary>
    /// 业务异常(记录日志)
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 消息
        /// </summary>
        public new string Message { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Errors { get; set; }
        /// <summary>
        /// 单据关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDateTime { get; set; }
        /// <summary>
        /// 传入参数
        /// </summary>
        public string Arguments { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Errors))
            {
                sb.AppendFormat("业务异常--错误码:{0} ", Errors);
            }
            sb.AppendFormat("错误原因:{0} ", Message);
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                sb.AppendFormat("备注:{0} ", Remark);
            }
            if (!string.IsNullOrWhiteSpace(Arguments))
            {
                sb.AppendFormat("传入参数:{0} ", Arguments);
            }
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sb.AppendFormat("传入参数:{0} ", Key);
            }
            return sb.ToString();
        }
    }
    /// <summary>
    /// 系统异常(记录日志)
    /// </summary>
    public class SystemCustomException : Exception
    {
        /// <summary>
        /// 消息
        /// </summary>
        public new string Message { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Errors { get; set; }
        /// <summary>
        /// 错误长描述
        /// </summary>
        public string LongMessage { get; set; }
        /// <summary>
        /// 错误堆栈
        /// </summary>
        public string ErrorStackTrace { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDateTime { get; set; }
        /// <summary>
        /// 传入参数(JSON格式)
        /// </summary>
        public string Arguments { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Errors))
            {
                sb.AppendFormat("系统异常--错误码:{0} ", Errors);
            }
            sb.AppendFormat("错误原因:{0} ", Message);
            if (!string.IsNullOrWhiteSpace(LongMessage))
            {
                sb.AppendFormat("内部错误:{0} ", LongMessage);
            }
            if (!string.IsNullOrWhiteSpace(ErrorStackTrace))
            {
                sb.AppendFormat("错误堆栈:{0} ", ErrorStackTrace);
            }
            if (!string.IsNullOrWhiteSpace(Arguments))
            {
                sb.AppendFormat("传入参数:{0} ", Arguments);
            }
            return sb.ToString();
        }
    }
    /// <summary>
    /// 网络连接异常或数据库连接异常（重试）
    /// </summary>
    public class ConntionException : Exception
    {
        private string _errors = "5000";//网络传输异常编号
        /// <summary>
        /// 消息
        /// </summary>
        public new string Message { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Errors { get { return _errors; } }
        /// <summary>
        /// 错误常描述
        /// </summary>
        public string LongMessage { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDateTime { get; set; }
        /// <summary>
        /// 传入参数(JSON格式)
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string ReMark { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Errors))
            {
                sb.AppendFormat("网络--错误码:{0} ", Errors);
            }
            sb.AppendFormat("错误原因:{0} ", Message);
            if (!string.IsNullOrWhiteSpace(LongMessage))
            {
                sb.AppendFormat("内部错误:{0} ", LongMessage);
            }
            if (!string.IsNullOrWhiteSpace(ReMark))
            {
                sb.AppendFormat("备注:{0} ", ReMark);
            }
            if (!string.IsNullOrWhiteSpace(Arguments))
            {
                sb.AppendFormat("传入参数:{0} ", Arguments);
            }
            return sb.ToString();
        }
    }
    public class BusinessMsg
    {
        /// <summary>
        /// 单据关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string ReMark { get; set; }
    }
}