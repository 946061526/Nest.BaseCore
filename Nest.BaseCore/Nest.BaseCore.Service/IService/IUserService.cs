using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using System.Collections.Generic;

namespace Nest.BaseCore.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel">参数</param>
        ApiResultModel<LoginResponseModel> Login(LoginRequestModel requestModel);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        List<LoginResponseModel> GetUserList();

        int Add(User user);
        int Add(List<User> users);

        int Update();
    }
}
