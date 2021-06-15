using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class DamageLinkCommand : ICommand
    {
        Game1 game;
        public DamageLinkCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            game.link.TakeDamage(1);
        }
    }
}
