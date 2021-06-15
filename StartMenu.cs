using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    public class StartMenu : IMenu
    {
        ISprite titleSprite = MenuSpriteFactory.Instance.CreateTitleSprite();
        ISprite fileScreenSprite = MenuSpriteFactory.Instance.CreateFileScreenSprite();
        ISprite nameScreenSprite = MenuSpriteFactory.Instance.CreateNameSelectSprite();


        ISprite[] hearts1 = new ISprite[16];
        ISprite[] hearts2 = new ISprite[16];
        ISprite[] hearts3 = new ISprite[16];

        ISprite fileSelectSprite = HUDSpriteFactory.Instance.CreateHeartFullSprite();
        ISprite nameSelectSprite = HUDSpriteFactory.Instance.CreateInventorySelectSprite();


        Camera cam;

        Text screenText;
        Text[] names = new Text[3];

        string newName = "";
        int fileNum;

        int link1HP = 0;
        int link2HP = 0;
        int link3HP = 0;

        public enum MenuState { Start, FileSelection, NewSave, Closing};
        public MenuState menuState;

        /*Sprite Locations*/
        private Vector2 screenLocation = new Vector2(0, 0);
        private Vector2 alphabetLocation = new Vector2(20, 154);

        private Vector2 fileSelectLocation = new Vector2(133, 48);
        private Vector2 nameSelectLocation = new Vector2(19, 16);

        private Dictionary<int, Vector2> selectLocations = new Dictionary<int, Vector2>()
        {
            {0,  new Vector2(36, 92)},
            {1, new Vector2(36, 116)},
            {2, new Vector2(36, 140)},
            {3, new Vector2(36, 168)},
            {4, new Vector2(36, 184)},
        };
        static private Dictionary<int, char> characterSelections = new Dictionary<int, char>()
        {
            {0, 'A'},
            {1, 'B'},
            {2, 'C'},
            {3, 'D'},
            {4, 'E'},
            {5, 'F'},
            {6, 'G'},
            {7, 'H'},
            {8, 'I'},
            {9, 'J'},
            {10, 'K'},
            {11, 'L'},
            {12, 'M'},
            {13, 'N'},
            {14, 'O'},
            {15, 'P'},
            {16, 'Q'},
            {17, 'R'},
            {18, 'S'},
            {19, 'T'},
            {20, 'U'},
            {21, 'V'},
            {22, 'W'},
            {23, 'X'},
            {24, 'Y'},
            {25, 'Z'},
            {26, '-'},
            {27, '.'},
            {28, ','},
            {29, '!'},
            {30, char.Parse("'")},
            {31, '&'},
            {32, '.'},
            {33, '0'},
            {34, '1'},
            {35, '2'},
            {36, '3'},
            {37, '4'},
            {38, '5'},
            {39, '6'},
            {40, '7'},
            {41, '8'},
            {42, '9'},
            {43, ' '},
        };

        int selection = 0;

        /*Heart Sprite Locations*/

        private Vector2 hearts1Location = new Vector2(170, 92);
        private Vector2 hearts2Location = new Vector2(170, 116);
        private Vector2 hearts3Location = new Vector2(170, 140);


        public Rectangle Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public StartMenu(Camera _cam)
        {
            cam = _cam;
            screenLocation = cam.Location;
            menuState = MenuState.Start;

            screenText = new Text("SPACE", screenLocation + new Vector2(103, 160));

            SaveManager.Instance.SaveLoadManager(Game1.game.link);

            foreach (SaveFile file in SaveManager.Instance.fileList)
            {
                file.LoadPreview();
            }


            names[0] = new Text(" ", screenLocation + new Vector2(70, 92));
            names[1] = new Text("  ---", screenLocation + new Vector2(70, 116));
            names[2] = new Text(" ---", screenLocation + new Vector2(70, 140));

            names[0].AddText(SaveManager.Instance.fileList[0].playerName);
            names[1].AddText(SaveManager.Instance.fileList[1].playerName);
            names[2].AddText(SaveManager.Instance.fileList[2].playerName);
            if (!SaveManager.Instance.fileList[0].empty)
            {
                names[0].AddText(SaveManager.Instance.fileList[0].playerName);
                link1HP = SaveManager.Instance.fileList[0].maxHealth;
            }
            else
                selection = 3;
            if (!SaveManager.Instance.fileList[1].empty)
            {
                names[1].AddText(SaveManager.Instance.fileList[1].playerName);
                link2HP = SaveManager.Instance.fileList[1].maxHealth;
            }
            if (!SaveManager.Instance.fileList[2].empty)
            {
                names[2].AddText(SaveManager.Instance.fileList[2].playerName);
                link3HP = SaveManager.Instance.fileList[2].maxHealth;
            }

            //for (int i = 0; i < (links[0].health.maxHealth+1)/2; i++)
            //hearts1[i] = HUDSpriteFactory.Instance.CreateHeartEmptySprite();
            for (int i = 0; i < (link1HP + 1) / 2; i++)
            {
                hearts1[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
            }
            for (int i = 0; i < (link2HP + 1) / 2; i++)
            {
                hearts2[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
            }
            for (int i = 0; i < (link3HP + 1) / 2; i++)
            {
                hearts3[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
            }
        }

        public void SelectUpFile()
        {
            selection--;
            if (selection < 0)
                selection = 4;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                selection--;
                if (selection < 0)
                    selection = 4;
                if (falseCount >= 5)
                {
                    selection = 3;
                    break;
                }
                falseCount++;
            }
        }

        public void SelectDownFile()
        {
            selection++;
            if (selection > 4)
                selection = 0;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                Debug.WriteLine(!CheckSelect(selection));
                selection++;
                if (selection > 4)
                    selection = 0;
                if (falseCount >= 5)
                {
                    selection = 3;
                    break;
                }
                falseCount++;
            }
        }
        public void SelectUp()
        {
            if (menuState == MenuState.FileSelection)
            {
                SelectUpFile();
            }
            else if (menuState == MenuState.NewSave)
            {
                SelectUpName();
            }
        }
        public void SelectDown()
        {
            if (menuState == MenuState.FileSelection)
            {
                SelectDownFile();
            }
            else if (menuState == MenuState.NewSave)
            {
                SelectDownName();
            }
        }
        public void SelectLeft()
        {
            if (menuState == MenuState.NewSave)
            {
                SelectLeftName();
            }
        }
        public void SelectRight()
        {
            if (menuState == MenuState.NewSave)
            {
                SelectRightName();
            }
        }
        public void SelectUpName()
        {
            if (selection <= 10)
                selection = -9;
            else
                selection -= 11;
        }
        public void SelectDownName()
        {
            if (selection >= 33)
                selection -= 33;
            else
                selection += 11;
        }
        public void SelectRightName()
        {
            if (selection == 10 || selection == 21 || selection == 32 || selection == 43)
                selection -= 10;
            else if (selection == -10)
                selection = 0;
            else
                selection++;
        }
        public void SelectLeftName()
        {            
            if (selection == 0 || selection == 11 || selection == 22 || selection == 33)
                selection += 10;
            else
                selection--;
        }
        public void Select()
        {
            if (menuState == MenuState.Start)
                menuState = MenuState.FileSelection;
            else if (menuState == MenuState.FileSelection)
            {
                switch (selection)
                {
                    case 0:
                        
                        SaveManager.Instance.Load(SaveManager.Instance.fileList[0]);
                        Game1.game.gameState.Pop();
                        Game1.game.currentMenu = Game1.game.UI;
                        Game1.game.fileNum = 0;
                        break;
                    case 1:
                        SaveManager.Instance.Load(SaveManager.Instance.fileList[1]);
                        Game1.game.gameState.Pop();
                        Game1.game.currentMenu = Game1.game.UI;
                        Game1.game.fileNum = 1;
                        break;
                    case 2:
                        SaveManager.Instance.Load(SaveManager.Instance.fileList[2]);
                        Game1.game.gameState.Pop();
                        Game1.game.currentMenu = Game1.game.UI;
                        Game1.game.fileNum = 2;
                        break;
                    case 3:                        
                        fileNum = FindEmptySave();
                        menuState = MenuState.NewSave;
                        break;
                    case 4:
                        break;
                }
            }
            else if (menuState == MenuState.NewSave)
            {
                if (selection <= -1)
                {
                    SaveManager.Instance.SaveLoadManager(Game1.game.link);
                    SaveManager.Instance.SaveNew(SaveManager.Instance.fileList[fileNum], newName);
                    SaveManager.Instance.Save(SaveManager.Instance.fileList[fileNum]);
                    Game1.game.gameState.Pop();
                    Game1.game.currentMenu = Game1.game.UI;
                    Game1.game.fileNum = fileNum;
                }
                else
                {
                    newName += characterSelections[selection];
                    names[fileNum].AddText(newName);
                }

            }

        }
        public int FindEmptySave()
        {
            int slot = 0;
            while (CheckSelect(slot))
            {
                slot++;
            }
            return slot;
        }
        public bool CheckSelect(int selection)
        {
            if (selection > 2)
                return true;
            else if (menuState != MenuState.NewSave)
            {
                if (!SaveManager.Instance.fileList[selection].empty)
                    return true;
            }
            return false;
        }
        public void Update(GameTime gameTime)
        {
            if (menuState == MenuState.Start)
            {
                titleSprite.Update(gameTime);
                SoundManager.Instance.PlaySong("MenuSong");
            }
            else if (menuState == MenuState.FileSelection)
            {
                /*Finds heart meter based on health*/
                for (int i = 0; i < (link1HP + 1) / 2; i++)
                {
                    hearts1[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
                }
                for (int i = 0; i < (link2HP + 1) / 2; i++)
                {
                    hearts2[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
                }
                for (int i = 0; i < (link3HP + 1) / 2; i++)
                {
                    hearts3[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite();
                }

                fileSelectLocation = selectLocations[selection];
                fileSelectSprite.Update(gameTime);
            }
            if (menuState == MenuState.NewSave)
            {
                nameSelectLocation = new Vector2(19, 16) + new Vector2(16 * (selection % 11), 16 * (selection / 11));
                nameSelectSprite.Update(gameTime);
                fileSelectLocation = selectLocations[fileNum];
                fileSelectSprite.Update(gameTime);
            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (menuState == MenuState.Start)
            {
                titleSprite.Draw(spriteBatch, screenLocation);
                screenText.Draw(spriteBatch);
            }
            else if (menuState == MenuState.FileSelection)
            {
                fileScreenSprite.Draw(spriteBatch, screenLocation);
                fileSelectSprite.Draw(spriteBatch, screenLocation + fileSelectLocation);
                names[0].Draw(spriteBatch);
                names[1].Draw(spriteBatch);
                names[2].Draw(spriteBatch);

                /*Draw heart meters*/
                for (int i = 0; i < (link1HP + 1) / 2; i++)
                {
                    Vector2 heartOffset = new Vector2(8 * (i % 8), (-i / 8) * 8);
                    hearts1[i].Draw(spriteBatch, screenLocation + hearts1Location + heartOffset);
                }
                for (int i = 0; i < (link2HP + 1) / 2; i++)
                {
                    Vector2 heartOffset = new Vector2(8 * (i % 8), (-i / 8) * 8);
                    hearts2[i].Draw(spriteBatch, screenLocation + hearts2Location + heartOffset);
                }
                for (int i = 0; i < (link3HP + 1) / 2; i++)
                {
                    Vector2 heartOffset = new Vector2(8 * (i % 8), (-i / 8) * 8);
                    hearts3[i].Draw(spriteBatch, screenLocation + hearts3Location + heartOffset);
                }
            }
            else if (menuState == MenuState.NewSave)
            {
                fileScreenSprite.Draw(spriteBatch, screenLocation);
                fileSelectSprite.Draw(spriteBatch, screenLocation + fileSelectLocation);
                nameScreenSprite.Draw(spriteBatch, screenLocation + alphabetLocation);
                nameSelectSprite.Draw(spriteBatch, screenLocation + alphabetLocation + nameSelectLocation);
                names[0].Draw(spriteBatch);
                names[1].Draw(spriteBatch);
                names[2].Draw(spriteBatch);
            }
            }

        public void SecondarySelectUp()
        {
            if (menuState == MenuState.NewSave)
            {
                newName = "  ---";
                names[fileNum].AddText(newName);
                newName = "";
                fileNum--;
                if (fileNum < 0)
                    fileNum = 2;
            }
        }

        public void SecondarySelectDown()
        {
            if (menuState == MenuState.NewSave)
            {
                newName = "  ---";
                names[fileNum].AddText(newName);
                newName = "";
                fileNum++;
                if (fileNum > 2)
                    fileNum = 0;
            }
        }

        public void Back()
        {
            if (menuState == MenuState.FileSelection)
            {
                menuState = MenuState.Start;
            }
            else if (menuState == MenuState.NewSave)
            {
                if (newName.Length > 0)
                {
                    newName = newName.Remove(newName.Length - 1, 1);
                    names[fileNum].AddText(newName);
                }
                else
                    menuState = MenuState.FileSelection;
            }
        }
    }
}
