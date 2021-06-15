using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class WalkingUpCommand : ICommand
    {
        private Game1 game;
        public WalkingUpCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.link.ChangeDirection(Directions.Up);
            game.link.BeMoving();
        }
    }
}
