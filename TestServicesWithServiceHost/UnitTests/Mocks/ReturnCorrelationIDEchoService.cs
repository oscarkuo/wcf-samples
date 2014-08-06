using Services;
using Behaviours;
using System;

namespace UnitTests.Mocks
{
    class ReturnCorrelationIDEchoService : IEcho
    {
        IInteractionState _state;

        public ReturnCorrelationIDEchoService(IInteractionState state)
        {
            _state = state;
        }

        public string Echo(string value)
        {
            var cid = (Guid)_state[Constants.InternalCorrelationIdentifierKey];
            return cid.ToString();
        }
    }
}
