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
    public class BrickSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private int size;

        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;


        public BrickSprite(Texture2D spriteSheet, int size)
        {
            _spriteSheet = spriteSheet;
            this.size = size;
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
            if (size == 1)
                sourceRectangle = new Rectangle(533, 1089, 16, 16);
            else if(size == 2)
                sourceRectangle = new Rectangle(533, 1089, 32, 16);
            else if(size == 3)
                sourceRectangle = new Rectangle(533, 1089, 64, 16);

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}