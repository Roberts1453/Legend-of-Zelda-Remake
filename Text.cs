using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    public class Text
    {
        ISprite[] letters = new ISprite[60];

        public string text;
        string fullText;
        int width = 240;
        int lettersWide = 26;
        int shownLength = 0;
        int characterLimit = 90;

        bool typed = false;

        Vector2 location;

        public Text(string text, Vector2 location)
        {
            this.text = text;
            this.location = location;
            shownLength = text.Length;
        }
        public Text(string text, Vector2 location, bool typed)
        {
            this.text = text;
            this.typed = typed;
            this.location = location;
            if (typed)
                shownLength = 0;
            else
                shownLength = text.Length;
        }
        public void AddText(string text)
        {
            this.fullText = text;
            if (fullText.Length >= characterLimit)
                this.text = fullText.Substring(0, characterLimit);
            else
                this.text = fullText;

            shownLength = 0;
        }
        public void NextText()
        {
            if (fullText.Length > text.Length)
            {
                fullText = fullText.Substring(text.Length, (fullText.Length - text.Length));
                if (fullText.Length > characterLimit)
                    text = fullText.Substring(0, characterLimit);
                else
                    text = fullText;
            }
            else
            {
                fullText = "";
                text = "";
            }
        }
        public void Update(GameTime gameTime)
        {
            if (typed && shownLength < text.Length)
                shownLength++;
            else
                shownLength = text.Length;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < text.Length; i++)
            {
                letters[i] = MenuSpriteFactory.Instance.CreateTextSprite(text[i]);
                letters[i].Draw(spriteBatch, location + new Vector2(8 * (i % lettersWide), 8 * (i / lettersWide)));

            }
        }
    }
}
