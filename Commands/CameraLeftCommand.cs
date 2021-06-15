using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class CameraLeftCommand : ICommand
    {
        Game1 game;
        public CameraLeftCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.cam.Location.X -= 10;
        }
    }
}

