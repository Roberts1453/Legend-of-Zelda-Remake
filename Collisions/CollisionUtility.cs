
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0Project
{

    public class CollisionUtility
    {
        Game1 game;
        Rectangle intersect;

        public CollisionUtility(Game1 gameState)
        {
            game = gameState;
        }

        static public bool DoesCollide(Rectangle instigator, Rectangle receiver)
        {
            bool doesCollide = false;
            if (instigator.Intersects(receiver))
                doesCollide = true;
            return doesCollide;
        }
        static public Directions FindDirection(Rectangle instigator, Rectangle receiver)
        {
            Directions direction = Directions.Down;
            Rectangle intersect = Rectangle.Intersect(instigator, receiver);
            if (intersect.Height < intersect.Width)
            {
                if (receiver.Y < intersect.Y)
                {
                    direction = Directions.Up;
                }

                if (receiver.Y >= intersect.Y)
                {
                    direction = Directions.Down;
                }
            }
            else
            {
                if (receiver.X < intersect.X)
                {
                    direction = Directions.Left;
                }
                if (receiver.X >= intersect.X)
                {
                    direction = Directions.Right;
                }
            }
            return direction;
        }

        static public void BombDoor(Door door)
        {
            door.ChangeDoor(Door.DoorState.Bombed);
        }

        static public void PushToward(ICharacter character, Rectangle intersect)
        {
            if (intersect.Width > intersect.Height)
            {
                if (intersect.Y > character.GetLocation().Y)
                    character.Push(Directions.Down);
                else
                    character.Push(Directions.Up);
            }
            else
            {
                if (intersect.X > character.GetLocation().X)
                    character.Push(Directions.Right);
                else
                    character.Push(Directions.Left);
            }
        }
        static public void PushTowardCenter(ICharacter character, Rectangle edges)
        {
            Rectangle intersect = Rectangle.Intersect(character.GetEdges(), edges);
            if (intersect.Width > intersect.Height)
            {
                if (edges.Location.Y + edges.Height/2 > character.GetLocation().Y)
                    character.Push(Directions.Down);
                else
                    character.Push(Directions.Up);
            }
            else
            {
                if (edges.Location.X + edges.Width / 2 > character.GetLocation().X)
                    character.Push(Directions.Right);
                else
                    character.Push(Directions.Left);
            }
        }
        static public void PushAway(ICharacter character, Rectangle intersect)
        {
            if (intersect.Width > intersect.Height)
            {
                if (intersect.Y < character.GetLocation().Y)
                    character.Push(Directions.Down);
                else
                    character.Push(Directions.Up);
            }
            else
            {
                if (intersect.X < character.GetLocation().X)
                    character.Push(Directions.Right);
                else
                    character.Push(Directions.Left);
            }
        }
        static public void PushAwayCenter(ICharacter character, Rectangle edges)
        {
            Rectangle intersect = Rectangle.Intersect(character.GetEdges(), edges);
            if (intersect.Width > intersect.Height)
            {
                if (edges.Location.Y + edges.Height / 2 < character.GetLocation().Y)
                    character.Push(Directions.Down);
                else
                    character.Push(Directions.Up);
            }
            else
            {
                if (edges.Location.X + edges.Width / 2 < character.GetLocation().X)
                    character.Push(Directions.Right);
                else
                    character.Push(Directions.Left);
            }
        }
        public void Update()
        {
            
        }
    }
}
