﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Project
{
    public class BombSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle = new Rectangle(364, 225, 8, 15);
        Vector2 spriteOffset;

        public BombSprite(Texture2D spriteSheet)
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
            spriteOffset.X = -4;
            spriteOffset.Y = -7;
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}