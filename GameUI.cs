using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    public class GameUI : IMenu
    {
        ISprite hudSprite = HUDSpriteFactory.Instance.CreateHUDSprite();
        ISprite hudInventorySprite = HUDSpriteFactory.Instance.CreateInventorySprite();
        ISprite hudMapSprite = HUDSpriteFactory.Instance.CreateMapScreenSprite();

        ISprite compassSprite = HUDSpriteFactory.Instance.CreateCompassSprite();
        ISprite mapSprite = HUDSpriteFactory.Instance.CreateMapSprite();

        /*Inventory Sprites*/
        ISprite raftSprite = HUDSpriteFactory.Instance.CreateRaftSprite();
        ISprite ladderSprite = HUDSpriteFactory.Instance.CreateLadderSprite();
        ISprite magicKeySprite = HUDSpriteFactory.Instance.CreateMagicKeySprite();
        ISprite boomerangSprite = HUDSpriteFactory.Instance.CreateBoomerangSprite();
        ISprite bombSprite = HUDSpriteFactory.Instance.CreateBombSprite();
        ISprite arrowSprite = HUDSpriteFactory.Instance.CreateArrowSprite();
        ISprite bowSprite = ItemSpriteFactory.Instance.CreateBowSprite();
        ISprite candleSprite = HUDSpriteFactory.Instance.CreateCandleSprite();
        ISprite rodSprite = HUDSpriteFactory.Instance.CreateRodSprite();

        ISprite levelCountSprite = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite rupeeCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite keyCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite bombCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite rupeeCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite keyCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite();
        ISprite bombCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite();

        ISprite equipedItemSprite = HUDSpriteFactory.Instance.CreateBoomerangSprite();
        ISprite swordSprite = HUDSpriteFactory.Instance.CreateSwordSprite();

        ISprite linkLocationSprite = HUDSpriteFactory.Instance.CreateLinkMapLocationSprite();

        ISprite[] hearts = new ISprite[16];
        ISprite[] minimapBlocks;

        ISprite inventorySelectSprite = HUDSpriteFactory.Instance.CreateInventorySelectSprite();

        Link link;
        Camera cam;

        public enum MenuState { Open, Closed, Opening, Closing};
        public MenuState menuState;

        /*HUD Sections*/
        private Vector2 hudLocation;
        private Vector2 hudInventoryLocation;
        private Vector2 hudMapLocation;

        private Vector2 mapItemLocation = new Vector2(48, 24);
        private Vector2 compassItemLocation = new Vector2(44, 64);

        /*Inventory*/        
        private Vector2 inventorySelectLocation = new Vector2(133, 48);
        private Vector2 raftLocation = new Vector2(128, 24);
        private Vector2 ladderLocation = new Vector2(177, 24);
        private Vector2 magicKeyLocation = new Vector2(196, 24);
        private Vector2 boomerangLocation = new Vector2(133, 48);
        private Vector2 bombLocation = new Vector2(156, 48);
        private Vector2 arrowLocation = new Vector2(176, 48);
        private Vector2 bowLocation = new Vector2(184, 48);
        private Vector2 candleLocation = new Vector2(204, 48);
        private Vector2 rodLocation = new Vector2(204, 64);
        private Dictionary<int, Vector2> selectLocations = new Dictionary<int, Vector2>()
        {
            {0,  new Vector2(129, 48)},
            {1, new Vector2(152, 48)},
            {2, new Vector2(172, 48)},
            {3, new Vector2(200, 48)},
            {4,  new Vector2(129, 64)},
            {5, new Vector2(152, 64)},
            {6, new Vector2(172, 64)},
            {7, new Vector2(200, 64)}
        };

        int selection = 0;

        /*Main HUD Items*/
        private Vector2 blockLocation;
        private Vector2 levelCountLocation = new Vector2(64, 17);
        private Vector2 minimapLocation = new Vector2(16, 25);
        private Vector2 rupeeCountLocation = new Vector2(104, 25);
        private Vector2 keyCountLocation = new Vector2(104, 41);
        private Vector2 bombCountLocation = new Vector2(104, 49);

        private Vector2 itemLocation = new Vector2(128, 33);
        private Vector2 itemLocation2 = new Vector2(68, 48);
        private Vector2 swordLocation = new Vector2(152, 33);

        private Vector2 heartLocation = new Vector2(176, 49);

        int currentDungeon;

        public Rectangle Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameUI(Link _link, Camera _cam)
        {
            link = _link;
            cam = _cam;
            currentDungeon = 1;
            menuState = MenuState.Closed;
            for (int i = 0; i < (link.health.maxHealthCap+1)/2; i++)
                hearts[i] = HUDSpriteFactory.Instance.CreateHeartEmptySprite();
            minimapBlocks = new ISprite[cam.dungeonCoodinates.Count];
            for (int i = 0; i < minimapBlocks.Length; i++)
                minimapBlocks[i] = HUDSpriteFactory.Instance.CreateMinimapBlockSprite();
        }
        public void ChangeDungeon(Camera _cam, int newDungeon)
        {
            cam = _cam;
            currentDungeon = newDungeon;
        }
        public void OpenCloseInventory()
        {
            if (menuState == MenuState.Open)
            {
                Game1.game.gameState.Pop();
                menuState = MenuState.Closing;
            }
            else if (menuState == MenuState.Closed)
            {
                Game1.game.gameState.Push(new BusyState(Game1.game));
                menuState = MenuState.Opening;
            }
        }
        public void SelectUp()
        {
            if (selection >= 4)
                selection -= 4;
            int falseCount = 0;
            while (!CheckInventory())
            {
                selection++;
                if (selection < 0)
                    selection = 7;
                if (falseCount >= 8)
                {
                    selection = 0;
                    break;
                }
                falseCount++;
            }
        }

        public void SelectDown()
        {
            if (selection <= 3)
                selection+=4;
            int falseCount = 0;
            while (!CheckInventory())
            {
                selection++;
                if (selection > 7)
                    selection = 0;
                if (falseCount >= 8)
                {
                    selection = 0;
                    break;
                }
                falseCount++;
            }
        }

        public void SelectRight()
        {
            selection++;
            if (selection > 7)
                selection = 0;
            int falseCount = 0;
            while (!CheckInventory())
            {
                selection++;
                if (selection > 7)
                    selection = 0;
                if (falseCount >= 8)
                {
                    selection = 0;
                    break;
                }
                falseCount++;
            }
            
        }

        public void SelectLeft()
        {
            selection--;
            if (selection < 0)
                selection = 7;
            int falseCount = 0;
            while (!CheckInventory())
            {
                selection++;
                if (selection < 0)
                    selection = 7;
                if (falseCount >= 8)
                {
                    selection = 0;
                    break;
                }
                falseCount++;
            }
        }
        public void Select()
        {
            switch (selection)
            {
                case 0:
                    if (link.obtainedItems.Contains(Items.Boomerang))
                        link.equipedItem = Items.Boomerang;
                    break;
                case 1:
                    if (link.obtainedItems.Contains(Items.Bomb))
                        link.equipedItem = Items.Bomb;
                    break;
                case 2:
                    if (link.obtainedItems.Contains(Items.Bow) && link.obtainedItems.Contains(Items.Arrow))
                        link.equipedItem = Items.Arrow;
                    //else if (link.obtainedItems.Contains(Items.Bow) && link.obtainedItems.Contains(Items.SilverArrow))
                        //link.equipedItem = Items.SilverArrow;
                    break;
                case 3:
                    if (link.obtainedItems.Contains(Items.Candle))
                        link.equipedItem = Items.Candle;
                    //else if (link.obtainedItems.Contains(Items.RedCandle))
                        //link.equipedItem = Items.RedCandle;
                    break;
                case 7:
                    if (link.obtainedItems.Contains(Items.Rod))
                        link.equipedItem = Items.Rod;
                    break;
            }
        }
        public bool CheckInventory()
        {
            switch (selection)
            {
                case 0:
                    if (link.obtainedItems.Contains(Items.Boomerang))
                        return true;
                    break;
                case 1:
                    if (link.obtainedItems.Contains(Items.Bomb))
                        return true;
                    break;
                case 2:
                    if (link.obtainedItems.Contains(Items.Bow) && link.obtainedItems.Contains(Items.Arrow))
                        return true;
                    break;
                case 3:
                    if (link.obtainedItems.Contains(Items.Candle))
                        return true;
                    break;
                case 7:
                    if (link.obtainedItems.Contains(Items.Rod))
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }
        public void Update(GameTime gameTime)
        {
            if (menuState == MenuState.Closed)
            {
                hudLocation = cam.Location;
                hudInventoryLocation = new Vector2(cam.Location.X, cam.Location.Y - cam.screenHeight);
                hudMapLocation = new Vector2(cam.Location.X, hudInventoryLocation.Y + hudInventorySprite.Edges().Height);
                //hudInventoryLocation = new Vector2(cam.Location.X, cam.inventoryLocation.Y);
                //hudMapLocation = new Vector2(cam.Location.X, cam.inventoryLocation.Y + hudInventorySprite.Edges().Height);

            }

            levelCountSprite = HUDSpriteFactory.Instance.CreateNumberSprite(currentDungeon);
            if (link.rupees >= 10)
            {
                rupeeCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.rupees / 10);
                rupeeCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite(link.rupees % 10);
            }
            else
                rupeeCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.rupees % 10);
            if (link.obtainedItems.Contains(Items.MagicKey))
                keyCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(10);
            else if (link.keys >= 10)
            {
                keyCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.keys / 10);
                keyCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite(link.keys % 10);
            }
            else
                keyCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.keys % 10);
            if (link.bombs >= 10)
            {
                bombCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.bombs / 10);
                bombCountSprite2 = HUDSpriteFactory.Instance.CreateNumberSprite(link.bombs % 10);
            }
            else
                bombCountSprite1 = HUDSpriteFactory.Instance.CreateNumberSprite(link.bombs % 10);  

            /*Finds heart meter based on health*/
            //each heart worth 2 health. heart# > (link.GetHealth()+1) /2 is empty
            // if health odd, heart# = (link.GetHealth()+1) /2 is half            
            for (int i = 0; i < (link.health.maxHealthCap+1) / 2; i++) {                 
                if (i < link.health.currentHealth / 2) //make hearts lower than current health full
                    hearts[i] = HUDSpriteFactory.Instance.CreateHeartFullSprite(); 
                if (i > (link.health.currentHealth / 2)-1) //make hearts higher than current health empty
                    hearts[i] = HUDSpriteFactory.Instance.CreateHeartEmptySprite();
                if (i == link.health.currentHealth / 2 && link.health.currentHealth % 2 != 0) //make current heart half if health is an odd number
                    hearts[i] = HUDSpriteFactory.Instance.CreateHeartHalfSprite();                
            }

            if (link.obtainedItems.Contains(Items.MagicBoomerang))
                boomerangSprite = HUDSpriteFactory.Instance.CreateMagicBoomerangSprite();

            switch (link.equipedItem)
            {
                case Items.Boomerang:
                    equipedItemSprite = boomerangSprite;
                    break;
                case Items.Bomb:
                    equipedItemSprite = bombSprite;
                    break;
                case Items.Arrow:
                    equipedItemSprite = arrowSprite;
                    break;
                case Items.Rod:
                    equipedItemSprite = rodSprite;
                    break;
                case Items.Candle:
                    equipedItemSprite = candleSprite;
                    break;
            }

            inventorySelectLocation = selectLocations[selection];
            inventorySelectSprite.Update(gameTime);


            if (menuState == MenuState.Opening)
            {
                if (!cam.showInventory)
                {
                    cam.OpenInventory();
                }
                else
                    menuState = MenuState.Open;
            }
            else if (menuState == MenuState.Closing)
            {
                if (cam.showInventory)
                {
                    cam.CloseInventory();
                }
                else
                    menuState = MenuState.Closed;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            /*HUD screen sections*/
            hudSprite.Draw(spriteBatch, hudLocation);
            hudInventorySprite.Draw(spriteBatch, hudInventoryLocation);
            hudMapSprite.Draw(spriteBatch, hudMapLocation);

            /*Draw inventory sprites*/
            inventorySelectSprite.Draw(spriteBatch, hudInventoryLocation + inventorySelectLocation);
            if (link.hasMap.Contains(cam.dungeonNumber))
                mapSprite.Draw(spriteBatch, hudMapLocation + mapItemLocation);
            if (link.hasCompass.Contains(cam.dungeonNumber))
                compassSprite.Draw(spriteBatch, hudMapLocation + compassItemLocation);
            if (link.obtainedItems.Contains(Items.Raft))
                raftSprite.Draw(spriteBatch, hudInventoryLocation + raftLocation);
            if (link.obtainedItems.Contains(Items.Ladder))
                ladderSprite.Draw(spriteBatch, hudInventoryLocation + ladderLocation);
            if (link.obtainedItems.Contains(Items.MagicKey))
                magicKeySprite.Draw(spriteBatch, hudInventoryLocation + magicKeyLocation);
            if (link.obtainedItems.Contains(Items.Boomerang))
                boomerangSprite.Draw(spriteBatch, hudInventoryLocation + boomerangLocation);
            if (link.obtainedItems.Contains(Items.Bomb))
                bombSprite.Draw(spriteBatch, hudInventoryLocation + bombLocation);
            if (link.obtainedItems.Contains(Items.Arrow))
                arrowSprite.Draw(spriteBatch, hudInventoryLocation + arrowLocation);
            if (link.obtainedItems.Contains(Items.Bow))
                bowSprite.Draw(spriteBatch, hudInventoryLocation + bowLocation);
            if (link.obtainedItems.Contains(Items.Candle))
                candleSprite.Draw(spriteBatch, hudInventoryLocation + candleLocation);
            if (link.obtainedItems.Contains(Items.Rod))
                rodSprite.Draw(spriteBatch, hudInventoryLocation + rodLocation);

            /*Draw main HUD sprites*/
            levelCountSprite.Draw(spriteBatch, hudLocation + levelCountLocation);
            rupeeCountSprite1.Draw(spriteBatch, hudLocation + rupeeCountLocation);
            keyCountSprite1.Draw(spriteBatch, hudLocation + keyCountLocation);            
            bombCountSprite1.Draw(spriteBatch, hudLocation + bombCountLocation);
            if (link.rupees >= 10)
                rupeeCountSprite2.Draw(spriteBatch, hudLocation + rupeeCountLocation + new Vector2(8, 0));
            if (link.keys >= 10)
                keyCountSprite2.Draw(spriteBatch, hudLocation + keyCountLocation + new Vector2(8, 0));
            if (link.bombs >= 10)
                bombCountSprite2.Draw(spriteBatch, hudLocation + bombCountLocation + new Vector2(8, 0));

            if (link.equipedItem != Items.None) {
                equipedItemSprite.Draw(spriteBatch, hudLocation + itemLocation);
                equipedItemSprite.Draw(spriteBatch, hudInventoryLocation + itemLocation2);
            }
            
            swordSprite.Draw(spriteBatch, hudLocation + swordLocation);

            /*Draw minimap*/
            for (int i = 0; i < minimapBlocks.Length; i++)
            {
                if (link.hasMap.Contains(cam.dungeonNumber) || cam.visitedRooms.Contains(i))
                {
                    blockLocation = new Vector2(cam.dungeonCoodinates[i].X / cam.roomWidth * 8, cam.dungeonCoodinates[i].Y / cam.roomHeight * 4);
                    minimapBlocks[i].Draw(spriteBatch, hudLocation + minimapLocation + blockLocation);
                }
            }
            blockLocation = new Vector2(cam.dungeonCoodinates[cam.roomNumber].X / cam.roomWidth * 8 + 2, cam.dungeonCoodinates[cam.roomNumber].Y / cam.roomHeight * 4);
            linkLocationSprite.Draw(spriteBatch, hudLocation + minimapLocation + blockLocation);
            
            /*Draw heart meter*/
            for (int i = 0; i < (link.health.maxHealth+1) / 2; i++)
            {
                Vector2 heartOffset = new Vector2(8 * (i % 8), (-i / 8)*8);
                hearts[i].Draw(spriteBatch, hudLocation + heartLocation + heartOffset);
            }
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
