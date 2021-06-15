using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class NextRoomCommand: ICommand
    {
        Game1 game;
        public NextRoomCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.cam.IncrementRoom();
            game.link.location = game.cam.Location + new Vector2(128, 192);
        }
    }
}
