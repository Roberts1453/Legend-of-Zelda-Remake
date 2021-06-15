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
    public class LinkMovingDownSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        public LinkMovingDownSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 2;

            millisecondsSinceLastUpdate = millisecondsSinceLastUpdate + gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastUpdate > millisecondsPerUpdate)
            {
                millisecondsSinceLastUpdate = millisecondsSinceLastUpdate - millisecondsPerUpdate;

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            Rectangle sourceRectangle;

            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(0, 0, 15, 15);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 30, 15, 15);
            }

            spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}