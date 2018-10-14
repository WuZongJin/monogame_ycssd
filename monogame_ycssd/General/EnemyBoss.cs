using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.General
{
    public abstract class EnemyBoss
    {
        #region Variables
        private Sprite _sprite;
        private int _health;
        #endregion

        #region Properties
        public Sprite EnemySprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        #endregion

        #region overrideMethod
        public Vector2 FindHero(Hero.Hero hero)
        {
            return hero.HeroSprite.Position;
        }
        public abstract void Appear();
        public abstract void Move();
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Boom();
        #endregion
        
    }
}
