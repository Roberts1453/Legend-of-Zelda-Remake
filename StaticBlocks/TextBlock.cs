using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class TextBlock : IStaticBlock

    {
        public Vector2 Location;
        public bool active = true;
        Rectangle edges;
        bool collide = true;
        Text textObject;

        public TextBlock(Vector2 location, string text)
        {
            Location = location;
            textObject = new Text(text, location, true);
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {

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
        public Vector2 GetLocation()
        {
            return Location;
        }

        public void Update(GameTime gameTime)
        {
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            if (active)
            {
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                textObject.Draw(spriteBatch);
            }
        }
    }
}
