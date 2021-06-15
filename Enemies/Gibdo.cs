using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Gibdo : ICharacter
    {
        public Rectangle edges;
        Rectangle verticalRange;
        Rectangle horizontalRange;
        public Vector2 movement;
        public Vector2 location;
        public Vector2 startLocation;
        Vector2 GibdoCenter;
        Vector2 GibdoSizeOffset = new Vector2(5, 0);
        ISprite GibdoSprite = EnemySpriteFactory.Instance.CreateGibdoSprite();
        public IActionState state;

        public float speed;
        int damage = 4;
        int health = 16;
        int maxHealth = 16;
		int damageTimer = 100;
		bool damaged = false;
        bool living = true;
        IItem usedItem = null;
        public ICollectibleItem dropItem;
        

        public Gibdo(Vector2 location)
        {
            this.location = location;
            startLocation = location;
            GibdoCenter = location + GibdoSizeOffset;
            verticalRange = new Rectangle((int)this.location.X, (int)this.location.Y - 96, 16, 192);
            horizontalRange = new Rectangle((int)this.location.X-176, (int)this.location.Y, 352, 16);
            speed = 1;
            movement = new Vector2(1, 0);
            state = new WanderState(this);
        }
        public Gibdo(Vector2 location, ICollectibleItem dropItem)
        {
            this.location = location;
            GibdoCenter = location + GibdoSizeOffset;
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
                speed = value * 0.3f;
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
                edges = GibdoSprite.Edges();
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
            return GibdoCenter;
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
			if (!damaged) {
				damaged = true;
            	health -= damage;
			}
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
            GibdoSprite.Update(gameTime);
            edges = GibdoSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
			
			if (damaged) {
				damageTimer--;
				if (damageTimer <= 0){
					damaged = false;
					damageTimer = 100;
				}
			}

            if (health <= 0)
                living = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GibdoSprite.Draw(spriteBatch, location);
        }
    }
    
}
