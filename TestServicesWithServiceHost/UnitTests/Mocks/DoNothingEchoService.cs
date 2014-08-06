namespace UnitTests.Mocks
{
    class DoNothingEchoService : Services.IEcho
    {
        public string Echo(string value)
        {
            return value;
        }
    }
}
