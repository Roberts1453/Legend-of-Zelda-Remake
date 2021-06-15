using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class PreviousDungeonCommand : ICommand
    {
        Game1 game;
        public PreviousDungeonCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.ChangeDungeon(game.currentDungeon - 1);
            game.link.location = game.cam.Location + new Vector2(128, 192);
        }
    }
}
