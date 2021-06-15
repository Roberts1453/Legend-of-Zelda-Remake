using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0Project
{
    public class Door : IStaticBlock

    {
        public Vector2 Location;
        ISprite doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorClosedSprite();
        Rectangle edges;        

        public Directions direction;
        public enum DoorState { Open, Closed, Locked, Bombable, Bombed, Wall };
        public DoorState doorState = DoorState.Wall;
        public Door pairedDoor;
        public int nextRoom;
        bool collide = true;
        bool loaded = false;

        public Door(Vector2 location, Directions direction)
        {
            Location = location;
            this.direction = direction;
            nextRoom = -1;
            pairedDoor = this;
            ChangeDoor(doorState);
        }
        public Door(Vector2 location, Directions direction, int nextRoom)
        {
            Location = location;
            this.direction = direction;
            this.nextRoom = nextRoom;
            pairedDoor = this;
            ChangeDoor(doorState);
        }
        public void SetPairedDoor(Door paired)
        {
            pairedDoor = paired;
        }
        public bool GetCollide()
        {
            return collide;
        }
        public void CollideAction(Link link)
        {
            if (link.keys > 0 && doorState == DoorState.Locked)
            {
                SoundManager.Instance.PlaySoundEffect("Door_Unlock");
                ChangeDoor(DoorState.Open);
                if (!link.obtainedItems.Contains(Items.MagicKey))
                    link.keys--;
            }
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
        public void BombDoor()
        {
            if (doorState == DoorState.Bombable)
                ChangeDoor(DoorState.Bombed);
        }
        public void ChangeDoor(DoorState door)
        {
            doorState = door;
            switch (door)
            {
                case DoorState.Open:
                    collide = false;
                    break;
                case DoorState.Closed:
                    collide = true;
                    break;
                case DoorState.Locked:
                    collide = true;
                    break;
                case DoorState.Bombable:
                    collide = true;
                    break;
                case DoorState.Bombed:
                    collide = false;
                    break;
                case DoorState.Wall:
                    collide = true;
                    break;
            }
            SetSprite();
        }

        public void SetSprite()
        {
            switch (direction)
            {
                case Directions.Down:
                    switch (doorState)
                    {
                        case DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorOpenSprite();
                            break;
                        case DoorState.Closed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorClosedSprite();
                            break;
                        case DoorState.Locked:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorLockedSprite();
                            break;
                        case DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomDoorBombedSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateBottomWallSprite();
                            break;
                    }
                    break;
                case Directions.Left:
                    switch (doorState)
                    {
                        case DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorOpenSprite();
                            break;
                        case DoorState.Closed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorClosedSprite();
                            break;
                        case DoorState.Locked:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorLockedSprite();
                            break;
                        case DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftDoorBombedSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateLeftWallSprite();
                            break;
                    }
                    break;
                case Directions.Up:
                    switch (doorState)
                    {
                        case DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorOpenSprite();
                            break;
                        case DoorState.Closed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorClosedSprite();
                            break;
                        case DoorState.Locked:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorLockedSprite();
                            break;
                        case DoorState.Bombable:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopWallSprite();
                            break;
                        case DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopDoorBombedSprite();
                            break;
                        default:
                            doorSprite = DungeonSpriteFactory.Instance.CreateTopWallSprite();
                            break;
                    }
                    break;
                case Directions.Right:
                    switch (doorState)
                    {
                        case DoorState.Open:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorOpenSprite();
                            break;
                        case DoorState.Closed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorClosedSprite();
                            break;
                        case DoorState.Locked:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorLockedSprite();
                            break;
                        case DoorState.Bombed:
                            doorSprite = DungeonSpriteFactory.Instance.CreateRightDoorBombedSprite();
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
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            doorSprite.Draw(spriteBatch, Location);
            
        }
    }
}
