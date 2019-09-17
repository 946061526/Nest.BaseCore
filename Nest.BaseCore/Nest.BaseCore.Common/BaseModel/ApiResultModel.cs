using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 返回状态码
    /// </summary>
    public enum ApiResultCode
    {
        /// <summary>
        /// 业务处理成功
        /// </summary>
        [Description("业务处理成功")]
        Success = 200,
        /// <summary>
        /// 业务处理失败（默认）
        /// </summary>
        [Description("业务处理失败")]
        Fail = 414,
        /// <summary>
        /// 业务处理异常
        /// </summary>
        [Description("业务处理异常")]
        Exception = 500,


        #region 授权服务，10开头，如1001,1002...


        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        NoToken = 1001,
        /// <summary>
        /// 用户登录失效
        /// </summary>
        [Description("用户登录失效，请重新登录")]
        UserInvalid = 1002,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        UserNotExists = 1003,
        /// <summary>
        /// 登录密码错误
        /// </summary>
        [Description("登录密码错误")]
        LoginPassError = 1004,

        #endregion
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T">数据</typeparam>
    public class ApiResultModel<T>
    {
        public ApiResultModel()
        {
            Code = ApiResultCode.Fail;
            Data = default(T);
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public ApiResultCode Code { set; get; }

        private string _msg = string.Empty;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get
            {
                if (string.IsNullOrEmpty(_msg))
                {
                    return Code.GetEnumDescription();
                }
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { set; get; }
    }

    /// <summary>
    /// 返回结果（分页）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPagedResultModel<T> : ApiResultModel<IEnumerable<T>>
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
