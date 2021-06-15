using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class OverworldSpriteFactory
    {
        private Texture2D dungeonSpritesheet;
        private Texture2D overworldTileset;

        private static OverworldSpriteFactory instance = new OverworldSpriteFactory();

        public static OverworldSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private OverworldSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            dungeonSpritesheet = content.Load<Texture2D>("DungeonSpritesheet");
            overworldTileset = content.Load<Texture2D>("tiles-overworld");
        }

        public ISprite CreateLandBackgroundSprite()
        {
            return new LandBackgroundSprite(overworldTileset);
        }
        public ISprite CreateLandSprite()
        {
            return new LandBlockSprite(overworldTileset);
        }


        public ISprite CreateWaterTopLeftSprite()
        {
            return new WaterTopLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterTopSprite()
        {
            return new WaterTopBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterTopRightSprite()
        {
            return new WaterTopRightBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterLeftSprite()
        {
            return new WaterLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterMiddleSprite()
        {
            return new WaterMiddleBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterRightSprite()
        {
            return new WaterRightBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterBottomLeftSprite()
        {
            return new WaterBottomLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterBottomSprite()
        {
            return new WaterBottomBlockSprite(overworldTileset);
        }
        public ISprite CreateWaterBottomRightSprite()
        {
            return new WaterBottomRightBlockSprite(overworldTileset);
        }

        public ISprite CreateStairsSprite()
        {
            return new StairsSprite(dungeonSpritesheet);
        }
        public ISprite CreateRockTopLeftSprite()
        {
            return new RockTopLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateRockTopSprite()
        {
            return new RockTopBlockSprite(overworldTileset);
        }
        public ISprite CreateRockTopRightSprite()
        {
            return new RockTopRightBlockSprite(overworldTileset);
        }
        public ISprite CreateSingleRockSprite()
        {
            return new SingleRockBlockSprite(overworldTileset);
        }
        public ISprite CreateRockBottomLeftSprite()
        {
            return new RockBottomLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateRockBottomSprite()
        {
            return new RockBottomBlockSprite(overworldTileset);
        }
        public ISprite CreateRockBottomRightSprite()
        {
            return new RockBottomRightBlockSprite(overworldTileset);
        }
        public ISprite CreateSingleTreeBrownSprite()
        {
            return new SingleTreeBrownBlockSprite(overworldTileset);
        }
		public ISprite CreateTreeTopLeftSprite()
        {
            return new TreeTopLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeTopSprite()
        {
            return new TreeTopBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeTopRightSprite()
        {
            return new TreeTopRightBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeBlockSprite()
        {
            return new TreeBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeBottomLeftSprite()
        {
            return new TreeBottomLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeBottomSprite()
        {
            return new TreeBottomBlockSprite(overworldTileset);
        }
        public ISprite CreateTreeBottomRightSprite()
        {
            return new TreeBottomRightBlockSprite(overworldTileset);
        }
		public ISprite CreateSingleTreeGreenSprite()
        {
            return new SingleTreeBlockSprite(overworldTileset);
        }
		
		/*Dungeon Entrances*/
		public ISprite CreateDungeonTreeBrownTopLeftSprite()
        {
            return new DungeonTreeBrownTopLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeBrownTopSprite()
        {
            return new DungeonTreeBrownTopBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeBrownTopRightSprite()
        {
            return new DungeonTreeBrownTopRightBlockSprite(overworldTileset);
        }
		public ISprite CreateDungeonTreeBrownBottomLeftSprite()
        {
            return new DungeonTreeBrownBottomLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeBrownBottomRightSprite()
        {
            return new DungeonTreeBrownBottomRightBlockSprite(overworldTileset);
        }
		
		public ISprite CreateDungeonTreeGreenTopLeftSprite()
        {
            return new DungeonTreeGreenTopLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeGreenTopSprite()
        {
            return new DungeonTreeGreenTopBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeGreenTopRightSprite()
        {
            return new DungeonTreeGreenTopRightBlockSprite(overworldTileset);
        }
		public ISprite CreateDungeonTreeGreenBottomLeftSprite()
        {
            return new DungeonTreeGreenBottomLeftBlockSprite(overworldTileset);
        }
        public ISprite CreateDungeonTreeGreenBottomRightSprite()
        {
            return new DungeonTreeGreenBottomRightBlockSprite(overworldTileset);
        }

    }
}
