using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class LeftStatue2Block : IStaticBlock

    {
        public Vector2 Location;
        ISprite leftStatue2BlockSprite = DungeonSpriteFactory.Instance.CreateLeftStatueSprite();
        Rectangle edges;
        bool collide = true;
        public LeftStatue2Block(Vector2 location)
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
                edges = leftStatue2BlockSprite.Edges();
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
            edges = leftStatue2BlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            leftStatue2BlockSprite.Draw(spriteBatch, Location);

        }
    }
}
