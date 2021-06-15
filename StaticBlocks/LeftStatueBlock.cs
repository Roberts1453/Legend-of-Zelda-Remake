using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class LeftStatueBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite leftStatueBlockSprite = DungeonSpriteFactory.Instance.CreateBlueLeftStatueSprite();
        Rectangle edges;
        bool collide = true;

        public LeftStatueBlock(Vector2 location)
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
                edges = leftStatueBlockSprite.Edges();
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
            edges = leftStatueBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            leftStatueBlockSprite.Draw(spriteBatch, Location);

        }
    }
}
