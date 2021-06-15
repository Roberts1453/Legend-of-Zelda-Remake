using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class SelectRightCommand : ICommand
    {
        Game1 game;
        public SelectRightCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
                game.currentMenu.SelectRight();
            
        }
    }
}

