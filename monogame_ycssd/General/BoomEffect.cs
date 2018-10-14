using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.General
{
    public abstract class BoomEffect
    {



        #region Variables
        private Animation _boomAnimation;
        private bool _isFinshed;
        private Texture2D _texture;
        #endregion

        #region Properties
        public Animation BoomAnimation
        {
            get { return _boomAnimation; }
            set { _boomAnimation = value; }
        }
        public bool IsFinshed
        {
            get { return _isFinshed; }
            set { _isFinshed = value; }
        }
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        #endregion

        #region Method

        public BoomEffect() { }

        public abstract void Updata(GameTime gametime);
        public abstract void Draw(SpriteBatch spritebatch);
        #endregion
    }
}
