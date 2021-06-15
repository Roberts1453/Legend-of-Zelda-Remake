using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class RedKeeseSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;
        Vector2 spriteOffset;

        public RedKeeseSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            Rectangle edges = sourceRectangle;
            edges.Offset(-spriteOffset.X, -spriteOffset.Y);
            return edges;
        }
        public void ChangeSpeed()
        {
            //change millisecondsPerUpdate based on speed of Keese;
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 2;

            millisecondsSinceLastUpdate = millisecondsSinceLastUpdate + gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastUpdate > millisecondsPerUpdate)
            {
                millisecondsSinceLastUpdate -= millisecondsPerUpdate;

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {


            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(452, 84, 16, 8);
            }
            else
            {
                sourceRectangle = new Rectangle(476, 84, 16, 8);
            }
            spriteOffset.X = -(sourceRectangle.Width / 2);
            spriteOffset.Y = -(sourceRectangle.Height / 2);

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}