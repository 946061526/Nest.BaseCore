using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Log;
using Nest.BaseCore.Service;

namespace Nest.BaseCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IExceptionlessLogger _Log { get; }
        private readonly IUserService _userService;
        public TestController(IExceptionlessLogger log, IUserService userService)
        {
            _Log = log;
            _userService = userService;
        }

        /// <summary>
        /// 测试Log
        /// </summary>
        [HttpPost]
        [Route("Test")]
        public ApiResultModel<string> TestLog()
        {
            Net4Logger.Debug("debug", "阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("debug"));
            Net4Logger.Error("error", "asfsafdsfasfdsf阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("error"));
            Net4Logger.Info("info", "1q324154354325654阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("info"));

            return new ApiResultModel<string>();
        }
    }
}