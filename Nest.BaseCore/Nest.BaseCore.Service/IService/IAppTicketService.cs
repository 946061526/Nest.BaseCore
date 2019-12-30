using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public interface IAppTicketService
    {
        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        ApiResultModel<AddAppTicketResponseModel> GetAppTicket(AddAppTicketRequestModel requestModel);
    }
}
