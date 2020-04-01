using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public class TestUser: BaseTest
    {
        DbContextOptions<MainContext> options;

        //private readonly IUserService _userService;
        //private readonly IUserRepository _userRepository;

        private readonly IUserService _userService;

        public TestUser()
        {
            //_userService = base.Resolve<IUserService>();


            ////IServiceProvider 
            ////options = new DbContextOptions<MainContext>();
            ////options.
            ////AutofacConfig.RegisterAutoFac();

            IServiceCollection services = new ServiceCollection();
            var connection = "server=192.168.1.230;port=3306;database=XFaceUserServiceDB;uid=root;pwd=123456;CharSet=utf8;TreatTinyAsBoolean=true";
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));
            
            ////services.AddSingleton<IUserRepository, TUserRepository>();
            ////services.AddScoped<IUserService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            
            //////var userResp = serviceProvider.GetService<IUserRepository>();
            ////_userService = serviceProvider.GetService<IUserService>();

            //var container = AutofacConfig.RegisterAutoFac();
            //var autoServiceProvider = new AutofacServiceProvider(container);
            //_userRepository = container.Resolve<IUserRepository>();
            //_userService = autoServiceProvider.GetService<IUserService>();
        }


        [TestMethod]
        public void Add()
        {
            var user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "Karroy",
                UserName = "Karroy"
            };
            var i = _userService.Add(user);


            List<UserInfo> users = new List<UserInfo>();
            users.Add(user);
            user = new UserInfo()
            {
                Id = GuidTool.GetGuid(),
                RealName = "jay",
                UserName = "jay"
            };
            users.Add(user);
            i = _userService.Add(users);

        }
    }
}
