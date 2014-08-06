using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Behaviours.Implementation
{
    public class WcfInteractionState : IInteractionState
    {
        private static DictionaryStateExtension State
        {
            get
            {
                var instance = OperationContext.Current.Extensions.Find<DictionaryStateExtension>();
                if (instance == null)
                {
                    instance = new DictionaryStateExtension();

                    // adds an instance to the thread specific instance of the operation context
                    OperationContext.Current.Extensions.Add(instance);
                }

                return instance;
            }
        }

        public object this[string key]
        {
            get { return State[key]; }
            set { State[key] = value; }
        }

        //IExtension methods
        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
