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
    public class NameSelectSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames = 1;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;

        public NameSelectSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            currentFrame = 0;
            sourceRectangle = new Rectangle(262, 637, 216, 86);
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
        }

        public void Update(GameTime gameTime)
        {            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
        }
    }
}