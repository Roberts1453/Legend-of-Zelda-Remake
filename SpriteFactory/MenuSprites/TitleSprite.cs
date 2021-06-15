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
    public class TitleSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames = 4;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;

        public TitleSprite(Texture2D spriteSheet)
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
            switch (currentFrame)
            {
                case 0:
                    sourceRectangle = new Rectangle(3, 4, 256, 240);
                    break;
                case 1:
                    sourceRectangle = new Rectangle(262, 4, 256, 240);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(3, 247, 256, 240);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(262, 247, 256, 240);
                    break;
            }

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}