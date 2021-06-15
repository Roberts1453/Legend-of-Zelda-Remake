using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class BladeTrap : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 bladeTrapCenter;
        Vector2 bladeTrapSizeOffset = new Vector2(5, 0);
        ISprite bladeTrapSprite = EnemySpriteFactory.Instance.CreateBladeTrapSprite();
        public IActionState state;

        public float speed;
        int damage = 1;
        int maxHealth = 4;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public BladeTrap(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            bladeTrapCenter = location + bladeTrapSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            speed = 0;
            movement = new Vector2(0, 0);
            state = new WaitingState(this);
        }
        public BladeTrap(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            bladeTrapCenter = location + bladeTrapSizeOffset;
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
                speed = value;
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
                edges = bladeTrapSprite.Edges();
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
            return bladeTrapCenter;
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
			if (state.GetType() == typeof(AttackVertState) || state.GetType() == typeof(AttackHorizState))
            	state = new ReturningState(this);
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
			if (location == startLocation && state.GetType() == typeof(ReturningState))
				state = new WaitingState(this);
            bladeTrapCenter = this.location + bladeTrapSizeOffset;
            edges = bladeTrapSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bladeTrapSprite.Draw(spriteBatch, location);
        }
    }    
}
