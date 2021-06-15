using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XMLData;
using System.Diagnostics;
using System.Collections;

namespace Sprint0Project
{
    public class DungeonLoader
    {
        public List<ICharacter> enemies = new List<ICharacter>();
        public List<IStaticBlock> allBlocks = new List<IStaticBlock>();
        public ArrayList collectibles = new ArrayList();
        public List<string> conditions = new List<string>();
        public Dictionary<int, List<IList>> roomContents = new Dictionary<int, List<IList>>();
        public List<Room> roomList = new List<Room>();
        public List<Door> doorList = new List<Door>();
        public List<DoorTop> doorTops = new List<DoorTop>();
        public List<XMLData.Dungeon> rooms = new List<XMLData.Dungeon>();

        public ICharacter addEnemy;
        public Goriya goriya;
        public Keese keese;
        public Stalfos stalfos;
        public Gel gel;
        public Aquamentus aquamentus;

        public IStaticBlock addBlock;
        public StoneBlock block;
        public BrickBlock brick;
        public LeftStatueBlock leftStatue;
        public RightStatueBlock rightStatue;
        public LeftStatue2Block leftStatue2;
        public RightStatue2Block rightStatue2;
        public WaterBlock water;
        public GravelBlock gravel;
        public StairsBlock stairs;
        public MoveableBlock moveable;
        public LadderBlock ladderBlock;
        public BlackBlock black;

        public ICollectibleItem addCollectible;
        public Clock clock;
        public Compass compass;
        public Heart heart;
        public Key key;
        public Map map;
        public BombCollectible bomb;
        public BoomerangCollectible boomerang;
        public Bow bow;
        public RaftCollectible raft;
        //public RodCollectible rod;
        public MagicKey magicKey;
        public HeartContainer heartContainer;

        public float screenOffset = 66;
        public float objectOffset = -16;
        public int roomWidth = 256;
        public int roomHeight = 176;
        public int blocksize = 16;
        public int gridStartX = 32;
        public int gridStartY = 32;

        public int dungeonNumber;
        public int roomCount;

        public DungeonLoader(int dungeonNumber)
        {
            this.dungeonNumber = dungeonNumber;
            roomCount = DungeonCount();
            for (int i = 0; i < roomCount; i++)
            {
                rooms.Add(GenerateRooms(i.ToString()));
            }
            PairDoors();

        }

        public int DungeonCount()
        {
            XmlDocument dungeon = new XmlDocument();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = "Dungeon" + dungeonNumber + ".xml";
            string filePath = System.IO.Path.Combine(currentDirectory, "Level", fileName);
            dungeon.Load(filePath);

            //XmlNode roomCountNode = dungeon.SelectSingleNode("XnaContent/Asset/Room[contains(roomNumber," + roomNum + ")]"); //Word in "" depends on the XML file

            string roomCountString = dungeon.SelectSingleNode("XnaContent/Asset/Dungeon/roomCount").InnerText;
            int roomCount = int.Parse(roomCountString);
            return roomCount;
        }

