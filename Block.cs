
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    
    public class Block
    {
        public Vector2 Location;
        ISprite blockSprite = DungeonSpriteFactory.Instance.CreateBlockSprite();
        Directions pushedDirection = Directions.Down;
        bool movable;
        bool pushed = false;
        bool moving = false;
        int pushedDistance;
        int moveDistance = 16;

        public Block(Vector2 location)
	    {
            Location = location;
            pushedDistance = 0;
            movable = false;
        }

        public void Push(Directions dir)
        {
            if (!pushed && movable)
            {
                moving = true;
                pushedDirection = dir;
            }
                
        }

        public void Update(GameTime gameTime)
        {
            if (moving)
            {
                switch (pushedDirection)
                {
                    case Directions.Down:
                        Location.Y++;
                        break;
                    case Directions.Left:
                        Location.X--;
                        break;
                    case Directions.Up:
                        Location.Y--;
                        break;
                    case Directions.Right:
                        Location.X++;
                        break;
                }

                if (pushedDistance >= moveDistance)
                {
                    moving = false;
                    pushed = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            blockSprite.Draw(spriteBatch, Location);            
        }
    }
}
