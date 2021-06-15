using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class SingleTreeBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite treeBlockSprite = OverworldSpriteFactory.Instance.CreateSingleTreeGreenSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = true;

        public SingleTreeBlock(Vector2 location)
        {
            Location = location;
            
        }
        public SingleTreeBlock(Vector2 location, int type)
        {
            Location = location;
            SetSprite(type);
        }
		
        public void SetSprite(int type)
        {
            switch(type)
            {
                case 0:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateSingleTreeBrownSprite();
                    break;
                case 1:
                    treeBlockSprite = OverworldSpriteFactory.Instance.CreateSingleTreeGreenSprite();
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
