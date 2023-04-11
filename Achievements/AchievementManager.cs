using System.Collections.Generic;

namespace StarterGame.Achievements
{
    public class AchievementManager : ISubject
    {
        private readonly List<Achievement> _achievements;
        private static AchievementManager _instance;

        private AchievementManager()
        {
            _achievements = new List<Achievement>();
        }
        
        public static AchievementManager Instance
        {
            get
            {
                // Lazy instantiation
                if (_instance == null)
                {
                    _instance = new AchievementManager();
                }
                return _instance;
            }
        }

        public void RegisterObserver(Achievement achievement)
        {
            _achievements.Add(achievement);
        }

        public void UnregisterObserver(Achievement achievement)
        {
            _achievements.Remove(achievement);
        }

        public void Notify(string eventType, object data)
        {
            foreach (var observer in _achievements)
                observer.Update(eventType, data);
        }
    }
}