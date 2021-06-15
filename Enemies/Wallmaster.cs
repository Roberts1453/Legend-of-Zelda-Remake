using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Wallmaster : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        Rectangle excludeRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 wallmasterCenter;
        Vector2 wallmasterSizeOffset = new Vector2(5, 0);
        ISprite wallmasterSprite = EnemySpriteFactory.Instance.CreateWallMasterSprite();
        public IActionState state;

        public float speed;
        int damage = 1;
        int maxHealth = 4;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Wallmaster(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            wallmasterCenter = location + wallmasterSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y+16, 192, 80);
            horizontalRange = new Rectangle((int)this.location.X, (int)this.location.Y, 192, 112);
            excludeRange = new Rectangle((int)this.location.X+16, (int)this.location.Y + 16, 160, 80);
            speed = 0;
            movement = new Vector2(0, 0);
            //state = new WaitingState(this);
        }
        public Wallmaster(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            wallmasterCenter = location + wallmasterSizeOffset;
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
                edges = wallmasterSprite.Edges();
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
            return wallmasterCenter;
        }
        public bool GetLiving()
        {
            return living;
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
		public void DetectVert(Vector2 target) 
		{
		}
		public void DetectHoriz(Vector2 target) 
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
            state.CollideAction();
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
            //state.Update(gameTime);
            wallmasterCenter = this.location + wallmasterSizeOffset;
            edges = wallmasterSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            wallmasterSprite.Draw(spriteBatch, location);
        }

        internal class ReturningState : IActionState
        {
            Wallmaster wallmaster;
            public ReturningState(Wallmaster wallmaster)
            {
                this.wallmaster = wallmaster;
                wallmaster.movement *= -1;
                wallmaster.speed *= 1;
                //wallmaster.Edges = 4;

            }
            public void AttackVert(Vector2 target)
            {

            }
            public void AttackHoriz(Vector2 target)
            {

            }
            public void CollideAction()
            {

            }
            public void Update(GameTime gameTime)
            {
                wallmaster.location += wallmaster.movement;
                //if (wallmaster.location == wallmaster.startLocation)
                    //wallmaster.state = new WaitingState(wallmaster);

            }
        }
    }
}
