using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class SelectLeftCommand : ICommand
    {
        Game1 game;
        public SelectLeftCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
                game.currentMenu.SelectLeft();
            
        }
    }
}

