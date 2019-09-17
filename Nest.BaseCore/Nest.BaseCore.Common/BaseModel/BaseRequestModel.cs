using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 接口请求参数基类
    /// </summary>
    public class BaseRequestModel
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required]
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required]
        public string Nonce { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Required]
        public string Signature { get; set; }
    }

    /// <summary>
    /// 分页基本请求参数模型
    /// </summary>
    public class BasePageRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required]
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页码大小
        /// </summary>
        [Required]
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// 分页关键字基本请求参数模型
    /// </summary>
    public class BasePageKeywordRequestModel : BasePageRequestModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWord { get; set; } = "";
    }

    /// <summary>
    /// 字符串Id基本请求参数模型
    /// </summary>
    public class BaseStringIdRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 字符串Id
        /// </summary>
        [Required]
        public string Id { get; set; } = "";
    }

    /// <summary>
    /// 整形Id基本请求参数模型
    /// </summary>
    public class BaseIntIdRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 整形Id
        /// </summary>
        [Required]
        public int Id { get; set; } = 0;
    }

    ///// <summary>
    ///// Api授权请求参数实体
    ///// </summary>
    //public class ApiAuthorizeRequestModel : ApiBaseRequestModel
    //{
    //    /// <summary>
    //    /// 应用号
    //    /// </summary>
    //    [Required]
    //    public string AppId { get; set; }
    //    /// <summary>
    //    /// 请求设备号（设备唯一标识符uuid）
    //    /// </summary>
    //    [Required]
    //    public string DeviceNo { get; set; }
    //    /// <summary>
    //    /// 客户端类型(必须与AppId一致)
    //    /// </summary>
    //    [Required]
    //    public string ClientType { get; set; }
    //}
}
