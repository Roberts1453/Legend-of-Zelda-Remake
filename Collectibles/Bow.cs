
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class Bow : ICollectibleItem
    {
        public Vector2 Location;
        ISprite bowSprite = ItemSpriteFactory.Instance.CreateBowSprite();
        public bool active = true;
        private Rectangle edges;

        private Bow()
        {
        }
        public Bow(Vector2 location)
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
                edges = bowSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = bowSprite.Edges();
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
            link.CollectBow();
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
                bowSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
