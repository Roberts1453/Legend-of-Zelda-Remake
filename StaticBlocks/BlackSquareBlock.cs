using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class BlackSquareBlock : ICrossableBlock , IStaticBlock

    {
        public Vector2 Location;
        ISprite blackBlockSprite = DungeonSpriteFactory.Instance.CreateBlackSquareBlockSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public BlackSquareBlock(Vector2 location)
        {
            Location = location;
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void Pass()
        {
            collide = false;
        }
        public void CollideAction(Link link)
        {
            link.UseLadder(this);
        }
        public bool Collide
        {
            get
            {
                return collide;
            }
            set
            {
                collide = value;
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
