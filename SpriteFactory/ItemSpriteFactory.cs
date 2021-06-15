using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpritesheet;
        private Texture2D itemSpritesheet2;
        private Texture2D itemSpritesheet3;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            itemSpritesheet = content.Load<Texture2D>("LOZLinkSpritesheet");
            itemSpritesheet2 = content.Load<Texture2D>("LOZEnemySpritesheet2");
            itemSpritesheet3 = content.Load<Texture2D>("LOZBossSpritesheet");

        }

        public ISprite CreateArrowDownSprite()
        {
            return new ArrowDownSprite(itemSpritesheet);
        }

        public ISprite CreateArrowLeftSprite()
        {
            return new ArrowLeftSprite(itemSpritesheet);
        }

        public ISprite CreateArrowUpSprite()
        {
            return new ArrowUpSprite(itemSpritesheet);
        }

        public ISprite CreateArrowRightSprite()
        {
            return new ArrowRightSprite(itemSpritesheet);
        }
        public ISprite CreateSilverArrowDownSprite()
        {
            return new SilverArrowDownSprite(itemSpritesheet);
        }

        public ISprite CreateSilverArrowLeftSprite()
        {
            return new SilverArrowLeftSprite(itemSpritesheet);
        }

        public ISprite CreateSilverArrowUpSprite()
        {
            return new SilverArrowUpSprite(itemSpritesheet);
        }

        public ISprite CreateSilverArrowRightSprite()
        {
            return new SilverArrowRightSprite(itemSpritesheet);
        }

        public ISprite CreateBombSprite()
        {
            return new BombSprite(itemSpritesheet);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new BoomerangSprite(itemSpritesheet2);
        }
        public ISprite CreateMagicBoomerangSprite()
        {
            return new MagicBoomerangSprite(itemSpritesheet2);
        }

        public ISprite CreateSwordDownSprite()
        {
            return new SwordDownSprite(itemSpritesheet);
        }

        public ISprite CreateSwordLeftSprite()
        {
            return new SwordLeftSprite(itemSpritesheet);
        }

        public ISprite CreateSwordUpSprite()
        {
            return new SwordUpSprite(itemSpritesheet);
        }

        public ISprite CreateSwordRightSprite()
        {
            return new SwordRightSprite(itemSpritesheet);
        }

        public ISprite CreateRodUpSprite()
        {
            return new RodUpSprite(itemSpritesheet);
        }

        public ISprite CreateHeartSprite()
        {
            return new HeartSprite(itemSpritesheet);
        }
        public ISprite CreateHeartContainerSprite()
        {
            return new HeartContainerSprite(itemSpritesheet);
        }

        public ISprite CreateOrangeRupeeSprite()
        {
            return new OrangeRupeeSprite(itemSpritesheet);
        }

        public ISprite CreateBlueRupeeSprite()
        {
            return new BlueRupeeSprite(itemSpritesheet);
        }
        public ISprite CreateCandleSprite()
        {
            return new CandleSprite(itemSpritesheet);
        }

        public ISprite CreateCompassSprite()
        {
            return new CompassSprite(itemSpritesheet);
        }

        public ISprite CreateBowSprite()
        {
            return new BowSprite(itemSpritesheet);
        }

        public ISprite CreateKeySprite()
        {
            return new KeySprite(itemSpritesheet);
        }

        public ISprite CreateClockSprite()
        {
            return new ClockSprite(itemSpritesheet);
        }

        public ISprite CreateMapSprite()
        {
            return new MapSprite(itemSpritesheet);
        }
        public ISprite CreateMagicKeySprite()
        {
            return new MagicKeySprite(itemSpritesheet);
        }
        public ISprite CreateRaftSprite()
        {
            return new RaftSprite(itemSpritesheet);
        }
        public ISprite CreateLadderSprite()
        {
            return new LadderSprite(itemSpritesheet);
        }
        public ISprite CreateTriforcePieceSprite()
        {
            return new TriforcePieceSprite(itemSpritesheet);
        }

        public ISprite CreateStoneBlockSprite()
        {
            return new StoneBlockSprite(itemSpritesheet);
        }

        public ISprite CreateFireballSprite()
        {
            return new FireballSprite(itemSpritesheet3);
        }
		
		public ISprite CreateRockPelletSprite()
        {
            return new RockPelletSprite(itemSpritesheet);
        }

        public ISprite CreateWhiteSwordDownSprite()
        {
            return new WhiteSwordDownSprite(itemSpritesheet);
        }

        public ISprite CreateWhiteSwordLeftSprite()
        {
            return new WhiteSwordLeftSprite(itemSpritesheet);
        }

        public ISprite CreateWhiteSwordUpSprite()
        {
            return new WhiteSwordUpSprite(itemSpritesheet);
        }

        public ISprite CreateWhiteSwordRightSprite()
        {
            return new WhiteSwordRightSprite(itemSpritesheet);
        }
        public ISprite CreateSmokeSprite()
        {
            return new SmokeSprite(itemSpritesheet);
        }

        public ISprite CreateOldManSprite()
        {
            return new OldManSprite(itemSpritesheet);
        }
        public ISprite CreateFireSprite()
        {
            return new FireSprite(itemSpritesheet2);
        }

        public ISprite CreateRodWaveDownSprite()
        {
            return new RodWaveDownSprite(itemSpritesheet);
        }

        public ISprite CreateRodWaveLeftSprite()
        {
            return new RodWaveLeftSprite(itemSpritesheet);
        }

        public ISprite CreateRodWaveUpSprite()
        {
            return new RodWaveUpSprite(itemSpritesheet);
        }

        public ISprite CreateRodWaveRightSprite()
        {
            return new RodWaveRightSprite(itemSpritesheet);
        }
    }
}