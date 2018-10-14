using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.Object.BulletObject;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.General
{
    public abstract class Weapon
    {
        #region Variables
        private static Sprite _sprite;
        private static int _damage;
        private static Bullet _bullet;

        #endregion

        #region Propreties
        //public Texture2D Texture
        //{
        //    get { return _texture; }
        //    set { _texture = value; }
        //}
        public Sprite WeaponSprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public Bullet Bullet
        {
            get { return _bullet; }
            set { _bullet = value; }
        }

        #endregion

        #region Method
        public abstract void Equip(Hero.Hero hero);
        public abstract void Updata();
        public abstract void SetRotation(Vector2 position);
        public abstract void Attack(Vector2 position);
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
