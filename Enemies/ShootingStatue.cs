using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class ShootingStatue : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        Vector2 target;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 shootingStatueCenter;
        Vector2 shootingStatueSizeOffset = new Vector2(5, 0);
        ISprite shootingStatueSprite = DungeonSpriteFactory.Instance.CreateLeftStatueSprite();
        public IActionState state;

        public float speed;
        int damage = 1;
        int maxHealth = 4;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public ShootingStatue(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            shootingStatueCenter = location + shootingStatueSizeOffset;
            verticalRange = new Rectangle((int)this.location.X -176, (int)this.location.Y - 96, 256, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 200);
            speed = 0;
            movement = new Vector2(0, 0);
            target = new Vector2(1, 0);
            state = new WaitingState(this);
        }
		public ShootingStatue(Vector2 location, int type)
        {
            this.location = location;
            startLocation = location;
            shootingStatueCenter = location + shootingStatueSizeOffset;
            verticalRange = new Rectangle((int)this.location.X -176, (int)this.location.Y - 96, 256, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 200);
            speed = 0;
            movement = new Vector2(0, 0);
            target = new Vector2(-1, 0);
            state = new WaitingState(this);
			shootingStatueSprite = DungeonSpriteFactory.Instance.CreateRightStatueSprite();
        }
        public ShootingStatue(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            shootingStatueCenter = location + shootingStatueSizeOffset;
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
                edges = shootingStatueSprite.Edges();
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
            return shootingStatueCenter;
        }
        public bool GetLiving()
        {
            return living;
        }
		public void DetectVert(Vector2 target) 
		{
            this.target = target;
		}
		public void DetectHoriz(Vector2 target) 
		{
            this.target = target;
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
            shootingStatueSprite = DungeonSpriteFactory.Instance.CreateRightStatueSprite();
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
        public void UseItem()
        {
            if (usedItem == null && target != null)
                usedItem = new Fireball(location, target);
        }
        public void Update(GameTime gameTime)
        {
            edges = shootingStatueSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

            if (usedItem != null)
            {
                usedItem.Update(gameTime);
                if (!usedItem.DestroyedState())
                {
                    usedItem = null;
                }
            }
            else
                UseItem();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            shootingStatueSprite.Draw(spriteBatch, location);
            if (usedItem != null)
                usedItem.Draw(spriteBatch);
        }
    }    
}
