using Nest.BaseCore.Common;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Service
{
    public interface IRoleService : IBaseService<BaseIdModel, AddRoleRequestModel, EditRoleRequestModel, QueryRoleRequestModel, QueryRoleResponseModel>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        List<RoleResponseModel> GetRoleList();
    }
}
