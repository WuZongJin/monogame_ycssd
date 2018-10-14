using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.General;
using monogame_ycssd.Manager;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.Object.EnemyBulletObject
{
    public class EnemyBullet1 : Bullet
    {
        #region Variables
        private Texture2D _texture;
        #endregion

        #region Method
        public EnemyBullet1(Vector2 position, int width, int height, Vector2 velocity, float rotation, Vector2 origin, Vector2 scale, Color color, bool islive)
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("bullet1");
            BulletSprite = new Sprite(position, width, height, velocity, rotation, origin, scale, color, islive);

        }
        #endregion


        public override void Bomb()
        {
            BulletSprite.IsLive = false;
            BoomEffectObject.boomEffect1 boom = new BoomEffectObject.boomEffect1(new Vector2(BulletSprite.X, BulletSprite.Y));
            GameManager.GetInstance().AddBoomEffect(boom);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!BulletSprite.IsLive) return;
                spritebatch.Draw(_texture, BulletSprite.Position, null, Color.White, BulletSprite.Rotation, BulletSprite.Origin, BulletSprite.Scale, SpriteEffects.None, 0.0f);
        }

        public override void Updata()
        {
            BulletSprite.X += BulletSprite.SpeedX;
            BulletSprite.Y += BulletSprite.SpeedY;
            BulletSprite.Update();
        }
    }
}
