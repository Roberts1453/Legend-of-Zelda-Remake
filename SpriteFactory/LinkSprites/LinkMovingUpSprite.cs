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
    public class LinkMovingUpSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;
        Vector2 spriteOffset;

        public LinkMovingUpSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public Rectangle Edges()
        {
            return sourceRectangle;
        }
        public Vector2 Offset()
        {
            return spriteOffset;
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
                sourceRectangle = new Rectangle(60, 0, 15, 15);
            }
            else
            {
                sourceRectangle = new Rectangle(60, 30, 15, 15);
            }
            spriteOffset.X = -(sourceRectangle.Width / 2);
            spriteOffset.Y = -(sourceRectangle.Height / 2);



            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location + spriteOffset, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}