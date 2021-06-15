using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class QuitGameCommand : ICommand
    {
        Game1 game;
        public QuitGameCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.Quit();
        }
    }
}
