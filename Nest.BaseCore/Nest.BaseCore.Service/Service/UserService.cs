using Microsoft.Extensions.Configuration;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Nest.BaseCore.Service
{
    public class UserService : IUserService
    {
        private readonly MainContext _db;
        private readonly IConfiguration _config;
        private HttpClient _httpClient;
        private readonly IUserRepository _userRepository;

        public UserService(MainContext db, IUserRepository userRepository)
        {
            _db = db;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel">参数</param>
        public ApiResultModel<LoginResponseModel> Login(LoginRequestModel requestModel)
        {
            var result = new ApiResultModel<LoginResponseModel>();
            var user = (from a in _db.User
                        where a.userName == requestModel.UserName
                        select new LoginResponseModel()
                        {
                            UserId = a.id,
                            UserName = a.userName,
                            Name = a.name,
                            RoleId = a.roleId,
                            Password = a.password,
                        }).FirstOrDefault();

            result.Data = user;
            result.Code = ApiResultCode.Success;
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        public List<LoginResponseModel> GetUserList()
        {
            var result = _userRepository.Find().Select(a => new LoginResponseModel()
            {
                UserId = a.id,
                UserName = a.userName,
                Name = a.name,
                RoleId = a.roleId,
                Password = a.password,
            }).ToList();

            return result;
        }


        public int Add(User a)
        {
            User user = new User()
            {
                id = GuidTool.GetGuid(),
                name = "Karroy",
                userName = "Karroy",
                password = "123",
                roleId = "1"
            };
            _userRepository.Add(user);
            return _userRepository.SaveChanges();
        }
        public int Add(List<User> t)
        {
            User user = new User()
            {
                id = GuidTool.GetGuid(),
                name = "Karroy1",
                userName = "Karroy1",
                password = "123",
                roleId = "1"
            };
            var users = new List<User>();
            users.Add(user);
            user = new User()
            {
                id = GuidTool.GetGuid(),
                name = "jay",
                userName = "jay",
                password = "123",
                roleId = "1"
            };
            users.Add(user);
            _userRepository.Add(users);
            return _userRepository.SaveChanges();
        }

        public int Update()
        {
            var user = _userRepository.FirstOrDefault(x => x.userName == "Karroy1");
            user.password = "";
            _userRepository.Update(user);
            var i=_userRepository.SaveChanges();


            var list = _userRepository.Find(x => x.password == "123").ToList();
            list.ForEach(item =>
            {
                item.password = "";
            });
             _userRepository.Update(list);

            i = _userRepository.SaveChanges();

            return i;
        }


    }
}
