
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{

    public class Randomizer : Random
    {
        static Randomizer _Instance;
        public static Randomizer Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Randomizer();
                return _Instance;
            }
        }
        public Randomizer()
        {

        }
    }
}
