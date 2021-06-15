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
    public class WalkingLeftSprite
    {
        private int currentFrame;
        private int totalFrames;
        private int currentDistance = 390;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        public void Update(GameTime gameTime)
        {

            totalFrames = 3;

            millisecondsSinceLastUpdate = millisecondsSinceLastUpdate + gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastUpdate > millisecondsPerUpdate)
            {
                millisecondsSinceLastUpdate = millisecondsSinceLastUpdate - millisecondsPerUpdate;

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }

            currentDistance = currentDistance - 2;
            if (currentDistance <= 0)
                currentDistance = 800;



        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet)
        {
            Rectangle sourceRectangle;

            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(90, 0, 16, 16);
            }
            else if (currentFrame == 1)
            {
                sourceRectangle = new Rectangle(90, 30, 16, 16);
            }
            else
            {
                sourceRectangle = new Rectangle(90, 60, 16, 16);
            }

            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, new Vector2(currentDistance, 200), sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
