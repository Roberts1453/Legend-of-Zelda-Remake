using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class LakeBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterMiddleSprite();
        Rectangle edges;
        bool collide = true;
        public LakeBlock(Vector2 location)
        {
            Location = location;
        }
        public LakeBlock(Vector2 location, int type)
        {
            Location = location;
            SetSprite(type);
        }
        public void SetSprite(int type)
        {
            switch (type)
            {
                case 0:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterMiddleSprite();
                    break;
                case 1:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterTopLeftSprite();
                    break;
                case 2:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterTopSprite();
                    break;
                case 3:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterTopRightSprite();
                    break;
                case 4:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterLeftSprite();
                    break;
                case 5:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterRightSprite();
                    break;
                case 6:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterBottomLeftSprite();
                    break;
                case 7:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterBottomSprite();
                    break;
                case 8:
                    lakeBlockSprite = OverworldSpriteFactory.Instance.CreateWaterBottomRightSprite();
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
                edges = lakeBlockSprite.Edges();
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
            edges = lakeBlockSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
                lakeBlockSprite.Draw(spriteBatch, Location);
            
        }
    }
}
