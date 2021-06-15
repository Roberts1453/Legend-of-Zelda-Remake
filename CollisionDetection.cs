
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint0Project
{

    public class CollisionDetection
    {
        public Vector2 Location;
        Game1 game;

        public CollisionDetection(Game1 gameState)
        {
            game = gameState;
        }

        
        public void Update(List<ICharacter> enemies)
        {
            foreach (ICharacter enemy in enemies) {
                if (game.link.edges.Intersects(enemy.GetEdges()))
                {
                    game.link.TakeDamage(enemy.GetDamage());
                    enemy.TakeDamage(4);
                    if (Math.Abs(game.link.location.Y - enemy.GetLocation().Y) > Math.Abs(game.link.location.X - enemy.GetLocation().X))
                    {
                        if (game.link.location.Y < enemy.GetLocation().Y)
                            game.link.Push(Directions.Up);
                        if (game.link.location.Y > enemy.GetLocation().Y)
                            game.link.Push(Directions.Down);
                    }
                    else
                    {
                        if (game.link.location.X < enemy.GetLocation().X)
                            game.link.Push(Directions.Left);
                        if (game.link.location.X > enemy.GetLocation().X)
                            game.link.Push(Directions.Right);
                    }                    
                    

                }
                if (game.link.usedSword != null)
                {
                    if (game.link.usedSword.GetEdges().Intersects(enemy.GetEdges()))
                    {
                        enemy.TakeDamage(game.link.usedSword.GetDamage());
                        if (Math.Abs(game.link.usedSword.GetLocation().Y - enemy.GetLocation().Y) > Math.Abs(game.link.usedSword.GetLocation().X - enemy.GetLocation().X))
                        {
                            if (game.link.usedSword.GetLocation().Y < enemy.GetLocation().Y)
                                enemy.MoveUp();
                            if (game.link.usedSword.GetLocation().Y > enemy.GetLocation().Y)
                                enemy.MoveDown();
                        }
                        else
                        {
                            if (game.link.usedSword.GetLocation().X < enemy.GetLocation().X)
                                enemy.MoveLeft();
                            if (game.link.usedSword.GetLocation().X > enemy.GetLocation().X)
                                enemy.MoveRight();
                        }
                    }
                }
                if (game.link.usedProjectileSword != null)
                {
                    if (game.link.usedProjectileSword.GetEdges().Intersects(enemy.GetEdges()))
                    {
                        enemy.TakeDamage(4);
                        if (Math.Abs(game.link.usedProjectileSword.GetLocation().Y - enemy.GetLocation().Y) > Math.Abs(game.link.usedProjectileSword.GetLocation().X - enemy.GetLocation().X))
                        {
                            if (game.link.usedProjectileSword.GetLocation().Y < enemy.GetLocation().Y)
                                enemy.MoveUp();
                            if (game.link.usedProjectileSword.GetLocation().Y > enemy.GetLocation().Y)
                                enemy.MoveDown();
                        }
                        else
                        {
                            if (game.link.usedProjectileSword.GetLocation().X < enemy.GetLocation().X)
                                enemy.MoveLeft();
                            if (game.link.usedProjectileSword.GetLocation().X > enemy.GetLocation().X)
                                enemy.MoveRight();
                        }
                    }
                }


            }
        }
    }
}
