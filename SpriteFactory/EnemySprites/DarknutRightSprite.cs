﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class DarknutRightSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;

        public DarknutRightSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 2;

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


            if (currentFrame == 0)
            {
                sourceRectangle = new Rectangle(476, 56, 16, 16);
            }
            else
            {
                sourceRectangle = new Rectangle(500, 56, 16, 16);
            }

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}