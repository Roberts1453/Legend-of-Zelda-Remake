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
    public class NaviRightSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames = 2;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 240;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;

        public NaviRightSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            currentFrame = 0;
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
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
                sourceRectangle = new Rectangle(32, 0, 16, 16);
            else
                sourceRectangle = new Rectangle(48, 0, 16, 16);

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}