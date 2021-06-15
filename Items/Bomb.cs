
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint0Project
{
    
    public class Bomb : IItem
    {
        public Vector2 Location;
        private BombStateMachine stateMachine;
        ISprite bombSprite = ItemSpriteFactory.Instance.CreateBombSprite();
        public bool active = true;
        public bool exploding = false;
        int damage;
        private Rectangle edges;

        public Bomb(Vector2 location)
	    {
            Location = location;
            stateMachine = new BombStateMachine(this);
            SoundManager.Instance.PlaySoundEffect("Bomb_Drop");
            damage = 0;
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = bombSprite.Edges();
            }
        }
        public Rectangle GetEdges()
        {
            edges = bombSprite.Edges();
            edges.X += (int)Location.X;
            edges.Y += (int)Location.Y;
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
        }
        public void Collide(ICharacter enemy)
        {
            enemy.TakeDamage(damage);
        }
        public void Collide(Door door)
        {
            if (exploding)
                door.BombDoor();
        }

        public class BombStateMachine
        {
            Bomb bomb;
            public int fuse = 1000; //total lifespan of bomb 1 sec, explosion happens before that, smoke fades at 1
            public bool lit = true;
            

            public BombStateMachine(Bomb bomb)
            {
                this.bomb = bomb;
            }

            public void Explode()
            {
                if (!bomb.exploding)
                {
                    bomb.exploding = true;
                    SoundManager.Instance.PlaySoundEffect("Bomb_BlowUp");
                    //screen flash
                    bomb.bombSprite = ItemSpriteFactory.Instance.CreateSmokeSprite();

                    //no collision
                    bomb.damage = 2;
                }
            }


            public void Update(GameTime gameTime)
            {
                if (fuse > 0)
                {
                    fuse -= gameTime.ElapsedGameTime.Milliseconds;
                    if (fuse <= 250 && fuse > 0)
                        Explode();
                    else if (fuse <= 0)
                        bomb.active = false;
                    
                }

                
            }
        }
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                stateMachine.Update(gameTime);
            }
            bombSprite.Update(gameTime);

            edges = bombSprite.Edges();
            edges.X += (int)Location.X;
            edges.Y += (int)Location.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                bombSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
