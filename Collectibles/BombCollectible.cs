
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class BombCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite bombSprite = ItemSpriteFactory.Instance.CreateBombSprite();
        public bool active = true;
        private Rectangle edges;

        private BombCollectible()
        {
        }
        public BombCollectible(Vector2 location)
	    {
            Location = location;
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = bombSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = bombSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            return edges;
        }

        public bool DestroyedState()
        {
            return active;
        }

        public void Collect(Link link)
        {
            SoundManager.Instance.PlaySoundEffect("Get_Item");
            link.CollectBomb();
            active = false;
        }

        public void Update(GameTime gameTime)
        {
            if (active)
            {
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                bombSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
