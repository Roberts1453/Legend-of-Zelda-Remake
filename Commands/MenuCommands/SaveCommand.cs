using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public class SaveCommand : ICommand
    {
        Game1 game;
        public SaveCommand(Game1 gameState)
        {
            game = gameState;
        }

        public void Execute()
        {
            SaveManager.Instance.Save(SaveManager.Instance.fileList[game.fileNum]);

        }
    }
}

