using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnitTestProject1
{
    public class AutofacConfig
    {
        protected static IContainer Container
        {
            get;
            set;
        }

        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();

            Container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}
