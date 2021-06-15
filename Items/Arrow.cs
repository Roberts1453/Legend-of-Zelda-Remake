
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Arrow : IItem
    {
        public Vector2 Location;
        Vector2 startLocation;
        private ArrowStateMachine stateMachine;
        ISprite arrowSprite = ItemSpriteFactory.Instance.CreateArrowUpSprite();
        bool active = true;
        int damage = 2;
        private Rectangle edges;

        public Arrow(Vector2 location, Directions direction)
        {
            Location = location;
            startLocation = Location;
            stateMachine = new ArrowStateMachine(this, direction);
            SoundManager.Instance.PlaySoundEffect("Arrow_Boomerang");
        }

        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = arrowSprite.Edges();
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
            active = false;
        }
        public void Collide(ICharacter enemy)
        {
            enemy.TakeDamage(damage);
            active = false;
        }
        public void BeMoving()
        {
            stateMachine.BeMoving();
        }




        public class ArrowStateMachine
        {
            Arrow arrow;
            Directions direction = Directions.Right;
            private bool ArrowMoving = true;

            public ArrowStateMachine(Arrow arrow, Directions direction)
            {
                this.arrow = arrow;
                this.direction = direction;
                BeMoving();
            }

            public void BeMoving()
            {
                ArrowMoving = true;
                
                switch (direction) // Compute and construct arrow sprite using logic with direction
                {
                    case Directions.Down:
                        /*down facing animations*/
                        arrow.arrowSprite = ItemSpriteFactory.Instance.CreateArrowDownSprite();
                        break;
                    case Directions.Left:
                        /*left facing animations*/
                        arrow.arrowSprite = ItemSpriteFactory.Instance.CreateArrowLeftSprite();
                        break;
                    case Directions.Up:
                        /*Draw up facing animations*/
                        arrow.arrowSprite = ItemSpriteFactory.Instance.CreateArrowUpSprite();
                        break;
                    case Directions.Right:
                        /*Draw right facing animations*/
                        arrow.arrowSprite = ItemSpriteFactory.Instance.CreateArrowRightSprite();
                        break;
                }
            }

            public void Update(GameTime gameTime)
            {
                arrow.edges.X = (int)arrow.Location.X;
                arrow.edges.Y = (int)arrow.Location.Y;
                // Switch logic based on the current values of direction to determine how to move
                if (ArrowMoving)
                {
                    arrow.arrowSprite.Update(gameTime);
                    switch (direction)
                    {
                        case Directions.Down:
                            /*Move down*/
                            arrow.Location.Y += 3;
                            break;
                        case Directions.Left:
                            /*Move left*/
                            arrow.Location.X -= 3;
                            break;
                        case Directions.Up:
                            /*Move up*/
                            arrow.Location.Y -= 3;
                            break;
                        case Directions.Right:
                            /*Move right*/
                            arrow.Location.X += 3;
                            break;

                    }
                }
                if (arrow.Location.X < arrow.startLocation.X - 128 || arrow.Location.X > arrow.startLocation.X + 128 || arrow.Location.Y < arrow.startLocation.Y - 100 || arrow.Location.Y > arrow.startLocation.Y + 100)
                {
                    arrow.active = false;
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            stateMachine.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            arrowSprite.Draw(spriteBatch, Location);
        }
    }
}
