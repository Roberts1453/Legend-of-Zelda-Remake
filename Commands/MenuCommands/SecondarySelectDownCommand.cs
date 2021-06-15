using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class SecondarySelectDownCommand : ICommand
    {
        Game1 game;
        public SecondarySelectDownCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
                game.currentMenu.SecondarySelectDown();
            
        }
    }
}

