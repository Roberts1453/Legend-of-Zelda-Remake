
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Boomerang : IItem
    {
        ICharacter owner;
        public Vector2 Location;
        private BoomerangStateMachine stateMachine;
        ISprite boomerangSprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
        public bool active = true;
        int damage = 0;
        private Rectangle edges;
        SoundEffectInstance boomerangSound;

        public Boomerang(ICharacter owner, Vector2 location, Directions direction)
        {
            this.owner = owner;
            Location = location;
            boomerangSprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
            stateMachine = new BoomerangStateMachine(this, direction);
            boomerangSound = SoundManager.Instance.PlaySoundEffectLoop("Boomerang");            
        }
        public Boomerang(ICharacter owner, Vector2 location, Directions direction, bool magic) //Magic Boomerang
        {
            this.owner = owner;
            Location = location;
            stateMachine = new BoomerangStateMachine(this, direction);
            if (magic)
            {
                boomerangSprite = ItemSpriteFactory.Instance.CreateMagicBoomerangSprite();
                stateMachine.flightTimer *= 2;
            }
            else
                boomerangSprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
            boomerangSound = SoundManager.Instance.PlaySoundEffectLoop("Boomerang");
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = boomerangSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public int GetDamage()
        {
            return damage;
        }

        public bool DestroyedState()
        {
            return active;
        }

        public void Collide()
        {
            stateMachine.BeReturning();
        }
        public void Collide(ICharacter enemy)
        {
                enemy.State = new StunState(enemy, enemy.State);
            stateMachine.BeReturning();
        }
		public void Collide(Link link)
        {
            link.TakeDamage(owner.GetDamage());
            stateMachine.BeReturning();
        }
        public void Destroy()
        {
            SoundManager.Instance.StopSoundEffectLoop(boomerangSound);
            //boomerangSound.Stop();
            active = false;
        }

        public class BoomerangStateMachine
        {
            Boomerang boomerang;
            Directions direction = Directions.Down;
            int speed = 2;
            public int flightTimer = 500;
            private bool reversed = false;
            Vector2 ownerLocation;
            Vector2 towardOwner;


            public BoomerangStateMachine(Boomerang boomerang, Directions direction)
            {
                this.boomerang = boomerang;
                this.direction = direction;
                boomerang.boomerangSprite = ItemSpriteFactory.Instance.CreateBoomerangSprite();
            }           

            public void BeReturning()
            {
                reversed = true;
            }

            public void Locate(Vector2 ownerLocation)
            {
                this.ownerLocation = ownerLocation;
            }

            public void Update(GameTime gameTime)
            {
                boomerang.edges.X = (int)boomerang.Location.X;
                boomerang.edges.Y = (int)boomerang.Location.Y;
                // switch logic based on the current values of direction to determine how to move
                if (reversed)
                {
                    ownerLocation = boomerang.owner.GetLocation();
                    if (ownerLocation.X > boomerang.Location.X)
                    {
                        towardOwner.X = 2;
                    }
                    else if (ownerLocation.X < boomerang.Location.X)
                    {
                        towardOwner.X = -2;
                    }
                    else
                    {
                        towardOwner.X = 0;
                    }
                    if (ownerLocation.Y > boomerang.Location.Y)
                    {
                        towardOwner.Y = 2;
                    }
                    else if (ownerLocation.Y < boomerang.Location.Y)
                    {
                        towardOwner.Y = -2;
                    }
                    else
                    {
                        towardOwner.Y = 0;
                    }
                    boomerang.boomerangSprite.Update(gameTime);
                    boomerang.Location += towardOwner;
                }
                else
                {
                    boomerang.boomerangSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            /*Move down*/
                            boomerang.Location.Y += speed;
                            break;
                        case Directions.Left:
                            /*Move left*/
                            boomerang.Location.X -= speed;
                            break;
                        case Directions.Up:
                            /*Move up*/
                            boomerang.Location.Y -= speed;
                            break;
                        case Directions.Right:
                            /*Move right*/
                            boomerang.Location.X += speed;
                            break;

                    }
                }
                
                if (speed > -2)
                {
                    flightTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (flightTimer <= 0)
                    {
                        speed--;
                        flightTimer = 100;
                    }
                    if (speed <= 0)
                    {
                        BeReturning();
                    }
                }
                /*if (boomerang.Location.X < 0 || boomerang.Location.X > 256 || boomerang.Location.Y < 0 || boomerang.Location.Y > 200)
                {
                    BeReturning();
                }*/
                /*if (boomerang.owner.Edges.Intersects(boomerang.boomerangSprite.Edges()))
                {
                    boomerang.active = false;
                }*/
                if (Math.Abs(ownerLocation.X - boomerang.Location.X) <=2 && Math.Abs(ownerLocation.Y - boomerang.Location.Y) <= 2)
                {
                   boomerang.Destroy();
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            stateMachine.Update(gameTime);
            boomerangSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            boomerangSprite.Draw(spriteBatch, Location);
        }
    }
}
