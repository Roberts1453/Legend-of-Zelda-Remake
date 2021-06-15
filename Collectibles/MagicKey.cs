
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class MagicKey : ICollectibleItem
    {
        public Vector2 Location;
        ISprite keySprite = ItemSpriteFactory.Instance.CreateMagicKeySprite();
        public bool active = true;
        private Rectangle edges;
        private MagicKey()
        {
        }
        public MagicKey(Vector2 location)
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
                edges = keySprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = keySprite.Edges();
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
            link.CollectMagicKey();
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
                keySprite.Draw(spriteBatch, Location);
            }
        }
    }
}
