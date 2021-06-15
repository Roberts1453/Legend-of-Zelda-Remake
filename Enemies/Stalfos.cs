using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Stalfos : ICharacter
    {
        public Vector2 location;
        Vector2 stalfosCenter;
        Vector2 stalfosSizeOffset = new Vector2(5, 0);
        private StalfosStateMachine stateMachine;
        StalfosSprite stalfosSprite = (StalfosSprite) EnemySpriteFactory.Instance.CreateStalfosSprite();
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        public Rectangle edges;
        int damage = 1;
        int maxHealth = 4;
        bool living = true;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;

        public Stalfos(Vector2 location)
        {
            this.location = location;
            stalfosCenter = location + stalfosSizeOffset;
            stateMachine = new StalfosStateMachine(this);
        }
        public Stalfos(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            stalfosCenter = location + stalfosSizeOffset;
            stateMachine = new StalfosStateMachine(this);
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
                edges = stalfosSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return stalfosCenter;
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
            return stateMachine.StalfosDamaged;
        }
        public void ChangeDirection()
        {
            stateMachine.ChangeDirection();
        }
        public void ChangeDirection(Directions direction)
        {
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
            ChangeDirection();
        }
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public class StalfosStateMachine
        {
            Directions direction = Directions.Up;
            Directions pushedDirection = Directions.Down;
            Stalfos stalfos;
            public bool StalfosAttacking = false;
            public bool StalfosMoving = false;
            public bool StalfosDamaged = false;
            public bool pushed = false;
            int moveDistance = 0;
            int tileDistance = 16;
            int attackTime = 10000;
            int moveTime = 500;
            int health;
            int damageTimer = 100;
            int pushDistance = 32;
            int pushedDistance = 0;

            public StalfosStateMachine(Stalfos stalfos)
            {
                this.stalfos = stalfos;
                health = stalfos.maxHealth;
            }
            public void TakeDamage(int damage)
            {
                if(!StalfosDamaged)
                {
                    health -= damage;
                    StalfosDamaged = true;
                }                
            }
            public void ChangeDirection()
            {
                
                Random rnd = new Random();
                int dir = rnd.Next(0, 4);
                this.direction = (Directions)dir;
            }

            public void BeMoving()
            {
                StalfosMoving = true;

            }

            public void BeIdle()
            {
                StalfosMoving = false;
            }

            public void BeAttacking()
            {
                if (!StalfosAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    StalfosAttacking = true;
                    BeIdle();
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
            public void IdleUpdate(GameTime gameTime)
            {
                moveTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since move started
                if (moveTime <= 0) // After move time has passed, move stops
                {
                    moveTime = 1000; // reset move timer
                    BeMoving();
                }
            }
            public void MoveUpdate(GameTime gameTime)
            {
                moveDistance++; // counts down time since move started
                if (moveDistance >= tileDistance) // After move distance has passed, decides next move
                {
                    moveDistance = 0; // reset move
                    BeIdle();
                    ChangeDirection();                    
                    int dir = Randomizer.Instance.Next(1, 100);
                    if (dir < 70)
                        BeMoving();
                }
            }
            public void AttackUpdate(GameTime gameTime)
            {
                attackTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since attack started
                if (attackTime <= 0) // After attack time has passed, attack stops
                {
                    attackTime = 5000; // reset attack timer                    
               }
            }

            public void Update(GameTime gameTime)
            {
                if (StalfosMoving == false)
                {
                    IdleUpdate(gameTime);
                }
                else if (StalfosMoving == true)
                {                    
                    stalfos.stalfosSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            stalfos.MoveDown();
                            break;
                        case Directions.Left:
                            stalfos.MoveLeft();
                            break;
                        case Directions.Up:
                            stalfos.MoveUp();
                            break;
                        case Directions.Right:
                            stalfos.MoveRight();
                            break;
                    }
                    MoveUpdate(gameTime);
                }
                AttackUpdate(gameTime);

                if (StalfosDamaged)
                {
                    damageTimer--;
					if ((damageTimer /5) % 2 == 0)
						stalfos.stalfosSprite.Damage(false);
					else
						stalfos.stalfosSprite.Damage(true);
                    if (damageTimer <= 0)
                    {
                        StalfosDamaged = false;
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
                                stalfos.MoveDown();
                                break;
                            case Directions.Left:
                                stalfos.MoveLeft();
                                break;
                            case Directions.Up:
                                stalfos.MoveUp();
                                break;
                            case Directions.Right:
                                stalfos.MoveRight();
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

                if (health == 0)
                    stalfos.living = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            stalfosCenter = this.location + stalfosSizeOffset;
            edges = stalfosSprite.Edges();
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
            stalfosSprite.Draw(spriteBatch, location);
            if (usedItem != null)
            {
                usedItem.Draw(spriteBatch);
            }
        }
    }
}
