
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Ladder : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
		Vector2 movement;
        public Directions direction;
        ISprite ladderSprite = ItemSpriteFactory.Instance.CreateLadderSprite();
        bool active = true;
        int damage = 1;
        float speed = 1.5f;
        private Rectangle edges;
        Link link;
        ICrossableBlock crossingBlock;

        public Ladder(Link link, ICrossableBlock crossingBlock)
        {
            Location = crossingBlock.GetLocation();
            this.link = link;
            this.crossingBlock = crossingBlock;
            crossingBlock.Collide = false;
            edges = ladderSprite.Edges();
            SoundManager.Instance.PlaySoundEffect("Stairs");
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
            return edges;
        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public int GetDamage()
        {
            return damage;
        }
        public bool DestroyedState()
        {
            return active;
        }
        public void BeMoving()
        {
        }
        public void Collide()
        {
        }
        public void Collide(ICharacter enemy)
        {
            	
            active = false;
        }

        public void Update(GameTime gameTime)
        {
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            if (!edges.Intersects(link.edges))
            {
                crossingBlock.Collide = true;
                active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
                ladderSprite.Draw(spriteBatch, Location);
        }
    }
}
