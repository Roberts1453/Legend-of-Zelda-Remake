using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint0Project
{

    public class Navi : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 naviCenter;
        Vector2 naviSizeOffset = new Vector2(5, -2);
        ISprite naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
        ISprite newHintSprite = LinkSpriteFactory.Instance.CreateNaviExclamationSprite();
        public IActionState state;
        Directions direction1;
        Directions direction2;

        public List<Type> knownTypes = new List<Type>();
        public Dictionary<Type, string> hintMap = new Dictionary<Type, string>();
        public Stack<string> hintStack = new Stack<string>();

        public Queue<ICharacter> currentEnemies = new Queue<ICharacter>();
        public Queue<IStaticBlock> currentBlocks = new Queue<IStaticBlock>();
        public Queue<ICollectibleItem> currentCollectibles = new Queue<ICollectibleItem>();

        public List<Type> blocksOfInterest = new List<Type>() { typeof(WaterBlock), typeof(MoveableBlock), typeof(BlackSquareBlock), typeof(StairsBlock), typeof(DungeonDoor), typeof(Door) };


        public float speed;
        float hoverHeight = 0;
        float hoverInc = 0.3f;
        int hoverMax = 5;        
        int damage = 0;
        bool damaged = false;
        bool living = true;
        bool newHint = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;

        Link owner;


        public Navi(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            naviCenter = location + naviSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 176, (int)this.location.Y, 352, 16);
            Speed = 1;
            movement = new Vector2(0, 1);
            state = new WanderState(this);
        }
        public Navi(Vector2 location, Link link)
        {
            this.location = location;
            startLocation = location;
            naviCenter = location + naviSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X - 32, (int)this.location.Y - 30, 64, 60);
            Speed = 1;
            movement = new Vector2(0, 1);
            state = new WanderState(this);
            owner = link;
            ConstructHints();
            hintStack.Push("HEY! LISTEN! I'M NAVI! I'MHERE TO HELP! PRESS SPACE IF YOU HAVE ANY QUESTIONS!");
        }
        public Navi(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            naviCenter = location + naviSizeOffset;
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
                edges = naviSprite.Edges();
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
            return naviCenter;
        }
        public bool GetLiving()
        {
            return living;
        }
        public void DetectVert(Vector2 target)
        {
        }
        public void DetectHoriz(Vector2 target)
        {
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
            direction1 = direction;
            Directions rndDirection = (Directions)Randomizer.Instance.Next(0, 4);
            float rndSpeed = (float)Randomizer.Instance.Next(0, 101) / 100f;
            switch (direction) {
                case Directions.Up:
                    if (rndDirection == Directions.Left)
                    {
                        movement += WanderState.leftVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
                    }
                    else if (rndDirection == Directions.Right)
                    {
                        movement += WanderState.rightVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviRightSprite();
                    }
                    break;
                case Directions.Down:
                    if (rndDirection == Directions.Left)
                    {
                        movement += WanderState.leftVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
                    }
                    else if (rndDirection == Directions.Right)
                    {
                        movement += WanderState.rightVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviRightSprite();
                    }
                    break;
                case Directions.Left:
                    if (rndDirection == Directions.Up)
                    {
                        movement += WanderState.upVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
                    }
                    else if (rndDirection == Directions.Down)
                    {
                        movement += WanderState.downVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
                    }
                    break;
                case Directions.Right:
                    if (rndDirection == Directions.Up)
                    {
                        movement += WanderState.upVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviRightSprite();
                    }
                    else if (rndDirection == Directions.Down)
                    {
                        movement += WanderState.downVector * rndSpeed;
                        direction2 = rndDirection;
                        naviSprite = LinkSpriteFactory.Instance.CreateNaviRightSprite();
                    }
                    break;
            }
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
        public void ConstructHints()
        {
            hintMap = new Dictionary<Type, string>()
            {
                {typeof(Octorok), "THESE ARE OCTOROKS. THEY  SHOOT ROCKS!" },
                {typeof(Moblin), "MOBLINS WILL SHOOT ARROWS AT YOU!" },
                {typeof(Stalfos), "THESE STALFOS WANDER AIM-LESSLY. DON'T LET THEM HIT YOU!" },
                {typeof(Keese), "KEESE ARE BATS THAT HANG  OUT IN DUNGEONS. THEY DIE IN ONE HIT!" },
                {typeof(Goriya), "GORIYAS LOVE TO THROW BOOMERANGS. MAYBE YOU CAN TAKE ONE FROM THEM!" },
                {typeof(Gel), "GELS ARE THE EASIEST ENEMIES. ONE HIT SHOULD TAKE CARE OF THEM!" },
                {typeof(Aquamentus), "THAT'S AN AQUAMENTUS.     WATCH OUT FOR THOSE FIRE- BALLS! THIS ONE WILL TAKE A LOT OF DAMAGE" },
                {typeof(BladeTrap), "STAY CLEAR OF BLADE TRAPS! THEY WILL CHARGE YOU IF YOU CROSS THEM!" },
                {typeof(Rope), "THESE ROPES ARE LIKE WANDERING BLADE TRAPS. THEY WILL CHARGE AT YOU IF THEY SEE YOU, BUT ONE HIT FROM YOUR SWORD SHOULD KILL THEM!" },
                {typeof(Zol), "THESE ZOLS CAN SPLIT INTO TWO GELS!" },
                {typeof(Gibdo), "THESE GIBDOS ARE STRONG   BUT SLOW!" },
                {typeof(Moldorm), "YOU HAVE TO KILL EVERY    PART OF THE MOLDORM!" },
                {typeof(Vire), "VIRES TURN INTO TWO RED   KEESE WHEN THEY DIE!" },
                {typeof(MoveableBlock), "MAYBE THERE'S A SECRET    SWITCH. TRY PUSHING SOMETHING!" },
                {typeof(Key), "A KEY! IT CAN OPEN LOCKED DOORS, BUT IT WILL ONLY   WORK ONCE!" },
                {typeof(Heart), "HEARTS WILL RESTORE YOUR  LOST HEALTH!" },
                {typeof(HeartContainer), "HEART CONTAINERS WILL INCREASE YOUR HEALTH!" },
                {typeof(Rupee), "THAT IS A RUPEE! IT CAN BEUSED TO BUY THINGS OR     SHOOT ARROWS!" },
                {typeof(BoomerangCollectible), "WITH THIS BOOMERANG, YOU  CAN STUN ENEMIES FROM A DISTANCE!" },
                {typeof(LadderCollectible), "WITH THIS LADDER, YOU  CAN CROSS SMALL GAPS!" },
                {typeof(Bow), "YOU WILL NEED ARROWS TO   USE THIS BOW!" }
            };
        }
        public void RemoveDoor()
        {
            Stack<string> tmp = new Stack<string>();
            int stackSize = hintStack.Count;
            for (int i = 0; i < stackSize; i++)
            {
                string hint = hintStack.Pop();
                if (!hint.Contains("THIS DOOR"))
                    tmp.Push(hint);
            }
            stackSize = tmp.Count;
            for (int i = 0; i < stackSize; i++)
                hintStack.Push(tmp.Pop());
        }
        public void CollectHint(IStaticBlock block)
        {
            string newHint = "";
            if (block.GetType() == typeof(DungeonDoor))
            {
                RemoveDoor();
                DungeonDoor dungeonDoor = (DungeonDoor)block;
                if (dungeonDoor.dungeonNum == 0)
                    newHint = "THIS DOOR WILL TAKE YOU TOTHE OVERWORLD";
                else
                {
                    if (dungeonDoor.roomNum <= 1)
                        newHint = "THIS DOOR WILL TAKE YOU TOLEVEL " + dungeonDoor.dungeonNum.ToString();
                    else
                        newHint = "THIS DOOR WILL TAKE YOU TOTHE END OF LEVEL " + dungeonDoor.dungeonNum.ToString();
                }
            }
            else if (block.GetType() == typeof(Door))
            {
                RemoveDoor();
                Door door = (Door)block;
                if (door.doorState == Door.DoorState.Locked)
                    newHint = "THIS DOOR IS LOCKED! DO   YOU HAVE ANY KEYS?";
                else if (door.doorState == Door.DoorState.Bombable)
                    newHint = "THIS WALL LOOKS WEAK. MAY-BE YOU CAN BREAK IT OPEN WITH SOMETHING!";

            }
            if (newHint.Length > 0)
                NewHint(newHint);
            else if (!knownTypes.Contains(block.GetType()) && hintMap.ContainsKey(block.GetType()))
            {
                NewHint(hintMap[block.GetType()]);
            }
        }

        public void ContactObject(ICharacter enemy)
        {
            currentEnemies = new Queue<ICharacter>();
            if (!currentEnemies.Contains(enemy))
            {
                bool seen = false;
                foreach (ICharacter seenBlock in currentEnemies)
                    if (seenBlock.GetType() == enemy.GetType())
                        seen = true;
                if (!seen)
                    currentEnemies.Enqueue(enemy);
            }
        }
        public void ContactObject(IStaticBlock block)
        {
            if (blocksOfInterest.Contains(block.GetType()))
            {
                if (!currentBlocks.Contains(block)) {
                    bool seen = false;
                    foreach (IStaticBlock seenBlock in currentBlocks)
                        if (seenBlock.GetType() == block.GetType())
                            seen = true;
                    if (!seen)
                        currentBlocks.Enqueue(block);
                }
            }
            
        }
        public void ContactObject(ICollectibleItem item)
        {
            if (!currentCollectibles.Contains(item))
            {
                bool seen = false;
                foreach (ICollectibleItem seenItem in currentCollectibles)
                    if (seenItem.GetType() == item.GetType())
                        seen = true;
                if (!seen)
                    currentCollectibles.Enqueue(item);
            }
        }
        public void NewHint(string addHint)
        {
            if (!hintStack.Contains(addHint))
            hintStack.Push(addHint);
            if (!newHint)
                SoundManager.Instance.PlaySoundEffect("Navi_Hey");
            newHint = true;
        }
        public void GiveHint()
        {
            SoundManager.Instance.PlaySoundEffect("Navi_Listen");
            if (hintStack.Count > 0)
                Game1.game.textbox.AddText(hintStack.Pop());
            else
                Game1.game.textbox.AddText("DO YOU NEED SOMETHING?");
            newHint = false;
        }
        public void VisitType(Type newType)
        {
            knownTypes.Add(newType);
        }
        public void Update(GameTime gameTime)
        {
            horizontalRange = new Rectangle((int)this.location.X - 32, (int)this.location.Y - 30, 64, 60);

            if (state != null)
            	state.Update(gameTime);
			if (owner != null) {
				if (Vector2.Distance(location, owner.Location) > 32) {
					movement = owner.Location - location;
					movement.Normalize();
					location += movement * speed;
				}
                if (Vector2.Distance(location, owner.Location) > 255)
                {
                    location = owner.Location;
                }
            }

            location.Y -= hoverInc;
            hoverHeight += Math.Abs(hoverInc);
            if (hoverHeight >= hoverMax)
            {
                hoverInc *= -1;
                hoverHeight = 0;
            }

            if (movement.X < 0 && naviSprite.GetType() != typeof(NaviLeftSprite))
                naviSprite = LinkSpriteFactory.Instance.CreateNaviLeftSprite();
            else if (movement.X > 0 && naviSprite.GetType() != typeof(NaviRightSprite))
                naviSprite = LinkSpriteFactory.Instance.CreateNaviRightSprite();

            naviCenter = this.location + naviSizeOffset;
            naviSprite.Update(gameTime);
            newHintSprite.Update(gameTime);
            edges = naviSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;

            if (currentCollectibles.Count > 0)
            {
                ICollectibleItem currentCollectible = currentCollectibles.Dequeue();
                if (!knownTypes.Contains(currentCollectible.GetType()))
                {
                    newHint = true;
                    VisitType(currentCollectible.GetType());
                    if (hintMap.ContainsKey(currentCollectible.GetType()))
                        NewHint(hintMap[currentCollectible.GetType()]);
                }
            }
            if (currentBlocks.Count > 0)
            {
                IStaticBlock currentBlock = currentBlocks.Dequeue();
                CollectHint(currentBlock);
                    VisitType(currentBlock.GetType());
            }
            if (currentEnemies.Count > 0)
            {
                ICharacter currentEnemy = currentEnemies.Dequeue();
                if (!knownTypes.Contains(currentEnemy.GetType()))
                {
                    newHint = true;
                    VisitType(currentEnemy.GetType());
                    if (hintMap.ContainsKey(currentEnemy.GetType()))
                        NewHint(hintMap[currentEnemy.GetType()]);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            naviSprite.Draw(spriteBatch, location);
            if (newHint)
                newHintSprite.Draw(spriteBatch, location + naviSizeOffset);
        }
    }
    
}
