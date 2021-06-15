using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Rope : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 ropeCenter;
        Vector2 ropeSizeOffset = new Vector2(5, 0);
        ISprite ropeSprite = EnemySpriteFactory.Instance.CreateRopeRightSprite();
        public IActionState state;

        public float speed;
        int damage = 1;
        int health = 2;
        int maxHealth = 2;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Rope(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            ropeCenter = location + ropeSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            speed = 1;
            movement = new Vector2(1, 0);
            state = new WanderState(this);
        }
        public Rope(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            ropeCenter = location + ropeSizeOffset;
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
                edges = ropeSprite.Edges();
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
            return ropeCenter;
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
            if (direction == Directions.Left)
                ropeSprite = EnemySpriteFactory.Instance.CreateRopeLeftSprite();
            else
                ropeSprite = EnemySpriteFactory.Instance.CreateRopeRightSprite();
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
            ropeSprite.Update(gameTime);
            ropeCenter = this.location + ropeSizeOffset;
            edges = ropeSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 176, (int)this.location.Y, 352, 16);

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ropeSprite.Draw(spriteBatch, location);
        }
    }
    
}
