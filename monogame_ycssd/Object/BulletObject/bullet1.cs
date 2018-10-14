using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.BulletObject
{
    public class bullet1:Bullet
    {
        
        #region Variables
        //private static MyXMLData.BulletData.bullet1_data _bulletdata;
        private Texture2D _texture;
        #endregion

        #region Method
        public bullet1(Vector2 position, int width, int height, Vector2 velocity, float rotation, Vector2 origin, Vector2 scale, Color color, bool islive)
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("bullet1");
            BulletSprite = new Sprite(position, width, height, velocity, rotation, origin, scale, color, islive);
        }
        public override void Updata()
        {
            //BulletSprite.X += BulletSprite.SpeedX * (float)(Math.Sin(BulletSprite.Rotation));
            //BulletSprite.Y += BulletSprite.SpeedY * (float)(Math.Cos(BulletSprite.Rotation));
            BulletSprite.X += BulletSprite.SpeedX;
            BulletSprite.Y += BulletSprite.SpeedY;
            BulletSprite.Update();
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            if (!BulletSprite.IsLive) return;
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth
            spritebatch.Draw(_texture, BulletSprite.Position,null, Color.White,BulletSprite.Rotation,BulletSprite.Origin,BulletSprite.Scale,SpriteEffects.None,0.0f);
        }
        public override void Bomb()
        {
            BulletSprite.IsLive = false;
            BoomEffectObject.boomEffect1 boom = new BoomEffectObject.boomEffect1(new Vector2(BulletSprite.X, BulletSprite.Y));
            GameManager.GetInstance().AddBoomEffect(boom);
        }
        
        #endregion
    }
}
