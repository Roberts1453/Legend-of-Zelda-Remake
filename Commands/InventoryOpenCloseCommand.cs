using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class InventoryOpenCloseCommand : ICommand
    {
        Game1 game;
        public InventoryOpenCloseCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            if (game.gameState.Peek().GetType() == typeof(TextboxState))
                game.textbox.NextText();
            else
            {
                game.link.BeIdle();
                game.UI.OpenCloseInventory();
            }
            
        }
    }
}

