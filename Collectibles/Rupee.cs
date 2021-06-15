
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    [Serializable]
    public class Rupee : ICollectibleItem
    {
        public Vector2 Location;
        ISprite rupeeSprite = ItemSpriteFactory.Instance.CreateOrangeRupeeSprite();
        Rectangle edges;
        public bool active = true;
        int value;

        private Rupee()
        {
        }
        public Rupee(Vector2 location)
	    {
            Location = location;
            value = 1;
        }
		public Rupee(Vector2 location, int value)
	    {
            Location = location;
            this.value = 5;
			rupeeSprite = ItemSpriteFactory.Instance.CreateBlueRupeeSprite();
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = rupeeSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = rupeeSprite.Edges();
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
            link.AddRupee(value);
            SoundManager.Instance.PlaySoundEffect("Get_Rupee");
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
                rupeeSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
