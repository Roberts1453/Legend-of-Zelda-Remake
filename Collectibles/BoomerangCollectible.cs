
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    [Serializable]
    public class BoomerangCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite boomerangSprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
        public bool active = true;
        private Rectangle edges;
        private BoomerangCollectible()
        {
        }
        public BoomerangCollectible(Vector2 location)
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
                edges = boomerangSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = boomerangSprite.Edges();
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
            link.CollectBoomerang();
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
                boomerangSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
