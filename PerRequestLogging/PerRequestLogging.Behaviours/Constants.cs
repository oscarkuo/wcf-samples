using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerRequestLogging.Behaviours
{
    public static class Constants
    {
        public const string ServiceRequestUriKey = "ServiceRequestUri";
        public const string InteractionLogBufferKey = "InteractionLogBuffer";
        public const string InternalCorrelationIdentifierKey = "InternalCorrelationIdentifier";
    }
}
