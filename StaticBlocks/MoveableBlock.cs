using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class MoveableBlock : IStaticBlock

    {
        public Vector2 Location;
        public Vector2 startLocation;
        ISprite moveableBlockSprite = DungeonSpriteFactory.Instance.CreateBlockSprite();
        Rectangle edges;
        Directions pushedDirection = Directions.Down;
        bool movable;
        public bool pushed = false;
        bool moving = false;
        int pushedDistance;
        int moveDistance = 16;
        bool collide = true;

        public MoveableBlock(Vector2 location)
        {
            Location = location;
            startLocation = Location;
            pushedDistance = 0;
            movable = true;
        }
        public bool GetCollide()
        {
            return collide;
        }
        public bool HasBeenPushed()
        {
            return pushed;
        }
        public void CollideAction(Link link)
        {
            if (Math.Abs(link.location.Y - Location.Y) > Math.Abs(link.location.X - Location.X))
            {
                if (link.location.Y < Location.Y)
                    Push(Directions.Down);
                if (link.location.Y > Location.Y)
                    Push(Directions.Up);
            }
            else
            {
                if (link.location.X < Location.X)
                    Push(Directions.Right);
                if (link.location.X > Location.X)
                    Push(Directions.Left);
            }
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = moveableBlockSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            return edges;

        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public void Push(Directions dir)
        {
            if (!pushed && movable)
            {
                SoundManager.Instance.PlaySoundEffect("Secret");
                moving = true;
                pushedDirection = dir;
            }

        }
        public void Reset()
        {
            Location = startLocation;
            pushed = false;
            movable = true;
            pushedDistance = 0;
        }

        public void Update(GameTime gameTime)
        {
            edges = moveableBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

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
                pushedDistance++;

                if (pushedDistance >= moveDistance)
                {
                    moving = false;
                    pushed = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            moveableBlockSprite.Draw(spriteBatch, Location);

        }
    }
}
