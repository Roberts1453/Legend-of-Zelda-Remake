
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0Project
{

    public class CollisionDetectionEnemys
    {
        public Vector2 Location;
        Game1 game;
        Link link;

        public CollisionDetectionEnemys(Game1 gameState)
        {
            game = gameState;
        }

        
        public void Update(List<ICharacter> Enemies)
        {
            link = game.link;
            foreach (ICharacter enemy in Enemies) {

                //link enemy collisions
                if (link.edges.Intersects(enemy.GetEdges()))
                {
                    if (!link.IsDamaged())
                    {
                        link.TakeDamage(enemy.GetDamage());
                        CollisionUtility.PushAway(link, Rectangle.Intersect(link.edges, enemy.GetEdges()));
                    }

                    if (link.GetHealth() <= 0) { link.KillLink(game); Debug.WriteLine("Dead"); }


                }
                game.navi.ContactObject(enemy);

                //projectile sword enemy collisions
                if (link.usedProjectileSword != null)
                {
                    Debug.WriteLine(link.usedProjectileSword.GetEdges());
                    if (link.usedProjectileSword.Edges.Intersects(enemy.GetEdges()))
                    {
                        SoundManager.Instance.PlaySoundEffect("Arrow_Boomerang");
                        CollisionUtility.PushAway(enemy, Rectangle.Intersect(link.usedProjectileSword.Edges, enemy.GetEdges()));
                        link.usedProjectileSword.Collide(enemy);
                        link.usedProjectileSword = null;
                    }
                    
                }

                //regular sword enemy collisions
                if (link.usedSword != null)
                {
                    if (link.usedSword.Edges.Intersects(enemy.GetEdges()))
                    {
                        SoundManager.Instance.PlaySoundEffect("Arrow_Boomerang");
                        if (!enemy.IsDamaged())
                        {
                            enemy.TakeDamage(link.usedSword.GetDamage());
                            CollisionUtility.PushAway(enemy, Rectangle.Intersect(link.usedSword.Edges, enemy.GetEdges()));
                        }
                    }
                }

                if (link.usedItem != null)
                {
                    if (link.usedItem.Edges.Intersects(enemy.GetEdges()))
                    {
                        if (!enemy.IsDamaged())
                        {
                            if (link.usedItem.GetDamage() > 0)
                                CollisionUtility.PushAway(enemy, Rectangle.Intersect(link.usedItem.Edges, enemy.GetEdges()));
                            
                        }
                        link.usedItem.Collide(enemy);
                            
                    }
                }

                if (enemy.GetItems() != null)
                {
                    if (enemy.GetItems().Edges.Intersects(link.GetEdges()))
                    {
                        if (!link.IsDamaged())
                        {
                            link.TakeDamage(enemy.GetItems().GetDamage());
                            CollisionUtility.PushAway(link, Rectangle.Intersect(enemy.GetItems().Edges, link.GetEdges()));
                        }
                        enemy.GetItems().Collide(link);
                    }
                }

                if (enemy.GetVertRange().Intersects(link.edges))
                {
                    enemy.DetectVert(link.location);
                }
                if (enemy.GetHorizRange().Intersects(link.edges))
                {
                    enemy.DetectHoriz(link.location);
                }
                if (enemy.GetType() == typeof(BladeTrap))
                {
                    foreach (ICharacter otherEnemy in Enemies)
                    {
                        if (enemy != otherEnemy)
                            if (enemy.GetEdges().Intersects(otherEnemy.GetEdges()))
                                enemy.CollideAction();
                    }
                }                
            }

        }
        private void PushLink(Link link, ICharacter enemy)
        {
            if (Math.Abs(link.location.Y - enemy.GetLocation().Y) > Math.Abs(link.location.X - enemy.GetLocation().X))
            {
                if (link.location.Y < enemy.GetLocation().Y)
                    link.Push(Directions.Up);
                if (link.location.Y > enemy.GetLocation().Y)
                    link.Push(Directions.Down);
            }
            else
            {
                if (link.location.X < enemy.GetLocation().X)
                    link.Push(Directions.Left);
                if (link.location.X > enemy.GetLocation().X)
                    link.Push(Directions.Right);
            }
        }
        private void PushEnemy(IItem item, ICharacter enemy)
        {
            if (Math.Abs(enemy.GetLocation().Y - item.GetLocation().Y) > Math.Abs(enemy.GetLocation().X - item.GetLocation().X))
            {
                if (enemy.GetLocation().Y < item.GetLocation().Y)
                    enemy.Push(Directions.Up);
                if (enemy.GetLocation().Y > item.GetLocation().Y)
                    enemy.Push(Directions.Down);
            }
            else
            {
                if (enemy.GetLocation().X < item.GetLocation().X)
                    enemy.Push(Directions.Left);
                if (enemy.GetLocation().X > item.GetLocation().X)
                    enemy.Push(Directions.Right);
            }
        }
    }
}
