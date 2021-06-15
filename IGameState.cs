using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Project
{
	
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
    internal class PlayState : IGameState
    {
        Game1 game;
        public PlayState(Game1 game)
        {
            this.game = game;

        }
        public void Update(GameTime gameTime)
        {
            

            game.UI.Update(gameTime);

            if (game.cam.cameraTransition == Camera.CameraTransition.Still)
            {
                if (game.UI.menuState == GameUI.MenuState.Closed)
                {
                    game.keyboardController.Update(game);
                    game.gamePadController.Update(game);

                    /*Update each item in current room*/
                    foreach (ICharacter enemy in game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][1])
                    {
                        enemy.Update(gameTime);
                    }
                    foreach (IStaticBlock block in game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][0])
                    {
                        block.Update(gameTime);
                        if (game.currentRoom != game.cam.roomNumber && block.GetType() == typeof(MoveableBlock))
                        {
                            MoveableBlock thisBlock = (MoveableBlock)block;
                            thisBlock.Reset();
                        }

                    }
                    foreach (ICollectibleItem item in game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][2])
                    {
                        item.Update(gameTime);
                    }

                    /*Check collision for items in current room*/
                    game.collisionDetectionEnemies.Update((List<ICharacter>)game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][1]);
                    game.collisionDetectionBlocks.Update(game.dungeonList[game.currentDungeon].allBlocks, (List<ICharacter>)game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][1]);
                    game.collisionDetectionBlocks.RoomCollision(game.dungeonList[game.currentDungeon].roomList, (List<ICharacter>)game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber][1], game.link, game.cam);
                    game.collisionDetectionCollectibles.Update(game.dungeonList[game.currentDungeon].collectibles);
                }
                else if (game.UI.menuState == GameUI.MenuState.Open)
                {
                    game.keyboardController.Update(game);
                }
            }
            game.link.Update(gameTime);


            /*Remove killed enemies and collected items*/
            game.enemyDeathHandler.Update(game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber]);
            ICollectibleItem toRemove = null;
            foreach (ICollectibleItem item in game.dungeonList[game.currentDungeon].collectibles)
            {
                if (!item.DestroyedState())
                {
                    toRemove = item;
                }
            }
            if (toRemove != null)
                game.dungeonList[game.currentDungeon].collectibles.Remove(toRemove);

            foreach (DoorTop doorTop in game.dungeonList[game.currentDungeon].doorTops)
            {
                doorTop.Update(gameTime);
            }

            RoomConditionChecker.RoomCondition(game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber]);
            RoomConditionChecker.MatchPairedDoors(game.dungeonList[game.currentDungeon].doorList);

            game.cam.Update();
            if (game.currentDungeon > 0)
                SoundManager.Instance.PlaySong("DungeonSong");
            else
                SoundManager.Instance.PlaySong("OverworldSong");
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
    internal class PauseState : IGameState
    {
        Game1 game;
        public PauseState(Game1 game)
        {
            this.game = game;

        }
        public void Update(GameTime gameTime)
        {
            game.UI.Update(gameTime);
            game.keyboardController.Update(game);
            game.gamePadController.Update(game);

            game.cam.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
    internal class BusyState : IGameState
    {
        Game1 game;
        public BusyState(Game1 game) //no input, ex room transition
        {
            this.game = game;

        }
        public void Update(GameTime gameTime)
        {
            game.UI.Update(gameTime);

            
            game.link.Update(gameTime);


            /*Remove killed enemies and collected items*/
            game.enemyDeathHandler.Update(game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber]);
            ICollectibleItem toRemove = null;
            foreach (ICollectibleItem item in game.dungeonList[game.currentDungeon].collectibles)
            {
                if (!item.DestroyedState())
                {
                    toRemove = item;
                }
            }
            if (toRemove != null)
                game.dungeonList[game.currentDungeon].collectibles.Remove(toRemove);

            foreach (DoorTop doorTop in game.dungeonList[game.currentDungeon].doorTops)
            {
                doorTop.Update(gameTime);
            }

            RoomConditionChecker.RoomCondition(game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber]);
            RoomConditionChecker.MatchPairedDoors(game.dungeonList[game.currentDungeon].doorList);

            game.cam.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
    internal class MenuState : IGameState
    {
        Game1 game;
        public MenuState(Game1 game)
        {
            this.game = game;

        }
        public void Update(GameTime gameTime)
        {
            game.currentMenu.Update(gameTime);
            game.keyboardController.Update(game);
            game.gamePadController.Update(game);

            //game.link.Update(gameTime);

            //RoomConditionChecker.RoomCondition(game.dungeonList[game.currentDungeon].roomContents[game.cam.roomNumber]);
            //RoomConditionChecker.MatchPairedDoors(game.dungeonList[game.currentDungeon].doorList);

            game.cam.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
    internal class TextboxState : IGameState
    {
        Game1 game;
        public TextboxState(Game1 game)
        {
            this.game = game;


        }
        public void Update(GameTime gameTime)
        {
            game.keyboardController.Update(game);
            game.gamePadController.Update(game);
            game.textbox.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            game.textbox.Draw(spriteBatch);

        }
    }
}
