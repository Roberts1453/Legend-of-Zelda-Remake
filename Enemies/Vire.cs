using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Vire : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 vireCenter;
        Vector2 vireSizeOffset = new Vector2(5, 0);
        ISprite vireSprite = EnemySpriteFactory.Instance.CreateVireDownSprite();
        public IActionState state;

        public float speed;
		int groundHeight;
        int damage = 2;
        int health = 4;
        int damageTimer = 100;
        bool damaged = false;
        bool living = true;
		bool jumping = true;
        public bool spawnKeese = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        Vector2 jumpVector = new Vector2(0, -0.6f);
        float jumpHeight = 0;
        int falling = 1;
        int jumpLimit = 16;

        public Vire(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            vireCenter = location + vireSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
			groundHeight = (int) location.Y;
            speed = 1;
            movement = new Vector2(1, 0);
            state = new WanderState(this);
            
        }
        public Vire(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            startLocation = location;
            vireCenter = location + vireSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 176, (int)this.location.Y, 352, 16);
            groundHeight = (int)location.Y;
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
                speed = value * 0.4f;
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
                edges = vireSprite.Edges();
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
            return vireCenter;
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
            if (!damaged)
            {
                damaged = true;
                health -= damage;
                if (damage >= 4)
                    spawnKeese = false;
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
			if (direction == Directions.Right || direction == Directions.Left){
				jumping = true;
				groundHeight = (int) location.Y;
                jumpHeight = 0;
                jumpVector.Y = -0.6f;
            }
			else
				jumping = false;
			if (direction == Directions.Up)
				vireSprite = EnemySpriteFactory.Instance.CreateVireUpSprite(); //VireUpSprite
			else
				vireSprite = EnemySpriteFactory.Instance.CreateVireDownSprite(); //VireDownSprite
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
            vireSprite.Update(gameTime);
            edges = vireSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
			
			if (jumping) {
                location += jumpVector*falling;
                jumpHeight -= jumpVector.Y * falling;
                if (jumpHeight >= jumpLimit)
                    falling = -1;
                if (jumpHeight <= 0)
                    falling = 1;
            }

            if (damaged)
            {
                damageTimer--;
                if (damageTimer <= 0)
                {
                    damaged = false;
                    damageTimer = 100;
                }
            }

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            vireSprite.Draw(spriteBatch, location);
        }
    }
    
}
