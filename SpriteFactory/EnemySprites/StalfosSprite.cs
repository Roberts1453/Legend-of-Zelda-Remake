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
    public class StalfosSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;

        Rectangle sourceRectangle;

        private Color[] _normalData;
        private Color[] _hurtData;

        public StalfosSprite(Texture2D spriteSheet, Color[] normalData, Color[] hurtData)
        {
            _spriteSheet = new Texture2D(spriteSheet.GraphicsDevice, spriteSheet.Width, spriteSheet.Height);
            _normalData = normalData;
            _hurtData = hurtData;
            _spriteSheet.SetData(normalData);
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
        }
        public void Damage(bool damaged)
        {
            if (damaged)
                _spriteSheet.SetData(_hurtData);
            else
                _spriteSheet.SetData(_normalData);
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
                sourceRectangle = new Rectangle(497, 8, 15, 16);
            }
            else
            {
                sourceRectangle = new Rectangle(520, 8, 15, 16);
            }

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}