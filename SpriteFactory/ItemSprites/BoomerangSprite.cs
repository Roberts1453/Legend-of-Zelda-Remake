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
    public class BoomerangSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;


        public BoomerangSprite(Texture2D spriteSheet)
        {
            sourceRectangle = new Rectangle(390, 244, 4, 7);
            currentFrame = 2;
            totalFrames = 8;
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
                    sourceRectangle = new Rectangle(394, 238, 8, 5);
                    break;
                case 1:
                    sourceRectangle = new Rectangle(388, 269, 8, 8);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(390, 244, 5, 8);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(388, 279, 8, 8);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(394, 253, 8, 5);
                    break;
                case 5:
                    sourceRectangle = new Rectangle(398, 279, 8, 8);
                    break;
                case 6:
                    sourceRectangle = new Rectangle(401, 244, 5, 8);
                    break;
                case 7:
                    sourceRectangle = new Rectangle(398, 269, 8, 8);
                    break;
            }
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location,sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}