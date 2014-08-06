using Behaviours;
using Ninject.Modules;

namespace UnitTests
{
    class UnitTestNinjectModule<L, S> : NinjectModule
        where L : IInteractionLog
        where S : IInteractionState
    {
        public override void Load()
        {
            Bind<IInteractionLog>().To<L>();
            Bind<IInteractionState>().To<S>();
        }
    }
}
