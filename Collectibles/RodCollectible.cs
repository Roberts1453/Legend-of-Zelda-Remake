
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    [Serializable]
    public class RodCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite rodSprite = ItemSpriteFactory.Instance.CreateRodUpSprite();
        public bool active = true;
        private Rectangle edges;
        private RodCollectible()
        {
        }
        public RodCollectible(Vector2 location)
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
                edges = rodSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = rodSprite.Edges();
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
            link.CollectRod();
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
                rodSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
