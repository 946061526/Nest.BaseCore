using Nest.BaseCore.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public interface IRoleService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        List<RoleResponseModel> GetRoleList();
    }
}
