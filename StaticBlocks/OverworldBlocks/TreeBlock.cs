using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class TreeBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeBlockSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public TreeBlock(Vector2 location)
        {
            Location = location;
            
        }
        public TreeBlock(Vector2 location, int type)
        {
            Location = location;
            SetSprite(type);
        }
        public void SetSprite(int type)
        {
            switch(type)
            {
                case 0:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeBlockSprite();
                    break;
                case 1:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeTopLeftSprite();
                    break;
                case 2:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeTopSprite();
                    break;
                case 3:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeTopRightSprite();
                    break;
                case 4:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeBottomLeftSprite();
                    break;
                case 5:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeBottomSprite();
                    break;
                case 6:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateTreeBottomRightSprite();
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
