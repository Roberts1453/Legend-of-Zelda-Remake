using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sprint0Project
{
    public interface IMenu
    {
        Rectangle Location
        {
            get;
            set;
        }
        void SecondarySelectUp();
        void SecondarySelectDown();
        void SelectUp();
        void SelectDown();
        void SelectRight();
        void SelectLeft();
        void Select();
        void Back();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
