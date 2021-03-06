using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class RodWaveLeftSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;

        public RodWaveLeftSprite(Texture2D spriteSheet)
        {
            sourceRectangle = new Rectangle(60, 160, 16, 16);
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
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
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}