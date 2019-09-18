using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Exceptionless;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest.BaseCore.Aop;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Log;
using Nest.BaseCore.Repository;
using Nest.BaseCore.Service;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nest.BaseCoreApi
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
            #region Swagger

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Nest.BaseCore.Web",
                    Description = "Nest",
                });
                c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            var connection = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilterAttribute>();//统一异常处理
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            ////注入逻辑层服务
            //services.AddScoped<IUserService, UserService>()
            //    .AddScoped<IRoleService, RoleService>();

            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nest.BaseCore.Web");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);
        }

        /// <summary>
        /// 请求头加Token参数
        /// </summary>
        public class AddAuthTokenHeaderParameter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
                var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

                //先判断是否是匿名访问,
                var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor != null)
                {
                    var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                    bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                    //非匿名的方法,链接中添加accesstoken值
                    if (!isAnonymous)
                    {
                        operation.Parameters.Add(new NonBodyParameter()
                        {
                            Name = "token",
                            In = "header",//query header body path formData
                            Type = "string",
                            Description = "用于登录验证，没有可不传。后台根据需要做验证",
                            Default = "token",
                            Required = true //是否必选
                        });
                    }
                }
            }
        }
    }
}
