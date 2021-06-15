using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class DungeonTreeBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownTopSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public DungeonTreeBlock(Vector2 location)
        {
            Location = location;
            
        }
        public DungeonTreeBlock(Vector2 location, int type)
        {
            Location = location;
            SetSprite(type);
        }
        public void SetSprite(int type)
        {
            switch(type)
            {
                case 0:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownTopLeftSprite();
                    break;
                case 1:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownTopSprite();
                    break;
                case 2:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownTopRightSprite();
                    break;
                case 3:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownBottomLeftSprite();
                    break;
                case 4:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeBrownBottomRightSprite();
                    break;
					
				case 5:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeGreenTopLeftSprite();
                    break;
                case 6:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeGreenTopSprite();
                    break;
                case 7:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeGreenTopRightSprite();
                    break;
                case 8:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeGreenBottomLeftSprite();
                    break;
                case 9:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateDungeonTreeGreenBottomRightSprite();
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
                edges = treeBlockSprite.Edges();
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
            edges = treeBlockSprite.Edges();
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
                treeBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
