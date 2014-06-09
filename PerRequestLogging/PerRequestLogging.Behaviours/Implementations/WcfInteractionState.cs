using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PerRequestLogging.Behaviours.Implementations
{
    public class WcfInteractionState : IInteractionState
    {
        private static IDictionary<string, object> State
        {
            get
            {
                var instance = OperationContext.Current.Extensions.Find<InteractionStateExtension>();
                if (instance == null)
                {
                    instance = new InteractionStateExtension();

                    // adds an instance to the thread specific instance of the operation context
                    OperationContext.Current.Extensions.Add(instance);
                }

                return instance.State;
            }
        }

        public T Get<T>(string key)
        {
            return (T)State[key];
        }

        public void Set(string key, object value)
        {
            State[key] = value;
        }
    }
}
