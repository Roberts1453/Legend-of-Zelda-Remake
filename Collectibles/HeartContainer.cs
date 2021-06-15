
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    [Serializable]
    public class HeartContainer : ICollectibleItem
    {
        public Vector2 Location;
        ISprite heartSprite = ItemSpriteFactory.Instance.CreateHeartContainerSprite();
        public bool active = true;
        private Rectangle edges;

        private HeartContainer()
        {
        }
        public HeartContainer(Vector2 location)
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
                edges = heartSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            Rectangle edges = heartSprite.Edges();
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
            link.CollectHeartContainer();
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
                heartSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
