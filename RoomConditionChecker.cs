
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace Sprint0Project
{

    public class RoomConditionChecker
    {
        Game1 game;

        public RoomConditionChecker(Game1 gameState)
        {
            game = gameState;
        }
        static public void RoomCondition(List<IList> roomContents)
        {
            if (roomContents[3].Count > 0)
            {
                string condition = roomContents[3][0].ToString();
                string response = roomContents[3][1].ToString();
                List<IStaticBlock> blockList = (List<IStaticBlock>)roomContents[0];
                List<ICharacter> enemyList = (List<ICharacter>)roomContents[1];
                ArrayList collectibleList = (ArrayList)roomContents[2];

                switch (condition)
                {
                    case "PushBlock":

                        
                        MoveableBlock moveBlock = (MoveableBlock) blockList.Find(x => x.GetType() == typeof(MoveableBlock));
                        

                        foreach (IStaticBlock checkBlock in blockList)
                        {
                            if (checkBlock.GetType() == typeof(MoveableBlock))
                            {
                                moveBlock = (MoveableBlock)checkBlock;

                            }
                                
                        }

                        if (moveBlock.HasBeenPushed())
                        {
                            
                            if (response.Contains("Open"))
                            {
                                Directions direction = Directions.Down;
                                if (response.Contains("Left"))
                                    direction = Directions.Left;
                                else if (response.Contains("Up"))
                                    direction = Directions.Up;
                                else if (response.Contains("Right"))
                                    direction = Directions.Right;

                                OpenDoor(direction, blockList);
                            }
                        }

                        break;
                    case "KillAll":
                        if (enemyList.Count == 0)
                        {
                            if (response.Contains("Open"))
                            {
                                Directions direction = Directions.Down;
                                if (response.Contains("Left"))
                                    direction = Directions.Left;
                                else if (response.Contains("Up"))
                                    direction = Directions.Up;
                                else if (response.Contains("Right"))
                                    direction = Directions.Right;

                                OpenDoor(direction, blockList);
                            }
                            else if (response.Contains("MakeKey"))
                            {
                                foreach (ICollectibleItem checkItem in collectibleList)
                                {
                                    if (checkItem.GetType() == typeof(Key))
                                    {
                                        Key key = (Key)checkItem;
                                        //key.Show();
                                    }
                                }
                            }

                        }
                        break;

                }
                if (condition.Contains("Left"))
                {
                    int remaining = int.Parse(condition.Substring(0, 1));
                   if (enemyList.Count <= remaining)
                    {
                        if (response.Contains("Open"))
                        {
                            Directions direction = Directions.Down;
                            if (response.Contains("Left"))
                                direction = Directions.Left;
                            else if (response.Contains("Up"))
                                direction = Directions.Up;
                            else if (response.Contains("Right"))
                                direction = Directions.Right;

                            OpenDoor(direction, blockList);
                        }
                        else if (response.Contains("MakeKey"))
                        {
                            foreach (ICollectibleItem checkItem in collectibleList)
                            {
                                if (checkItem.GetType() == typeof(Key))
                                {
                                    Key key = (Key)checkItem;
                                    //key.Show();
                                }
                            }
                        }

                    }
                }
            }
        }

        static private void OpenDoor(Directions direction, List<IStaticBlock> blockList)
        {
            List<IStaticBlock> doorList = blockList.FindAll(x => x.GetType() == typeof(Door));
            foreach (IStaticBlock checkDoor in blockList)
            {
                if (checkDoor.GetType() == typeof(Door))
                    doorList.Add((Door)checkDoor);
            }
            foreach (IStaticBlock doorBlock in doorList)
            {
                Door door = (Door)doorBlock;

                if (door.direction == direction)
                    door.ChangeDoor(Door.DoorState.Open);
            }

        }

        static public void MatchPairedDoors(List<Door> doorList)
        {
            foreach (Door door in doorList)
            {
                if (door.doorState == Door.DoorState.Open)
                    door.pairedDoor.ChangeDoor(Door.DoorState.Open);
                if (door.doorState == Door.DoorState.Bombed)
                    door.pairedDoor.ChangeDoor(Door.DoorState.Bombed);
            }
        }
        public void Update()
        {
            
        }
    }
}
