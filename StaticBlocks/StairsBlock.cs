using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class StairsBlock : IStaticBlock

    {
        public Vector2 Location;
        ISprite stairsBlockSprite = DungeonSpriteFactory.Instance.CreateStairsSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = false;
        int roomNum;
        Vector2 exitLocation;

        public StairsBlock(Vector2 location, int roomNum, Vector2 exitLocation)
        {
            Location = location;
            this.roomNum = roomNum;
            this.exitLocation = exitLocation;
            edges = new Rectangle((int)Location.X + 6, (int)Location.Y + 6, 4, 4);
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {

        }
        public void CollideAction(Link link, Camera cam)
        {
            SoundManager.Instance.PlaySoundEffect("Stairs");
            cam.UseStairs(roomNum, exitLocation, link);
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = new Rectangle((int)Location.X + 6, (int)Location.Y + 6, 4, 4);
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                stairsBlockSprite.Draw(spriteBatch, Location);
            }
        }
    }
}
