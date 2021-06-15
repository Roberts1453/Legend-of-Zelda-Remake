using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sprint0Project
{ 
    public interface IStaticBlock
    {
        Rectangle Edges
        {
            get;
            set;
        }
        Rectangle GetEdges();
        Vector2 GetLocation();

        bool GetCollide();
        void CollideAction(Link link);

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
