using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class RockBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite rockBlockSprite = OverworldSpriteFactory.Instance.CreateSingleRockSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public RockBlock(Vector2 location)
        {
            Location = location;
            
        }
        public RockBlock(Vector2 location, int type)
        {
            Location = location;
            SetSprite(type);
        }
        public void SetSprite(int type)
        {
            switch(type)
            {
                case 0:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateSingleRockSprite();
                    break;
                case 1:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockTopLeftSprite();
                    break;
                case 2:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockTopSprite();
                    break;
                case 3:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockTopRightSprite();
                    break;
                case 4:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockBottomLeftSprite();
                    break;
                case 5:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockBottomSprite();
                    break;
                case 6:
                    rockBlockSprite = OverworldSpriteFactory.Instance.CreateRockBottomRightSprite();
                    break;
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
                edges = rockBlockSprite.Edges();
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
            edges = rockBlockSprite.Edges();
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
                rockBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
