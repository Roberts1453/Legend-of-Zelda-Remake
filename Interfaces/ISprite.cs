using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Project
{
    public interface ISprite
    {
        Rectangle Edges();
        
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Vector2 location);

    }
}