        public Dungeon GenerateRooms(string roomNum)
        {
            List<IList> contents = new List<IList>();
            List<IStaticBlock> blocks = new List<IStaticBlock>();
            enemies = new List<ICharacter>();
            conditions = new List<string>();


            XMLData.Dungeon room = new XMLData.Dungeon();
            XmlDocument dungeon = new XmlDocument();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = "d" + dungeonNumber + "Room" + roomNum + ".xml";
            string filePath = System.IO.Path.Combine(currentDirectory, "Level", "d" + dungeonNumber, fileName);
            dungeon.Load(filePath); //Change for other files
            //var ns = new XmlNamespaceManager(dungeon.NameTable);
            //ns.AddNamespace(String.Empty, "room1");

            XmlNode roomNode = dungeon.SelectSingleNode("XnaContent/Asset/Room[contains(roomNumber," + roomNum + ")]"); //Word in "" depends on the XML file


            string roomNumber = dungeon.SelectSingleNode("XnaContent/Asset/Room/roomNumber").InnerText;
            string roomLocation = dungeon.SelectSingleNode("XnaContent/Asset/Room/roomLocation").InnerText;
            string roomTypeName = dungeon.SelectSingleNode("XnaContent/Asset/Room/roomType").InnerText;
            string roomArchetype = dungeon.SelectSingleNode("XnaContent/Asset/Room/roomArchetype").InnerText;
            string conditionLine = dungeon.SelectSingleNode("XnaContent/Asset/Room/roomCondition").InnerText;
            string topDoor = dungeon.SelectSingleNode("XnaContent/Asset/Room/topDoor").InnerText;
            string bottomDoor = dungeon.SelectSingleNode("XnaContent/Asset/Room/bottomDoor").InnerText;
            string leftDoor = dungeon.SelectSingleNode("XnaContent/Asset/Room/leftDoor").InnerText;
            string rightDoor = dungeon.SelectSingleNode("XnaContent/Asset/Room/rightDoor").InnerText;
            string topRoom = dungeon.SelectSingleNode("XnaContent/Asset/Room/topRoom").InnerText;
            string bottomRoom = dungeon.SelectSingleNode("XnaContent/Asset/Room/bottomRoom").InnerText;
            string leftRoom = dungeon.SelectSingleNode("XnaContent/Asset/Room/leftRoom").InnerText;
            string rightRoom = dungeon.SelectSingleNode("XnaContent/Asset/Room/rightRoom").InnerText;

            if (conditionLine.Length > 0)
            {
                string condition = conditionLine.Substring(0, conditionLine.IndexOf(" "));
                string response = conditionLine.Substring(conditionLine.IndexOf(" ") + 1, conditionLine.Length - conditionLine.IndexOf(" ") - 1);
                conditions.Add(condition);
                conditions.Add(response);
            }




            /*string roomNumber = roomNode.SelectSingleNode("roomNumber").InnerText;
            string roomLocation = roomNode.SelectSingleNode("roomLocation").InnerText;
            string topDoor = roomNode.SelectSingleNode("topDoor").InnerText;
            string bottomDoor = roomNode.SelectSingleNode("bottomDoor").InnerText;
            string leftDoor = roomNode.SelectSingleNode("leftDoor").InnerText;
            string rightDoor = roomNode.SelectSingleNode("rightDoor").InnerText;*/
            //dungeonData.xml version, above for individual room xml

            int yLoc = roomLocation.IndexOf(" ") + 1;
            float roomX = float.Parse(roomLocation.Substring(0, yLoc - 1)) * roomWidth;
            float roomY = float.Parse(roomLocation.Substring(yLoc, roomLocation.Length - yLoc)) * roomHeight + screenOffset;

            room.roomNumber = int.Parse(roomNumber);

            Room.RoomType roomType;
            switch (roomTypeName)
            {
                case "Open":
                    roomType = Room.RoomType.Open;
                    break;
                case "OldMan":
                    roomType = Room.RoomType.OldMan;
                    break;
                case "ItemRoom":
                    roomType = Room.RoomType.ItemRoom;
                    break;
                case "Passage":
                    roomType = Room.RoomType.Passage;
                    break;
                case "OldManCave":
                    roomType = Room.RoomType.OldManCave;
                    break;
                case "Overworld":
                    roomType = Room.RoomType.Overworld;
                    break;
                default:
                    roomType = Room.RoomType.Open;
                    break;

            }

            Room roomBackground = new Room(new Vector2(roomX, roomY), roomType);
            roomList.Add(roomBackground);

            room.topDoor = topDoor;
            room.bottomDoor = bottomDoor;
            room.leftDoor = leftDoor;
            room.rightDoor = rightDoor;

            Door tDoor;
            Door bDoor;
            Door lDoor;
            Door rDoor;

            int topRoomNum;
            int bottomRoomNum;
            int leftRoomNum;
            int rightRoomNum;

            if (topRoom.Length > 0)
            {
                topRoomNum = int.Parse(topRoom);
                tDoor = new Door(new Vector2(roomX + 112, roomY), Directions.Up, topRoomNum);
            }
            else
                tDoor = new Door(new Vector2(roomX + 112, roomY), Directions.Up);
            if (bottomRoom.Length > 0) { 
                bottomRoomNum = int.Parse(bottomRoom);
                bDoor = new Door(new Vector2(roomX + 112, roomY + 32 + 112), Directions.Down, bottomRoomNum);
            }
            else
                bDoor = new Door(new Vector2(roomX + 112, roomY + 32 + 112), Directions.Down);
            if (leftRoom.Length > 0) { 
                leftRoomNum = int.Parse(leftRoom);
                lDoor = new Door(new Vector2(roomX, roomY + 72), Directions.Left, leftRoomNum);
            }
            else
                lDoor = new Door(new Vector2(roomX, roomY + 72), Directions.Left);
            if (rightRoom.Length > 0) { 
                rightRoomNum = int.Parse(rightRoom);
                rDoor = new Door(new Vector2(roomX + 224, roomY + 72), Directions.Right, rightRoomNum);
            }
            else
                rDoor = new Door(new Vector2(roomX + 224, roomY + 72), Directions.Right);



            switch (topDoor)
            {
                case "open":
                    tDoor.ChangeDoor(Door.DoorState.Open);
                    break;
                case "closed":
                    tDoor.ChangeDoor(Door.DoorState.Closed);
                    break;
                case "locked":
                    tDoor.ChangeDoor(Door.DoorState.Locked);
                    break;
                case "bombable":
                    tDoor.ChangeDoor(Door.DoorState.Bombable);
                    break;
                case "bombed":
                    tDoor.ChangeDoor(Door.DoorState.Bombed);
                    break;
                default:
                    tDoor.ChangeDoor(Door.DoorState.Wall);
                    break;

            }

            switch (bottomDoor)
            {
                case "open":
                    bDoor.ChangeDoor(Door.DoorState.Open);
                    break;
                case "closed":
                    bDoor.ChangeDoor(Door.DoorState.Closed);
                    break;
                case "locked":
                    bDoor.ChangeDoor(Door.DoorState.Locked);
                    break;
                case "bombable":
                    bDoor.ChangeDoor(Door.DoorState.Bombable);
                    break;
                case "bombed":
                    bDoor.ChangeDoor(Door.DoorState.Bombed);
                    break;
                default:
                    bDoor.ChangeDoor(Door.DoorState.Wall);
                    break;

            }

            switch (leftDoor)
            {
                case "open":
                    lDoor.ChangeDoor(Door.DoorState.Open);
                    break;
                case "closed":
                    lDoor.ChangeDoor(Door.DoorState.Closed);
                    break;
                case "locked":
                    lDoor.ChangeDoor(Door.DoorState.Locked);
                    break;
                case "bombable":
                    lDoor.ChangeDoor(Door.DoorState.Bombable);
                    break;
                case "bombed":
                    lDoor.ChangeDoor(Door.DoorState.Bombed);
                    break;
                default:
                    lDoor.ChangeDoor(Door.DoorState.Wall);
                    break;

            }

            switch (rightDoor)
            {
                case "open":
                    rDoor.ChangeDoor(Door.DoorState.Open);
                    break;
                case "closed":
                    rDoor.ChangeDoor(Door.DoorState.Closed);
                    break;
                case "locked":
                    rDoor.ChangeDoor(Door.DoorState.Locked);
                    break;
                case "bombable":
                    rDoor.ChangeDoor(Door.DoorState.Bombable);
                    break;
                case "bombed":
                    rDoor.ChangeDoor(Door.DoorState.Bombed);
                    break;
                default:
                    rDoor.ChangeDoor(Door.DoorState.Wall);
                    break;

            }
            if (roomType != Room.RoomType.ItemRoom && roomType != Room.RoomType.ItemRoom && roomType != Room.RoomType.Overworld && roomType != Room.RoomType.OldManCave)
            {
                blocks.Add(tDoor);
                blocks.Add(bDoor);
                blocks.Add(lDoor);
                blocks.Add(rDoor);

                doorTops.Add(new DoorTop(tDoor));
                doorTops.Add(new DoorTop(bDoor));
                doorTops.Add(new DoorTop(lDoor));
                doorTops.Add(new DoorTop(rDoor));
            }


            

            XmlNodeList objects = dungeon.SelectNodes("XnaContent/Asset/Room/Object"); //Word in "" depends on the XML file
            //XmlNodeList objects = roomNode.SelectNodes("Object"); //dungeonData.xml version, above for individual room xml

            room.objects = new XMLData.Object[objects.Count];

            int i = 0;

            foreach (XmlNode obj in objects)
            {

                string type = obj.SelectSingleNode("objectType").InnerText;
                string name = obj.SelectSingleNode("objectName").InnerText;
                string location = obj.SelectSingleNode("location").InnerText;

                int yStart = location.IndexOf(" ")  + 1;
                float xPosition = float.Parse(location.Substring(0, yStart - 1));
                float yPosition = float.Parse(location.Substring(yStart, location.Length - yStart));
                room.objects[i] = new XMLData.Object();
                room.objects[i].location = new Vector2(roomX+gridStartX+ (xPosition * blocksize), roomY + gridStartY + (yPosition * blocksize));

                room.objects[i].objectName = name;
                room.objects[i].objectType = type;



                if (type == "block")
                {
                    switch (name)
                    {
                        case "Block":
                            addBlock = new StoneBlock(room.objects[i].location);
                            blocks.Add(addBlock);
                            break;
                        case "Black":
                            black = new BlackBlock(room.objects[i].location);
                            blocks.Add(black);
                            break;
                        case "Brick":
                            brick = new BrickBlock(room.objects[i].location, 1);
                            blocks.Add(brick);
                            break;
                        case "DoubleBrick":
                            brick = new BrickBlock(room.objects[i].location, 2);
                            blocks.Add(brick);
                            break;
                        case "LongBrick":
                            brick = new BrickBlock(room.objects[i].location, 3);
                            blocks.Add(brick);
                            break;
                        case "MoveableBlock":
                            moveable = new MoveableBlock(room.objects[i].location);
                            blocks.Add(moveable);
                            break;
                        case "BlueLeftStatue":
                            leftStatue = new LeftStatueBlock(room.objects[i].location);
                            blocks.Add(leftStatue);
                            break;
                        case "BlueRightStatue":
                            rightStatue = new RightStatueBlock(room.objects[i].location);
                            blocks.Add(rightStatue);
                            break;
                        case "Gravel":
                            gravel = new GravelBlock(room.objects[i].location);
                            blocks.Add(gravel);
                            break;
                        case "LeftStatue":
                            leftStatue2 = new LeftStatue2Block(room.objects[i].location);
                            blocks.Add(leftStatue2);
                            break;
                        case "RightStatue":
                            rightStatue2 = new RightStatue2Block(room.objects[i].location);
                            blocks.Add(rightStatue2);
                            break;
                        case "OldMan":
                            addBlock = new OldManBlock(room.objects[i].location);
                            blocks.Add(addBlock);
                            break;
                        case "Fire":
                            addBlock = new FireBlock(room.objects[i].location);
                            blocks.Add(addBlock);
                            break;
                        case "Stairs":
                            string exitLocation = obj.SelectSingleNode("exitLocation").InnerText;
                            string exitRoom = obj.SelectSingleNode("exitRoom").InnerText;

                            yStart = location.IndexOf(" ") + 1;
                            int exitRoomNum = int.Parse(exitRoom.Substring(0, exitRoom.Length));
                            float xExit = float.Parse(exitLocation.Substring(0, yStart - 1));
                            float yExit = float.Parse(exitLocation.Substring(yStart, exitLocation.Length - yStart));

                            Vector2 exit = new Vector2(gridStartX + (xExit * blocksize), gridStartY + (yExit * blocksize));

                            stairs = new StairsBlock(room.objects[i].location, exitRoomNum, exit);
                            blocks.Add(stairs);
                            break;
                        case "DungeonDoor":
                            exitLocation = obj.SelectSingleNode("exitLocation").InnerText;
                            exitRoom = obj.SelectSingleNode("exitRoom").InnerText;
                            string exitDungeon = obj.SelectSingleNode("exitDungeon").InnerText;

                            yStart = location.IndexOf(" ") + 1;
                            exitRoomNum = int.Parse(exitRoom.Substring(0, exitRoom.Length));
                            int exitDungeonNum = int.Parse(exitDungeon.Substring(0, exitDungeon.Length));
                            xExit = float.Parse(exitLocation.Substring(0, yStart - 1));
                            yExit = float.Parse(exitLocation.Substring(yStart, exitLocation.Length - yStart));

                            Vector2 tileOffset = new Vector2(0, 0);
                            if (exitDungeonNum == 0)
                                tileOffset = new Vector2(8, 8);

                            exit = new Vector2(gridStartX + tileOffset.X + (xExit * blocksize), gridStartY + screenOffset + (yExit * blocksize));
                                       
                            addBlock = new DungeonDoor(room.objects[i].location, exitDungeonNum, exitRoomNum, exit);
                            blocks.Add(addBlock);
                            break;
                        case "Water":
                            water = new WaterBlock(room.objects[i].location);
                            blocks.Add(water);
                            break;
                        case "Text":
                            string text = obj.SelectSingleNode("text").InnerText;
                            addBlock = new TextBlock(room.objects[i].location, text);
                            blocks.Add(addBlock);
                            break;
                        /*OVERWORLD BLOCKS*/
                        case "Ladder":
                            ladderBlock = new LadderBlock(room.objects[i].location, 2);
                            blocks.Add(ladderBlock);
                            break;
                        case "HalfLadder":
                            ladderBlock = new LadderBlock(room.objects[i].location, 1);
                            blocks.Add(ladderBlock);
                            break;
                        case "Land":
                            addBlock = new LandBlock(room.objects[i].location);
                            blocks.Add(addBlock);
                            break;
                        case "SingleRock":
                            addBlock = new RockBlock(room.objects[i].location, 0);
                            blocks.Add(addBlock);
                            break;
                        case "TopLeftRock":
                            addBlock = new RockBlock(room.objects[i].location, 1);
                            blocks.Add(addBlock);
                            break;
                        case "TopRock":
                            addBlock = new RockBlock(room.objects[i].location, 2);
                            blocks.Add(addBlock);
                            break;
                        case "TopRightRock":
                            addBlock = new RockBlock(room.objects[i].location, 3);
                            blocks.Add(addBlock);
                            break;
                        case "BottomLeftRock":
                            addBlock = new RockBlock(room.objects[i].location, 4);
                            blocks.Add(addBlock);
                            break;
                        case "BottomRock":
                            addBlock = new RockBlock(room.objects[i].location, 5);
                            blocks.Add(addBlock);
                            break;
                        case "BottomRightRock":
                            addBlock = new RockBlock(room.objects[i].location, 6);
                            blocks.Add(addBlock);
                            break;
                        case "TreeBlock":
                            addBlock = new TreeBlock(room.objects[i].location, 0);
                            blocks.Add(addBlock);
                            break;
                        case "TopLeftTree":
                            addBlock = new TreeBlock(room.objects[i].location, 1);
                            blocks.Add(addBlock);
                            break;
                        case "TopTree":
                            addBlock = new TreeBlock(room.objects[i].location, 2);
                            blocks.Add(addBlock);
                            break;
                        case "TopRightTree":
                            addBlock = new TreeBlock(room.objects[i].location, 3);
                            blocks.Add(addBlock);
                            break;
                        case "BottomLeftTree":
                            addBlock = new TreeBlock(room.objects[i].location, 4);
                            blocks.Add(addBlock);
                            break;
                        case "BottomTree":
                            addBlock = new TreeBlock(room.objects[i].location, 5);
                            blocks.Add(addBlock);
                            break;
                        case "BottomRightTree":
                            addBlock = new TreeBlock(room.objects[i].location, 6);
                            blocks.Add(addBlock);
                            break;
                        case "MiddleLake":
                            addBlock = new LakeBlock(room.objects[i].location, 0);
                            blocks.Add(addBlock);
                            break;
                        case "TopLeftLake":
                            addBlock = new LakeBlock(room.objects[i].location, 1);
                            blocks.Add(addBlock);
                            break;
                        case "TopLake":
                            addBlock = new LakeBlock(room.objects[i].location, 2);
                            blocks.Add(addBlock);
                            break;
                        case "TopRightLake":
                            addBlock = new LakeBlock(room.objects[i].location, 3);
                            blocks.Add(addBlock);
                            break;
                        case "LeftLake":
                            addBlock = new LakeBlock(room.objects[i].location, 4);
                            blocks.Add(addBlock);
                            break;
                        case "RightLake":
                            addBlock = new LakeBlock(room.objects[i].location, 5);
                            blocks.Add(addBlock);
                            break;
                        case "BottomLeftLake":
                            addBlock = new LakeBlock(room.objects[i].location, 6);
                            blocks.Add(addBlock);
                            break;
                        case "BottomLake":
                            addBlock = new LakeBlock(room.objects[i].location, 7);
                            blocks.Add(addBlock);
                            break;
                        case "BottomRightLake":
                            addBlock = new LakeBlock(room.objects[i].location, 8);
                            blocks.Add(addBlock);
                            break;
						case "DeadTreeBrown":
                            addBlock = new DungeonTreeBlock(room.objects[i].location, 0);
                            blocks.Add(addBlock);
							addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(16, 0), 2);
                            blocks.Add(addBlock);
							addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(0, 16), 3);
                            blocks.Add(addBlock);
							addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(16, 16), 4);
                            blocks.Add(addBlock);
                            break;
                        case "DungeonTreeBrown":
                            for (int di = 0; di <= 3; di++)
                            {
                                addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(16 * (di % 3), 16 * (di / 3)), di);
                                blocks.Add(addBlock);
                            }
                            addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(32, 16), 4);
                            blocks.Add(addBlock);
                            break;
                        case "DungeonTreeGreen":
                            for (int j = 0; j <= 3; j++)
                            {
                                addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(16 * (j % 3), 16 * (j / 3)), j+5);
                                blocks.Add(addBlock);
                            }
                            addBlock = new DungeonTreeBlock(room.objects[i].location + new Vector2(32, 16), 9);
                            blocks.Add(addBlock);
                            break;
                        case "RockRow":
                            string rowWidth = obj.SelectSingleNode("rowWidth").InnerText;
                            string rowHeight = obj.SelectSingleNode("rowHeight").InnerText;
                            string excludeX = obj.SelectSingleNode("excludeX").InnerText;
                            string excludeY = obj.SelectSingleNode("excludeY").InnerText;
                            string stepX = obj.SelectSingleNode("stepX").InnerText;
                            string stepY = obj.SelectSingleNode("stepY").InnerText;
							
							List<IStaticBlock> rockRow = RockRow(room.objects[i].location, rowWidth, rowHeight, excludeX, excludeY, stepX, stepY);
							blocks.AddRange(rockRow);
                            break;
                        case "TreeRow":
                            rowWidth = obj.SelectSingleNode("rowWidth").InnerText;
                            rowHeight = obj.SelectSingleNode("rowHeight").InnerText;
                            excludeX = obj.SelectSingleNode("excludeX").InnerText;
                            excludeY = obj.SelectSingleNode("excludeY").InnerText;
                            stepX = obj.SelectSingleNode("stepX").InnerText;
                            stepY = obj.SelectSingleNode("stepY").InnerText;
							
							List<IStaticBlock> treeRow = TreeRow(room.objects[i].location, rowWidth, rowHeight, excludeX, excludeY, stepX, stepY);
							blocks.AddRange(treeRow);
                            break;
                        case "SingleTreeBrownRow":
                            rowWidth = obj.SelectSingleNode("rowWidth").InnerText;
                            rowHeight = obj.SelectSingleNode("rowHeight").InnerText;
                            excludeX = obj.SelectSingleNode("excludeX").InnerText;
                            excludeY = obj.SelectSingleNode("excludeY").InnerText;
                            stepX = obj.SelectSingleNode("stepX").InnerText;
                            stepY = obj.SelectSingleNode("stepY").InnerText;
							
							treeRow = SingleTreeRow(room.objects[i].location, rowWidth, rowHeight, excludeX, excludeY, stepX, stepY, 0);
							blocks.AddRange(treeRow);
                            break;
						case "SingleTreeRow":
                            rowWidth = obj.SelectSingleNode("rowWidth").InnerText;
                            rowHeight = obj.SelectSingleNode("rowHeight").InnerText;
                            excludeX = obj.SelectSingleNode("excludeX").InnerText;
                            excludeY = obj.SelectSingleNode("excludeY").InnerText;
                            stepX = obj.SelectSingleNode("stepX").InnerText;
                            stepY = obj.SelectSingleNode("stepY").InnerText;
							
							treeRow = SingleTreeRow(room.objects[i].location, rowWidth, rowHeight, excludeX, excludeY, stepX, stepY, 1);
							blocks.AddRange(treeRow);
                            break;
						case "WaterRow":
                            rowWidth = obj.SelectSingleNode("rowWidth").InnerText;
                            rowHeight = obj.SelectSingleNode("rowHeight").InnerText;
							
							List<IStaticBlock> waterRow = WaterRow(room.objects[i].location, rowWidth, rowHeight);
							blocks.AddRange(waterRow);
                            break;
                        default:
                            break;
                    }
                }
                else if (type == "enemy")
                {
                    switch (name)
                    {
                        case "Aquamentus":
                            aquamentus = new Aquamentus(room.objects[i].location);
                            enemies.Add(aquamentus);
                            break;
                        case "Moblin":
                            addEnemy = new Moblin(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Octorok":
                            addEnemy = new Octorok(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Goriya":
                            goriya = new Goriya(room.objects[i].location);
                            enemies.Add(goriya);
                            break;
                        case "GoriyaKey":
                            addCollectible = new Key(room.objects[i].location);
                            goriya = new Goriya(room.objects[i].location, addCollectible);
                            enemies.Add(goriya);
                            break;
                        case "GoriyaBoomerang":
                            boomerang = new BoomerangCollectible(room.objects[i].location);
                            goriya = new Goriya(room.objects[i].location, boomerang);
                            enemies.Add(goriya);
                            break;
                        case "Keese":
                            keese = new Keese(room.objects[i].location);
                            enemies.Add(keese);
                            break;
                        case "KeeseKey":
                            addCollectible = new Key(room.objects[i].location);
                            keese = new Keese(room.objects[i].location, addCollectible);
                            enemies.Add(keese);
                            break;
                        case "RedKeese":
                            keese = new Keese(room.objects[i].location, 2);
                            enemies.Add(keese);
                            break;
                        case "Vire":
                            addEnemy = new Vire(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Gel":
                            gel = new Gel(room.objects[i].location);
                            enemies.Add(gel);
                            break;
                        case "Zol":
                            addEnemy = new Zol(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Stalfos":
                            stalfos = new Stalfos(room.objects[i].location);
                            enemies.Add(stalfos);
                            break;
                        case "StalfosKey":
                            Key key = new Key(room.objects[i].location);
                            stalfos = new Stalfos(room.objects[i].location,key);
                            enemies.Add(stalfos);
                            break;
                        case "StalfosRupee":
                            Rupee rupee = new Rupee(room.objects[i].location);
                            stalfos = new Stalfos(room.objects[i].location, rupee);
                            enemies.Add(stalfos);
                            break;
                        case "Gibdo":
                            addEnemy = new Gibdo(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "GibdosKey":
                            key = new Key(room.objects[i].location);
                            addEnemy = new Gibdo(room.objects[i].location, key);
                            enemies.Add(addEnemy);
                            break;
                        case "BladeTrap":
                            addEnemy = new BladeTrap(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "WallMaster":
                            addEnemy = new Wallmaster(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Rope":
                            addEnemy = new Rope(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "BlueGoriya":
                            addEnemy = new BlueGoriya(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "BlueGoriyaBoomerang":
                            MagicBoomerangCollectible magicBoomerang = new MagicBoomerangCollectible(room.objects[i].location);
                            addEnemy = new BlueGoriya(room.objects[i].location, magicBoomerang);
                            enemies.Add(addEnemy);
                            break;
                        case "ShootingStatueLeft":
                            addEnemy = new ShootingStatue(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "ShootingStatueRight":
                            addEnemy = new ShootingStatue(room.objects[i].location, 2);
                            enemies.Add(addEnemy);
                            break;
                        case "Darknut":
                            addEnemy = new Darknut(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Dodongo":
                            addEnemy = new Dodongo(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Manhandla":
                            addEnemy = new Manhandla(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "PolsVoice":
                            addEnemy = new PolsVoice(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Bubble":
                            addEnemy = new Bubble(room.objects[i].location);
                            enemies.Add(addEnemy);
                            break;
                        case "Moldorm":
                            Moldorm frontMoldorm = new Moldorm(room.objects[i].location);
                            enemies.Add(frontMoldorm);
                            Moldorm nextMoldorm = new Moldorm(room.objects[i].location, frontMoldorm);
                            frontMoldorm.SetBack(nextMoldorm);
                            enemies.Add(nextMoldorm);
                            frontMoldorm = new Moldorm(room.objects[i].location, nextMoldorm);
                            nextMoldorm.SetBack(frontMoldorm);
                            enemies.Add(frontMoldorm);
                            nextMoldorm = new Moldorm(room.objects[i].location, frontMoldorm);
                            frontMoldorm.SetBack(nextMoldorm);
                            enemies.Add(nextMoldorm);
                            frontMoldorm = new Moldorm(room.objects[i].location, nextMoldorm);
                            nextMoldorm.SetBack(frontMoldorm);
                            enemies.Add(frontMoldorm);
                            break;
                        default:
                            break;
                    }
                }
                else if (type == "item")
                {
                    switch (name)
                    {
                        case "Bow":
                            addCollectible = new Bow(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Boomerang":
                            boomerang = new BoomerangCollectible(room.objects[i].location);
                            collectibles.Add(boomerang);
                            break;
                        case "Rod":
                            addCollectible = new RodCollectible(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Raft":
                            raft = new RaftCollectible(room.objects[i].location);
                            collectibles.Add(raft);
                            break;
                        case "Ladder":
                            addCollectible = new LadderCollectible(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Triforce":
                            addCollectible = new TriforcePiece(room.objects[i].location, dungeonNumber);
                            collectibles.Add(addCollectible);
                            break;
                        case "Compass":
                            compass = new Compass(room.objects[i].location, dungeonNumber);
                            collectibles.Add(compass);
                            break;
                        case "Key":
                            key = new Key(room.objects[i].location);
                            collectibles.Add(key);
                            break;
                        case "MagicKey":
                            magicKey = new MagicKey(room.objects[i].location);
                            collectibles.Add(magicKey);
                            break;
                        case "Map":
                            map = new Map(room.objects[i].location, dungeonNumber);
                            collectibles.Add(map);
                            break;
                        case "Heart":
                            heart = new Heart(room.objects[i].location);
                            collectibles.Add(heart);
                            break;
                        case "HeartContainer":
                            heartContainer = new HeartContainer(room.objects[i].location);
                            collectibles.Add(heartContainer);
                            break;
                        case "Bomb":
                            bomb = new BombCollectible(room.objects[i].location);
                            collectibles.Add(bomb);
                            break;
                        case "BombShop":
                            addCollectible = new BombShop(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "ArrowShop":
                            addCollectible = new ArrowShop(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Arrow":
                            addCollectible = new ArrowCollectible(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "CandleShop":
                            addCollectible = new CandleShop(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Rupee":
                            addCollectible = new Rupee(room.objects[i].location, 5);
                            collectibles.Add(addCollectible);
                            break;
                        case "OrangeRupee":
                            addCollectible = new Rupee(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        case "Candle":
                            addCollectible = new CandleCollectible(room.objects[i].location);
                            collectibles.Add(addCollectible);
                            break;
                        default:
                            break;
                    }
                }

                i++;
            }
			
			List<IStaticBlock> archetypeBlocks = RoomArchetype(roomArchetype, roomX, roomY);
            blocks.AddRange(archetypeBlocks);
			
            allBlocks.AddRange(blocks);
            contents.Add(blocks);
            contents.Add(enemies);
            contents.Add(collectibles);
            contents.Add(conditions);
            roomContents.Add(room.roomNumber, contents);

            return room;
        }
		
		public List<IStaticBlock> TreeRow(Vector2 startLoc, string rowWidthS, string rowHeightS, string excludeXS, string excludeYS, string stepXS, string stepYS)
        {
			List<IStaticBlock> treeList = new List<IStaticBlock>();
			
			int rowWidth = int.Parse(rowWidthS);
			int rowHeight = int.Parse(rowHeightS);
			List<int> excludeX = new List<int>();
			foreach (string x in excludeXS.Split(' '))
				if (x.Length > 0)
					excludeX.Add(int.Parse(x));
			List<int> excludeY = new List<int>();				
			foreach (string y in excludeYS.Split(' '))
				if (y.Length > 0)
					excludeY.Add(int.Parse(y));
			int stepX = int.Parse(stepXS);
			int stepY = int.Parse(stepYS);
			
			for (int x = 0; x < rowWidth; x+=stepX)
			{
				for (int y = 0; y < rowHeight; y+=stepY)
				{
					if (!excludeX.Contains(x) && !excludeY.Contains(y))
						treeList.Add(new TreeBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 5));
				}
			}
			
			
			return treeList;
		}

        public List<IStaticBlock> SingleTreeRow(Vector2 startLoc, string rowWidthS, string rowHeightS, string excludeXS, string excludeYS, string stepXS, string stepYS, int type)
        {
            List<IStaticBlock> treeList = new List<IStaticBlock>();

            int rowWidth = int.Parse(rowWidthS);
            int rowHeight = int.Parse(rowHeightS);
            List<int> excludeX = new List<int>();
            foreach (string x in excludeXS.Split(' '))
                if (x.Length > 0)
                    excludeX.Add(int.Parse(x));
            List<int> excludeY = new List<int>();
            foreach (string y in excludeYS.Split(' '))
                if (y.Length > 0)
                    excludeY.Add(int.Parse(y));
            int stepX = int.Parse(stepXS);
            int stepY = int.Parse(stepYS);

            for (int x = 0; x < rowWidth; x += stepX)
            {
                for (int y = 0; y < rowHeight; y += stepY)
                {
                    if (!excludeX.Contains(x) && !excludeY.Contains(y))
                        treeList.Add(new SingleTreeBlock(startLoc + new Vector2(x * blocksize, y * 16), type));
                }
            }


            return treeList;
        }
		
		public List<IStaticBlock> RockRow(Vector2 startLoc, string rowWidthS, string rowHeightS, string excludeXS, string excludeYS, string stepXS, string stepYS)
        {
			List<IStaticBlock> rockList = new List<IStaticBlock>();
			
			int rowWidth = int.Parse(rowWidthS);
			int rowHeight = int.Parse(rowHeightS);
			List<int> excludeX = new List<int>();
			foreach (string x in excludeXS.Split(' '))
				if (x.Length > 0)
					excludeX.Add(int.Parse(x));
			List<int> excludeY = new List<int>();				
			foreach (string y in excludeYS.Split(' '))
				if (y.Length > 0)
					excludeY.Add(int.Parse(y));
			int stepX = int.Parse(stepXS);
			int stepY = int.Parse(stepYS);
			
			for (int x = 0; x < rowWidth; x+=stepX)
			{
				for (int y = 0; y < rowHeight; y+=stepY)
				{
					if (!excludeX.Contains(x) && !excludeY.Contains(y))
						rockList.Add(new RockBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 5));
				}
			}
			
			
			return rockList;
		}
		
		public List<IStaticBlock> WaterRow(Vector2 startLoc, string rowWidthS, string rowHeightS)
        {
			List<IStaticBlock> waterList = new List<IStaticBlock>();
			
			int rowWidth = int.Parse(rowWidthS);
			int rowHeight = int.Parse(rowHeightS);
			
			for (int x = 0; x < rowWidth; x++)
			{
				for (int y = 0; y < rowHeight; y++)
				{
					if (rowWidth == 2)
						waterList.Add(new LakeBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 4+x));
					else if (rowHeight == 2) {
						if (y == 0)
							waterList.Add(new LakeBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 2));
						else if (y ==1)
							waterList.Add(new LakeBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 7));
					}
					else
						waterList.Add(new LakeBlock(startLoc + new Vector2(x*blocksize, y*blocksize), 0));					
				}
			}
			
			
			return waterList;
		}

        public List<IStaticBlock> RoomArchetype(string roomArchetype, float roomX, float roomY)
        {
            IStaticBlock block;
            Vector2 location;

            List<IStaticBlock> roomBlocks = new List<IStaticBlock>();
            switch(roomArchetype)
            {
                case "1": //1st room

                    break;
                case "2":
                    for (int i = 2; i <= 3; i++)
                    {
                        for (int j = 2; j <= 4; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 8; i <= 9; i++)
                    {
                        for (int j = 2; j <= 4; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "3":
                    for (int i = 5; i <= 6; i++)
                    {
                        for (int j = 2; j <= 4; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "4":
                    for (int i = 2; i <= 9; i+=7)
                    {
                        for (int j = 2; j <= 4; j+=2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "5":
                    for (int i = 2; i <= 9; i += 7)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "6":
                    location = new Vector2(roomX + gridStartX + (5 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new MoveableBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "7":
                    for (int i = 2; i <= 9; i += 7)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 1; i <= 10; i += 9)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    location = new Vector2(roomX + gridStartX + (5 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);
                    location = new Vector2(roomX + gridStartX + (6 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "8":
                    for (int i = 1; i <= 5; i += 2)
                    {
                        for (int j = 1; j <= 5; j += 2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 6; i <= 10; i += 2)
                    {
                        for (int j = 1; j <= 5; j += 2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "9":
                    break;
                case "10":
                    for (int i = 7; i <= 11; i++)
                    {
                        for (int j = 0; j <= 6; j += 6)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 9; i <= 11; i++)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 10; i <= 11; i++)
                    {
                        for (int j = 2; j <= 4; j += 2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "11": //Triforce Room
                    for (int i = 1; i <= 10; i += 9)
                    {
                        for (int j = 2; j <= 4; j ++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 1; i <= 10; i ++)
                    {
                        for (int j = 1; j <= 5; j+= 4)
                        {
                            if (!(j == 5 && (i == 5 || i == 6)))
                            {
                                location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                                block = new StoneBlock(location);
                                roomBlocks.Add(block);
                            }                            
                        }
                    }
                    location = new Vector2(roomX + gridStartX + (4 * blocksize), roomY + gridStartY + (2 * blocksize));
                    block = new LeftStatueBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (3 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new LeftStatueBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (7 * blocksize), roomY + gridStartY + (2 * blocksize));
                    block = new RightStatueBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (8 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new RightStatueBlock(location);
                    roomBlocks.Add(block);


                    break;
                case "12":
                    for (int i = 5; i <= 7; i+=2)
                    {
                        for (int j = 2; j <= 4; j+=2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    location = new Vector2(roomX + gridStartX + (6 * blocksize), roomY + gridStartY + (1 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (8 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (6 * blocksize), roomY + gridStartY + (5 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (4 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new MoveableBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "13":
                    for (int i = 1; i <= 10; i++)
                    {
                        if (i < 4 || i > 7)
                        {
                            for (int j = 1; j <= 5; j += 4)
                            {
                                location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                                block = new WaterBlock(location);
                                roomBlocks.Add(block);
                            }
                        }
                    }
                    for (int i = 1; i <= 10; i+=9)
                    {
                        for (int j = 2; j <= 4; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new WaterBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 3; i <= 8; i++)
                    {
                        location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (3 * blocksize));
                        block = new WaterBlock(location);
                        roomBlocks.Add(block);
                        
                    }
                    break;
                case "14":
                    //attacking statues
                    break;
                case "15":
                    for (int i = 0; i <= 11; i++)
                    {
                        for (int j = 0; j <= 6; j ++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new GravelBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "16":
                    for (int i = 8; i <= 11; i++)
                    {
                        for (int j = 0; j <= 6; j += 6)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 8; i <= 11; i++)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 10; i <= 11; i++)
                    {
                        for (int j = 2; j <= 4; j += 2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "17":
                    for (int i = 0; i <= 5; i++)
                    {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + ((6-i) * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                    }
                    for (int i = 6; i <= 11; i++)
                    {
                        location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + ((11 - i) * blocksize));
                        block = new StoneBlock(location);
                        roomBlocks.Add(block);
                    }
                    location = new Vector2(roomX + gridStartX + (2 * blocksize), roomY + gridStartY + (0 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (1 * blocksize), roomY + gridStartY + (1 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (10 * blocksize), roomY + gridStartY + (5 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (9 * blocksize), roomY + gridStartY + (6 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "18":
                    for (int i = 0; i <= 11; i++)
                    {
                        for (int j = 2; j <= 4; j += 2)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "19":
                    for (int i = 1; i <= 10; i++)
                    {
                        for (int j = 1; j <= 5; j += 4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int j = 2; j <= 4; j++)
                    {
                        location = new Vector2(roomX + gridStartX + (10 * blocksize), roomY + gridStartY + (j * blocksize));
                        block = new StoneBlock(location);
                        roomBlocks.Add(block);
                    }

                    break;
                case "20":
                    location = new Vector2(roomX + gridStartX + (4 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new MoveableBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (7 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "21":
                    for (int i = 1; i <= 3; i += 2)
                    {
                        for (int j = 0; j <= 4; j++)
                        {
                            if (i == 1 || j != 2) { 
                                location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                                block = new StoneBlock(location);
                                roomBlocks.Add(block);
                            }
                        }
                    }
                    for (int i = 5; i <= 9; i += 4)
                    {
                        for (int j = 1; j <= 5; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    location = new Vector2(roomX + gridStartX + (4 * blocksize), roomY + gridStartY + (1 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (7 * blocksize), roomY + gridStartY + (1 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (8 * blocksize), roomY + gridStartY + (1 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (10 * blocksize), roomY + gridStartY + (2 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (11 * blocksize), roomY + gridStartY + (2 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (2 * blocksize), roomY + gridStartY + (4 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    location = new Vector2(roomX + gridStartX + (2 * blocksize), roomY + gridStartY + (6 * blocksize));
                    block = new StoneBlock(location);
                    roomBlocks.Add(block);

                    for (int i = 7; i <= 11; i += 4)
                    {
                        for (int j = 5; j <= 6; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "22":
                    for (int i = 0; i <= 11; i++)
                    {
                        location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (2 * blocksize));
                        block = new WaterBlock(location);
                        roomBlocks.Add(block);
                    }
                    break;
                case "23":
                    break;
                case "24":
                    break;
                case "25":
                    break;
                case "26":
                    break;
                case "27":
                    break;
                case "28":
                    break;
                case "29":
                    break;
                case "30":
                    break;
                case "31":
                    break;
                case "32":
                    break;
                case "33":
                    for (int i = 0; i <= 11; i++)
                    {
                        for (int j = 1; j <= 5; j+=4)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new WaterBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "34":
                    break;
                case "35":
                    break;
                case "36":
                    break;
                case "37":
                    for (int i = 4; i <= 7; i += 3)
                    {
                        for (int j = 0; j <= 6; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "38":
                    for (int i = 1; i <= 5; i += 2)
                    {
                        for (int j = 1; j <= 5; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 6; i <= 10; i += 2)
                    {
                        for (int j = 1; j <= 5; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    break;
                case "39":
                    for (int i = 0; i <= 4; i++)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 0; i <= 4; i++)
                    {
                        for (int j = 4; j <= 6; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 7; i <= 11; i++)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int i = 7; i <= 11; i++)
                    {
                        for (int j = 4; j <= 6; j++)
                        {
                            location = new Vector2(roomX + gridStartX + (i * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }
                    }
                    for (int j = 1; j <= 5; j++)
                    {
                        if (j != 3)
                        {
                            location = new Vector2(roomX + gridStartX + (5 * blocksize), roomY + gridStartY + (j * blocksize));
                            block = new StoneBlock(location);
                            roomBlocks.Add(block);
                        }                        
                    }
                    location = new Vector2(roomX + gridStartX + (6 * blocksize), roomY + gridStartY + (3 * blocksize));
                    block = new MoveableBlock(location);
                    roomBlocks.Add(block);
                    break;
                case "40":
                    break;
                case "41":
                    break;
                case "42":
                    break;
                default:
                    break;
            }
            return roomBlocks;
        }

        public void PairDoors()
        {
            List<Door> doors = new List<Door>();
            foreach (IStaticBlock block in allBlocks)
            {
                if (block.GetType() == typeof(Door))
                    doors.Add((Door)block);
            }
            foreach (Door door in doors)
            {
                if (door.nextRoom >= 0)
                {
                    foreach (Door otherDoor in doors)
                    {
                        if (roomContents[door.nextRoom][0].Contains(otherDoor))
                        {
                            switch (door.direction)
                            {
                                case Directions.Up:
                                    if (otherDoor.direction == Directions.Down)
                                        door.SetPairedDoor(otherDoor);
                                    break;
                                case Directions.Down:
                                    if (otherDoor.direction == Directions.Up)
                                        door.SetPairedDoor(otherDoor);
                                    break;
                                case Directions.Left:
                                    if (otherDoor.direction == Directions.Right)
                                        door.SetPairedDoor(otherDoor);
                                    break;
                                case Directions.Right:
                                    if (otherDoor.direction == Directions.Left)
                                        door.SetPairedDoor(otherDoor);
                                    break;
                            }
                        }
                    }
                }
            }
            doorList = doors;
        }
    }
}