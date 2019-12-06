
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
        /// 未找到相关记录
        /// </summary>
        [Description("未找到相关记录")]
        NoRecord = 416,
        /// <summary>
        /// 业务处理异常
        /// </summary>
        [Description("业务处理异常")]
        Exception = 500,


        #region 授权服务，100开头，如1001,1002...


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

        #region 记录查询，7000开头，如7001,7002...
        /// <summary>
        /// 查无数据
        /// </summary>
        [Description("查无数据")]
        RS_NoData =7001,
        #endregion

        #region 微信服务，800开头，如8001,8002...
        /// <summary>
        /// 微信消息发送失败
        /// </summary>
        [Description("微信消息发送失败")]
        WX_SendError = 8001,
        /// <summary>
        /// 微信消息发送异常
        /// </summary>
        [Description("微信消息发送异常")]
        WX_SendException = 8002,

        /// <summary>
        /// 微信发送消息班级名称不能为空
        /// </summary>
        [Description("微信发送消息班级名称不能为空")]
        WX_NoClassName = 8003,

        /// <summary>
        /// 微信发送消息总人数不能小于0
        /// </summary>
        [Description("微信发送消息总人数不能小于0")]
        WX_NoTotal = 8004,
        /// <summary>
        /// 微信发送请假消息请假对象名称不能为空
        /// </summary>
        [Description("微信发送请假消息请假对象名称不能为空")]
        WX_NoStudentName = 8005,
        /// <summary>
        /// 微信发送请假消息请假天数错误
        /// </summary>
        [Description("微信发送请假消息请假天数错误")]
        WX_CountEroor = 8006,
        /// <summary>
        /// 微信发送请假消息请假事由不能为空
        /// </summary>
        [Description("微信发送请假消息请假事由不能为空")]
        WX_NoContent = 8007,
        /// <summary>
        /// 微信发送请假消息请假开始时间不能大于结束时间
        /// </summary>
        [Description("微信发送请假消息请假开始时间不能大于结束时间")]
        WX_EroorEnd = 8008,
        /// <summary>
        /// 微信发送出入校消息学生名称不能为空
        /// </summary>
        [Description("微信发送出入校消息学生名称不能为空")]
        WX_NoName = 8009,
        /// <summary>
        /// 微信发送出消息未找到对应手机号绑定微信openid
        /// </summary>
        [Description("微信发送出消息未找到对应手机号绑定微信openid")]
        WX_NoOpenid = 8010,
        /// <summary>
        /// 微信发送消息未获取到accesstoken
        /// </summary>
        [Description("微信发送消息未获取到accesstoken")]
        WX_NoAccesstoken = 8011,
        #endregion

        #region 图片服务，900开头，如9001,9002...
        /// <summary>
        /// 图片存储异常
        /// </summary>
        [Description("图片存储异常")]
        IMG_ImageSaveException = 9001,
        /// <summary>
        /// 服务器未找到该图片
        /// </summary>
        [Description("服务器未找到该图片")]
        IMG_ImageNoPath = 9002,
        /// <summary>
        /// 图片存储传入base64字符串为空
        /// </summary>
        [Description("图片存储传入base64字符串为空")]
        IMG_ImageNoContent = 9003,
        // <summary>
        /// 图片删除传入图片路径空
        /// </summary>
        [Description("图片删除传入图片路径空")]
        IMG_ImageTempPath = 9004,
        /// <summary>
        /// 图片删除异常
        /// </summary>
        [Description("图片删除异常")]
        IMG_ImageDelException = 9005,
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
        public string Message
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

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }
}
