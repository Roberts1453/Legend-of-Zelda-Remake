
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class SwordCurse : IItem
    {
        public Vector2 Location;
        public Vector2 startLocation;
        ISprite swordCurseCurseSprite;
        bool active = true;
        bool fullHealth = false;
        int curseTimer;
        private Rectangle edges;

        public SwordCurse()
        {
            edges = new Rectangle(0, 0, 0, 0);
            curseTimer = 90;
            SoundManager.Instance.PlaySoundEffect("Shore");
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = value;
            }
        }
        public Rectangle GetEdges()
        {
            return edges;
        }
        public int GetDamage()
        {
            return 0;
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
        }
        public void Collide()
        {
        }
        public void Collide(ICharacter enemy)
        {
        }
        


       
            
        
        public void Update(GameTime gameTime)
        {
            curseTimer--;
            if (curseTimer <= 0)
                active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
