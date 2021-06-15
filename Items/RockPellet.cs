
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class RockPellet : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
		Vector2 movement;
        public Directions direction;
        ISprite rockPelletSprite = ItemSpriteFactory.Instance.CreateRockPelletSprite();
        bool active = true;
        int damage = 1;
        float speed = 1.5f;
        private Rectangle edges;

        public RockPellet(Vector2 location, Directions direction)
        {
            Location = location;
            startLocation = Location;
            this.direction = direction;
            edges = rockPelletSprite.Edges();
			FindMovement();
            SoundManager.Instance.PlaySoundEffect("Arrow_Boomerang");
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = rockPelletSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
		public void FindMovement()
		{
			switch (direction)
			{
				case Directions.Up:
					movement = new Vector2(0, -1);
					break;
				case Directions.Down:
					movement = new Vector2(0, 1);
					break;
				case Directions.Left:
					movement = new Vector2(-1, 0);
					break;
				case Directions.Right:
					movement = new Vector2(1, 0);
					break;
			}
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
            active = false;
        }
        public void Collide(ICharacter enemy)
        {
			Link link = (Link) enemy;
			if (direction.CompareTo(link.direction) != 2)
				enemy.TakeDamage(damage);
            else
                SoundManager.Instance.PlaySoundEffect("Shield");

            active = false;
        }

        public void Update(GameTime gameTime)
        {
            Location += movement * speed;
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rockPelletSprite.Draw(spriteBatch, Location);
        }
    }
}
