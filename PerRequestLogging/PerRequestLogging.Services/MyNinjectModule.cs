using Ninject.Activation;
using Ninject.Modules;
using PerRequestLogging.Behaviours;
using PerRequestLogging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerRequestLogging.Services
{
    public class MyNinjectModule : NinjectModule
    {
        private static IRequestLog Get(IContext c)
        {
            return RequestLoggingExtension.Current.Log;
        }

        public override void Load()
        {
            Bind<IRequestLog>().To<OperationContextRequestLog>();
        }
    }
}