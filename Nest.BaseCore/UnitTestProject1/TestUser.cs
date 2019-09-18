using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Repository;
using Nest.BaseCore.Service;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUser
    {
        DbContextOptions<MainContext> options;

        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public TestUser()
        {
            //IServiceProvider 
            //options = new DbContextOptions<MainContext>();
            //options.
            //AutofacConfig.RegisterAutoFac();
            using (var scope = AutofacConfig.RegisterAutoFac().BeginLifetimeScope())
            {
                _userService = scope.Resolve<IUserService>();
               // _userRepository = scope.Resolve<IUserRepository>();
            }
        }


        [TestMethod]
        public void Add()
        {
            var user = new User()
            {
                id = GuidTool.GetGuid(),
                name = "Karroy",
                userName = "Karroy"
            };
            var i = _userService.Add(user);


            List<User> users = new List<User>();
            users.Add(user);
            user = new User()
            {
                id = GuidTool.GetGuid(),
                name = "jay",
                userName = "jay"
            };
            users.Add(user);
            i = _userService.Add(users);

        }
    }
}
