
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace Sprint0Project
{

    public class CollisionDetectionCollectibles
    {
        public Vector2 Location;
        Game1 game;
        Link link;

        public CollisionDetectionCollectibles(Game1 gameState)
        {
            game = gameState;
        }

        
        public void Update(ArrayList items)
        {
            link = game.link;
            foreach (ICollectibleItem item in items)
            {
                //link enemy collisions
                Rectangle intersect = Rectangle.Intersect(link.edges, item.GetEdges());
                if (intersect.Width > 0 || intersect.Height > 0)
                {
                    item.Collect(link);
                }
                if (item.GetEdges().Intersects(game.navi.GetHorizRange()))
                    game.navi.ContactObject(item);
            }

        }
    }
}
