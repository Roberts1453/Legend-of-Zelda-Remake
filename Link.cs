using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Collections.Generic;

namespace Sprint0Project
{

    public class Link : ICharacter
    {
        public Vector2 location;
        public Vector2 prevLocation;
        public int prevHealth;
        public float rewindTimer = 0;
        public float rewindDistance = 250f;
        public Directions direction;
        Vector2 linkCenter;
        Vector2 linkSizeOffset = new Vector2(5, 0);
        private LinkStateMachine stateMachine;
        ISprite linkSprite = LinkSpriteFactory.Instance.CreateLinkMovingDownSprite();
        public Items equipedItem = Items.None;
        public IItem usedItem = null;
        public IItem usedSword = null;
        public IItem usedProjectileSword = null;
        public Rectangle edges;

        public Health health;
        bool living = true;


        public int bombs = 2;
        public int arrows = 2;
        public int keys = 1;
        public int rupees = 0;
        int maxRupees = 255;

        public List<int> triforcePieces;

        public List<int> hasCompass;
        public List<int> hasMap;

        public List<Items> obtainedItems;
		
		public IActionState state;
		public Vector2 movement;
        public float speed;

        public Link(Vector2 location)
        {
            this.location = location;
            prevLocation = location;
            linkCenter = location + linkSizeOffset;
            stateMachine = new LinkStateMachine(this);
            health = new Health(this);
            prevHealth = health.currentHealth;
            hasCompass = new List<int>();
            hasMap = new List<int>();
            triforcePieces = new List<int>();
            obtainedItems = new List<Items>();
        }
		#region ICharacter Properties
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
                edges = linkSprite.Edges();
                edges.Y += edges.Height / 2;
                edges.Height /= 2;
            }
        }
		#endregion
		
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return location;
        }
        public bool GetLiving()
        {
            return living;
        }
        public int GetHealth()
        {
            return health.currentHealth;
        }
        public void AddRupee(int value)
        {
            rupees += value;
            if (rupees > maxRupees)
            {
                rupees = maxRupees;
            }
        }
        #region Inventory Collection

        public void CollectTriforce(int dungeonNumber)
        {
            triforcePieces.Add(dungeonNumber);
        }
        public void CollectCompass(int dungeonNumber)
        {
            hasCompass.Add(dungeonNumber);
        }
        public void CollectMap(int dungeonNumber)
        {
            hasMap.Add(dungeonNumber);
        }
        public void CollectHeartContainer()
        {
            health.IncreaseMax();
        }
        public void CollectBow()
        {
            if (!obtainedItems.Contains(Items.Bow))
                obtainedItems.Add(Items.Bow);
        }
        public void CollectArrow()
        {
            if (!obtainedItems.Contains(Items.Arrow))
                obtainedItems.Add(Items.Arrow);
        }
        public void CollectSilverArrow()
        {
            if (!obtainedItems.Contains(Items.SilverArrow))
                obtainedItems.Add(Items.SilverArrow);
        }
        public void CollectBoomerang()
        {
            if (!obtainedItems.Contains(Items.Boomerang))
                obtainedItems.Add(Items.Boomerang);
        }
        public void CollectMagicBoomerang()
        {
            if (!obtainedItems.Contains(Items.MagicBoomerang))
                obtainedItems.Add(Items.MagicBoomerang);
        }
        public void CollectBomb()
        {
            if (!obtainedItems.Contains(Items.Bomb))
                obtainedItems.Add(Items.Bomb);
            bombs++;
        }
        public void CollectBomb(int amount)
        {
            if (!obtainedItems.Contains(Items.Bomb))
                obtainedItems.Add(Items.Bomb);
            bombs += amount;
        }
        public void CollectRaft()
        {
            if (!obtainedItems.Contains(Items.Raft))
                obtainedItems.Add(Items.Raft);
        }
        public void CollectLadder()
        {
            if (!obtainedItems.Contains(Items.Ladder))
                obtainedItems.Add(Items.Ladder);
        }
        public void CollectMagicKey()
        {
            if (!obtainedItems.Contains(Items.MagicKey))
                obtainedItems.Add(Items.MagicKey);
            keys++;
        }
        public void CollectRod()
        {
            if (!obtainedItems.Contains(Items.Rod))
                obtainedItems.Add(Items.Rod);
        }
        public void CollectCandle()
        {
            if (!obtainedItems.Contains(Items.Candle))
                obtainedItems.Add(Items.Candle);
        }
		#endregion
		
        public void Equip(Items equipment)
        {
            equipedItem = equipment;
        }
        public ICollectibleItem GetDropItem()
        {
            return null;
        }
        public void TakeDamage(int damage)
        {            
            stateMachine.TakeDamage(damage);
        }
        public bool IsDamaged()
        {
            return stateMachine.LinkDamaged;
        }
        public void Heal(int heal)
        {
            stateMachine.Heal(heal);
        }
            public void KillLink(Game1 game)
        {
            stateMachine.KillLink(game);
        }
        public void ChangeDirection()
        {
        }
        public void ChangeDirection(Directions direction)
        {
            stateMachine.ChangeDirection(direction);
        }
        public void CollideAction()
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
        public void UseBomb()
        {
            stateMachine.UseItem(Items.Bomb);
        }
        public void UseArrow()
        {
            stateMachine.UseItem(Items.Arrow);
        }
        public void UseBoomerang()
        {
            stateMachine.UseItem(Items.Boomerang);
        }
        public void UseEquiped()
        {
            stateMachine.UseItem(equipedItem);
        }
        public void UseLadder(ICrossableBlock block)
        {
            if (usedItem == null && obtainedItems.Contains(Items.Ladder))
                usedItem = new Ladder(this, block);
        }
        public void UseKey()
        {
            if (!obtainedItems.Contains(Items.MagicKey))
            {
                keys--;
            }
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
            return 0;
        }
        public IItem GetItems()
        {
            return usedItem;
        }
        public void Rewind()
        {
            stateMachine.Rewind();
        }
        public void ResetSprite()
        {
            stateMachine.ResetSprite();
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
		
		#region State Machine
        public class LinkStateMachine
        {
            Directions direction = Directions.Down;
            Directions pushedDirection = Directions.Down;
            Link link;
            public bool LinkAttacking = false;
            public bool SwordProjectile = false;
            public bool LinkMoving = false;
            public bool LinkDamaged = false;
            public bool LinkDead = false;
            public bool pushed = false;
            int attackTime = 500;
            int damageTimer = 100;
            Game1 game;

            private float timeToDeath = 100f;
            private float deathTimer = 0f;

            int pushDistance = 32;
            int pushedDistance = 0;
            
            public LinkStateMachine(Link link)
            {
                this.link = link;
            }
            public void TakeDamage(int damage)
            {
				if (!LinkDamaged) {
					SoundManager.Instance.PlaySoundEffect("Link_Hurt");
                	link.health.TakeDamage(damage);
                	LinkDamaged = true;
				}
            }
            
            public void Heal(int heal)
            {
                link.health.Heal(heal);
            }
            public void KillLink(Game1 _game)
            {
                link.living = false;
                game = _game;
            }
            public void ChangeDirection(Directions direction)
            {
                this.direction = direction;
            }
            public Directions GetDirection()
            {
                return this.direction;
            }

            public void BeMoving()
            {
                if (!LinkAttacking) // Note: So Link is stopped when attacking
                {
                    LinkMoving = true;
                    ResetSprite();
                }
            }

            public void BeIdle()
            {
                LinkMoving = false;
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

            public void Rewind()
            {
                link.location = link.prevLocation;
                link.health.currentHealth = link.prevHealth;
                link.rewindTimer = 0f;
            }

            public void ResetSprite()
            {
                // Compute and construct link sprite using logic with direction
                switch (direction)
                {
                    case Directions.Down:
                        /*down facing animations*/
                        link.linkSprite = LinkSpriteFactory.Instance.CreateLinkMovingDownSprite();
                        break;
                    case Directions.Left:
                        /*left facing animations*/
                        link.linkSprite = LinkSpriteFactory.Instance.CreateLinkMovingLeftSprite();
                        break;
                    case Directions.Up:
                        /*up facing animations*/
                        link.linkSprite = LinkSpriteFactory.Instance.CreateLinkMovingUpSprite();
                        break;
                    case Directions.Right:
                        /*right facing animations*/
                        link.linkSprite = LinkSpriteFactory.Instance.CreateLinkMovingRightSprite();
                        break;
                }
            }

            public void BeAttacking()
            {
                if (!LinkAttacking) // Prevent attacking before previous item use or attack is finished
                {
                    LinkAttacking = true;
                    LinkMoving = false;
                    // Compute and construct link sprite using logic with direction
                    switch (direction)
                    {
                        case Directions.Down:
                            /*down facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemDownSprite();
                            break;
                        case Directions.Left:
                            /*left facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemLeftSprite();
                            break;
                        case Directions.Up:
                            /*up facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemUpSprite();
                            break;
                        case Directions.Right:
                            /*right facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemRightSprite();
                            break;
                    }
                    if (link.usedProjectileSword == null && link.GetHealth() == link.health.maxHealth) {
                        link.usedProjectileSword = new Sword(link.location, direction, true);                        
                    }
                    else
                    {
                        link.usedSword = new Sword(link.location, direction, false);
                    }
                }
            }
            public void AttackUpdate(GameTime gameTime)
            {
                attackTime -= gameTime.ElapsedGameTime.Milliseconds; // counts down time since attack started
                if (attackTime <= 0) // After attack time has passed, attack stops
                {
                    attackTime = 500; // reset attack timer
                    LinkAttacking = false;
                    ResetSprite();
                }
            }
            private void HandleAttack(GameTime gameTime)
            {
                link.linkSprite.Update(gameTime);
                AttackUpdate(gameTime);
            }
            private void HandleMovement(GameTime gameTime)
            {
                link.linkSprite.Update(gameTime);
                switch (direction)
                {
                    case Directions.Down:
                        link.MoveDown();
                        break;
                    case Directions.Left:
                        link.MoveLeft();
                        break;
                    case Directions.Up:
                        link.MoveUp();
                        break;
                    case Directions.Right:
                        link.MoveRight();
                        break;
                }
            }
            private void HandlePush()
            {
                link.usedSword = null;
                for (int i = 0; i < 4; i++)
                {
                    switch (pushedDirection)
                    {
                        case Directions.Down:
                            link.MoveDown();
                            break;
                        case Directions.Left:
                            link.MoveLeft();
                            break;
                        case Directions.Up:
                            link.MoveUp();
                            break;
                        case Directions.Right:
                            link.MoveRight();
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
            private void HandleDeath()
            {
                SoundManager.Instance.PlaySoundEffect("Link_Die");
                if (link.direction != Directions.Right && deathTimer <= timeToDeath && deathTimer % 6 == 0)
                {
                    link.direction += 1;
                }
                else if (link.direction == Directions.Right && deathTimer <= timeToDeath && deathTimer % 6 == 0)
                {
                    link.direction = 0;
                }
                else if (deathTimer >= timeToDeath)
                {
                    link.living = true;
                    game.gameState.Push(new MenuState(Game1.game));
                    game.PlayerDeath();
                }
                link.ChangeDirection(link.direction);
                ResetSprite();
                deathTimer++;
            }

            private void IncrimentRewind()
            {
                link.rewindTimer++;
                if (link.rewindDistance <= link.rewindTimer)
                {
                    link.prevHealth = link.GetHealth();
                    link.prevLocation = link.location;
                    link.rewindTimer = 0;
                }
            }
            private void HandleIFrames()
            {
                damageTimer--;
                if ((damageTimer / 5) % 2 == 0)
                    LinkSpriteFactory.Instance.NormalTexture();
                else
                    LinkSpriteFactory.Instance.HurtTexture();
                if (damageTimer <= 0)
                {
                    damageTimer = 100;
                    LinkDamaged = false;
                }
            }

            public void UseItem(Items item)
            {
                if (!LinkAttacking && link.usedItem == null) // Prevent using more than one item at once
                {
                    LinkAttacking = true;
                    LinkMoving = false;
                    switch (item)
                    {
                        case Items.Bomb:
                            if (link.bombs > 0)
                            {
                                Vector2 bombOffset = new Vector2(0, 0); ;
                                switch (direction)
                                {
                                    case Directions.Down:
                                        /*down facing animations*/
                                        bombOffset = new Vector2(0, 16);
                                        break;
                                    case Directions.Left:
                                        /*left facing animations*/
                                        bombOffset = new Vector2(-16, 0);
                                        break;
                                    case Directions.Up:
                                        /*up facing animations*/
                                        bombOffset = new Vector2(0, -16);
                                        break;
                                    case Directions.Right:
                                        /*right facing animations*/
                                        bombOffset = new Vector2(16, 0);
                                        break;
                                }
                                link.usedItem = new Bomb(link.location + bombOffset);
                                link.bombs--;
                            }                            
                            break;
                        case Items.Arrow:
                            if (link.rupees > 0)
                            {
                                link.usedItem = new Arrow(link.location, direction);
                                link.rupees--;
                            }
                            break;
                        case Items.Boomerang:
                            if (link.obtainedItems.Contains(Items.MagicBoomerang))
                                link.usedItem = new Boomerang(link, link.location, direction, true);
                            else
                                link.usedItem = new Boomerang(link, link.location, direction);
                            break;
                        case Items.Candle:
                            link.usedItem = new Candle(link.location, direction);
                            break;
                        case Items.Rod:
                            link.usedItem = new RodWave(link.location, direction);
                            break;
                    }

                    // Compute and construct link sprite using logic with direction
                    switch (direction)
                    {
                        case Directions.Down:
                            /*down facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemDownSprite();
                            
                            break;
                        case Directions.Left:
                            /*left facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemLeftSprite();
                            break;
                        case Directions.Up:
                            /*up facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemUpSprite();
                            break;
                        case Directions.Right:
                            /*right facing animations*/
                            link.linkSprite = LinkSpriteFactory.Instance.CreateLinkItemRightSprite();
                            break;
                    }
                }
            }

            public void Update(GameTime gameTime)
            {
                if (LinkAttacking)
                {
                    HandleAttack(gameTime);
                }
                else if (LinkMoving)
                {
                    HandleMovement(gameTime);
                }

                if (pushed)
                {
                    HandlePush(); 
                }
                if (LinkDamaged)
                {
                    HandleIFrames();
                }
                if (!link.living)
                {
                    HandleDeath();
                }

                IncrimentRewind();
            }
        }
		#endregion
		
        public void Update(GameTime gameTime)
        {            
            stateMachine.Update(gameTime);            
            edges = linkSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
            linkSizeOffset = new Vector2((linkSprite.Edges().Width) / 2, (linkSprite.Edges().Height) / 2);
            edges.Offset(-linkSizeOffset.X, -linkSizeOffset.Y);
            edges.Y += edges.Height / 2;
            edges.Height /= 2;

            direction = stateMachine.GetDirection();

            linkCenter = this.location + linkSizeOffset;
            if (usedItem != null)
            {
                usedItem.Update(gameTime);
                if (!usedItem.DestroyedState())
                {
                    usedItem = null;
                }
            }
            if (usedSword != null)
            {
                usedSword.Update(gameTime);
                if (!usedSword.DestroyedState())
                {
                    usedSword = null;
                }
            }
            if (usedProjectileSword != null)
            {
                usedProjectileSword.Update(gameTime);
                if (!usedProjectileSword.DestroyedState())
                {
                    usedProjectileSword = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            if (usedItem != null)
            {
                usedItem.Draw(spriteBatch);
            }
            if (usedSword != null)
            {
                usedSword.Draw(spriteBatch);
            }
            if (usedProjectileSword != null)
            {
                usedProjectileSword.Draw(spriteBatch);
            }
            linkSprite.Draw(spriteBatch, location);
        }
    }
}
