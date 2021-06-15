using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class LadderBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite ladderBlockSprite = DungeonSpriteFactory.Instance.CreateLadderSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = false;
        int size;

        public LadderBlock(Vector2 location, int size)
        {
            Location = location;
            if (size == 1)
                ladderBlockSprite = DungeonSpriteFactory.Instance.CreateHalfLadderSprite();
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
                edges = ladderBlockSprite.Edges();
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
            edges = ladderBlockSprite.Edges();
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
                ladderBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
