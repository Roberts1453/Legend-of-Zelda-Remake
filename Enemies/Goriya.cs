using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Goriya : ICharacter
    {
        public Vector2 location;
        Vector2 goriyaCenter;
        Vector2 goriyaSizeOffset = new Vector2(5, 0);
        private GoriyaStateMachine stateMachine;
        ISprite goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaMovingDownSprite();
        IItem usedItem = null;
        ICollectibleItem dropItem;
        public Rectangle edges;
        int damage = 2;
        int maxHealth = 6;
        bool living = true;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;

        public Goriya(Vector2 location)
        {
            this.location = location;
            goriyaCenter = location + goriyaSizeOffset;
            stateMachine = new GoriyaStateMachine(this);
        }
        public Goriya(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            goriyaCenter = location + goriyaSizeOffset;
            stateMachine = new GoriyaStateMachine(this);
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
                edges = goriyaSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return goriyaCenter;
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
            stateMachine.TakeDamage(damage);
        }
        public bool IsDamaged()
        {
            return stateMachine.GoriyaDamaged;
        }
        public void ChangeDirection()
        {
            stateMachine.ChangeDirection();
        }
        public void ChangeDirection(Directions direction)
        {
        }
		public void CollideAction()
        {
            ChangeDirection();
        }
		public Rectangle GetVertRange()
        {
            return new Rectangle((int)location.X, (int)location.Y, 1, 1);
        }
        public Rectangle GetHorizRange()
        {
            return new Rectangle((int)location.X, (int)location.Y, 1, 1);
        }
		public void DetectVert(Vector2 target) 
		{
		}
		public void DetectHoriz(Vector2 target) 
		{
		}
        public void Push(Directions direction)
        {
            stateMachine.Push(direction);
        }
        public void Stop()
        {
            stateMachine.Stop();
        }

        public void BeMoving()
        {
            stateMachine.BeMoving();
        }

        public void BeAttacking()
        {
            stateMachine.BeAttacking();
        }
        public void BeIdle()
        {
            stateMachine.BeIdle();
        }
        public void UseBoomerang()
        {
            stateMachine.UseItem();
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
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public class GoriyaStateMachine
        {
            Directions direction = Directions.Down;
            Directions pushedDirection = Directions.Down;
            Goriya goriya;
            public bool GoriyaAttacking = false;
            public bool GoriyaMoving = false;
            public bool GoriyaDamaged = false;
            public bool pushed = false;
            int attackTime = 10000;
            int moveTime = 500;
            int health;
            int damageTimer = 100;
            int pushDistance = 32;
            int pushedDistance = 0;

            public GoriyaStateMachine(Goriya goriya)
            {
                this.goriya = goriya;
                health = goriya.maxHealth;
            }
            public void TakeDamage(int damage)
            {
                if (!GoriyaDamaged)
                {
                    health -= damage;
                    GoriyaDamaged = true;
                }
            }
            public void ChangeDirection()
            {
                int dir = Randomizer.Instance.Next(0, 500);
                if (dir <= 3)
                {
                    if (direction != (Directions)dir)
                        direction = (Directions)dir;
                    BeMoving();
                }
                else
                    BeIdle();
            }

            public void BeMoving()
            {
                if (!GoriyaAttacking) // Note: So Goriya is stopped when attacking
                {
                    GoriyaMoving = true;
                    // Compute and construct goriya sprite using logic with direction
                    switch (direction)
                    {
                        case Directions.Down:
                            /*down facing animations*/
                            goriya.goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaMovingDownSprite();
                            break;
                        case Directions.Left:
                            /*left facing animations*/
                            goriya.goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaMovingLeftSprite();
                            break;
                        case Directions.Up:
                            /*up facing animations*/
                            goriya.goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaMovingUpSprite();
                            break;
                        case Directions.Right:
                            /*right facing animations*/
                            goriya.goriyaSprite = EnemySpriteFactory.Instance.CreateGoriyaMovingRightSprite();
                            break;
                    }
                }

            }
            public void Push(Directions push)
            {
                pushedDirection = push;
                pushed = true;
            }
            public void Stop()
            {
                pushed = false;
            }

            public void BeIdle()
            {
                GoriyaMoving = false;
            }

            public void BeAttacking()
            {
                if (!GoriyaAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    GoriyaAttacking = true;
                    BeIdle();
                }
            }
            public void MoveUpdate(GameTime gameTime)
            {
                moveTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since move started
                if (moveTime <= 0) // After ,ove time has passed, move stops
                {
                    moveTime = 250; // reset move timer
                    BeIdle();
                }
            }
            public void AttackUpdate(GameTime gameTime)
            {
                attackTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since attack started
                if (attackTime <= 0) // After attack time has passed, attack stops
                {
                    attackTime = 5000; // reset attack timer
                    UseItem();
                }
            }
            public void UseItem()
            {
                if (goriya.usedItem == null) // Prevent using more than one item at once
                {
                    BeIdle();
                    goriya.usedItem = new Boomerang(goriya, goriya.goriyaCenter, direction);
                }
            }

            public void Update(GameTime gameTime)
            {
                if (GoriyaMoving == false)
                {
                    ChangeDirection();
                }
                else if (GoriyaMoving == true)
                {
                    MoveUpdate(gameTime);
                    goriya.goriyaSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            goriya.MoveDown();
                            break;
                        case Directions.Left:
                            goriya.MoveLeft();
                            break;
                        case Directions.Up:
                            goriya.MoveUp();
                            break;
                        case Directions.Right:
                            goriya.MoveRight();
                            break;
                    }
                }
                AttackUpdate(gameTime);

                if (GoriyaDamaged)
                {
                    damageTimer--;
                    if (damageTimer <= 0)
                    {
                        GoriyaDamaged = false;
                        damageTimer = 100;
                    }
                }
                if (pushed)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        switch (pushedDirection)
                        {
                            case Directions.Down:
                                goriya.MoveDown();
                                break;
                            case Directions.Left:
                                goriya.MoveLeft();
                                break;
                            case Directions.Up:
                                goriya.MoveUp();
                                break;
                            case Directions.Right:
                                goriya.MoveRight();
                                break;
                        }
                    }
                    pushedDistance += 4;
                    if (pushedDistance >= pushDistance)
                    {
                        pushed = false;
                        pushedDistance = 0;
                    }

                }

                if (health <= 0)
                    goriya.living = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            goriyaCenter = this.location + goriyaSizeOffset;
            edges = goriyaSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
            if (State == null)
                stateMachine.Update(gameTime);
            else
                state.Update(gameTime);
            if (usedItem != null)
            {
                usedItem.Update(gameTime);
                if (!usedItem.DestroyedState())
                {
                    usedItem = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            goriyaSprite.Draw(spriteBatch, location);
            if (usedItem != null)
            {
                usedItem.Draw(spriteBatch);
            }
        }
    }
}
