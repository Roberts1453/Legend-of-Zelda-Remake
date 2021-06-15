
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    [Serializable]
    public class Map : ICollectibleItem
    {
        public Vector2 Location;
        ISprite mapSprite = ItemSpriteFactory.Instance.CreateMapSprite();
        public bool active = true;
        private Rectangle edges;
        int dungeonNumber;

        private Map()
        {
        }
        public Map(Vector2 location, int dungeonNumber)
	    {
            Location = location;
            this.dungeonNumber = dungeonNumber;
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = mapSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            edges = mapSprite.Edges();
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
            link.CollectMap(dungeonNumber);
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
                mapSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
