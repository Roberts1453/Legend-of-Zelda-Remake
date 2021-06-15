using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Moblin : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 moblinCenter;
        Vector2 moblinSizeOffset = new Vector2(5, 0);
        ISprite moblinSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaDownSprite();
        public IActionState state;
        Directions direction;


        public float speed;
        int damage = 1;
        int health =4;
        int maxHealth = 4;
        int attackTimer = 500;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Moblin(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            moblinCenter = location + moblinSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            direction = Directions.Down;
            speed = 1;
            movement = new Vector2(0, 1);
            state = new WanderState(this);
        }
        public Moblin(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            moblinCenter = location + moblinSizeOffset;
            this.dropItem = dropItem;
            state = new WanderState(this);
        }
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value * 0.5f;
            }
        }
        public Vector2 Movement
        {
            get
            {
                return movement;
            }
            set
            {
                movement = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        public IActionState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = moblinSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Rectangle GetVertRange()
        {
            return verticalRange;
        }
        public Rectangle GetHorizRange()
        {
            return horizontalRange;
        }
        public Vector2 GetLocation()
        {
            return moblinCenter;
        }
        public bool GetLiving()
        {
            return living;
        }
		public void DetectVert(Vector2 target) 
		{
		}
		public void DetectHoriz(Vector2 target) 
		{
		}
        public ICollectibleItem GetDropItem()
        {
            return dropItem;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
        public bool IsDamaged()
        {
            return false;
        }
        public void ChangeDirection()
        {
        }
        public void ChangeDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    moblinSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaUpSprite();
                    this.direction = Directions.Up;
                    break;
                case Directions.Down:
                    moblinSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaDownSprite();
                    this.direction = Directions.Down;
                    break;
                case Directions.Left:
                    moblinSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaLeftSprite();
                    this.direction = Directions.Left;
                    break;
                case Directions.Right:
                    moblinSprite = EnemySpriteFactory.Instance.CreateBlueGoriyaRightSprite();
                    this.direction = Directions.Right;
                    break;
            }
        }
        public void Push(Directions direction)
        {
        }
        public void Stop()
        {
        }

        public void BeMoving()
        {
        }

        public void BeAttacking()
        {
        }
        public void BeIdle()
        {
            speed = 0;
        }
        public void MoveDown()
        {
            location.Y++;
        }
        public void MoveLeft()
        {
            location.X--;
        }
        public void MoveUp()
        {
            location.Y--;
        }
        public void MoveRight()
        {
            location.X++;
        }
        public void CollideAction()
        {
            speed = 0;
            movement = new Vector2(0, 0);
            state = new WanderState(this);
        }
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }

        public void UseItem()
        {
            state = new WaitingState(this);
            if (usedItem == null)
                usedItem = new Arrow(location, direction);
        }
        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
            moblinSprite.Update(gameTime);
            moblinCenter = this.location + moblinSizeOffset;
            edges = moblinSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

            attackTimer--;
            if (attackTimer <= 100 && attackTimer >= 50)
            {
                UseItem();    
				speed = 0;            
            }
            if (attackTimer <= 0)
            {
				int rndTime = Randomizer.Instance.Next(0, 256);
                attackTimer = 300 + rndTime;
				usedItem = null;

                state = new WanderState(this);
            }

            if (usedItem != null)
            {
                usedItem.Update(gameTime);
                if (!usedItem.DestroyedState())
                {
                    usedItem = null;
                }
            }

            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 176, (int)this.location.Y, 352, 16);

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            moblinSprite.Draw(spriteBatch, location);
            if (usedItem != null)
                usedItem.Draw(spriteBatch);
        }
    }
    
}
