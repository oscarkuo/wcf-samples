using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Behaviours.Implementation
{
    class DictionaryStateExtension : IExtension<OperationContext>
    {
        private Dictionary<string, object> _state = new Dictionary<string, object>();

        public object this[string key]
        {
            get { return _state[key]; }
            set { _state[key] = value; }
        }

        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
