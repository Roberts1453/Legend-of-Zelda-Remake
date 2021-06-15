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
    public class SmokeSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames = 2;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;
        Vector2 spriteOffset;

        public SmokeSprite(Texture2D spriteSheet)
        {
            currentFrame = 0;
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            Rectangle edges = sourceRectangle;
            edges.X = (int) -spriteOffset.X;
            edges.Y = (int) -spriteOffset.Y;
            return edges;
        }

        public void Update(GameTime gameTime)
        {
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
                sourceRectangle = new Rectangle(357, 0, 36, 43);
            else if (currentFrame == 1)
                sourceRectangle = new Rectangle(395, 0, 36, 43);

            spriteOffset.X = -(sourceRectangle.Width / 2);
            spriteOffset.Y = -(sourceRectangle.Height / 2);

            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White);
        }
    }
}