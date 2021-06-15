using System;

namespace Sprint0Project
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpriteSheet;

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
            enemySpriteSheet = content.Load<Texture2D>("LOZEnemySpritesheet");
        }

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

    }
}