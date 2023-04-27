namespace StarterGame.Commands
{

    /*
     * Spring 2023
     */
    public abstract class Command
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        private string _thirdWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        public string ThirdWord { get { return _thirdWord; } set { _thirdWord = value; } }

        public Command()
        {
            Name = "";
            SecondWord = null;
            ThirdWord = null;
        }

        public bool HasSecondWord()
        {
            return SecondWord != null;
        }
        
        public bool HasThirdWord()
        {
            return ThirdWord != null;
        }

        public abstract bool Execute(Player.Player player);
    }
}
