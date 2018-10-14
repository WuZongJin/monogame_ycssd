using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Manager
{
    public class SoundManager
    {
        #region Variables
        private static SoundManager instance;
        private Dictionary<string, SoundEffect> _soundList = new Dictionary<string, SoundEffect>();

        Song song;
        #endregion

        #region Method
        public static SoundManager Getinstance()
        {
            if (instance != null)
            {
                return instance;
            }
            return instance = new SoundManager();
        }

        public void Initialize()
        {
            SoundEffect s1 = MyContentManager.GetInstance().LoadContent<SoundEffect>("Sound\\smallgun1");
            SoundEffect s2 = MyContentManager.GetInstance().LoadContent<SoundEffect>("Sound\\pigDeath");
            SoundEffect s3 = MyContentManager.GetInstance().LoadContent<SoundEffect>("Sound\\pigAttack1");
            SoundEffect s4 = MyContentManager.GetInstance().LoadContent<SoundEffect>("Sound\\heroGetHert");
            song = MyContentManager.GetInstance().LoadContent<Song>("Sound\\bgm");
            _soundList.Add("smallgun", s1);
            _soundList.Add("pigDeath", s2);
            _soundList.Add("pigAttack", s3);
            _soundList.Add("heroGetHert", s4);
        }

        public void PlayBGM()
        {
            if (MediaPlayer.GameHasControl && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(this.song);
            }
        }

        public void PlaySoundEffect(string name)
        {
            _soundList[name].Play();
        }

        #endregion
    }
}
