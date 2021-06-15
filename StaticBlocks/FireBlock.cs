using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class FireBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite fireBlockSprite = ItemSpriteFactory.Instance.CreateFireSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public FireBlock(Vector2 location)
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
                edges = fireBlockSprite.Edges();
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
            edges = fireBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

            fireBlockSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fireBlockSprite.Draw(spriteBatch, Location);
        }
    }
}
