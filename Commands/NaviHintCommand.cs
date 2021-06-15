using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class NaviHintCommand : ICommand
    {
        Game1 game;
        public NaviHintCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            if (game.gameState.Peek().GetType() == typeof(TextboxState))
                game.textbox.NextText();
            else if (game.gameState.Peek().GetType() == typeof(PlayState))
            {
                game.navi.GiveHint();
            }
            
        }
    }
}

