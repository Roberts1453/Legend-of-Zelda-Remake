using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class RightStatue2Block : IStaticBlock

    {
        public Vector2 Location;
        ISprite rightStatue2BlockSprite = DungeonSpriteFactory.Instance.CreateRightStatueSprite();
        Rectangle edges;
        bool collide = true;

        public RightStatue2Block(Vector2 location)
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
                edges = rightStatue2BlockSprite.Edges();
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
            edges = rightStatue2BlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            rightStatue2BlockSprite.Draw(spriteBatch, Location);

        }
    }
}
