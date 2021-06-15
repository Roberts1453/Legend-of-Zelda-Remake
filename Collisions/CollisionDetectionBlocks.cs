
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0Project
{

    public class CollisionDetectionBlocks
    {
        public Vector2 Location;
        Game1 game;
        Link link;
        Rectangle intersect;

        public CollisionDetectionBlocks(Game1 gameState)
        {
            game = gameState;
        }

        public void MovementCollide(ICharacter character, Rectangle intersect)
        {
            if (intersect.Height < intersect.Width)
            {
                if (character.Edges.Y < intersect.Y)
                {
                    character.MoveUp();
                    character.Stop();
                }

                if (character.Edges.Y >= intersect.Y)
                {
                    character.MoveDown();
                    character.Stop();
                }
            }
            else
            {
                if (character.Edges.X < intersect.X)
                {
                    character.MoveLeft();
                    character.Stop();
                }
                if (character.Edges.X >= intersect.X)
                {
                    character.MoveRight();
                    character.Stop();
                }
            }
        }

        public void RoomCollision(List<Room> roomList, List<ICharacter> Enemies, Link link, Camera cam)
        {            

            foreach (Rectangle wall in roomList[cam.roomNumber].GetWalls())
            {
                intersect = Rectangle.Intersect(link.edges, wall);
                if (link.edges.Intersects(wall))
                {
                    MovementCollide(link, intersect);
                }
                if (link.usedProjectileSword != null)
                {
                    //if(link.usedProjectileSword.Edges.Intersects(wall))
                    intersect = Rectangle.Intersect(link.usedProjectileSword.Edges, wall);
                    if (intersect.Width > 0 || intersect.Height > 0)
                    {
                        link.usedProjectileSword.Collide();
                    }
                }
                if (link.usedItem != null)
                {
                    if (link.usedItem.Edges.Intersects(wall))
                    {
                        link.usedItem.Collide();
                    }
                }
                foreach (ICharacter enemy in Enemies)
                {
                    intersect = Rectangle.Intersect(enemy.GetEdges(), wall);
                    if (intersect.Width > 0 || intersect.Height > 0)
                    {
                        MovementCollide(enemy, intersect);
                        enemy.CollideAction();
                    }
                }

            }
            foreach (Room room in roomList)
            {
                if (room != roomList[cam.roomNumber])
                {
                    foreach (Rectangle door in room.GetDoors())
                    {
                        intersect = Rectangle.Intersect(link.edges, door);
                        if (door.Intersects(link.edges) && !cam.roomBounds.Contains(link.location))
                        {
                            cam.RoomTransition(roomList.IndexOf(room), link);
                            if (link.direction == Directions.Up)
                                link.MoveUp();
                            else if (link.direction == Directions.Down)
                                link.MoveDown();
                            else if (link.direction == Directions.Left)
                                link.MoveLeft();
                            else if (link.direction == Directions.Right)
                                link.MoveRight();
                        }
                    }     

                }            
            }
        }


        public void Update(List<IStaticBlock> blocks, List<ICharacter> Enemies)
        {
            link = game.link;
            foreach (IStaticBlock block in blocks)
            {
                if (block.GetEdges().Intersects(game.navi.GetHorizRange()))
                    game.navi.ContactObject(block);
                if (block.GetCollide())
                {
                    intersect = Rectangle.Intersect(link.edges, block.GetEdges());
                    if (intersect.Width > 0 || intersect.Height > 0)
                    {
                        MovementCollide(link, intersect);
                        block.CollideAction(link);                        
                    }                    
                    if (link.usedProjectileSword != null)
                    {
                        intersect = Rectangle.Intersect(link.edges, block.GetEdges());
                        if (intersect.Width > 0 || intersect.Height > 0)
                        {
                            link.usedProjectileSword.Collide();
                        }                        
                    }
                    if (link.usedItem != null)
                    {
                        if (link.usedItem.Edges.Intersects(block.GetEdges()))
                        {
                            if (block.GetType() == typeof(Door))
                            {
                                link.usedItem.Collide();
                                if (link.usedItem.GetType() == typeof(Bomb)) {
                                    Bomb bomb = (Bomb)link.usedItem;
                                    bomb.Collide((Door)block);
                                }

                            }
                        }
                    }

                    foreach (ICharacter enemy in Enemies)
                    {
                        if (enemy.GetType() != typeof(Keese))
                        {
                            intersect = Rectangle.Intersect(enemy.GetEdges(), block.GetEdges());
                            if (intersect.Width > 0 || intersect.Height > 0)
                            {
                                MovementCollide(enemy, intersect);
                                enemy.CollideAction();
                            }
                        }
                    }

                }
                else
                {
					if (block.GetType() == typeof(Door)) {
						foreach (ICharacter enemy in Enemies)
                    	{
                            intersect = Rectangle.Intersect(enemy.GetEdges(), block.GetEdges());
                            if (intersect.Width > 0 || intersect.Height > 0)
                            {
                                MovementCollide(enemy, intersect);
                                enemy.CollideAction();
                            }
                        }
					}
                    intersect = Rectangle.Intersect(link.edges, block.GetEdges());
                    if (intersect.Width > 0 || intersect.Height > 0)
                    {
                        block.CollideAction(link);
                        if (block.GetType() == typeof(StairsBlock))
                        {
                            StairsBlock stairs = (StairsBlock)block;
                            stairs.CollideAction(link, game.cam);
                        }
                        if (block.GetType() == typeof(DungeonDoor))
                        {
                            DungeonDoor dungeonDoor = (DungeonDoor)block;
                            dungeonDoor.CollideAction(link, game);
                        }
                    }
                }

            }
            /*foreach (Room room in blocks)
            {
                foreach (Rectangle wall in room.GetWalls())
                {
                    intersect = Rectangle.Intersect(link.edges, wall);

                }
            }*/
        }
    }
}
