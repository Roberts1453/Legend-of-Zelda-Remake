
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class LadderCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite ladderSprite = ItemSpriteFactory.Instance.CreateLadderSprite();
        public bool active = true;
        private Rectangle edges;

        private LadderCollectible()
        {
        }
        public LadderCollectible(Vector2 location)
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
                edges = ladderSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = ladderSprite.Edges();
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
            link.CollectLadder();
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
                ladderSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
