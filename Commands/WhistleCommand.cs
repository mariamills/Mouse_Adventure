using StarterGame.Achievements;

namespace StarterGame.Commands
{
    public class WhistleCommand : Command
    {
        private AchievementManager _achievementManager;
        private Achievement _bestFriendAchievement = new BestFriendAchievement();
        private bool _whistled;
        public WhistleCommand() : base()
        {
            Name = "whistle";
            _whistled = false;
            _achievementManager = AchievementManager.Instance;
            _achievementManager.RegisterObserver(_bestFriendAchievement);
        }
        public override bool Execute(Player.Player player)
        {
            if (_bestFriendAchievement.Unlocked)
            {
                if (!_whistled)
                {
                    player.Whistle();
                    _whistled = true;
                }
                else
                {
                    player.ErrorMessage("You've already whistled for help once.");
                }
            }
            else
            {
                player.ErrorMessage("You whistle, but no one comes.");
            }
            return false;
        }
    }
}