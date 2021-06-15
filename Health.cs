using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Sprint0Project
{
    public class Health
    {
        Link link;
        public int maxHealthCap = 32;
        public int maxHealth = 6;
        public int currentHealth;
        SoundEffectInstance lowHealth;
        public Health(Link _link)
        {
            link = _link;
            currentHealth = maxHealth;
        }
        public void Update(GameTime gametime)
        {
            
        }
        public void TakeDamage(int damage)
        {
            if (currentHealth == 1)
                SoundManager.Instance.StopSoundEffectLoop(lowHealth);
            currentHealth -= damage;
            if (currentHealth == 1)
                lowHealth = SoundManager.Instance.PlaySoundEffectLoop("Low_Health");
        }
        public void Heal(int heal)
        {
            if (currentHealth == 1)
                SoundManager.Instance.StopSoundEffectLoop(lowHealth);
            currentHealth += heal;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            
            
        }
        public void IncreaseMax()
        {
            maxHealth += 2;
            if (maxHealth > maxHealthCap)
                maxHealth = maxHealthCap;
        }
        public void SetHealth(int newHealth)
        {
            maxHealth = newHealth;
            if (maxHealth > maxHealthCap)
                maxHealth = maxHealthCap;
            currentHealth = maxHealth;
        }
    }
}
