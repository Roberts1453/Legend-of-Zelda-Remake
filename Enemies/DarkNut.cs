using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Darknut : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 darknutCenter;
        Vector2 darknutSizeOffset = new Vector2(5, 0);
        ISprite darknutSprite = EnemySpriteFactory.Instance.DarknutRightSprite();
        public IActionState state;
		Directions direction;

        public float speed;
        int damage = 2;
        int health = 8;
        int maxHealth = 8;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Darknut(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            darknutCenter = location + darknutSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
			direction = Directions.Down;
            speed = 1;
            movement = new Vector2(1, 0);
            state = new WanderState(this);
        }
        public Darknut(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            startLocation = location;
            darknutCenter = location + darknutSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
			direction = Directions.Down;
            speed = 1;
            movement = new Vector2(1, 0);
            state = new WanderState(this);
			this.dropItem = dropItem;
        }
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value * 0.6f;
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
                edges = darknutSprite.Edges();
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
            return darknutCenter;
        }
        public bool GetLiving()
        {
            return living;
        }
		public void DetectVert(Vector2 target) 
		{
				state.AttackVert(target);
		}
		public void DetectHoriz(Vector2 target) 
		{
				state.AttackHoriz(target);
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
                    darknutSprite = EnemySpriteFactory.Instance.DarknutUpSprite();
                    this.direction = Directions.Up;
                    break;
                case Directions.Down:
                    darknutSprite = EnemySpriteFactory.Instance.DarknutDownSprite();
                    this.direction = Directions.Down;
                    break;
                case Directions.Left:
                    darknutSprite = EnemySpriteFactory.Instance.DarknutLeftSprite();
                    this.direction = Directions.Left;
                    break;
                case Directions.Right:
                    darknutSprite = EnemySpriteFactory.Instance.DarknutRightSprite();
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
        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
            darknutSprite.Update(gameTime);
            darknutCenter = this.location + darknutSizeOffset;
            edges = darknutSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 176, (int)this.location.Y, 352, 16);

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            darknutSprite.Draw(spriteBatch, location);
        }
    }
    
}
