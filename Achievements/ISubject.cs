namespace StarterGame.Achievements
{
    public interface ISubject
    {
        void RegisterObserver(Achievement observer);
        void UnregisterObserver(Achievement observer);
        void Notify(string eventType, object data);
    }
}