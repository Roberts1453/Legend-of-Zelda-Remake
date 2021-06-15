using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint0Project
{
    public enum Items { None, Sword, Bomb, Bow, Arrow, Boomerang, Candle, Rod, Raft, Ladder, MagicKey, MagicBoomerang, SilverArrow };
    
    public interface IItem
    {
        Rectangle Edges
        {
            get;
            set;
        }
        Rectangle GetEdges();
        Vector2 GetLocation();
        int GetDamage();
        bool DestroyedState();
        void Collide();
        void Collide(ICharacter character);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
