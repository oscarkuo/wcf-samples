using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PerRequestLogging.Behaviours.Implementations
{
    // This class provides an area for state storage for each interaction session (i.e. from operation
    // invocation to providing response). It is effectively a wrapper around the shared IDictionary 
    // instance stored in OperationContext.Current and therefore each instance of this class shares
    // the same state. See http://elegantcode.com/2009/01/17/abstracting-request-state/ for further
    // information of this class. Further more, this class make it possible to inject the state data
    // with Ninject to other classes.
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
