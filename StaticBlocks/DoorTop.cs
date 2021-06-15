using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class DoorTop : IStaticBlock

    {
        public Vector2 Location;
        ISprite doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorOpenTopSprite();
        Rectangle edges;        

        Directions direction;
        
        Door.DoorState doorState = Door.DoorState.Closed;
        Door door;

        bool collide = false;
        bool loaded = false;

        public DoorTop(Door door)
        {
            this.door = door;
            Location = door.Location;
            this.direction = door.direction;
            ChangeDoor(door.doorState);
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {

        }
        public Rectangle Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = doorSprite.Edges();
            }
        }

        public Rectangle GetEdges()
        {
            return edges;

        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public void ChangeDoor(Door.DoorState door)
        {
            doorState = door;
            SetSprite();
        }
        public void SetSprite()
        {
            switch (direction)
            {
                case Directions.Down:
                    switch (doorState)
                    {
                        case Door.DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorOpenTopSprite();
                            break;
                        case Door.DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorBombedTopSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomWallSprite();
                            break;
                    }
                    break;
                case Directions.Left:
                    switch (doorState)
                    {
                        case Door.DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorOpenTopSprite();
                            break;
                        case Door.DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorBombedTopSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftWallSprite();
                            break;
                    }
                    break;
                case Directions.Up:
                    switch (doorState)
                    {
                        case Door.DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorOpenTopSprite();
                            break;
                        case Door.DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorBombedTopSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopWallSprite();
                            break;
                    }
                    break;
                case Directions.Right:
                    switch (doorState)
                    {
                        case Door.DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorOpenTopSprite();
                            break;
                        case Door.DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorBombedTopSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightWallSprite();
                            break;
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            edges = doorSprite.Edges();
            edges.X = (int)Location.X;
            edges.Y = (int)Location.Y;
            if (door.doorState != doorState)
            {
                ChangeDoor(door.doorState);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (doorState == Door.DoorState.Open || doorState == Door.DoorState.Bombed)
                doorSprite.Draw(spriteBatch, Location);
            
        }
    }
}
