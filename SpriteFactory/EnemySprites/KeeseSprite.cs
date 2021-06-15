using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{
    public class KeeseSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private Texture2D _spriteSheet;
        private int millisecondsSinceLastUpdate = 0;
        private int millisecondsPerUpdate = 120;
		bool isRed = false;

        Rectangle sourceRectangle;
        Vector2 spriteOffset;

        public KeeseSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public Rectangle Edges()
        {
            Rectangle edges = sourceRectangle;
            return edges;
        }
		public void MakeRed() {
			isRed = true;
		}
        public void ChangeSpeed(float speed)
        {
            //change millisecondsPerUpdate based on speed of Keese;
			millisecondsPerUpdate = (int)(120 / speed);
        }

        public void Update(GameTime gameTime)
        {
            totalFrames = 2;

            millisecondsSinceLastUpdate = millisecondsSinceLastUpdate + gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastUpdate > millisecondsPerUpdate)
            {
                millisecondsSinceLastUpdate -=  millisecondsPerUpdate;

                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
			if (isRed)
			{
				if (currentFrame == 0)
            	{
                	sourceRectangle = new Rectangle(452, 84, 16, 8);
            	}
            	else
            	{
                sourceRectangle = new Rectangle(476, 84, 16, 10);
            	}
			}
			else 
			{
				if (currentFrame == 0)
            	{
                	sourceRectangle = new Rectangle(184, 12, 16, 8);
            	}
            	else
            	{
                	sourceRectangle = new Rectangle(208, 12, 16, 10);
            	}
			}
			
			

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}