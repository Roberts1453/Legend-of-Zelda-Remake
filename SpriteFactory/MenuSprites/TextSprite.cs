using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0Project
{
    public class TextSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames = 10;
        private Texture2D _spriteSheet;
        Rectangle sourceRectangle;
        Dictionary<char, Tuple<int, int>> textCoords = new Dictionary<char, Tuple<int, int>>()
        {
            {'0', new Tuple<int, int>(336,24) },
            {'1', new Tuple<int, int>(352,24) },
            {'2', new Tuple<int, int>(368,24) },
            {'3', new Tuple<int, int>(384,24) },
            {'4', new Tuple<int, int>(400,24) },
            {'5', new Tuple<int, int>(416,24) },
            {'6', new Tuple<int, int>(432,24) },
            {'7', new Tuple<int, int>(448,24) },
            {'8', new Tuple<int, int>(464,24) },
            {'9', new Tuple<int, int>(480,24) },
            {'A', new Tuple<int, int>(496,24) },
            {'B', new Tuple<int, int>(512,24) },
            {'C', new Tuple<int, int>(528,24) },
            {'D', new Tuple<int, int>(544,24) },
            {'E', new Tuple<int, int>(560,24) },
            {'F', new Tuple<int, int>(576,24) },
            {'G', new Tuple<int, int>(336,40) },
            {'H', new Tuple<int, int>(352,40) },
            {'I', new Tuple<int, int>(368,40) },
            {'J', new Tuple<int, int>(384,40) },
            {'K', new Tuple<int, int>(400,40) },
            {'L', new Tuple<int, int>(416,40) },
            {'M', new Tuple<int, int>(432,40) },
            {'N', new Tuple<int, int>(448,40) },
            {'O', new Tuple<int, int>(464,40) },
            {'P', new Tuple<int, int>(480,40) },
            {'Q', new Tuple<int, int>(496,40) },
            {'R', new Tuple<int, int>(512,40) },
            {'S', new Tuple<int, int>(528,40) },
            {'T', new Tuple<int, int>(544,40) },
            {'U', new Tuple<int, int>(560,40) },
            {'V', new Tuple<int, int>(576,40) },
            {'W', new Tuple<int, int>(336,56) },
            {'X', new Tuple<int, int>(352,56) },
            {'Y', new Tuple<int, int>(368,56) },
            {'Z', new Tuple<int, int>(384,56) },
            {',', new Tuple<int, int>(400,56) },
            {'!', new Tuple<int, int>(416,56) },
            {Char.Parse("'"), new Tuple<int, int>(432,56) },
            {'&', new Tuple<int, int>(448,56) },
            {'.', new Tuple<int, int>(464,56) },
            {'"', new Tuple<int, int>(480,56) },
            {'?', new Tuple<int, int>(496,56) },
            {'-', new Tuple<int, int>(512,56) },
            {' ', new Tuple<int, int>(528,56) }

        };

        public TextSprite(Texture2D spriteSheet)
        {
            _spriteSheet = spriteSheet;
            currentFrame = 0;
            sourceRectangle = new Rectangle(textCoords['A'].Item1, textCoords['A'].Item2, 8, 8);
        }
        public TextSprite(Texture2D spriteSheet, char letter)
        {
            _spriteSheet = spriteSheet;;
            sourceRectangle = new Rectangle(textCoords[letter].Item1, textCoords[letter].Item2, 8, 8);
        }
        public Rectangle Edges()
        {
            return sourceRectangle;
        }

        public void ChangeNumber(int number)
        {
            currentFrame = number;
        }

        public void Update(GameTime gameTime)
        {            
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            //sourceRectangle = new Rectangle(528 + (currentFrame*9), 117, 8, 8);

            //spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet, location, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}