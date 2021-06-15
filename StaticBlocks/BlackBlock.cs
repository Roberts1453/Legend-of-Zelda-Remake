using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class BlackBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite blackBlockSprite = DungeonSpriteFactory.Instance.CreateBlackBlockSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public BlackBlock(Vector2 location)
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
                edges = blackBlockSprite.Edges();
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
            edges = blackBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            if (active)
            {
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                blackBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
