using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Service;

namespace Nest.BaseCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTicketController : ControllerBase
    {
        private readonly IAppTicketService _appTicketService;
        public AppTicketController(IAppTicketService appTicketService)
        {
            _appTicketService = appTicketService;
        }

        /// <summary>
        /// 获取秘钥
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppSecret")]
        public ApiResultModel<string> GetAppSecret(AddAppTicketRequestModel requestModel)
        {
            return _appTicketService.GetAppSecret(requestModel);
        }
    }
}