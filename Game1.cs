using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Collections.Generic;
using XMLData;
/*
 * Authors:
 * Joshua Jacobs
 * Geoffrey Heider 
 * Connor Redslob
 * Cody Roberts
 * */
namespace Sprint0Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        public IController keyboardController;
        public IController gamePadController;
        public Texture2D spriteSheet;
        public Texture2D dungeonSpriteSheet;
        private SpriteBatch spriteBatch;
        public List<ICharacter> enemies = new List<ICharacter>();
        public List<IStaticBlock> blocks = new List<IStaticBlock>();
        public Stack<IGameState> gameState = new Stack<IGameState>();
        public Link link;
        public Navi navi;

        public static Game1 game;
        public CollisionDetectionEnemys collisionDetectionEnemies;
        public CollisionDetectionBlocks collisionDetectionBlocks;
        public CollisionDetectionCollectibles collisionDetectionCollectibles;


        float scaleX;
        float scaleY;
        Matrix scaleMatrix;
        Matrix cameraMatrix;
        int WIDTH = 256;
        int HEIGHT = 240;

        public List<Camera> camList;
        public Camera cam;
        public GameUI UI;
        public StartMenu startMenu;
        public DeathMenu deathMenu;
        public IMenu currentMenu;

        public Textbox textbox;

        public EnemyDeathHandler enemyDeathHandler;
        public Dungeon dungeon;
        public DungeonLoader dungeonLoader;
        public List<DungeonLoader> dungeonList;
        public int currentDungeon;
        public int dungeonCount = 7;
        public int currentRoom;

        public int fileNum = 0;

        


        public Game1()
        {
            game = this;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            float scaler = (float)3.0 / (float)4.0;
            scaleX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / WIDTH * scaler;
            scaleY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / HEIGHT * scaler;
            scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            currentDungeon = 1;
            currentRoom = 0;
            cam = new Camera(new Vector2(0,0));
            cameraMatrix = Matrix.CreateTranslation(new Vector3(-cam.Location.X, -cam.Location.Y, 0));
            cameraMatrix *= scaleMatrix;

            

        }

        public void Quit()
        {
            this.Exit();
        }

        public void Reset()
        {
            
            dungeon = new Dungeon();
            currentDungeon = 1;
            currentRoom = 1;
            dungeonList = new List<DungeonLoader>();
            for (int i = 0; i < dungeonCount; i++)
            {
                dungeonLoader = new DungeonLoader(i);
                dungeonList.Add(dungeonLoader);
            }
            cam = new Camera(dungeonList[currentDungeon].roomList, currentDungeon);
            cam.ChangeRoom(currentRoom);

            enemyDeathHandler = new EnemyDeathHandler(this);

            link = new Link(new Vector2(cam.Location.X + 128, cam.Location.Y + 200)); /*Create link at start location*/
            navi = new Navi(link.location, link);
            UI = new GameUI(link, cam);
        }
        public void Continue()
        {
            if (currentDungeon!= 1)
                currentRoom = 0;
            else
                currentRoom = 1;
            cam.ChangeRoom(currentRoom);
            link.location = new Vector2(cam.Location.X + 128, cam.Location.Y + 200);
            link.health.currentHealth = link.health.maxHealth;
            enemyDeathHandler = new EnemyDeathHandler(this);
        }

        protected override void Initialize()
        {
            keyboardController = new KeyboardController();
            keyboardController.SetKeyCommands(this);
            gamePadController = new GamePadController();
            gamePadController.SetKeyCommands(this);

            graphics.PreferredBackBufferWidth = (int) (WIDTH * scaleX);
            graphics.PreferredBackBufferHeight = (int) (HEIGHT * scaleY);
            graphics.ApplyChanges();

            base.Initialize();

            gameState.Push(new PlayState(this));
            gameState.Push(new MenuState(this));

            

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            DungeonSpriteFactory.Instance.LoadAllTextures(Content);
            OverworldSpriteFactory.Instance.LoadAllTextures(Content);
            HUDSpriteFactory.Instance.LoadAllTextures(Content);
            MenuSpriteFactory.Instance.LoadAllTextures(Content);
            textbox = new Textbox();

            SoundManager.Instance.LoadSounds(Content);

            spriteSheet = Content.Load<Texture2D>("LOZLinkSpritesheet");

            currentDungeon = 0;
            currentRoom = 0;
            dungeon = new Dungeon();            
            dungeonList = new List<DungeonLoader>();
            camList = new List<Camera>();
            for (int i = 0; i < dungeonCount; i++)
            {
                dungeonLoader = new DungeonLoader(i);
                dungeonList.Add(dungeonLoader);
                camList.Add(new Camera(dungeonList[i].roomList, i));
            }
            cam = camList[currentDungeon];
            cam.ChangeRoom(currentRoom);



            enemies.Clear();
            blocks.Clear();
            enemyDeathHandler = new EnemyDeathHandler(this);
            link = new Link(new Vector2(cam.Location.X + 128, cam.Location.Y + 200)); /*Create link at start location*/
            navi = new Navi(link.location, link);

            collisionDetectionEnemies = new CollisionDetectionEnemys(this);
            collisionDetectionBlocks = new CollisionDetectionBlocks(this);
            collisionDetectionCollectibles = new CollisionDetectionCollectibles(this);

            UI = new GameUI(link, cam);
            startMenu = new StartMenu(cam);
            deathMenu = new DeathMenu(cam, 0);
            currentMenu = startMenu;
        }

        public void ChangeDungeon(int newDungeon)
        {
            currentDungeon = newDungeon;
            if (currentDungeon == dungeonCount)
                currentDungeon = 0;
            else if (currentDungeon < 0)
                currentDungeon = dungeonCount - 1;
            currentRoom = 0;
            cam = camList[currentDungeon];
            cam.ChangeRoom(currentRoom);
            UI.ChangeDungeon(cam, currentDungeon);
            link.location = new Vector2(cam.Location.X + 128, cam.Location.Y + 192);
        }
        public void ChangeDungeon(int newDungeon, int newRoom)
        {
            currentDungeon = newDungeon;
            if (currentDungeon == dungeonCount)
                currentDungeon = 0;
            else if (currentDungeon < 0)
                currentDungeon = dungeonCount - 1;
            currentRoom = newRoom;
            cam = camList[currentDungeon];
            cam.ChangeRoom(currentRoom);
            UI.ChangeDungeon(cam, currentDungeon);
            if (currentRoom <= 1)
                link.location = new Vector2(cam.Location.X + 128, cam.Location.Y + 192);
            else
                link.location = new Vector2(cam.Location.X + 128, cam.Location.Y + 100);
        }
        public void ChangeDungeon(int newDungeon, int newRoom, Vector2 exitLocation)
        {
            currentDungeon = newDungeon;
            if (currentDungeon == dungeonCount)
                currentDungeon = 0;
            else if (currentDungeon < 0)
                currentDungeon = dungeonCount - 1;
            currentRoom = newRoom;
            cam = camList[currentDungeon];
            cam.ChangeRoom(currentRoom);
            UI.ChangeDungeon(cam, currentDungeon);
            link.location = cam.Location + exitLocation;
        }
        public void PlayerDeath()
        {
            currentMenu = new DeathMenu(cam, fileNum);
        }

        protected override void Update(GameTime gameTime)
        {
            cameraMatrix = Matrix.CreateTranslation(new Vector3(-cam.Location.X * scaleX, -cam.Location.Y * scaleY, 0));
            
            gameState.Peek().Update(gameTime);

            navi.Update(gameTime);
            currentRoom = cam.roomNumber;            
        }
    protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Texture2D hitBox = new Texture2D(graphics.GraphicsDevice, 80, 48);
            Color[] colorData = new Color[80 * 48];
            for (int i = 0; i < colorData.Length; i++) colorData[i] = Color.Magenta;
            hitBox.SetData(colorData);

            spriteBatch.Begin(transformMatrix: this.scaleMatrix * this.cameraMatrix);

            if (currentMenu == UI)
            {
                foreach (Room room in dungeonList[currentDungeon].roomList)
                {
                    room.Draw(spriteBatch);
                }
                foreach (IStaticBlock block in dungeonList[currentDungeon].allBlocks)
                {
                    block.Draw(spriteBatch);
                }
                //Draws enemy hurt boxes for testing
                foreach (ICharacter enemy in dungeonList[currentDungeon].roomContents[cam.roomNumber][1])
                {
                    //if (enemy.GetVertRange() != null)
                    // spriteBatch.Draw(hitBox, new Vector2(enemy.GetVertRange().X, enemy.GetVertRange().Y), enemy.GetVertRange(), Color.White);
                    //if (enemy.GetHorizRange() != null)
                    //spriteBatch.Draw(hitBox, new Vector2(enemy.GetHorizRange().X, enemy.GetHorizRange().Y), enemy.GetHorizRange(), Color.White);

                    if (enemy.GetItems() != null)
                        spriteBatch.Draw(hitBox, new Vector2(enemy.GetItems().GetEdges().X, enemy.GetItems().GetEdges().Y), enemy.GetItems().GetEdges(), Color.White);

                }
                foreach (ICharacter enemy in dungeonList[currentDungeon].roomContents[cam.roomNumber][1])
                {
                    enemy.Draw(spriteBatch);
                }
                foreach (ICollectibleItem item in dungeonList[currentDungeon].collectibles)
                {
                    item.Draw(spriteBatch);
                }

                link.Draw(spriteBatch);
                navi.Draw(spriteBatch);

                foreach (DoorTop doorTop in dungeonList[currentDungeon].doorTops)
                {
                    doorTop.Draw(spriteBatch);
                }
            }
            //UI.Draw(spriteBatch);

            currentMenu.Draw(spriteBatch);

            gameState.Peek().Draw(spriteBatch);

            

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
