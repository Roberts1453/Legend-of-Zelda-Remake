using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class UseEquipedCommand : ICommand
    {
        private Game1 game;
        public UseEquipedCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.link.UseEquiped();
        }
    }
}
