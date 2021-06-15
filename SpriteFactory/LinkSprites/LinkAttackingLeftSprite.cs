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
    public class LinkAttackingLeftSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;

        Rectangle sourceRectangle = new Rectangle(24, 90, 26, 14);
        Vector2 spriteOffset;

        public LinkAttackingLeftSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
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
            spriteOffset.X = -(sourceRectangle.Width / 2)-6;
            spriteOffset.Y = -(sourceRectangle.Height / 2)+1;
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}