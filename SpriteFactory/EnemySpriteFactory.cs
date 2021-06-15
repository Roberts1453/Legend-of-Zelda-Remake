using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpritesheet;
		private Texture2D enemySpritesheet2;
        private Texture2D enemyHurtSpritesheet;
        private Texture2D bossSpritesheet;

        private Color[] normalData;
        private Color[] hurtData;
		
		private Color[] normalBossData;
        private Color[] hurtBossData;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpritesheet = content.Load<Texture2D>("LOZEnemySpritesheet");
			enemySpritesheet2 = content.Load<Texture2D>("LOZEnemySpritesheet2");
            bossSpritesheet = content.Load<Texture2D>("LOZBossSpritesheet");
			LoadHurtData();
			LoadHurtBossData();
            
            enemyHurtSpritesheet = new Texture2D(enemySpritesheet.GraphicsDevice, enemySpritesheet.Width, enemySpritesheet.Height);
            //enemyHurtSpritesheet = enemySpritesheet;
            //enemySpritesheet.SetData(normalData);
            enemyHurtSpritesheet.SetData(hurtData);


        }
		
		public void LoadHurtData(){
			Color[] data = new Color[enemySpritesheet.Width * enemySpritesheet.Height];
            normalData = new Color[enemySpritesheet.Width * enemySpritesheet.Height];
            enemySpritesheet.GetData(data);
            enemySpritesheet.GetData(normalData);

            for (int i = 0; i < data.Length; i++)
            {
                normalData[i] = data[i];
                //if(IsSimilar(data[i], Color.Green, 30, 30, 30))
                //if (Math.Abs(data[i].R - Color.LimeGreen.R) < 30 && Math.Abs(data[i].B - Color.LimeGreen.B) < 30 && Math.Abs(data[i].G - Color.LimeGreen.G) < 30)
                if (data[i].R == 224 && data[i].G == 80)
                    data[i] = Color.DarkBlue;
                if (data[i].R == 255)
                    data[i] = Color.MediumSlateBlue;
            }
            hurtData = data;
		}
		public void LoadHurtBossData(){
			Color[] data = new Color[bossSpritesheet.Width * bossSpritesheet.Height];
            normalBossData = new Color[bossSpritesheet.Width * bossSpritesheet.Height];
            bossSpritesheet.GetData(data);
            bossSpritesheet.GetData(normalBossData);

            for (int i = 0; i < data.Length; i++)
            {
                normalBossData[i] = data[i];
                //if(IsSimilar(data[i], Color.Green, 30, 30, 30))
                //if (Math.Abs(data[i].R - Color.LimeGreen.R) < 30 && Math.Abs(data[i].B - Color.LimeGreen.B) < 30 && Math.Abs(data[i].G - Color.LimeGreen.G) < 30)
                if (data[i].G == 212)
                    data[i] = Color.Red;
                if (data[i].R == 228)
                    data[i] = Color.White;
            }
            hurtBossData = data;
		}
		public void HurtTexture()
        {
            enemySpritesheet.SetData(hurtData);
        }
        public void NormalTexture()
        {
            enemySpritesheet.SetData(normalData);
        }
		public void HurtBossTexture()
        {
            bossSpritesheet.SetData(hurtBossData);
        }
        public void NormalBossTexture()
        {
            bossSpritesheet.SetData(normalBossData);
        }
        /* Sprites not added yet
        public ISprite CreateGoriyaIdleDownSprite()
        {
            return new GoriyaIdleDownSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaIdleLeftSprite()
        {
            return new GoriyaIdleLeftSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaIdleUpSprite()
        {
            return new GoriyaIdleUpSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaIdleRightSprite()
        {
            return new GoriyaIdleRightSprite(enemySpritesheet);
        }
        */
        public ISprite CreateGoriyaMovingDownSprite()
        {
            return new GoriyaMovingDownSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaMovingLeftSprite()
        {
            return new GoriyaMovingLeftSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaMovingUpSprite()
        {
            return new GoriyaMovingUpSprite(enemySpritesheet);
        }

        public ISprite CreateGoriyaMovingRightSprite()
        {
            return new GoriyaMovingRightSprite(enemySpritesheet);
        }
        public ISprite CreateKeeseSprite()
        {
            return new KeeseSprite(enemySpritesheet);
        }

        public ISprite CreateStalfosSprite()
        {
            return new StalfosSprite(enemySpritesheet, normalData, hurtData);
        }
        public ISprite CreateHurtStalfosSprite()
        {
            return new StalfosSprite(enemyHurtSpritesheet, normalData, hurtData);
        }

        public ISprite CreateGelSprite()
        {
            return new GelSprite(enemySpritesheet);
        }

        public ISprite CreateAquamentusSprite() 
        {
            return new AquamentusSprite(bossSpritesheet);
        }

        public ISprite CreateBladeTrapSprite() 
        {
            return new BladeTrapSprite(enemySpritesheet);
        }

        public ISprite CreateWallMasterSprite()
        {
            return new WallMasterSprite(enemySpritesheet);
        }

        public ISprite CreateRopeLeftSprite()
        {
            return new RopeLeftSprite(enemySpritesheet);
        }

        public ISprite CreateRopeRightSprite()
        {
            return new RopeRightSprite(enemySpritesheet);
        }

        public ISprite CreateBlueGoriyaDownSprite()
        {
            return new BlueGoriyaDownSprite(enemySpritesheet);
        }
        public ISprite CreateBlueGoriyaLeftSprite()
        {
            return new BlueGoriyaLeftSprite(enemySpritesheet);
        }

        public ISprite CreateBlueGoriyaUpSprite()
        {
            return new BlueGoriyaUpSprite(enemySpritesheet);
        }

        public ISprite CreateBlueGoriyaRightSprite()
        {
            return new BlueGoriyaRightSprite(enemySpritesheet);
        }

        public ISprite CreateBubbleSprite()
        {
            return new BubbleSprite(enemySpritesheet);
        }

        public ISprite CreateZolSprite()
        {
            return new ZolSprite(enemySpritesheet);
        }

        public ISprite CreateRedKeeseSprite()
        {
            return new RedKeeseSprite(enemySpritesheet);
        }

        public ISprite CreateGibdoSprite()
        {
            return new GibdoSprite(enemySpritesheet);
        }
		public ISprite CreateMoldormSprite()
        {
            return new MoldormSprite(enemySpritesheet);
        }
		public ISprite CreateOctorokDownSprite()
        {
            return new OctorokDownSprite(enemySpritesheet2);
        }
        public ISprite CreateOctorokLeftSprite()
        {
            return new OctorokLeftSprite(enemySpritesheet2);
        }

        public ISprite CreateOctorokUpSprite()
        {
            return new OctorokUpSprite(enemySpritesheet2);
        }

        public ISprite CreateOctorokRightSprite()
        {
            return new OctorokRightSprite(enemySpritesheet2);
        }
        public ISprite CreateLikeLikeSprite()
        {
            return new LikeLikeSprite(enemySpritesheet);
        }
        public ISprite CreateVireDownSprite()
        {
            return new VireDownSprite(enemySpritesheet);
        }

        public ISprite CreateVireUpSprite()
        {
            return new VireUpSprite(enemySpritesheet);
        }

        public ISprite CreatePolsVoiceSprite()
        {
            return new PolsVoiceSprite(enemySpritesheet);
        }

        public ISprite CreateWizrobeLeftSprite()
        {
            return new WizrobeLeftSprite(enemySpritesheet);
        }

        public ISprite CreateWizrobeRightSprite()
        {
            return new WizrobeRightSprite(enemySpritesheet);
        }

        public ISprite CreateWizrobeUpSprite()
        {
            return new WizrobeUpSprite(enemySpritesheet);
        }

        public ISprite CreateBlueWizrobeLeftSprite()
        {
            return new BlueWizrobeLeftSprite(enemySpritesheet);
        }

        public ISprite CreateBlueWizrobeRightSprite()
        {
            return new BlueWizrobeRightSprite(enemySpritesheet);
        }

        public ISprite CreateBlueWizrobeUpSprite()
        {
            return new BlueWizrobeUpSprite(enemySpritesheet);
        }

        public ISprite DarknutLeftSprite()
        {
            return new DarknutLeftSprite(enemySpritesheet);
        }

        public ISprite DarknutDownSprite()
        {
            return new DarknutDownSprite(enemySpritesheet);
        }

        public ISprite DarknutRightSprite()
        {
            return new DarknutRightSprite(enemySpritesheet);
        }
        public ISprite DarknutUpSprite()
        {
            return new DarknutUpSprite(enemySpritesheet);
        }

        public ISprite LanmolaHeadSprite()
        {
            return new LanmolaHeadSprite(enemySpritesheet);
        }

        public ISprite LanmolaBodySprite()
        {
            return new LanmolaBodySprite(enemySpritesheet);
        }

    }
}