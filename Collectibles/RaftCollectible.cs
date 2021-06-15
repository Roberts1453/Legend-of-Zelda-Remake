
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class RaftCollectible : ICollectibleItem
    {
        public Vector2 Location;
        ISprite raftSprite = ItemSpriteFactory.Instance.CreateRaftSprite();
        public bool active = true;
        private Rectangle edges;

        private RaftCollectible()
        {
        }
        public RaftCollectible(Vector2 location)
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
                edges = raftSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = raftSprite.Edges();
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
            link.CollectRaft();
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
                raftSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
