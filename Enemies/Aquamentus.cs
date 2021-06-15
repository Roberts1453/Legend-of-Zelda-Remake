using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace Sprint0Project
{

    public class Aquamentus : ICharacter
    {
        public Vector2 location;
        Vector2 aquamentusCenter;
        Vector2 aquamentusSizeOffset = new Vector2(5, 0);
        Vector2 target;
        private AquamentusStateMachine stateMachine;
        ISprite aquamentusSprite = EnemySpriteFactory.Instance.CreateAquamentusSprite();
        IItem usedItem = null;
        Fireball fireball2 = null;
        Fireball fireball3 = null;
        ICollectibleItem dropItem;
        public Rectangle edges;
        public Rectangle range;
        int damage = 1;
        int maxHealth = 12;
        bool living = true;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;

        public Aquamentus(Vector2 location)
        {
            this.location = location;
            aquamentusCenter = location + aquamentusSizeOffset;
            range = new Rectangle((int)location.X - 160, (int)location.Y - 60, 260, 200);
            target = new Vector2((int)location.X - 100, (int)location.Y);
            dropItem = new HeartContainer(this.location);
            stateMachine = new AquamentusStateMachine(this);
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
                edges = aquamentusSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            return edges;
        }
        public Rectangle GetRange()
        {
            return range;
        }
        public Vector2 GetLocation()
        {
            return aquamentusCenter;
        }
        public bool GetLiving()
        {
            return living;
        }
        public void TakeDamage(int damage)
        {
            stateMachine.TakeDamage(damage);
        }
        public bool IsDamaged()
        {
            return stateMachine.AquamentusDamaged;
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
        }
		public Rectangle GetVertRange()
        {
            return range;
        }
        public Rectangle GetHorizRange()
        {
            return range;
        }
		public void DetectVert(Vector2 target) 
		{
				SetTarget(target);
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
        public void UseFireball()
        {
            stateMachine.UseItem();
        }
        public void SetTarget(Vector2 target)
        {
            this.target = target;
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
        public ICollectibleItem GetDropItem()
        {
            return dropItem;
        }
        public class AquamentusStateMachine
        {
            Directions direction = Directions.Down;
            Aquamentus aquamentus;
            public bool AquamentusAttacking = false;
            public bool AquamentusMoving = false;
            public bool AquamentusDamaged = false;
            int attackTime = 500;
            int moveTime = 500;
            int health;
            int damageTimer = 100;

            public AquamentusStateMachine(Aquamentus aquamentus)
            {
                this.aquamentus = aquamentus;
                health = aquamentus.maxHealth;
            }
            public void TakeDamage(int damage)
            {
                if (!AquamentusDamaged)
                {
                    health -= damage;
                    AquamentusDamaged = true;
                }
            }
            public void ChangeDirection()
            {
                int dir = Randomizer.Instance.Next(1, 500);
                if (dir <= 4)
                {
                    if (dir <= 2)
                        direction = Directions.Right;
                    else
                        direction = Directions.Left;
                    BeMoving();
                }
                else
                    BeIdle();
            }

            public void BeMoving()
            {
                if (!AquamentusAttacking) // Note: So Aquamentus is stopped when attacking
                {
                    AquamentusMoving = true;
                    
                }

            }

            public void BeIdle()
            {
                AquamentusMoving = false;
            }

            public void BeAttacking()
            {
                if (!AquamentusAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    AquamentusAttacking = true;
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
                    attackTime = 2500; // reset attack timer
                    UseItem();
                }
            }
            public void UseItem()
            {
                    BeIdle();
                    Vector2 aim = new Vector2(aquamentus.target.X - aquamentus.location.X, aquamentus.target.Y - aquamentus.location.Y);
                    aim.Normalize();
                    aquamentus.usedItem = new Fireball(aquamentus.aquamentusCenter, aim);
            }

            public void Update(GameTime gameTime)
            {
                if (AquamentusMoving == false)
                {
                    ChangeDirection();
                }
                else if (AquamentusMoving == true)
                {
                    MoveUpdate(gameTime);
                    aquamentus.aquamentusSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            aquamentus.MoveDown();
                            break;
                        case Directions.Left:
                            aquamentus.MoveLeft();
                            break;
                        case Directions.Up:
                            aquamentus.MoveUp();
                            break;
                        case Directions.Right:
                            aquamentus.MoveRight();
                            break;
                    }
                }
                AttackUpdate(gameTime);

                if (AquamentusDamaged)
                {
                    damageTimer--;
					if ((damageTimer /5) % 2 == 0)
						EnemySpriteFactory.Instance.NormalBossTexture();  
					else
						EnemySpriteFactory.Instance.HurtBossTexture(); 
                    if (damageTimer <= 0)
                    {
                        AquamentusDamaged = false;
                        damageTimer = 100;
                    }
                }

                if (health == 0)
                    aquamentus.living = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            aquamentusCenter = this.location + aquamentusSizeOffset;
            edges = aquamentusSprite.Edges();
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
            aquamentusSprite.Draw(spriteBatch, location);
            if (usedItem != null)
            {
                usedItem.Draw(spriteBatch);
            }
        }
    }
}
