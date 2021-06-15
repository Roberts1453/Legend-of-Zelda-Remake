using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class StandingInPlaceCommand : ICommand
    {
        private Game1 game;
        public StandingInPlaceCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            //game.roboSprite = new StandingInPlaceRoboSprite();
        }
    }
}
