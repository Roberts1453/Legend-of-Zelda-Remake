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
    public class FireSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;


        public FireSprite(Texture2D spriteSheet)
        {
            sourceRectangle = new Rectangle(300, 0, 4, 7);
            currentFrame = 0;
            totalFrames = 2;
            _spriteSheet = spriteSheet;
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
                    sourceRectangle = new Rectangle(300, 0, 16, 16);
                    break;
                case 1:
                    sourceRectangle = new Rectangle(300, 30, 16, 16);
                    break;
            }
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location,sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}