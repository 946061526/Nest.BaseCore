using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Log;
using Nest.BaseCore.Service;

namespace Nest.BaseCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IExceptionlessLogger _Log { get; }
        private readonly IRoleService _roleService;
        public RoleController(IExceptionlessLogger log, IRoleService roleService)
        {
            _Log = log;
            _roleService = roleService;
        }


        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="requestModel">参数</param>
        [HttpPost]
        [Route("GetRoleList")]
        public ApiResultModel<List<RoleResponseModel>> GetRoleList()
        {
            var result = new ApiResultModel<List<RoleResponseModel>>();
            result.Data = _roleService.GetRoleList();
            return result;
        }
    }
}