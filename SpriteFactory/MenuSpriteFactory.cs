using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class MenuSpriteFactory
    {
        private Texture2D hudSpritesheet;
        private Texture2D textSpritesheet;
        private Texture2D titleSpritesheet;

        private static MenuSpriteFactory instance = new MenuSpriteFactory();

        public static MenuSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private MenuSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            hudSpritesheet = content.Load<Texture2D>("LOZMenuSpritesheet");
            textSpritesheet = content.Load<Texture2D>("LOZTextSpritesheet");
            titleSpritesheet = content.Load<Texture2D>("titleSpritesheet");
        }

        public ISprite CreateNumberSprite(int number)
        {
            return new NumbersSprite(hudSpritesheet, number);
        }

        public ISprite CreateTextSprite(char letter)
        {
            return new TextSprite(textSpritesheet, letter);
        }
        public ISprite CreateTextboxSprite()
        {
            return new TextboxSprite(textSpritesheet);
        }
        public ISprite CreateTextTriangleSprite()
        {
            return new TextTriangleSprite(textSpritesheet);
        }
        public ISprite CreateTitleSprite()
        {
            return new TitleSprite(titleSpritesheet);
        }
        public ISprite CreateFileScreenSprite()
        {
            return new FileScreenSprite(titleSpritesheet);
        }
        public ISprite CreateNameSelectSprite()
        {
            return new NameSelectSprite(titleSpritesheet);
        }
    }
}