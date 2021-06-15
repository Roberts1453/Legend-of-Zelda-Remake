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
    public class LinkIdleLeftSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;

        Rectangle sourceRectangle;

        public LinkIdleLeftSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            sourceRectangle = new Rectangle(30, 0, 14, 15);
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
            spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}