
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Fireball : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
        public Vector2 direction;
        private FireballStateMachine stateMachine;
        ISprite fireballSprite = ItemSpriteFactory.Instance.CreateFireballSprite();
        bool active = true;
        int damage = 2;
        float speed = 1.5f;
        private Rectangle edges;
        int timer = 90;

        public Fireball(Vector2 location, Vector2 direction)
        {
            Location = location;
            startLocation = Location;
            this.direction = direction;
            edges = fireballSprite.Edges();
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
                edges = fireballSprite.Edges();
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
            stateMachine.BeMoving();
        }
        public void Collide()
        {
            active = false;
        }
        public void Collide(ICharacter enemy)
        {
            enemy.TakeDamage(damage);
            active = false;
        }

        public class FireballStateMachine
        {
            Fireball fireball;
            Directions direction = Directions.Down;
            private bool FireballMoving = true;

            public FireballStateMachine(Fireball fireball, Directions direction)
            {
                this.fireball = fireball;
                this.direction = direction;
                BeMoving();
            }

            public void BeMoving()
            {
                FireballMoving = true;                
            }

            public void Update(GameTime gameTime)
            {
                fireball.edges.X = (int)fireball.Location.X;
                fireball.edges.Y = (int)fireball.Location.Y;
                // Switch logic based on the current values of direction to determine how to move
                if (FireballMoving)
                {
                    fireball.fireballSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            /*Move down*/
                            fireball.Location.Y += 3;
                            break;
                        case Directions.Left:
                            /*Move left*/
                            fireball.Location.X -= 3;
                            break;
                        case Directions.Up:
                            /*Move up*/
                            fireball.Location.Y -= 3;
                            break;
                        case Directions.Right:
                            /*Move right*/
                            fireball.Location.X += 3;
                            break;

                    }
                }
                if (fireball.Location.X < fireball.startLocation.X - 128 || fireball.Location.X > fireball.startLocation.X + 128 || fireball.Location.Y < fireball.startLocation.Y - 100 || fireball.Location.Y > fireball.startLocation.Y + 100)
                {
                    fireball.active = false;
                }
                

            }
        }
        public void Update(GameTime gameTime)
        {
            //stateMachine.Update(gameTime);
            Location += direction * speed;
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;

            if (Location.X < startLocation.X - 128 || Location.X > startLocation.X + 128 || Location.Y < startLocation.Y - 100 || Location.Y > startLocation.Y + 100)
            {
                active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fireballSprite.Draw(spriteBatch, Location);
        }
    }
}
