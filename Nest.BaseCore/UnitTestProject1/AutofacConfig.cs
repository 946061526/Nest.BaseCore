using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnitTestProject1
{
    public class AutofacConfig
    {
        public static IContainer RegisterAutoFac()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();

            var container = builder.Build();
            return container;

        }
    }
}
