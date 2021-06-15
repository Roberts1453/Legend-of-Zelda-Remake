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
    public class LinkAttackingUpSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;

        Rectangle sourceRectangle;
        Vector2 spriteOffset;

        public LinkAttackingUpSprite(Texture2D spriteSheet)
        {
            sourceRectangle = new Rectangle(60, 84, 15, 27);
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
            spriteOffset.X = -(sourceRectangle.Width / 2)-7;
            spriteOffset.Y = -(sourceRectangle.Height / 2)-19;
            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White); ;
            //spriteBatch.End();
        }
    }
}