using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class BlueGoriyaLeftSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;

        public BlueGoriyaLeftSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
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


            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(394, 32, 13, 15);
            }
            else
            {
                sourceRectangle = new Rectangle(416, 32, 13, 15);
            }

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}