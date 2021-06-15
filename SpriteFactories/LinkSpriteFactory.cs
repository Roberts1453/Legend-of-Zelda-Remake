using System;

namespace Sprint0Project
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpriteSheet;

        private static LinkSpriteFactory instance = new LinkSpriteFactory();

        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private LinkSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            linkSpriteSheet = content.Load<Texture2D>("LOZLinkSpritesheet");
        }

        public ISprite CreateLinkIdleDownSprite()
        {
            return new LinkIdleDownSprite(linkSpritesheet);
        }

        public ISprite CreateLinkIdleLeftSprite()
        {
            return new LinkIdleLeftSprite(linkSpritesheet);
        }

        public ISprite CreateLinkIdleUpSprite()
        {
            return new LinkIdleUpSprite(linkSpritesheet);
        }

        public ISprite CreateLinkIdleRightSprite()
        {
            return new LinkIdleRightSprite(linkSpritesheet);
        }

        public ISprite CreateLinkMovingDownSprite()
        {
            return new LinkMovingDownSprite(linkSpritesheet);
        }

        public ISprite CreateLinkMovingLeftSprite()
        {
            return new LinkMovingLeftSprite(linkSpritesheet);
        }

        public ISprite CreateLinkMovingUpSprite()
        {
            return new LinkMovingUpSprite(linkSpritesheet);
        }

        public ISprite CreateLinkMovingRightSprite()
        {
            return new LinkMovingRightSprite(linkSpritesheet);
        }

        public ISprite CreateLinkItemDownSprite()
        {
            return new LinkItemDownSprite(linkSpritesheet);
        }

        public ISprite CreateLinkItemLeftSprite()
        {
            return new LinkItemLeftSprite(linkSpritesheet);
        }

        public ISprite CreateLinkItemUpSprite()
        {
            return new LinkItemUpSprite(linkSpritesheet);
        }

        public ISprite CreateLinkItemRightSprite()
        {
            return new LinkItemRightSprite(linkSpritesheet);
        }

    }
}