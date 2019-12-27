using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public interface IAppTicketService
    {
        /// <summary>
        /// 获取秘钥
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        ApiResultModel<string> GetAppSecret(AddAppTicketRequestModel requestModel);
    }
}
