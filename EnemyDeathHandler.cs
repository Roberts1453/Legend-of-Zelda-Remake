
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace Sprint0Project
{

    public class EnemyDeathHandler
    {
        Game1 game;
        int killCount = 0;
        int killStreak = 0;
        List<ICharacter> toKill = new List<ICharacter>();
        List<Type> groupA = new List<Type>() { typeof(Octorok) , typeof(Moblin) };
        List<Type> groupB = new List<Type>() { typeof(Goriya), typeof(Gibdo), typeof(Vire), typeof(Darknut) /*typeof(Wizrobe)*/ };
        List<Type> groupC = new List<Type>() { typeof(Stalfos), typeof(Zol), typeof(Rope), typeof(Wallmaster) };
        List<Type> groupD = new List<Type>() { typeof(BlueGoriya), /*typeof(BlueDarknut)*/ typeof(Moldorm), typeof(Aquamentus) };
        int aDropChance = 80;
        int bDropChance = 104;
        int cDropChance = 152;
        int dDropChance = 104;

        public EnemyDeathHandler(Game1 gameState)
        {
            game = gameState;
        }
        public void RemoveEnemy(List<IList> roomContents, ICharacter toKill)
        {
            SoundManager.Instance.PlaySoundEffect("Enemy_Die");
            roomContents[1].Remove(toKill);
            if (toKill.GetType() == typeof(Moldorm))
            {
                Moldorm deadMoldorm = (Moldorm)toKill;
                if (deadMoldorm.front == null && deadMoldorm.back == null)
                    toKill = null;
                else
                {
                    if (deadMoldorm.back != null)
                        deadMoldorm.back.front = deadMoldorm.front;
                    if (deadMoldorm.front != null)
                        deadMoldorm.front.SetBack(deadMoldorm.back);
                }
            }
            else if (toKill.GetType() == typeof(Zol))
            {
                Zol deadZol = (Zol)toKill;
                if (deadZol.spawnGels)
                {
                    roomContents[1].Add(new Gel(toKill.Location));
                    roomContents[1].Add(new Gel(toKill.Location));
                }
            }
            else if (toKill.GetType() == typeof(Vire))
            {
                Vire deadVire = (Vire)toKill;
                if (deadVire.spawnKeese)
                {
                    roomContents[1].Add(new Keese(toKill.Location, 2));
                    roomContents[1].Add(new Keese(toKill.Location, 2));
                }
            }
            if (toKill != null)
            {
                killCount++;
                CreateDropItem(roomContents, toKill);
                if (killCount == 16)
                    killCount = 0;
            }

        }
        public void CreateDropItem(List<IList> roomContents, ICharacter enemy)
        {
            if (enemy.GetDropItem() != null)
            {
                roomContents[2].Add(enemy.GetDropItem());
                SoundManager.Instance.PlaySoundEffect("Key_Appear");
            }
            else if (killStreak == 10)
                roomContents[2].Add(new Rupee(enemy.Location, 5));
            else
            {
                int chance = Randomizer.Instance.Next(1, 256);
                if (groupA.Contains(enemy.GetType()))
                {
                    if (chance <= aDropChance)
                        roomContents[2].Add(GroupADrop(enemy.Location));
                }
                else if (groupB.Contains(enemy.GetType()))
                {
                    if (chance <= bDropChance)
                        roomContents[2].Add(GroupBDrop(enemy.Location));
                }
                else if (groupC.Contains(enemy.GetType()))
                {
                    if (chance <= cDropChance)
                        roomContents[2].Add(GroupCDrop(enemy.Location));
                }
                else if (groupD.Contains(enemy.GetType()))
                {
                    if (chance <= dDropChance)
                        roomContents[2].Add(GroupDDrop(enemy.Location));
                }


            }
        }
        public ICollectibleItem GroupADrop(Vector2 location)
        {
            ICollectibleItem dropItem = new Rupee(location);
            switch (killCount % 10)
            {
                case 0:
                    break;
                case 1:
                    dropItem = new Heart(location);
                    break;
                case 2:
                    break;
                case 3:
                    dropItem = new Heart(location);
                    break;
                case 4:
                    break;
                case 5:
                    dropItem = new Heart(location);
                    break;
                case 6:
                    dropItem = new Heart(location);
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    dropItem = new Heart(location);
                    break;
            }
            return dropItem;
        }
        public ICollectibleItem GroupBDrop(Vector2 location)
        {
            ICollectibleItem dropItem = new Rupee(location);
            switch (killCount % 10)
            {
                case 0:
                    dropItem = new Heart(location);
                    break;
                case 1:
                    dropItem = new BombCollectible(location);
                    break;
                case 2:
                    break;
                case 3:
                    //dropItem = new StopWatch(location);
                    break;
                case 4:
                    break;
                case 5:
                    dropItem = new Heart(location);
                    break;
                case 6:
                    dropItem = new BombCollectible(location);
                    break;
                case 7:
                    break;
                case 8:
                    dropItem = new BombCollectible(location);
                    break;
                case 9:
                    dropItem = new Heart(location);
                    break;
            }
            return dropItem;
        }
        public ICollectibleItem GroupCDrop(Vector2 location)
        {
            ICollectibleItem dropItem = new Rupee(location);
            switch (killCount % 10)
            {
                case 0:
                    dropItem = new Rupee(location, 5);
                    break;
                case 1:
                    break;
                case 2:
                    dropItem = new Heart(location);
                    break;
                case 3:
                    break;
                case 4:
                    dropItem = new Rupee(location, 5);
                    break;
                case 5:
                    dropItem = new Heart(location);
                    break;
                case 6:
                    //dropItem = new StopWatch(location);
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }
            return dropItem;
        }
        public ICollectibleItem GroupDDrop(Vector2 location)
        {
            ICollectibleItem dropItem = new Rupee(location);
            switch (killCount % 10)
            {
                case 0:
                    dropItem = new Heart(location);
                    break;
                case 1:
                    dropItem = new Heart(location);
                    break;
                case 2:
                    dropItem = new Heart(location);
                    break;
                case 3:
                    break;
                case 4:
                    dropItem = new Heart(location);
                    break;
                case 5:
                    dropItem = new Heart(location);
                    break;
                case 6:
                    dropItem = new Heart(location);
                    break;
                case 7:
                    dropItem = new Heart(location);
                    break;
                case 8:
                    dropItem = new Heart(location);
                    break;
                case 9:
                    break;
            }
            return dropItem;
        }
        public void Update(List<IList> roomContents)
        {
            ICharacter toKill = null;
            foreach (ICharacter enemy in (List<ICharacter>)roomContents[1])
            {
                if (!enemy.GetLiving())
                {
                    toKill = enemy;
                }
            }
            if (toKill != null)
            {
                RemoveEnemy(roomContents, toKill);
            }

        }
    }
}
