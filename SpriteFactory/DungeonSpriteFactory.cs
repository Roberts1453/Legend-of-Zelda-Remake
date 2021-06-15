using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0Project
{
    public class DungeonSpriteFactory
    {
        private Texture2D dungeonSpritesheet;
        private Texture2D dungeonTileset;

        private static DungeonSpriteFactory instance = new DungeonSpriteFactory();

        public static DungeonSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private DungeonSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            dungeonSpritesheet = content.Load<Texture2D>("DungeonSpritesheet");
            dungeonTileset = content.Load<Texture2D>("DungeonTileset");
        }

        public ISprite CreateBackgroundSprite()
        {
            return new BackgroundSprite(dungeonSpritesheet);
        }
        public ISprite CreateBackgroundSecretSprite()
        {
            return new BackgroundSecretSprite(dungeonTileset);
        }
        public ISprite CreateUndergroundSprite()
        {
            return new UndergroundSprite(dungeonTileset);
        }

        public ISprite CreateTopDoorOpenSprite()
        {
            return new TopDoorOpenSprite(dungeonTileset);
        }

        public ISprite CreateTopDoorClosedSprite()
        {
            return new TopDoorClosedSprite(dungeonTileset);
        }

        public ISprite CreateTopDoorLockedSprite()
        {
            return new TopDoorLockedSprite(dungeonTileset);
        }

        public ISprite CreateTopDoorBombedSprite()
        {
            return new TopDoorBombedSprite(dungeonTileset);
        }
        public ISprite CreateTopWallSprite()
        {
            return new TopWallSprite(dungeonTileset);
        }
        public ISprite CreateTopDoorOpenTopSprite()
        {
            return new TopDoorOpenTopSprite(dungeonTileset);
        }
        public ISprite CreateTopDoorBombedTopSprite()
        {
            return new TopDoorBombedTopSprite(dungeonTileset);
        }

        public ISprite CreateBottomDoorOpenSprite()
        {
            return new BottomDoorOpenSprite(dungeonTileset);
        }

        public ISprite CreateBottomDoorClosedSprite()
        {
            return new BottomDoorClosedSprite(dungeonTileset);
        }

        public ISprite CreateBottomDoorLockedSprite()
        {
            return new BottomDoorLockedSprite(dungeonTileset);
        }

        public ISprite CreateBottomDoorBombedSprite()
        {
            return new BottomDoorBombedSprite(dungeonTileset);
        }
        public ISprite CreateBottomWallSprite()
        {
            return new BottomWallSprite(dungeonTileset);
        }
        public ISprite CreateBottomDoorOpenTopSprite()
        {
            return new BottomDoorOpenTopSprite(dungeonTileset);
        }
        public ISprite CreateBottomDoorBombedTopSprite()
        {
            return new BottomDoorBombedTopSprite(dungeonTileset);
        }        

        public ISprite CreateRightDoorOpenSprite()
        {
            return new RightDoorOpenSprite(dungeonTileset);
        }

        public ISprite CreateRightDoorClosedSprite()
        {
            return new RightDoorClosedSprite(dungeonTileset);
        }

        public ISprite CreateRightDoorLockedSprite()
        {
            return new RightDoorLockedSprite(dungeonTileset);
        }

        public ISprite CreateRightDoorBombedSprite()
        {
            return new RightDoorBombedSprite(dungeonTileset);
        }
        public ISprite CreateRightWallSprite()
        {
            return new RightWallSprite(dungeonTileset);
        }
        public ISprite CreateRightDoorBombedTopSprite()
        {
            return new RightDoorBombedTopSprite(dungeonTileset);
        }
        public ISprite CreateRightDoorOpenTopSprite()
        {
            return new RightDoorOpenTopSprite(dungeonTileset);
        }
        public ISprite CreateLeftDoorOpenSprite()
        {
            return new LeftDoorOpenSprite(dungeonTileset);
        }

        public ISprite CreateLeftDoorClosedSprite()
        {
            return new LeftDoorClosedSprite(dungeonTileset);
        }

        public ISprite CreateLeftDoorLockedSprite()
        {
            return new LeftDoorLockedSprite(dungeonTileset);
        }

        public ISprite CreateLeftDoorBombedSprite()
        {
            return new LeftDoorBombedSprite(dungeonTileset);
        }
        public ISprite CreateLeftWallSprite()
        {
            return new LeftWallSprite(dungeonTileset);
        }
        public ISprite CreateLeftDoorBombedTopSprite()
        {
            return new LeftDoorBombedTopSprite(dungeonTileset);
        }
        public ISprite CreateLeftDoorOpenTopSprite()
        {
            return new LeftDoorOpenTopSprite(dungeonTileset);
        }

        public ISprite CreateMoveableBlockSprite()
        {
            return new MoveableBlockSprite(dungeonSpritesheet);
        }

        public ISprite CreateBlockSprite()
        {
            return new StoneBlockSprite(dungeonSpritesheet);
        }

        public ISprite CreateBlueLeftStatueSprite()
        {
            return new BlueLeftStatueSprite(dungeonSpritesheet);
        }

        public ISprite CreateBlueRightStatueSprite()
        {
            return new BlueRightStatueSprite(dungeonSpritesheet);
        }

        public ISprite CreateLeftStatueSprite()
        {
            return new LeftStatueSprite(dungeonSpritesheet);
        }

        public ISprite CreateRightStatueSprite()
        {
            return new RightStatueSprite(dungeonSpritesheet);
        }

        public ISprite CreateWaterSprite()
        {
            return new WaterSprite(dungeonSpritesheet);
        }

        public ISprite CreateGravelSprite()
        {
            return new GravelSprite(dungeonSpritesheet);
        }

        public ISprite CreateStairsSprite()
        {
            return new StairsSprite(dungeonSpritesheet);
        }
        public ISprite CreateBlackBlockSprite()
        {
            return new BlackBlockSprite(dungeonTileset);
        }
        public ISprite CreateBlackSquareBlockSprite()
        {
            return new BlackBlockSprite(dungeonTileset, 2);
        }
        public ISprite CreateLadderSprite()
        {
            return new LadderBlockSprite(dungeonTileset, 2);
        }
        public ISprite CreateHalfLadderSprite()
        {
            return new LadderBlockSprite(dungeonTileset, 1);
        }
        public ISprite CreateBrickSprite()
        {
            return new BrickSprite(dungeonTileset, 1);
        }
        public ISprite CreateDoubleBrickSprite()
        {
            return new BrickSprite(dungeonTileset, 2);
        }
        public ISprite CreateLongBrickSprite()
        {
            return new BrickSprite(dungeonTileset, 3);
        }


    }
}
