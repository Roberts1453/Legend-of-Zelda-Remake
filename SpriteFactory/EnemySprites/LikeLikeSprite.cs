using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class LikeLikeSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;

        public LikeLikeSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 4;

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


            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(500, 80, 16, 16);
            }
            else if (currentFrame == 1)
            {
                sourceRectangle = new Rectangle(524, 80, 16, 16);
            }
            else if (currentFrame == 2)
            {
                sourceRectangle = new Rectangle(548, 80, 16, 16);
            }
            else
            {
                sourceRectangle = new Rectangle(524, 80, 16, 16);
            }

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}