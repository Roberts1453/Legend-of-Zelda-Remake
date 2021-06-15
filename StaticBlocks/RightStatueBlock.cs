using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class RightStatueBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite rightStatueBlockSprite = DungeonSpriteFactory.Instance.CreateBlueRightStatueSprite();
        Rectangle edges;
        bool collide = true;

        public RightStatueBlock(Vector2 location)
        {
            Location = location;
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {

        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = rightStatueBlockSprite.Edges();
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

        public void Update(GameTime gameTime)
        {
            edges = rightStatueBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            rightStatueBlockSprite.Draw(spriteBatch, Location);

        }
    }
}
