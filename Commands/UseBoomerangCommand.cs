using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class UseBoomerangCommand : ICommand
    {
        private Game1 game;
        public UseBoomerangCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.link.Equip(Items.Boomerang);
            game.link.UseBoomerang();
        }
    }
}
