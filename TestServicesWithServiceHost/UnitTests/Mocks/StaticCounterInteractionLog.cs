using Behaviours;

namespace UnitTests.Mocks
{
    //NOTE: this class is not thread safe, don't use it in multi-thread tests
    public class StaticCounterInteractionLog : IInteractionLog
    {
        private static int _errorCount;
        private static int _warningCount;
        private static int _informationCount;
        private static int _messageCount;
        private static int _flushCount;

        static StaticCounterInteractionLog()
        {
            Reset();
        }

        public static void Reset()
        {
            _errorCount = 0;
            _warningCount = 0;
            _informationCount = 0;
            _messageCount = 0;
            _flushCount = 0;
        }

        public static int ErrorCount { get { return _errorCount; } }
        public static int WarningCount { get { return _warningCount; } }
        public static int InformationCount { get { return _informationCount; } }
        public static int MessageCount { get { return _messageCount; } }
        public static int FlushCount { get { return _flushCount; } }

        public void WriteError(string message, System.Exception exception)
        {
            _errorCount++;
        }

        public void WriteWarning(string message)
        {
            _warningCount++;
        }

        public void WriteInformation(string message)
        {
            _informationCount++;
        }

        public void WriteMessage(System.ServiceModel.Channels.Message message)
        {
            _messageCount++;
        }

        public void Flush()
        {
            _flushCount++;
        }
    }
}
