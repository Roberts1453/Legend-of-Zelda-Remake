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
    public class LinkIdleDownSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;

        public LinkIdleDownSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 1;
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, new Rectangle(0, 0, 15, 15), Color.White);
            spriteBatch.End();
        }
    }
}