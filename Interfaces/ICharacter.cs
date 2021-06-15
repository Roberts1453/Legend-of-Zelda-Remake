using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sprint0Project
{
    public enum Directions { Down, Left, Up, Right };
    public interface ICharacter
    {
        float Speed
        {
            get;
            set;
        }
        Vector2 Movement
        {
            get;
            set;
        }
        Vector2 Location
        {
            get;
            set;
        }
        IActionState State
        {
            get;
            set;
        }

        void CollideAction();
        Rectangle Edges
        {
            get;
            set;
        }
        Rectangle GetEdges();
        Vector2 GetLocation();
        IItem GetItems();
        ICollectibleItem GetDropItem();
        int GetDamage();
        bool GetLiving();
        void BeMoving();
        void ChangeDirection();
        void ChangeDirection(Directions direction);
        Rectangle GetVertRange();
        Rectangle GetHorizRange();
        void DetectVert(Vector2 target);
        void DetectHoriz(Vector2 target);
        void Push(Directions direction);
        void Stop();
        void MoveDown();
        void MoveLeft();
        void MoveUp();
        void MoveRight();
        void TakeDamage(int damage);
        bool IsDamaged();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
