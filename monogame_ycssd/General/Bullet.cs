using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace monogame_ycssd.General
{
    public abstract class Bullet
    {

        #region Variables
        private Texture2D _texture;
        private Sprite _sprite;
        private int _damage;
        private int live;                       //子弹的生命
        private bool _ispenetrate;              //是否能穿透
        private bool _isrebound;                //碰到墙壁是否能反弹
        private bool _isclearbullet;            //刀剑类能够抵挡子弹
        #endregion

        #region Propreties
        public Texture2D Texture
        {
            get { return _texture; }
            protected set { _texture = value; }
        }
        public Sprite BulletSprite
        {
            get { return _sprite; }
            protected set { _sprite = value; }
        }

        #endregion

        #region Method
        public abstract void Updata();
        public abstract void Draw(SpriteBatch spritebatch);
        public abstract void Bomb();
        #endregion


    }
}
