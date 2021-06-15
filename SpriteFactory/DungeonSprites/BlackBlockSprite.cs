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
    public class BlackBlockSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;
        int type = 1;

        public BlackBlockSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public BlackBlockSprite(Texture2D spriteSheet, int type)
        {
            _spriteSheet = spriteSheet;
            this.type = type;
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
            if (type == 2)
                sourceRectangle = new Rectangle(549, 1056, 16, 16);
            else
                sourceRectangle = new Rectangle(549, 1056, 16, 24);

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}