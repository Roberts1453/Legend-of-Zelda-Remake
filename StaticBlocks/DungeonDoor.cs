using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class DungeonDoor : IStaticBlock

    {
        public Vector2 Location;
        ISprite dungeonDoorSprite = DungeonSpriteFactory.Instance.CreateBlackSquareBlockSprite();
        public bool active = true;
        Rectangle edges;
        bool collide = false;
        public int roomNum;
		public int dungeonNum;
        Vector2 exitLocation;

        public DungeonDoor(Vector2 location, int dungeonNum, int roomNum, Vector2 exitLocation)
        {
            Location = location;
			this.dungeonNum = dungeonNum;
            this.roomNum = roomNum;
            this.exitLocation = exitLocation;
			edges = new Rectangle((int) location.X, (int) location.Y, 32, 16);
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {

        }
        public void CollideAction(Link link, Game1 game)
        {
			game.ChangeDungeon(dungeonNum, roomNum, exitLocation);
            //game.cam.UseStairs(roomNum, exitLocation, link);
        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = new Rectangle((int)Location.X, (int)Location.Y, 32, 16);
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
            dungeonDoorSprite.Draw(spriteBatch, Location);
        }
    }
}
