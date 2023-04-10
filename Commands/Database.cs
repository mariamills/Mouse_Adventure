namespace StarterGame.Commands
{
    public class Database
    {
        private static Database _instance = null;
        
        private Database() {}
        
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
    }
}