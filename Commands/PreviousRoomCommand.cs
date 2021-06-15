using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class PreviousRoomCommand : ICommand
    {
        Game1 game;
        public PreviousRoomCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.cam.DecrementRoom();
            game.link.location = game.cam.Location + new Vector2(128, 192);
        }
    }
}
