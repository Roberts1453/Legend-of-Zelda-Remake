
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class CandleCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite candleSprite = ItemSpriteFactory.Instance.CreateCandleSprite();
        public bool active = true;
        private Rectangle edges;

        private CandleCollectible()
        {
        }
        public CandleCollectible(Vector2 location)
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
                edges = candleSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = candleSprite.Edges();
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
            link.CollectCandle();
            SoundManager.Instance.PlaySoundEffect("Get_Item");
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
                candleSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
