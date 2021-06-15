
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Candle : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
		Vector2 movement;
        public Directions direction;
        ISprite candleSprite = ItemSpriteFactory.Instance.CreateFireSprite();
        bool active = true;
        int damage = 2;
        float speed = 0.6f;
        private Rectangle edges;
        int tileDistance = 16;
        float moveDistance = 0;
        int timer = 60;

        public Candle(Vector2 location, Directions direction)
        {
            Location = location;
            startLocation = Location;
            this.direction = direction;
            edges = candleSprite.Edges();
			FindMovement();
            SoundManager.Instance.PlaySoundEffect("Candle");
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = candleSprite.Edges();
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
        }
        public void Collide(ICharacter enemy)
        {
            enemy.TakeDamage(damage);
        }

        public void Update(GameTime gameTime)
        {
            Location += movement * speed;
            moveDistance += speed;
            if (moveDistance >= tileDistance)
                speed = 0;

            timer--;
            if (timer <= 0)
                active = false;

            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            candleSprite.Draw(spriteBatch, Location);
        }
    }
}
