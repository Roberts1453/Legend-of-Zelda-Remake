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
    public class StandingInPlaceRoboSprite
    {
        private int currentFrame;
        private int totalFrames;

        public void Update(GameTime gameTime)
        {
            totalFrames = 0;
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, new Rectangle(390,200,24,24), new Rectangle(0, 0, 16, 16), Color.White);
            spriteBatch.End();
        }
    }
}
