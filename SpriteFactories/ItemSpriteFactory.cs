using System;

namespace Sprint0Project
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
        private Texture2D itemSpriteSheet2;

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
            itemSpriteSheet = content.Load<Texture2D>("LOZLinkSpritesheet");
            itemSpriteSheet2 = content.Load<Texture2D>("LOZLinkSpritesheet2");

        }

        public ISprite CreateArrowDownSprite()
        {
            return new ArrowDownSprite(itemSpritesheet);
        }

        public ISprite CreateArrowLeftSprite()
        {
            return new ArrowLeftSprite(itemSpritesheet);
        }

        public ISprite CreateArrowDownSprite()
        {
            return new ArrowUpSprite(itemSpritesheet);
        }

        public ISprite CreateArrowDownSprite()
        {
            return new ArrowRightSprite(itemSpritesheet);
        }

        public ISprite CreateBombSprite()
        {
            return new BombSprite(itemSpritesheet);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new BoomerangSprite(itemSpritesheet2);
        }

    }
}