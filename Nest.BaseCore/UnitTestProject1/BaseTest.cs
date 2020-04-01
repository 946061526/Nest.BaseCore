using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    public class BaseTest
    {
        public BaseTest()
        {
            AutofacConfig.Register();
        }

        public T Resolve<T>()
        {
            return AutofacConfig.Resolve<T>();
        }
    }
}
