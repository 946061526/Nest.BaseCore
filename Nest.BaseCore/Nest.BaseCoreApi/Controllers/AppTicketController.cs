using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Aop;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Service;

namespace Nest.BaseCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateSignature]
    public class AppTicketController : ControllerBase
    {
        private readonly IAppTicketService _appTicketService;
        public AppTicketController(IAppTicketService appTicketService)
        {
            _appTicketService = appTicketService;
        }

        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppTicket")]
        public ApiResultModel<AddAppTicketResponseModel> GetAppTicket([FromBody]AddAppTicketRequestModel requestModel)
        {
            return _appTicketService.GetAppTicket(requestModel);
        }
    }
}