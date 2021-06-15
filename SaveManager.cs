using System;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.IO;
using System.Collections;

namespace Sprint0Project
{
    [XmlInclude(typeof(Compass))]
    public class SaveManager
    {
        private static SaveManager instance = new SaveManager();
        static Type[] savedTypes = new Type[]
        {
            typeof(BombCollectible),
            typeof(BoomerangCollectible),
            typeof(Bow),
            typeof(Clock),
            typeof(Compass),
            typeof(Heart),
            typeof(HeartContainer),
            typeof(Key),
            typeof(MagicKey),
            typeof(Map),
            typeof(RaftCollectible),
            typeof(Rupee),
            typeof(TriforcePiece)
        };
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData), savedTypes);
        Link link;

        public SaveFile[] fileList = new SaveFile[3] {
            new SaveFile() { fileName = "1.sav" },
            new SaveFile() { fileName = "2.sav" },
            new SaveFile() { fileName = "3.sav" },
        };

        public ArrayList[] dungeonCollectibles;


        public static SaveManager Instance
        {
            get
            {
                return instance;
            }
        }

        [XmlInclude(typeof(ArrowShop))]
        [XmlInclude(typeof(ArrowCollectible))]
        [XmlInclude(typeof(Compass))]
        [XmlInclude(typeof(BombCollectible))]
        [XmlInclude(typeof(BombShop))]
        [XmlInclude(typeof(BoomerangCollectible))]
            [XmlInclude(typeof(Bow))]
        [XmlInclude(typeof(CandleCollectible))]
        [XmlInclude(typeof(CandleShop))]
        [XmlInclude(typeof(Clock))]
            [XmlInclude(typeof(Compass))]
            [XmlInclude(typeof(Heart))]
            [XmlInclude(typeof(HeartContainer))]
            [XmlInclude(typeof(Key))]
        [XmlInclude(typeof(LadderCollectible))]
        [XmlInclude(typeof(MagicBoomerangCollectible))]
        [XmlInclude(typeof(MagicKey))]
        [XmlInclude(typeof(Map))]
            [XmlInclude(typeof(RaftCollectible))]
        [XmlInclude(typeof(RodCollectible))]
        [XmlInclude(typeof(Rupee))]
        [XmlInclude(typeof(TriforcePiece))]
        [Serializable]
        public struct SaveData
        {
            public string PlayerName;
            public int maxHealth;
            public List<Items> obtainedItems;
            public List<ArrayList> collectibleList;
            public int rupees;
            public int bombs;
            public int keys;
            public List<int> triforcePieces;
            public List<int> hasCompass;
            public List<int> hasMap;
            public int dungeonNumber;
        }

        public void SaveLoadManager(Link link)
        {
            this.link = link;
        }

        public void SaveNew(SaveFile saveFile, string playerName)
        {
            saveFile.playerName = playerName;
            string fileName = saveFile.fileName;
            IsolatedStorageFile dataFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream isolatedFileStream = null;

            SaveData saveData = new SaveData()
            {
                PlayerName = playerName,
                maxHealth = 6,
                rupees = 0,
                bombs = 0,
                keys = 0,
                dungeonNumber = 0,
                collectibleList = new List<ArrayList>(new ArrayList[10])
            };
            for (int i = 0; i < Game1.game.dungeonList.Count; i++)
            {
                saveData.collectibleList[i] = Game1.game.dungeonList[i].collectibles;
            }

            if (dataFile.FileExists(fileName))
            {
                dataFile.DeleteFile(fileName);
            }
            using (isolatedFileStream = dataFile.CreateFile(fileName))
            {
                isolatedFileStream.Seek(0, SeekOrigin.Begin);
                serializer.Serialize(isolatedFileStream, saveData);
                isolatedFileStream.SetLength(isolatedFileStream.Position);
            }
            dataFile.Close();
            isolatedFileStream.Dispose();



        }
        public void Save(SaveFile saveFile)
        {
            link = Game1.game.link;
            string fileName = saveFile.fileName;
            IsolatedStorageFile dataFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream isolatedFileStream = null;            

            SaveData saveData = new SaveData()
            {
                PlayerName = saveFile.playerName,
                maxHealth = link.health.maxHealth,
                obtainedItems = link.obtainedItems,
                collectibleList = new List<ArrayList>(new ArrayList[10]),


                rupees = link.rupees,
                bombs = link.bombs,
                keys = link.keys,
                triforcePieces = link.triforcePieces,
                hasCompass = link.hasCompass,
                hasMap = link.hasMap,
                dungeonNumber = Game1.game.currentDungeon
            };
            for (int i = 0; i < Game1.game.dungeonList.Count; i++)
            {
                saveData.collectibleList[i] = Game1.game.dungeonList[i].collectibles;
            }


            if (dataFile.FileExists(fileName))
            {
                dataFile.DeleteFile(fileName);
            }
            using (isolatedFileStream = dataFile.CreateFile(fileName))
            {
                isolatedFileStream.Seek(0, SeekOrigin.Begin);
                serializer.Serialize(isolatedFileStream, saveData);
                isolatedFileStream.SetLength(isolatedFileStream.Position);
            }
            dataFile.Close();
            isolatedFileStream.Dispose();
        }
        public void Save(int selection)
        {
            link = Game1.game.link;
            string fileName = fileList[selection].fileName;
            IsolatedStorageFile dataFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream isolatedFileStream = null;

            SaveData saveData = new SaveData()
            {
                PlayerName = fileList[selection].playerName,
                maxHealth = link.health.maxHealth,
                obtainedItems = link.obtainedItems,
                collectibleList = new List<ArrayList>(new ArrayList[10]),


                rupees = link.rupees,
                bombs = link.bombs,
                keys = link.keys,
                triforcePieces = link.triforcePieces,
                hasCompass = link.hasCompass,
                hasMap = link.hasMap,
                dungeonNumber = Game1.game.currentDungeon
            };
            for (int i = 0; i < Game1.game.dungeonList.Count; i++)
            {
                saveData.collectibleList[i] = Game1.game.dungeonList[i].collectibles;
            }


            if (dataFile.FileExists(fileName))
            {
                dataFile.DeleteFile(fileName);
            }
            using (isolatedFileStream = dataFile.CreateFile(fileName))
            {
                isolatedFileStream.Seek(0, SeekOrigin.Begin);
                serializer.Serialize(isolatedFileStream, saveData);
                isolatedFileStream.SetLength(isolatedFileStream.Position);
            }
            dataFile.Close();
            isolatedFileStream.Dispose();
        }
        public void Load (SaveFile saveFile)
        {
            string fileName = saveFile.fileName;
            IsolatedStorageFile dataFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream isolatedFileStream = null;

            if (dataFile.FileExists(fileName))
            {
                using (isolatedFileStream = dataFile.OpenFile(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    SaveData saveData = (SaveData)serializer.Deserialize(isolatedFileStream);

                    //Load save data to link
                    link.health.SetHealth(saveData.maxHealth);
                    link.obtainedItems = saveData.obtainedItems;
                        for (int i = 0; i < Game1.game.dungeonList.Count; i++)
                        {
                            Game1.game.dungeonList[i].collectibles = saveData.collectibleList[i];
                        }

                    link.rupees = saveData.rupees;
                    link.bombs = saveData.bombs;
                    link.keys = saveData.keys;
                    link.triforcePieces = saveData.triforcePieces;
                    link.hasCompass = saveData.hasCompass;
                    link.hasMap = saveData.hasMap;
                    Game1.game.currentDungeon = saveData.dungeonNumber;
                    Game1.game.ChangeDungeon(Game1.game.currentDungeon);
                }
                dataFile.Close();
                isolatedFileStream.Dispose();
            }
            
        }
        public void LoadPreviewData(SaveFile saveFile)
        {
            IsolatedStorageFile dataFile = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream isolatedFileStream = null;

            if (dataFile.FileExists(saveFile.fileName))
            {
                using (isolatedFileStream = dataFile.OpenFile(saveFile.fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    SaveData saveData = (SaveData)serializer.Deserialize(isolatedFileStream);

                    //Load save data to file
                    saveFile.empty = false;
                    saveFile.playerName = saveData.PlayerName;
                    saveFile.maxHealth = saveData.maxHealth;


                }
                dataFile.Close();
                isolatedFileStream.Dispose();
            }

        }
    }
}
