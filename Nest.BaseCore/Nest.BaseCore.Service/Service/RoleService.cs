using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Nest.BaseCore.Service
{
    public class RoleService : IRoleService
    {
        private HttpClient _httpClient;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        public List<RoleResponseModel> GetRoleList()
        {
            var result = _roleRepository.Find().Select(a => new RoleResponseModel()
            {
                Id = a.id,
                Name = a.name
            }).ToList();

            return result;
        }
    }
}
