using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class SelectUpCommand : ICommand
    {
        Game1 game;
        public SelectUpCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
                game.currentMenu.SelectUp();
            
        }
    }
}

