
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Sword : IItem
    {
        public Vector2 Location;
        public Vector2 startLocation;
        private SwordStateMachine stateMachine;
        ISprite swordSprite;
        bool active = true;
        bool fullHealth = false;
        int damage = 2;
        private Rectangle edges;
        private Vector2 swordSizeOffset;
        public enum SwordType { Wood, White, Magical};
        SwordType swordType;

        public Sword(Vector2 location, Directions direction, bool fullHealth)
        {
            setSprite(direction);
            Location = setLocation(location, direction);
            startLocation = Location;
            stateMachine = new SwordStateMachine(this, direction);
            this.fullHealth = fullHealth;
            SoundManager.Instance.PlaySoundEffect("Sword_Slash");
        }
        public Sword(Vector2 location, Directions direction, bool fullHealth, SwordType swordType)
        {
            this.swordType = swordType;
            damage = (int)Math.Pow(2, (int)swordType + 1);
            setSprite(direction);
            Location = setLocation(location, direction);
            startLocation = Location;
            stateMachine = new SwordStateMachine(this, direction);
            this.fullHealth = fullHealth;
            SoundManager.Instance.PlaySoundEffect("Sword_Slash");
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = swordSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public int GetDamage()
        {
            return damage;
        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public bool DestroyedState()
        {
            return active;
        }
        public void BeMoving()
        {
            stateMachine.BeMoving();
        }
        public void Collide()
        {
            active = false;
        }
        public void Collide(ICharacter enemy)
        {
            enemy.TakeDamage(damage);
            active = false;
        }
        public Vector2 setLocation(Vector2 location, Directions direction)
        {
            Vector2 swordOffset = new Vector2(0, 0);
            int width = swordSprite.Edges().Width;
            int height = swordSprite.Edges().Height;

            switch (direction)
            {
                case Directions.Down:
                    /*down facing animations*/
                    swordOffset = new Vector2(-2, 4);
                    break;
                case Directions.Left:
                    /*left facing animations*/
                    swordOffset = new Vector2(-19, -1);
                    break;
                case Directions.Up:
                    /*up facing animations*/
                    swordOffset = new Vector2(-4, -19);
                    break;
                case Directions.Right:
                    /*right facing animations*/
                    swordOffset = new Vector2(5, -1);
                    break;
            }
            location += swordOffset;

            edges = swordSprite.Edges();
            edges.X = (int)location.X;
            edges.Y = (int)location.Y;
            swordSizeOffset = new Vector2((swordSprite.Edges().Width) / 2, (swordSprite.Edges().Height) / 2);
            edges.Offset(-swordSizeOffset.X, -swordSizeOffset.Y);

            return location;
        }

        public void setSprite(Directions direction)
        {
            switch (direction) // Compute and construct sword sprite using logic with direction
            {
                case Directions.Down:
                    /*down facing animations*/
                    swordSprite = ItemSpriteFactory.Instance.CreateSwordDownSprite();
                    break;
                case Directions.Left:
                    /*left facing animations*/
                    swordSprite = ItemSpriteFactory.Instance.CreateSwordLeftSprite();
                    break;
                case Directions.Up:
                    /*Draw up facing animations*/
                    swordSprite = ItemSpriteFactory.Instance.CreateSwordUpSprite();
                    break;
                case Directions.Right:
                    /*Draw right facing animations*/
                    swordSprite = ItemSpriteFactory.Instance.CreateSwordRightSprite();
                    break;
            }
        }


        public class SwordStateMachine
        {
            Sword sword;
            Directions direction = Directions.Down;
            private bool SwordMoving = false;
            int speed = 3;
            int attackTimer = 250;

            public SwordStateMachine(Sword sword, Directions direction)
            {
                this.sword = sword;
                this.direction = direction;
            }

            public void BeMoving()
            {
                SoundManager.Instance.PlaySoundEffect("Sword_Shoot");
                SwordMoving = true;
                
                switch (direction) // Compute and construct sword sprite using logic with direction
                {
                    case Directions.Down:
                        /*down facing animations*/
                        sword.swordSprite = ItemSpriteFactory.Instance.CreateWhiteSwordDownSprite();
                        break;
                    case Directions.Left:
                        /*left facing animations*/
                        sword.swordSprite = ItemSpriteFactory.Instance.CreateWhiteSwordLeftSprite();
                        break;
                    case Directions.Up:
                        /*Draw up facing animations*/
                        sword.swordSprite = ItemSpriteFactory.Instance.CreateWhiteSwordUpSprite();
                        break;
                    case Directions.Right:
                        /*Draw right facing animations*/
                        sword.swordSprite = ItemSpriteFactory.Instance.CreateWhiteSwordRightSprite();
                        break;
                }
            }

            public void Update(GameTime gameTime)
            {
                sword.edges.X = (int)sword.Location.X;
                sword.edges.Y = (int)sword.Location.Y;
                // Switch logic based on the current values of direction to determine how to move
                if (SwordMoving)
                {
                    sword.swordSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            /*Move down*/
                            sword.Location.Y += speed;
                            break;
                        case Directions.Left:
                            /*Move left*/
                            sword.Location.X -= speed;
                            break;
                        case Directions.Up:
                            /*Move up*/
                            sword.Location.Y -= speed;
                            break;
                        case Directions.Right:
                            /*Move right*/
                            sword.Location.X += speed;
                            break;

                    }
                }
                else
                {
                    attackTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (attackTimer <= 0)
                    {
                        if (sword.fullHealth) { 
                            BeMoving();
                        }
                        else
                        {
                            sword.active = false;
                        }
                    }
                }
                if (sword.Location.X < sword.startLocation.X - 128 || sword.Location.X > sword.startLocation.X + 128 || sword.Location.Y < sword.startLocation.Y - 100 || sword.Location.Y > sword.startLocation.Y + 100)
                {
                    sword.active = false;
                }
            }
        }
        
        public void Update(GameTime gameTime)
        {
            stateMachine.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            swordSprite.Draw(spriteBatch, Location);
        }
    }
}
