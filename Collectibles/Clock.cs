
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class Clock : ICollectibleItem
    {
        public Vector2 Location;
        ISprite clockSprite = ItemSpriteFactory.Instance.CreateClockSprite();
        public bool active = true;
        private Rectangle edges;

        private Clock()
        {
        }
        public Clock(Vector2 location)
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
                edges = clockSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = clockSprite.Edges();
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
            //freeze enemies
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
                clockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
