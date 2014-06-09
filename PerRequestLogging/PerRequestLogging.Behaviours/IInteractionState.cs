namespace PerRequestLogging.Behaviours
{
    public interface IInteractionState
    {
        T Get<T>(string key);
        void Set(string key, object value);
    }
}
