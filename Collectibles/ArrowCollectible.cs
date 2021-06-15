
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    [Serializable]
    public class ArrowCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite arrowSprite = ItemSpriteFactory.Instance.CreateArrowUpSprite();
        public bool active = true;
        private Rectangle edges;
        private ArrowCollectible()
        {
        }
        public ArrowCollectible(Vector2 location)
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
                edges = arrowSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = arrowSprite.Edges();
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
            link.CollectArrow();
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
                arrowSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
