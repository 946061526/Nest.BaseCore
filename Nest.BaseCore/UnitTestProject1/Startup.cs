using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest.BaseCore.Domain;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnitTestProject1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var connection = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));
            
            #region AutoFac 注入仓储、业务逻辑服务

            //批量匹配注入，使用AutoFac提供的容器接管当前项目默认容器
            var builder = new ContainerBuilder();


            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();
            builder.Populate(services);
            var container = builder.Build();
            //ConfigureServices方法由void改为返回IServiceProvider
            return new AutofacServiceProvider(container);

            #endregion
        }
    }
}

