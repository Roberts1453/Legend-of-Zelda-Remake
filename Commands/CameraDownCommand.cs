using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class CameraDownCommand : ICommand
    {
        Game1 game;
        public CameraDownCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.cam.Location.Y += 10;
        }
    }
}

