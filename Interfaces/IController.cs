using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0Project
{
    public interface IController
    {
        void Update(Game1 gameState);
        void SetKeyCommands(Game1 gameState);
    }
}
