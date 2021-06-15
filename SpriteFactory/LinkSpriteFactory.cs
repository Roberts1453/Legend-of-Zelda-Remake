using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class LinkSpriteFactory
    {
        private Texture2D linkSpritesheet;
        private Texture2D naviSpritesheet;

        private Color[] normalData;
        private Color[] hurtData;

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
            linkSpritesheet = content.Load<Texture2D>("LOZLinkSpritesheet");
            naviSpritesheet = content.Load<Texture2D>("NaviSpritesheet");

            Color[] data = new Color[linkSpritesheet.Width * linkSpritesheet.Height];
            normalData = new Color[linkSpritesheet.Width * linkSpritesheet.Height];
            linkSpritesheet.GetData(data);
            linkSpritesheet.GetData(normalData);

            for (int i = 0; i < data.Length; i++)
            {
                normalData[i] = data[i];
                //if(IsSimilar(data[i], Color.Green, 30, 30, 30))
                //if (Math.Abs(data[i].R - Color.LimeGreen.R) < 30 && Math.Abs(data[i].B - Color.LimeGreen.B) < 30 && Math.Abs(data[i].G - Color.LimeGreen.G) < 30)
                if (data[i].G == 248)
                    data[i] = Color.Red;
                if (data[i].R == 228)
                    data[i] = Color.White;
            }
            hurtData = data;
        }
        public void HurtTexture()
        {
            linkSpritesheet.SetData(hurtData);
        }
        public void NormalTexture()
        {
            linkSpritesheet.SetData(normalData);
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

        public ISprite CreateLinkAttackingDownSprite()
        {
            return new LinkAttackingDownSprite(linkSpritesheet);
        }

        public ISprite CreateLinkAttackingLeftSprite()
        {
            return new LinkAttackingLeftSprite(linkSpritesheet);
        }

        public ISprite CreateLinkAttackingUpSprite()
        {
            return new LinkAttackingUpSprite(linkSpritesheet);
        }

        public ISprite CreateLinkAttackingRightSprite()
        {
            return new LinkAttackingRightSprite(linkSpritesheet);
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

        /*Navi Sprites*/
        public ISprite CreateNaviLeftSprite()
        {
            return new NaviLeftSprite(naviSpritesheet);
        }
        public ISprite CreateNaviRightSprite()
        {
            return new NaviRightSprite(naviSpritesheet);
        }
        public ISprite CreateNaviExclamationSprite()
        {
            return new NaviExclamationSprite(naviSpritesheet);
        }

    }
}