using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;


namespace Sprint0Project
{
    public class SoundManager
    {
        string currentSong;

        Song dungeonSong;
        Song menuSong;
        Song overworldSong;

        SoundEffect arrowBoomerang;
        SoundEffect bombBlowUp;
        SoundEffect bombDrop;
        SoundEffect bossHit;
        SoundEffect bossScream1;
        SoundEffect bossScream2;
        SoundEffect bossScream3;
        SoundEffect candle;
        SoundEffect doorUnlock;
        SoundEffect enemyDie;
        SoundEffect enemyHit;
        SoundEffect fanfare;
        SoundEffect getHeart;
        SoundEffect getItem;
        SoundEffect getRupee;
        SoundEffect keyAppear;
        SoundEffect linkDie;
        SoundEffect linkHurt;
        SoundEffect lowHealth;
        SoundEffect magicalRod;
        SoundEffect recorder;
        SoundEffect refillLoop;
        SoundEffect secret;
        SoundEffect shield;
        SoundEffect stairs;
        SoundEffect shore;
        SoundEffect swordCombined;
        SoundEffect swordShoot;
        SoundEffect swordSlash;
        SoundEffect text;
        SoundEffect textSlow;
        SoundEffect naviHey;
        SoundEffect naviListen;

        SoundEffectInstance Boomerang;
        SoundEffectInstance LowHealth;

        private static SoundManager instance = new SoundManager();

        private readonly Dictionary<string, Song> songs = new Dictionary<string, Song>();
        private readonly Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private readonly Dictionary<string, SoundEffectInstance> soundEffectLoops = new Dictionary<string, SoundEffectInstance>();

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }


