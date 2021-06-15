using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Gel : ICharacter
    {
        public Vector2 location;
        Vector2 gelCenter;
        Vector2 gelSizeOffset = new Vector2(5, 0);
        private GelStateMachine stateMachine;
        ISprite gelSprite = EnemySpriteFactory.Instance.CreateGelSprite();
        IItem usedItem = null;
        ICollectibleItem dropItem;
        public Rectangle edges;
        int maxHealth = 1;
        int damage = 1;
        bool living = true;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;

        public Gel(Vector2 location)
        {
            this.location = location;
            gelCenter = location + gelSizeOffset;
            stateMachine = new GelStateMachine(this);
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
                edges = gelSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return gelCenter;
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
            return stateMachine.GelDamaged;
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
        }
        public void Stop()
        {
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
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public class GelStateMachine
        {
            Directions direction = Directions.Up;
            Gel gel;
            public bool GelAttacking = false;
            public bool GelMoving = false;
            public bool GelDamaged = false;
            int moveDistance = 0;
            int tileDistance = 16;
            int attackTime = 10000;
            int moveTime = 500;
            int health = 4;
            int damageTimer = 500;

            public GelStateMachine(Gel gel)
            {
                this.gel = gel;
                health = gel.maxHealth;
            }
            public void TakeDamage(int damage)
            {
                health -= damage;
                GelDamaged = true;
            }
            public void ChangeDirection()
            {
                
                Random rnd = new Random();
                int dir = rnd.Next(0, 4);
                this.direction = (Directions)dir;
            }

            public void BeMoving()
            {
                GelMoving = true;

            }

            public void BeIdle()
            {
                GelMoving = false;
            }

            public void BeAttacking()
            {
                if (!GelAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    GelAttacking = true;
                    BeIdle();
                }
            }
            public void IdleUpdate(GameTime gameTime)
            {
                moveTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since move started
                if (moveTime <= 0) // After ,ove time has passed, move stops
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
                if (GelMoving == false)
                {
                    IdleUpdate(gameTime);
                }
                else if (GelMoving == true)
                {                   
                    
                    switch (direction)
                    {
                        case Directions.Down:
                            gel.MoveDown();
                            break;
                        case Directions.Left:
                            gel.MoveLeft();
                            break;
                        case Directions.Up:
                            gel.MoveUp();
                            break;
                        case Directions.Right:
                            gel.MoveRight();
                            break;
                    }
                    MoveUpdate(gameTime);
                }
                AttackUpdate(gameTime);

                if (GelDamaged)
                {
                    damageTimer--;
                    if (damageTimer <= 0)
                    {
                        GelDamaged = false;
                        damageTimer = 500;
                    }
                }

                if (health <= 0)
                    gel.living = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            gelSprite.Update(gameTime);
            gelCenter = this.location + gelSizeOffset;
            edges = gelSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
            stateMachine.Update(gameTime);
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
            gelSprite.Draw(spriteBatch, location);
            if (usedItem != null)
            {
                usedItem.Draw(spriteBatch);
            }
        }
    }
}
