using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace XMLData
{
    public class Dungeon
    {
        public int roomNumber;
        public string topDoor;
        public string bottomDoor;
        public string leftDoor;
        public string rightDoor;
        public Object[] objects;
    }
    
    public class Object
    {
        public string objectType;
        public string objectName;
        public Vector2 location;

    }

}
