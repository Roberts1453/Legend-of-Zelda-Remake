using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class LandBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite landBlockSprite = OverworldSpriteFactory.Instance.CreateLandSprite();
        Rectangle edges;
        bool collide = false;

        public LandBlock(Vector2 location)
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
                edges = landBlockSprite.Edges();
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
            edges = landBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            landBlockSprite.Draw(spriteBatch, Location);

        }
    }
}
