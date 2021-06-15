
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Sprint0Project
{
    [XmlInclude(typeof(TriforcePiece))]
    [Serializable]
    public class TriforcePiece : ICollectibleItem
    {
        public Vector2 Location;
        ISprite triforcePieceSprite = ItemSpriteFactory.Instance.CreateTriforcePieceSprite();
        public bool active = true;
        private Rectangle edges;
        int dungeonNumber;
        private TriforcePiece()
        {
        }
        public TriforcePiece(Vector2 location, int dungeonNumber)
	    {
            Location = location;
            edges = triforcePieceSprite.Edges();
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
                edges = triforcePieceSprite.Edges();                
            }
        }

        public Rectangle GetEdges()
        {
            edges = triforcePieceSprite.Edges();
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
            link.CollectTriforce(dungeonNumber);
            SoundManager.Instance.PlaySoundEffect("Get_Item");
            active = false;
        }

        public void Update(GameTime gameTime)
        {            
            if (active)
            {
                triforcePieceSprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                triforcePieceSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