        public void LoadSounds(ContentManager content)
        {
            dungeonSong = content.Load<Song>("DungeonSong");
            menuSong = content.Load<Song>("MenuTheme");
            overworldSong = content.Load<Song>("Overworld");

            songs.Add("DungeonSong", dungeonSong);
            songs.Add("MenuSong", menuSong);
            songs.Add("OverworldSong", overworldSong);
            

            arrowBoomerang = content.Load<SoundEffect>("LOZ_Arrow_Boomerang");
            bombBlowUp = content.Load<SoundEffect>("LOZ_Bomb_Blow");
            bombDrop = content.Load<SoundEffect>("LOZ_Bomb_Drop");
            linkHurt = content.Load<SoundEffect>("LOZ_Link_Hurt");
            swordShoot = content.Load<SoundEffect>("LOZ_Sword_Shoot");
            swordSlash = content.Load<SoundEffect>("LOZ_Sword_Slash");
            text = content.Load<SoundEffect>("LOZ_Text");
            textSlow = content.Load<SoundEffect>("LOZ_Text_Slow");

            bossHit = content.Load<SoundEffect>("LOZ_Boss_Hit");
            bossScream1 = content.Load<SoundEffect>("LOZ_Boss_Scream1");
            bossScream2 = content.Load<SoundEffect>("LOZ_Boss_Scream2");
            bossScream3 = content.Load<SoundEffect>("LOZ_Boss_Scream3");
            candle = content.Load<SoundEffect>("LOZ_Candle");
            doorUnlock = content.Load<SoundEffect>("LOZ_Door_Unlock");
            enemyDie = content.Load<SoundEffect>("LOZ_Enemy_Die");
            enemyHit = content.Load<SoundEffect>("LOZ_Enemy_Hit");
            fanfare = content.Load<SoundEffect>("LOZ_Fanfare");
            getHeart = content.Load<SoundEffect>("LOZ_Get_Heart");
            getItem = content.Load<SoundEffect>("LOZ_Get_Item");
            getRupee = content.Load<SoundEffect>("LOZ_Get_Rupee");
            keyAppear = content.Load<SoundEffect>("LOZ_Key_Appear");
            linkDie = content.Load<SoundEffect>("LOZ_Link_Die");
            lowHealth = content.Load<SoundEffect>("LOZ_LowHealth");
            magicalRod = content.Load<SoundEffect>("LOZ_MagicalRod");
            recorder = content.Load<SoundEffect>("LOZ_Recorder");
            refillLoop = content.Load<SoundEffect>("LOZ_Refill_Loop");
            secret = content.Load<SoundEffect>("LOZ_Secret");
            shield = content.Load<SoundEffect>("LOZ_Shield");
            shore = content.Load<SoundEffect>("LOZ_Shore");
            stairs = content.Load<SoundEffect>("LOZ_Stairs");
            swordCombined = content.Load<SoundEffect>("LOZ_Sword_Combined");

            naviHey = content.Load<SoundEffect>("Navi_Hey");
            naviListen = content.Load<SoundEffect>("Navi_Listen");


            soundEffects.Add("Arrow_Boomerang", arrowBoomerang);
            soundEffects.Add("Bomb_BlowUp", bombBlowUp);
            soundEffects.Add("Bomb_Drop", bombDrop);
            soundEffects.Add("Link_Hurt", linkHurt);
            soundEffects.Add("Sword_Shoot", swordShoot);
            soundEffects.Add("Sword_Slash", swordSlash);
            soundEffects.Add("Text", text);
            soundEffects.Add("Text_Slow", textSlow);

            soundEffects.Add("Boss_Hit", bossHit);
            soundEffects.Add("Boss_Scream1", bossScream1);
            soundEffects.Add("Boss_Scream2", bossScream2);
            soundEffects.Add("Boss_Scream3", bossScream3);
            soundEffects.Add("Candle", candle);
            soundEffects.Add("Door_Unlock", doorUnlock);
            soundEffects.Add("Enemy_Die", enemyDie);
            soundEffects.Add("Enemy_Hit", enemyHit);
            soundEffects.Add("Fanfare", fanfare);
            soundEffects.Add("Get_Heart", getHeart);
            soundEffects.Add("Get_Item", getItem);
            soundEffects.Add("Get_Rupee", getRupee);
            soundEffects.Add("Key_Appear", keyAppear);
            soundEffects.Add("Link_Die", linkDie);
            soundEffects.Add("Low_Health", lowHealth);
            soundEffects.Add("Magical_Rod", magicalRod);
            soundEffects.Add("Recorder", recorder);
            soundEffects.Add("Refill_Loop", refillLoop);
            soundEffects.Add("Secret", secret);
            soundEffects.Add("Shield", shield);
            soundEffects.Add("Shore", shore);
            soundEffects.Add("Stairs", stairs);
            soundEffects.Add("Sword_Combined", swordCombined);

            soundEffects.Add("Navi_Hey", naviHey);
            soundEffects.Add("Navi_Listen", naviListen);


            Boomerang = arrowBoomerang.CreateInstance();
            soundEffectLoops.Add("Boomerang", Boomerang);
            LowHealth = lowHealth.CreateInstance();
            soundEffectLoops.Add("Low_Health", LowHealth);
        }


        public void PlaySong(string songName)
        {
            if (MediaPlayer.State != MediaState.Stopped && currentSong == songName)
            {
                MediaPlayer.Resume();
            }
            else
            {                
                MediaPlayer.Play(songs[songName]);
                MediaPlayer.IsRepeating = true;
                currentSong = songName;
            }
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        public void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public void PlaySoundEffect(string soundEffectName)
        {
            soundEffects[soundEffectName].Play();
        }

        public SoundEffectInstance PlaySoundEffectLoop(string soundEffectName)
        {
            soundEffectLoops[soundEffectName].IsLooped = true;
            soundEffectLoops[soundEffectName].Play();
            return soundEffectLoops[soundEffectName];
        }
        public void StopSoundEffectLoop(SoundEffectInstance soundEffectName)
        {
            soundEffectName.IsLooped = false;
            soundEffectName.Stop();
        }

    }
}