using System;
using System.Collections.Generic;
using System.ServiceModel;

using PerRequestLogging.ValueObjects;

namespace PerRequestLogging.Behaviours
{
    public class InteractionStateExtension : IExtension<OperationContext>
    {
        public InteractionStateExtension()
        {
            State = new Dictionary<string, object>();
        }

        public IDictionary<string, object> State { get; private set; }

        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
