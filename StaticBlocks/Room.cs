using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class Room : IStaticBlock

    {
        public Vector2 Location;
        ISprite roomSprite = DungeonSpriteFactory.Instance.CreateBackgroundSprite();
        Rectangle edges;
        bool collide = false;

        public enum RoomType { Open, OldMan, ItemRoom, Passage, OldManCave, Overworld};
        RoomType roomType = RoomType.Open;

        List<Rectangle> walls;
        List<Rectangle> doors;

        Rectangle topUpperLeftWall;
        Rectangle leftUpperLeftWall;

        Rectangle topUpperRightWall;
        Rectangle rightUpperRightWall;

        Rectangle bottomLowerLeftWall;
        Rectangle leftLowerLeftWall;

        Rectangle bottomLowerRightWall;
        Rectangle rightLowerRightWall;

        Rectangle leftDoor;
        Rectangle topDoor;
        Rectangle rightDoor;
        Rectangle bottomDoor;

        int wallHeight = 32;
        int sideWallHeight = 32;
        int wallLength = 120;
        int sideWallLength = 88;
        int doorWidth = 16;
        int sideDoorWidth = 8;
        int roomWidth = 192;
        int roomHeight = 112;
        public Room(Vector2 location)
        {
            Location = location;
            topUpperLeftWall = new Rectangle((int) location.X, (int) location.Y, wallLength, wallHeight);
            leftUpperLeftWall = new Rectangle((int)location.X, (int)location.Y, wallHeight, sideWallLength);

            topUpperRightWall = new Rectangle((int)location.X + wallLength+doorWidth, (int)location.Y, wallLength, wallHeight);
            rightUpperRightWall = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y, wallHeight, sideWallLength);

            bottomLowerLeftWall = new Rectangle((int)location.X, (int)location.Y + wallHeight + roomHeight, wallLength, wallHeight);
            leftLowerLeftWall = new Rectangle((int)location.X, (int)location.Y + sideWallLength + doorWidth, wallHeight, sideWallLength);

            bottomLowerRightWall = new Rectangle((int)location.X + wallLength + doorWidth, (int)location.Y+wallHeight+roomHeight, wallLength, wallHeight);
            rightLowerRightWall = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y + sideWallLength + doorWidth, wallHeight, sideWallLength);

            leftDoor = new Rectangle((int)location.X, (int)location.Y + sideWallLength, wallHeight, doorWidth);
            rightDoor = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y + sideWallLength, wallHeight, doorWidth);

            topDoor = new Rectangle((int)location.X + wallLength, (int)location.Y, doorWidth, wallHeight);
            bottomDoor = new Rectangle((int)location.X + wallLength, (int)location.Y + wallHeight + roomHeight, doorWidth, wallHeight);

            walls = new List<Rectangle>();
            doors = new List<Rectangle>();
            walls.Add(topUpperLeftWall);
            walls.Add(leftUpperLeftWall);

            walls.Add(topUpperRightWall);
            walls.Add(rightUpperRightWall);

            walls.Add(bottomLowerLeftWall);
            walls.Add(leftLowerLeftWall);

            walls.Add(bottomLowerRightWall);
            walls.Add(rightLowerRightWall);

            doors.Add(leftDoor);
            doors.Add(rightDoor);

            doors.Add(topDoor);
            doors.Add(bottomDoor);
        }
        public Room(Vector2 location, RoomType roomType)
        {
            Location = location;
            this.roomType = roomType;
            walls = new List<Rectangle>();
            doors = new List<Rectangle>();

            if (roomType == RoomType.ItemRoom || roomType == RoomType.Passage)
            {
                roomSprite = DungeonSpriteFactory.Instance.CreateUndergroundSprite();
                Rectangle LeftWall = new Rectangle((int)location.X, (int)location.Y, wallHeight, wallLength + wallHeight);

                //Rectangle TopWall = new Rectangle((int)location.X, (int)location.Y, wallLength, 1);

                Rectangle RightWall = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y, wallHeight, 2* sideWallLength + sideDoorWidth);

                Rectangle BottomWall = new Rectangle((int)location.X + wallHeight, (int)location.Y + 2* doorWidth + roomHeight, roomWidth, wallHeight);

                walls = new List<Rectangle>();
                doors = new List<Rectangle>();

                walls.Add(LeftWall);
                walls.Add(RightWall);
                walls.Add(BottomWall);
            }
            else if (roomType == RoomType.Overworld)
            {
                roomSprite = OverworldSpriteFactory.Instance.CreateLandBackgroundSprite();
                leftDoor = new Rectangle((int)location.X, (int)location.Y, wallHeight, 176);
                rightDoor = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y, wallHeight, 176);

                topDoor = new Rectangle((int)location.X, (int)location.Y, 256, wallHeight);
                bottomDoor = new Rectangle((int)location.X, (int)location.Y + wallHeight + roomHeight, 256, wallHeight);

                walls = new List<Rectangle>();
                doors = new List<Rectangle>();

                doors.Add(leftDoor);
                doors.Add(rightDoor);

                doors.Add(topDoor);
                doors.Add(bottomDoor);
            }
            else
            {
                if (roomType == RoomType.OldMan)
                    roomSprite = DungeonSpriteFactory.Instance.CreateBackgroundSecretSprite();
                topUpperLeftWall = new Rectangle((int)location.X, (int)location.Y, wallLength, wallHeight);
                leftUpperLeftWall = new Rectangle((int)location.X, (int)location.Y, wallHeight, sideWallLength-2);

                topUpperRightWall = new Rectangle((int)location.X + wallLength + doorWidth, (int)location.Y, wallLength, wallHeight);
                rightUpperRightWall = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y, wallHeight, sideWallLength-2);

                bottomLowerLeftWall = new Rectangle((int)location.X, (int)location.Y + wallHeight + roomHeight, wallLength, wallHeight);
                leftLowerLeftWall = new Rectangle((int)location.X, (int)location.Y + sideWallLength + sideDoorWidth, wallHeight, sideWallLength);

                bottomLowerRightWall = new Rectangle((int)location.X + wallLength + doorWidth, (int)location.Y + wallHeight + roomHeight, wallLength, wallHeight);
                rightLowerRightWall = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y + sideWallLength + sideDoorWidth, wallHeight, sideWallLength);

                leftDoor = new Rectangle((int)location.X, (int)location.Y + sideWallLength, wallHeight, doorWidth);
                rightDoor = new Rectangle((int)location.X + sideWallHeight + roomWidth, (int)location.Y + sideWallLength, wallHeight, doorWidth);

                topDoor = new Rectangle((int)location.X + wallLength, (int)location.Y, doorWidth, wallHeight);
                bottomDoor = new Rectangle((int)location.X + wallLength, (int)location.Y + wallHeight + roomHeight, doorWidth, wallHeight);

                walls = new List<Rectangle>();
                doors = new List<Rectangle>();
                walls.Add(topUpperLeftWall);
                walls.Add(leftUpperLeftWall);

                walls.Add(topUpperRightWall);
                walls.Add(rightUpperRightWall);

                walls.Add(bottomLowerLeftWall);
                walls.Add(leftLowerLeftWall);

                walls.Add(bottomLowerRightWall);
                walls.Add(rightLowerRightWall);

                doors.Add(leftDoor);
                doors.Add(rightDoor);

                doors.Add(topDoor);
                doors.Add(bottomDoor);
            }
        }
        public RoomType GetRoomType()
        {
            return roomType;
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
                edges = roomSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            return roomSprite.Edges();

        }
        public List<Rectangle> GetWalls()
        {
            
            return walls;

        }
        public List<Rectangle> GetDoors()
        {
            return doors;
        }
        public Vector2 GetLocation()
        {
            return Location;
        }

        public void Update(GameTime gameTime)
        {
            edges = roomSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
                roomSprite.Draw(spriteBatch, Location);
            
        }
    }
}
