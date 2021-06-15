using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Moldorm : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 moldormCenter;
        Vector2 moldormSizeOffset = new Vector2(5, 0);
        ISprite moldormSprite = EnemySpriteFactory.Instance.CreateMoldormSprite();
        public IActionState state;

        public float speed;
        int damage = 1;
        int health = 4;
        int maxHealth = 5;
		int damageTimer = 100;
		bool damaged = false;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
		
		public Moldorm front;
		public Moldorm back;
        

        public Moldorm(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            moldormCenter = location + moldormSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            Speed = 1;
            movement = new Vector2(0, 1);
            state = new WanderState(this);
			front = null;
        }
		public Moldorm(Vector2 location, Moldorm front)
        {
            this.location = location;
            startLocation = location;
            moldormCenter = location + moldormSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            Speed = 1;
            movement = new Vector2(0, 0);
			state = null;
			this.front = front;
        }
        public Moldorm(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            moldormCenter = location + moldormSizeOffset;
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
                edges = moldormSprite.Edges();
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
            return moldormCenter;
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
			if (!damaged) {
				damaged = true;
            	health -= damage;
			}
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
			Directions rndDirection = (Directions) Randomizer.Instance.Next(0, 4);
			switch (direction) {
				case Directions.Up:
					if (rndDirection == Directions.Left)
						movement += WanderState.leftVector;
					else if (rndDirection == Directions.Right)
						movement += WanderState.rightVector;
				break;
				case Directions.Down:
					if (rndDirection == Directions.Left)
						movement += WanderState. leftVector;
					else if (rndDirection == Directions.Right)
						movement += WanderState.rightVector;
				break;
				case Directions.Left:
					if (rndDirection == Directions.Up)
						movement += WanderState. upVector;
					else if (rndDirection == Directions.Down)
						movement += WanderState.downVector;
				break;
				case Directions.Right:
					if (rndDirection == Directions.Up)
						movement += WanderState.upVector;
					else if (rndDirection == Directions.Down)
						movement += WanderState.downVector;
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
			if (front == null){
            	speed = 0;
            	movement = new Vector2(0, 0);
            	state = new WanderState(this);
			}
        }
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
		public void SetBack(Moldorm back){
			this.back = back;
		}
        public void Update(GameTime gameTime)
        {
			if (state == null && front == null)
				state = new WanderState(this);
			if (state != null)
            	state.Update(gameTime);
			if (front != null) {
				if (!edges.Intersects(front.GetEdges())) {
					movement = front.Location - location;
					movement.Normalize();
					location += movement * speed;
				}
			}
				
				
			if (damaged)
            	moldormSprite.Update(gameTime);
            moldormCenter = this.location + moldormSizeOffset;
            edges = moldormSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

			
			if (damaged) {
				damageTimer--;
				if (damageTimer <= 0){
					damaged = false;
					damageTimer = 100;
				}
			}

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            moldormSprite.Draw(spriteBatch, location);
        }
    }
    
}
