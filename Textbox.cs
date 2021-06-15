using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
    public class Textbox
    {
        ISprite textboxSprite;
        ISprite textboxTriangleSprite;
        
        string text;
        string fullText;
        int width = 240;
        int lettersWide = 26;
        int shownLength = 0;
        int characterLimit = 90;

        

        ISprite[] letters = new ISprite[90];

        Vector2 location = new Vector2(25, 197);
        Vector2 screenLocation = new Vector2(25, 197);
        Vector2 boxLocation = new Vector2(0, 176);
        Vector2 boxScreenLocation = new Vector2(0, 176);
        Vector2 triangleLocation;

        Text textObject = new Text("", new Vector2(25, 197));

        public Textbox()
        {
            text = "";
            fullText = "";
            letters[0] = MenuSpriteFactory.Instance.CreateTextSprite('A');
            textboxSprite = MenuSpriteFactory.Instance.CreateTextboxSprite();
            textboxTriangleSprite = MenuSpriteFactory.Instance.CreateTextTriangleSprite();
        }
        public void AddText(string text)
        {
            
            this.fullText = text;
            if (fullText.Length >= characterLimit)
                this.text = fullText.Substring(0, characterLimit);
            else
                this.text = fullText;
            boxLocation = Game1.game.cam.Location + boxScreenLocation;
            location = Game1.game.cam.Location + screenLocation;
            triangleLocation = boxLocation + new Vector2(118, 2);
            shownLength = 0;
            //textObject = new Text(text, location, true);
            if (Game1.game.gameState.Peek().GetType() != typeof(TextboxState))
                Game1.game.gameState.Push(new TextboxState(Game1.game));
        }
        public void NextText()
        {
            shownLength = 0;
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
                if (Game1.game.gameState.Peek().GetType() == typeof(TextboxState))
                    Game1.game.gameState.Pop();
            }

        }
        public void Update(GameTime gameTime)
        {
            textboxTriangleSprite.Update(gameTime);
            if (shownLength < text.Length)
            {
                shownLength++;
                SoundManager.Instance.PlaySoundEffect("Text_Slow");
            }
                
            textObject.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            textboxSprite.Draw(spriteBatch, boxLocation);
            textboxTriangleSprite.Draw(spriteBatch, triangleLocation);

            for (int i = 0; i < shownLength; i++)
            {
                letters[i] = MenuSpriteFactory.Instance.CreateTextSprite(text[i]);
                letters[i].Draw(spriteBatch, location + new Vector2(8 * (i % lettersWide), 8 * (i / lettersWide)));

            }
            //textObject.Draw(spriteBatch);
        }
    }
}
