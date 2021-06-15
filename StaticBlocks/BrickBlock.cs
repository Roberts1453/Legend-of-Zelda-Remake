using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class BrickBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite stoneBlockSprite = DungeonSpriteFactory.Instance.CreateBrickSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public BrickBlock(Vector2 location)
        {
            Location = location;
        }
        public BrickBlock(Vector2 location, int size)
        {
            Location = location;
            if (size== 1)
            {
                stoneBlockSprite = DungeonSpriteFactory.Instance.CreateBrickSprite();
            }
            else if (size == 2)
            {
                stoneBlockSprite = DungeonSpriteFactory.Instance.CreateDoubleBrickSprite();
            }
            else if (size == 3)
            {
                stoneBlockSprite = DungeonSpriteFactory.Instance.CreateLongBrickSprite();
            }
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
                edges = stoneBlockSprite.Edges();
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
            edges = stoneBlockSprite.Edges();
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
                stoneBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
