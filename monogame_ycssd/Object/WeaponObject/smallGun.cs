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

namespace monogame_ycssd.Object.WeaponObject
{
    public class smallGun:Weapon
    {


        #region Variables
        private static Texture2D _bulletTexture;
        private static Texture2D _weaponTexture;
        private Vector2 _firePosition;
        private static MyXMLData.BulletData.bullet1_data _bulletData;
        #endregion


        #region Propreties
        
        #endregion

        #region Method
        public smallGun(Vector2 position,int width,int height,Vector2 velocity,float rotation,Vector2 origin,Vector2 scale,Color color, bool islive)
        {
            _bulletData = MyContentManager.GetInstance().LoadContent<MyXMLData.BulletData.bullet1_data>("bullet1_data");
            _weaponTexture = MyContentManager.GetInstance().LoadContent<Texture2D>("samllgun");
            WeaponSprite = new Sprite(position, width, height, velocity, rotation,origin,scale,color, islive);
            var tempx = (float)(WeaponSprite.Width * Math.Cos(WeaponSprite.Rotation));
            var tempy = (float)(WeaponSprite.Width * Math.Sin(WeaponSprite.Rotation));
            _firePosition = WeaponSprite.Position + new Vector2(tempx,tempy);

        }

        public override void Equip(Hero.Hero hero)
        {

            var temp = hero.HeroSprite.Center;
            Vector2 tempPosition = new Vector2(temp.X,temp.Y+50);
            WeaponSprite.Position = tempPosition;

            
            Vector2 tempVelocity = new Vector2(hero.HeroSprite.SpeedX, hero.HeroSprite.SpeedY);
            WeaponSprite.Velocity = tempVelocity;

            float tempRotation = hero.HeroSprite.Rotation;
            WeaponSprite.Rotation = tempRotation;
            WeaponSprite.IsLive = true;
            WeaponSprite.Update();
        }
        public override void Attack(Vector2 position)
        {

            SoundManager.Getinstance().PlaySoundEffect("smallgun");

            var tempx = position.X - WeaponSprite.Position.X;
            var tempy = position.Y - WeaponSprite.Position.Y;
            Vector2 direction = new Vector2(tempx, tempy);
            direction.Normalize();
            //var rotation = Math.Atan(tempy / tempx);
            //float speedx = (float)(10*Math.Sin(rotation));
            //float speedy = (float)(10*Math.Cos(rotation));
            float speedx = direction.X * 10;
            float speedy = direction.Y * 10;

            BulletObject.bullet1 bullet1 = new BulletObject.bullet1(_firePosition, 10,10, new Vector2(speedx,speedy), WeaponSprite.Rotation,new Vector2(0,0),new Vector2(1,1),Color.White, true);
            GameManager.GetInstance().AddPlayerBullet(bullet1);
        }

        public override void SetRotation(Vector2 position)
        {
           
            var temp = position - (WeaponSprite.Position);
            //temp.Normalize();
            WeaponSprite.Rotation = (float)(Math.Atan2((temp.Y), (temp.X)));
        }

        public override void Updata()
        {
            WeaponSprite.Update();
            var tempx = (float)(WeaponSprite.Width * Math.Cos(WeaponSprite.Rotation));
            var tempy = (float)(WeaponSprite.Width * Math.Sin(WeaponSprite.Rotation));
            _firePosition = WeaponSprite.Position + new Vector2(tempx, tempy);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth
            if(WeaponSprite.Rotation>=-Math.PI/2 && WeaponSprite.Rotation<=Math.PI/2)
                spriteBatch.Draw(
                    _weaponTexture,
                    WeaponSprite.Position,
                    null,
                    Color.White,
                    WeaponSprite.Rotation,
                    WeaponSprite.Origin,
                    WeaponSprite.Scale,
                    SpriteEffects.None,
                    0f
                    );
            else
            {
                spriteBatch.Draw(
                    _weaponTexture,
                    WeaponSprite.Position,
                    null,
                    Color.White,
                    WeaponSprite.Rotation,
                    WeaponSprite.Origin,
                    WeaponSprite.Scale,
                    SpriteEffects.FlipVertically,
                    0f
                    );
            }
            //spriteBatch.Draw(_weaponTexture, WeaponSprite.Rectangle, Color.White);
        }
        #endregion
    }
}
