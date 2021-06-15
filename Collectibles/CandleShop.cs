
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class CandleShop : ICollectibleItem
    {
        int price = 60;
        public Vector2 Location;
        ISprite bombSprite = ItemSpriteFactory.Instance.CreateCandleSprite();
        ISprite priceSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(60 / 10);
        ISprite priceSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite(60 % 10);
        public Vector2 priceOffset1 = new Vector2(-8, 16);
        public Vector2 priceOffset2 = new Vector2(0, 16);

        public bool active = true;
        private Rectangle edges;

        private CandleShop()
        {
        }
        public CandleShop(Vector2 location)
	    {
            Location = location;
            priceSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(price / 10);
            priceSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite(price % 10);
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
            if (link.rupees >= price)
            {
                link.rupees -= price;
                SoundManager.Instance.PlaySoundEffect("Get_Item");
                link.CollectCandle();
                active = false;
            }
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
                priceSprite1.Draw(spriteBatch, Location + priceOffset1);
                priceSprite2.Draw(spriteBatch, Location + priceOffset2);
            }
        }
    }
}
