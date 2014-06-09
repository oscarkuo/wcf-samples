using Ninject.Activation;
using Ninject.Modules;
using PerRequestLogging.Behaviours;
using PerRequestLogging.Behaviours.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerRequestLogging.Services
{
    public class MyNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInteractionLog>().To<FileInteractionLog>();
            Bind<IInteractionState>().To<WcfInteractionState>();            
        }
    }
}