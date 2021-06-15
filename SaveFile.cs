using System;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.IO;

namespace Sprint0Project
{
    public class SaveFile
    {

            public bool empty = true;
            public string fileName;
            public string playerName = "  ---";
            public int maxHealth = 6;

        public SaveFile()
        {

        }
        public void LoadPreview()
        {
                SaveManager.Instance.LoadPreviewData(this);
        }
    }
}
