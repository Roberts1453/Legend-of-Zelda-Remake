using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Project
{

    public class Keese : ICharacter
    {
        public Vector2 location;
        Vector2 keeseCenter;
        Vector2 keeseSizeOffset = new Vector2(0, 0);
        private KeeseStateMachine stateMachine;
        //ISprite keeseSprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
        KeeseSprite keeseSprite = (KeeseSprite) EnemySpriteFactory.Instance.CreateKeeseSprite();

        IItem usedItem = null;
        ICollectibleItem dropItem;
        public Rectangle edges;
        int damage = 1;
        int maxHealth = 1;
        bool living = true;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;
        float topSpeed;

        public Keese(Vector2 location)
        {
            keeseSizeOffset.X = keeseSprite.Edges().Width / 2;
            keeseSizeOffset.Y = keeseSprite.Edges().Height / 2;
            this.location = location;
            keeseCenter = location + keeseSizeOffset;
            speed = 0;
            topSpeed = 1.2f;
            stateMachine = new KeeseStateMachine(this);
        }
        public Keese(Vector2 location, int type)
        {
            keeseSprite.MakeRed();
            keeseSizeOffset.X = keeseSprite.Edges().Width / 2;
            keeseSizeOffset.Y = keeseSprite.Edges().Height / 2;
            this.location = location;
            keeseCenter = location + keeseSizeOffset;
            speed = 0.5f;
            topSpeed = 2.3f;
            stateMachine = new KeeseStateMachine(this);
        }
        public Keese(Vector2 location, ICollectibleItem dropItem)
        {
            keeseSizeOffset.X = keeseSprite.Edges().Width / 2;
            keeseSizeOffset.Y = keeseSprite.Edges().Height / 2;
            this.location = location;
            keeseCenter = location + keeseSizeOffset;
            this.dropItem = dropItem;
            stateMachine = new KeeseStateMachine(this);
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
                edges = keeseSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return keeseCenter;
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
            return stateMachine.KeeseDamaged;
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
            location.Y += speed;
        }
        public void MoveLeft()
        {
            location.X -= speed;
        }
        public void MoveUp()
        {
            location.Y -= speed;
        }
        public void MoveRight()
        {
            location.X += speed;
        }
        public int GetDamage()
        {
            return damage;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public class KeeseStateMachine
        {
            Directions direction = Directions.Up;
            Directions direction2 = Directions.Right;
            Keese keese;
            public bool KeeseAttacking = false;
            public bool KeeseMoving = false;
            public bool KeeseDamaged = false;
            int attackTime = 10000;
            int moveTime = 500;
            int health;
            int damageTimer = 500;

            public KeeseStateMachine(Keese keese)
            {
                this.keese = keese;
                health = keese.maxHealth;
            }
            public void TakeDamage(int damage)
            {
                health -= damage;
                KeeseDamaged = true;
            }
            public void ChangeDirection()
            {
                int dir = Randomizer.Instance.Next(1, 500);
                if (dir <= 50)
                {
                    if (direction != (Directions)(dir % 4))
                        direction = (Directions)(dir % 4);
                    BeMoving();
                }
                dir = Randomizer.Instance.Next(1, 500);
                if (dir <= 50)
                {
                    if (direction2 != (Directions)(dir % 4))
                        direction2 = (Directions)(dir % 4);
                    BeMoving();
                }
                else if (dir > 495)
                    BeIdle();
            }

            public void BeMoving()
            {
                    KeeseMoving = true;
            }

            public void BeIdle()
            {
                KeeseMoving = false;
            }

            public void BeAttacking()
            {
                if (!KeeseAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    KeeseAttacking = true;
                    BeIdle();
                }
            }
            public void IdleUpdate(GameTime gameTime)
            {
                moveTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since move started
                if (moveTime <= 0) // After ,ove time has passed, move stops
                {
                    moveTime = 250; // reset move timer
                    BeMoving();
                }
            }
                public void MoveUpdate(GameTime gameTime)
            {
                /*moveTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since move started
                if (moveTime <= 0) // After ,ove time has passed, move stops
                {
                    moveTime = 250; // reset move timer
                    BeIdle();
                }*/

                ChangeDirection();
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
                if (KeeseMoving == true)
                {
                    if (keese.speed < keese.topSpeed)
                        keese.speed += 0.1f;
                    MoveUpdate(gameTime);
                    keese.keeseSprite.ChangeSpeed(keese.speed);
                    keese.keeseSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            keese.MoveDown();
                            break;
                        case Directions.Up:
                            keese.MoveUp();
                            break;
                    }
                    switch (direction2)
                    {
                        case Directions.Left:
                            keese.MoveLeft();
                            break;
                        case Directions.Right:
                            keese.MoveRight();
                            break;
                    }
                }
                else
                {
                    IdleUpdate(gameTime);
                }
                AttackUpdate(gameTime);

                if (KeeseDamaged)
                {
                    damageTimer--;
                    if (damageTimer <= 0)
                    {
                        KeeseDamaged = false;
                        damageTimer = 500;
                    }
                }

                if (health <= 0)
                    keese.living = false;
            }
        }
        public void Update(GameTime gameTime)
        {
			stateMachine.Update(gameTime);
            keeseCenter = this.location + keeseSizeOffset;
            edges = keeseSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)keeseCenter.Y;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			keeseSprite.Draw(spriteBatch, location);
        }
    }
}
