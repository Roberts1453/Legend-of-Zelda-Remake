
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Sprint0Project
{
    [XmlInclude(typeof(Compass))]
    [Serializable]
    public class Compass : ICollectibleItem
    {
        public Vector2 Location;
        ISprite compassSprite = ItemSpriteFactory.Instance.CreateCompassSprite();
        public bool active = true;
        private Rectangle edges;
        int dungeonNumber;
        private Compass()
        {
        }
        public Compass(Vector2 location, int dungeonNumber)
	    {
            Location = location;
            edges = compassSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
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
                edges = compassSprite.Edges();                
            }
        }

        public Rectangle GetEdges()
        {
            edges = compassSprite.Edges();
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
            link.CollectCompass(dungeonNumber);
            SoundManager.Instance.PlaySoundEffect("Get_Item");
            active = false;
        }

        public void Update(GameTime gameTime)
        {            
            if (active)
            {
                compassSprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                compassSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
