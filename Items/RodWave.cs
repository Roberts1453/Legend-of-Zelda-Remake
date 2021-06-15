
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class RodWave : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
        Vector2 movement;
        public Directions direction;
        ISprite rodWaveSprite = ItemSpriteFactory.Instance.CreateRodWaveDownSprite();
        bool active = true;
        int damage = 4;
        float speed = 1.8f;
        private Rectangle edges;

        public RodWave(Vector2 location, Directions direction)
        {
            Location = location;
            startLocation = Location;
            this.direction = direction;
            edges = rodWaveSprite.Edges();
            FindMovement();
            SoundManager.Instance.PlaySoundEffect("Magical_Rod");
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = rodWaveSprite.Edges();
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
                    rodWaveSprite = ItemSpriteFactory.Instance.CreateRodWaveUpSprite();
                    break;
                case Directions.Down:
                    movement = new Vector2(0, 1);
                    rodWaveSprite = ItemSpriteFactory.Instance.CreateRodWaveDownSprite();
                    break;
                case Directions.Left:
                    movement = new Vector2(-1, 0);
                    rodWaveSprite = ItemSpriteFactory.Instance.CreateRodWaveLeftSprite();
                    break;
                case Directions.Right:
                    movement = new Vector2(1, 0);
                    rodWaveSprite = ItemSpriteFactory.Instance.CreateRodWaveRightSprite();
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
            enemy.TakeDamage(damage);

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
            rodWaveSprite.Draw(spriteBatch, Location);
        }
    }
}
