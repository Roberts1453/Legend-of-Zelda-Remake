using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Bubble : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 bubbleCenter;
        Vector2 bubbleSizeOffset = new Vector2(5, 0);
        ISprite bubbleSprite = EnemySpriteFactory.Instance.CreateBubbleSprite();
        public IActionState state;

        public float speed;
        int damage = 0;
        int health = 1;
        int maxHealth = 1;
		int damageTimer = 100;
		bool damaged = false;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Bubble(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            bubbleCenter = location + bubbleSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            Speed = 1;
            movement = new Vector2(0, 1);
            state = new WanderState(this);
        }
        public Bubble(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            bubbleCenter = location + bubbleSizeOffset;
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
                speed = value * 0.8f;
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
                edges = bubbleSprite.Edges();
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
            return bubbleCenter;
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
            	speed = 0;
            	movement = new Vector2(0, 0);
            	state = new WanderState(this);
        }
        public void CollideAction(Link link)
        {
            link.usedSword = new SwordCurse();
        }
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
				
			bubbleSprite.Update(gameTime);

            edges = bubbleSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;			
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bubbleSprite.Draw(spriteBatch, location);
        }
    }
    
}
