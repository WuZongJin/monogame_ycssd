using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace monogame_ycssd.Manager
{
    public class MyContentManager:IDisposable
    {
        #region Variables
        public static MyContentManager instance;
        private ContentManager _contentManager;

        private SpriteFont _defaultFont;
        private static Dictionary<string, Texture2D> _textureList;
        private SoundEffect sound;
        #endregion

        #region Properties
        public MyContentManager Instance
        {
            set { instance = value; }
        }

        public SpriteFont defaultFont
        {
            get { return _defaultFont; }
            set { _defaultFont = value; }
        }
        #endregion

        #region Method
        public MyContentManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
            
        }

        public static MyContentManager GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                System.Console.WriteLine("MyConsole is null");
                return null;
            }
        }
        public Texture2D GetTexture(string name)
        {
            return _textureList[name];
        }
        public void Loadtexture(string path)
        {
            _contentManager.Load<Texture2D>(path);
        }
        public T LoadContent<T>(string path)
        {
            return _contentManager.Load<T>(path);
        }
        public void Dispose()
        {
            _textureList.Clear();
        }
        #endregion
    }
}
