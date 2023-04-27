namespace StarterGame.Achievements
{
    public class CheeseTaxAchievement : Achievement
    {
        public CheeseTaxAchievement() : base("Easy Way Out", "You paid the cheese tax to the Mouse Mafia.")
        {
        }
        public override void Update(string eventType, object data)
        {
            var player = (Player.Player) data;
            if (eventType == "PaidCheeseTax" && !Unlocked)
            {
                Achieve(player);
                Unlocked = true;
            }
        }
    }
}