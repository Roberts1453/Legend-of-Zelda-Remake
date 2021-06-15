using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    public class DeathMenu : IMenu
    {
        ISprite titleSprite = MenuSpriteFactory.Instance.CreateTitleSprite();
        ISprite fileScreenSprite = MenuSpriteFactory.Instance.CreateFileScreenSprite();
        ISprite nameScreenSprite = MenuSpriteFactory.Instance.CreateNameSelectSprite();

        ISprite fileSelectSprite = HUDSpriteFactory.Instance.CreateHeartFullSprite();
        ISprite nameSelectSprite = HUDSpriteFactory.Instance.CreateInventorySelectSprite();


        Link[] links = new Link[3];
        Camera cam;
        int saveNumber = 0;
        Text testText;
        Text[] names = new Text[3];

        public enum MenuState { Start, FileSelection, NewSave, Closing };
        public MenuState menuState;

        /*HUD Sections*/
        private Vector2 screenLocation = new Vector2(0, 0);
        private Vector2 alphabetLocation = new Vector2(20, 154);

        private Vector2 fileSelectLocation = new Vector2(133, 48);
        private Vector2 nameSelectLocation = new Vector2(19, 16);

        private Dictionary<int, Vector2> selectLocations = new Dictionary<int, Vector2>()
        {
            {0,  new Vector2(36, 92)},
            {1, new Vector2(36, 116)},
            {2, new Vector2(36, 140)},
        };

        int selection = 0;


        public Rectangle Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DeathMenu(Camera _cam, int _saveNumber)
        {
            cam = _cam;
            saveNumber = _saveNumber;
            screenLocation = cam.Location;
            menuState = MenuState.Start;


            SaveManager.Instance.SaveLoadManager(Game1.game.link);

            foreach (SaveFile file in SaveManager.Instance.fileList)
            {
                file.LoadPreview();
            }


            names[0] = new Text("CONTINUE", screenLocation + new Vector2(70, 92));
            names[1] = new Text("SAVE", screenLocation + new Vector2(70, 116));
            names[2] = new Text("RETRY", screenLocation + new Vector2(70, 140));

            names[0].AddText("CONTINUE");
            names[1].AddText("SAVE");
            names[2].AddText("RETRY");

        }

        public void SelectUpFile()
        {
            selection--;
            if (selection < 0)
                selection = 2;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                selection--;
                if (selection < 0)
                    selection = 2;
                if (falseCount >= 8)
                {
                    selection = 2;
                    break;
                }
                falseCount++;
            }
        }

        public void SelectDownFile()
        {
            selection++;
            if (selection > 2)
                selection = 0;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                Debug.WriteLine(!CheckSelect(selection));
                selection++;
                if (selection > 2)
                    selection = 0;
                if (falseCount >= 8)
                {
                    selection = 2;
                    break;
                }
                falseCount++;
            }
        }
        public void SelectLeftFile()
        {
            selection--;
            if (selection < 0)
                selection = 2;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                selection--;
                if (selection < 0)
                    selection = 2;
                if (falseCount >= 8)
                {
                    selection = 2;
                    break;
                }
                falseCount++;
            }
        }

        public void SelectRightFile()
        {
            selection++;
            if (selection > 2)
                selection = 0;
            int falseCount = 0;
            while (!CheckSelect(selection))
            {
                selection++;
                if (selection > 2)
                    selection = 0;
                if (falseCount >= 8)
                {
                    selection = 2;
                    break;
                }
                falseCount++;
            }
        }
        public void SelectUp()
        {
            SelectUpFile();
        }
        public void SelectDown()
        {
            SelectDownFile();

        }
        public void SelectLeft()
        {
            SelectLeftFile();
        }
        public void SelectRight()
        {
            SelectRightFile();
        }
        public void Select()
        {
            switch (selection)
            {
                case 0:
                    Game1.game.Continue();
                    Game1.game.currentMenu = Game1.game.UI;
                    Game1.game.gameState.Pop();

                    break;
                case 1:
                    Game1.game.currentMenu = Game1.game.startMenu;
                    Game1.game.startMenu.menuState = StartMenu.MenuState.FileSelection;
                    break;
                case 2:
                    SaveManager.Instance.Load(SaveManager.Instance.fileList[saveNumber]);
                    Game1.game.Continue();
                    Game1.game.gameState.Pop();
                    Game1.game.currentMenu = Game1.game.UI;
                    Game1.game.ChangeDungeon(0);
                    break;
                case 3:
                    break;
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
            return true;
        }
        public void Update(GameTime gameTime)
        {
            SoundManager.Instance.PlaySong("MenuSong");
            fileSelectLocation = selectLocations[selection];
            fileSelectSprite.Update(gameTime);


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            fileSelectSprite.Draw(spriteBatch, screenLocation + fileSelectLocation);
            names[0].Draw(spriteBatch);
            names[1].Draw(spriteBatch);
            names[2].Draw(spriteBatch);
        }

        public void SecondarySelectUp()
        {
            throw new NotImplementedException();
        }

        public void SecondarySelectDown()
        {
            throw new NotImplementedException();
        }

        public void Back()
        {
            throw new NotImplementedException();
        }
    }
}
