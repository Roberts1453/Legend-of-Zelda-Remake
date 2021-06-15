using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class WaterBlock : ICrossableBlock, IStaticBlock

    {
        public Vector2 Location;
        ISprite waterBlockSprite = DungeonSpriteFactory.Instance.CreateWaterSprite();
        Rectangle edges;
        bool collide = true;
        public WaterBlock(Vector2 location)
        {
            Location = location;
        }
        public bool GetCollide()
        {
            return collide;
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
                edges = waterBlockSprite.Edges();
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
            edges = waterBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
                waterBlockSprite.Draw(spriteBatch, Location);
            
        }
    }
}
