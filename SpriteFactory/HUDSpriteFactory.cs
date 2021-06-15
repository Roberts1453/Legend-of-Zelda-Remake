using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class HUDSpriteFactory
    {
        private Texture2D hudSpritesheet;
        private Texture2D textSpritesheet;

        private static HUDSpriteFactory instance = new HUDSpriteFactory();

        public static HUDSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private HUDSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            hudSpritesheet = content.Load<Texture2D>("LOZMenuSpritesheet");
            textSpritesheet = content.Load<Texture2D>("LOZTextSpritesheet");

        }
        public ISprite CreateTextboxSprite()
        {
            return new TextboxSprite(textSpritesheet);
        }
        public ISprite CreateHUDSprite()
        {
            return new HUDSprite(hudSpritesheet);
        }
        public ISprite CreateInventorySprite()
        {
            return new InventorySprite(hudSpritesheet);
        }
        public ISprite CreateMapScreenSprite()
        {
            return new MapScreenSprite(hudSpritesheet);
        }
        public ISprite CreateCompassSprite()
        {
            return new CompassHUDSprite(hudSpritesheet);
        }
        public ISprite CreateMapSprite()
        {
            return new MapHUDSprite(hudSpritesheet);
        }

        public ISprite CreateMapBlockTopSprite()
        {
            return new MapBlockTopSprite(hudSpritesheet);
        }

        public ISprite CreateMapBlockBottomSprite()
        {
            return new MapBlockBottomSprite(hudSpritesheet);
        }

        public ISprite CreateMapBlockTopAndBottomSprite()
        {
            return new MapBlockTopAndBottomSprite(hudSpritesheet);
        }

        public ISprite CreateBlankLevelMapSprite()
        {
            return new BlankLevelMapSprite(hudSpritesheet);
        }
        public ISprite CreateMinimapBlockSprite()
        {
            return new MinimapBlockSprite(hudSpritesheet);
        }
        public ISprite CreateLinkMapLocationSprite()
        {
            return new LinkMapLocationSprite(hudSpritesheet);
        }
        public ISprite CreateHeartFullSprite()
        {
            return new HeartFullSprite(hudSpritesheet);
        }
        public ISprite CreateHeartHalfSprite()
        {
            return new HeartHalfSprite(hudSpritesheet);
        }
        public ISprite CreateHeartEmptySprite()
        {
            return new HeartEmptySprite(hudSpritesheet);
        }
        public ISprite CreateNumberSprite()
        {
            return new NumbersSprite(hudSpritesheet);
        }
        public ISprite CreateNumberSprite(int number)
        {
            return new NumbersSprite(hudSpritesheet, number);
        }

        public ISprite CreateInventorySelectSprite()
        {
            return new InventorySelectSprite(hudSpritesheet);
        }
        public ISprite CreateSwordSprite()
        {
            return new SwordInventorySprite(hudSpritesheet);
        }
        public ISprite CreateBoomerangSprite()
        {
            return new BoomerangInventorySprite(hudSpritesheet);
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            return new MagicBoomerangInventorySprite(hudSpritesheet);
        }
        public ISprite CreateLadderSprite()
        {
            return new LadderInventorySprite(hudSpritesheet);
        }
        public ISprite CreateBombSprite()
        {
            return new BombInventorySprite(hudSpritesheet);
        }
        public ISprite CreateArrowSprite()
        {
            return new ArrowInventorySprite(hudSpritesheet);
        }
        public ISprite CreateRaftSprite()
        {
            return new RaftInventorySprite(hudSpritesheet);
        }
        public ISprite CreateMagicKeySprite()
        {
            return new MagicKeyInventorySprite(hudSpritesheet);
        }
        public ISprite CreateRodSprite()
        {
            return new RodInventorySprite(hudSpritesheet);
        }
        public ISprite CreateCandleSprite()
        {
            return new CandleInventorySprite(hudSpritesheet);
        }
    }
}