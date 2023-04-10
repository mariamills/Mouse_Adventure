using System;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            game.Play();
            game.End();
        }
    }
}
