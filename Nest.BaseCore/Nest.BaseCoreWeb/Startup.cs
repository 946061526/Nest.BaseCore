using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest.BaseCore.Aop;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Log;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nest.BaseCoreWeb
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//统一异常处理
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            ////注入逻辑层服务
            //services.AddScoped<IUserService, UserService>()
            //    .AddScoped<IRoleService, RoleService>();

            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
