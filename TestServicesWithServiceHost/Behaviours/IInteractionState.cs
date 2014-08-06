namespace Behaviours
{
    public interface IInteractionState
    {
        object this[string key] { get; set; }
    }
}
