﻿using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.RequestModel
{
    public class AppTicketRequestModel
    {
    }

    public class AddAppTicketRequestModel : BaseRequestModel
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 客户端类型（ios、android）
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNo { get; set; }
    }
}
