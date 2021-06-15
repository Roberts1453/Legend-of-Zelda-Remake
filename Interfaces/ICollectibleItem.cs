using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sprint0Project
{
    public enum CollectibleItems { Sword, Bomb, Arrow, Boomerang, Key, Heart, Compass, Map, Rupee};
    
    public interface ICollectibleItem
    {
        Rectangle Edges
        {
            get;
            set;
        }
        Rectangle GetEdges();
        bool DestroyedState();
        void Collect(Link link);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
