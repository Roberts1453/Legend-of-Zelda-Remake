
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    
    public class Camera
    {
        Link link;
        public Vector2 Location;
        public Vector2 roomLocation;
        public Vector2 inventoryLocation;
        public Rectangle roomBounds;
        public Dictionary<int, Vector2> dungeonCoodinates;
        public bool showInventory;

        public List<int> visitedRooms;

        public int dungeonNumber;
        public int roomNumber = 0;
        public int roomCount = 17;
        public int roomWidth = 256;
        public int roomHeight = 176;
        public int screenHeight = 176;
        public int hudHeight = 65;

        int newRoom;

        public enum CameraTransition { Still, Down, Left, Up, Right };
        public CameraTransition cameraTransition;

        public Camera(Vector2 location)
	    {
            Location = location;
            roomLocation = Location;
            inventoryLocation = new Vector2(roomLocation.X, roomLocation.Y - screenHeight);
            roomBounds = new Rectangle((int) roomLocation.X, (int) roomLocation.Y + hudHeight, roomWidth, roomHeight);

            dungeonCoodinates = new Dictionary<int, Vector2>();
            dungeonCoodinates.Add(0, new Vector2(0, roomHeight));
            dungeonCoodinates.Add(1, new Vector2(0, 0));
            dungeonCoodinates.Add(2, new Vector2(roomWidth, 0));
            dungeonCoodinates.Add(3, new Vector2(-roomWidth, 0));
            dungeonCoodinates.Add(4, new Vector2(0, -roomHeight));
            dungeonCoodinates.Add(5, new Vector2(0, -2* roomHeight));
            dungeonCoodinates.Add(6, new Vector2(roomWidth, -2 * roomHeight));
            dungeonCoodinates.Add(7, new Vector2(-roomWidth, -2 * roomHeight));
            dungeonCoodinates.Add(8, new Vector2(-roomWidth, -3 * roomHeight));
            dungeonCoodinates.Add(9, new Vector2(0, -3 * roomHeight));
            dungeonCoodinates.Add(10, new Vector2(roomWidth, -3 * roomHeight));
            dungeonCoodinates.Add(11, new Vector2(2* roomWidth, -3 * roomHeight));
            dungeonCoodinates.Add(12, new Vector2(2* roomWidth, -4 * roomHeight)); //boss room
            dungeonCoodinates.Add(13, new Vector2(3* roomWidth, -4 * roomHeight)); //triforce room
            dungeonCoodinates.Add(14, new Vector2(0, -4 * roomHeight));
            dungeonCoodinates.Add(15, new Vector2(0, -5 * roomHeight));
            dungeonCoodinates.Add(16, new Vector2(-roomWidth, -5 * roomHeight));
            dungeonCoodinates.Add(17, new Vector2(-2* roomWidth, -3 * roomHeight)); //old man room

            showInventory = false;
            cameraTransition = CameraTransition.Still;


        }
        public Camera(List<Room> roomList, int dungeonNumber)
        {
            this.dungeonNumber = dungeonNumber;
            roomNumber = 0;
            newRoom = roomNumber;
            roomCount = roomList.Count - 1;
            dungeonCoodinates = new Dictionary<int, Vector2>();
            for (int i = 0; i<roomList.Count; i++)
            {
                dungeonCoodinates.Add(i, new Vector2(roomList[i].Location.X, roomList[i].Location.Y-hudHeight));
            }

            Location = dungeonCoodinates[roomNumber];
            roomLocation = Location;
            inventoryLocation = new Vector2(roomLocation.X, roomLocation.Y - screenHeight);

            showInventory = false;
            visitedRooms = new List<int>();
            visitedRooms.Add(roomNumber);


        }
        public void ChangeRoom(int room)
        {
            roomNumber = room;
            roomLocation = dungeonCoodinates[roomNumber];
            inventoryLocation = new Vector2(roomLocation.X, roomLocation.Y - screenHeight);
            Location = roomLocation;
            visitedRooms.Add(roomNumber);
        }
        public void IncrementRoom()
        {
            roomNumber++;
            if (roomNumber > roomCount)
                roomNumber = 0;
            roomLocation = dungeonCoodinates[roomNumber];            
            inventoryLocation = new Vector2(roomLocation.X, roomLocation.Y - screenHeight);
            Location = roomLocation;
            visitedRooms.Add(roomNumber);
        }
        public void DecrementRoom()
        {
            roomNumber--;
            if (roomNumber < 0)
                roomNumber = roomCount;
            roomLocation = dungeonCoodinates[roomNumber];
            inventoryLocation = new Vector2(roomLocation.X, roomLocation.Y - screenHeight);
            Location = roomLocation;
            visitedRooms.Add(roomNumber);
        }
        public void RoomTransition(int newRoom, Link link)
        {
            if (cameraTransition == CameraTransition.Still)
            {
                Game1.game.gameState.Push(new BusyState(Game1.game));
                this.link = link;
                link.BeIdle();
                this.newRoom = newRoom;
                if (Location.Y > dungeonCoodinates[newRoom].Y)
                {
                    cameraTransition = CameraTransition.Up;
                }
                else if (Location.Y < dungeonCoodinates[newRoom].Y)
                {
                    cameraTransition = CameraTransition.Down;
                }
                if (Location.X > dungeonCoodinates[newRoom].X)
                {
                    cameraTransition = CameraTransition.Left;
                }
                else if (Location.X < dungeonCoodinates[newRoom].X)
                {
                    cameraTransition = CameraTransition.Right;
                }
            }

        }

        public void OpenInventory()
        {
            if(!showInventory)
            {
                if (Location.Y > inventoryLocation.Y)
                    Location.Y -= 3;
                if (Location.Y <= inventoryLocation.Y)
                {
                    Game1.game.gameState.Push(new MenuState(Game1.game));
                    showInventory = true;
                }
            }
        }
        public void CloseInventory()
        {
            if (showInventory)
            {
                if (Location.Y < roomLocation.Y)
                    Location.Y += 3;
                if (Location.Y == roomLocation.Y)
                {
                    Game1.game.gameState.Pop();
                    showInventory = false;
                }
            }
        }
        public void UseStairs(int newRoom, Vector2 exitLocation, Link link)
        {
            ChangeRoom(newRoom);
            link.location = roomLocation + exitLocation + new Vector2(0, hudHeight);
        }

        public void Update()
        {
            switch(cameraTransition) {
                case CameraTransition.Up:
                    Location.Y--;
                    break;
                case CameraTransition.Down:
                    Location.Y++;
                    break;
                case CameraTransition.Left:
                    Location.X--;
                    break;
                case CameraTransition.Right:
                    Location.X++;
                    break;
            }
            if (Location.Y == dungeonCoodinates[newRoom].Y && Location.X == dungeonCoodinates[newRoom].X && roomNumber != newRoom)
            {
                Game1.game.gameState.Pop();
                cameraTransition = CameraTransition.Still;
                ChangeRoom(newRoom);
                link.Push(link.direction);
            }
            roomBounds = new Rectangle((int)Location.X+1, (int)roomLocation.Y + hudHeight, roomWidth-1, roomHeight);

        }
    }
}
