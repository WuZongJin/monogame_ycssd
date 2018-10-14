using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.General;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.BoomEffectObject
{
    public class boomEffect1 : BoomEffect
    {

        #region Variables
        private Vector2 _position;
        private Animation _currentAnimation;
        #endregion
        


        public boomEffect1(Vector2 position)
        {
            _position = position;
            BoomAnimation = new Animation();
            BoomAnimation.IsLoop = false;
            BoomAnimation.FrameNum = 5;
            Texture = MyContentManager.GetInstance().LoadContent<Texture2D>("boomEffect1");
            BoomAnimation.AddFrame(new Rectangle(0, 0, 33, 32), TimeSpan.FromSeconds(.15));
            BoomAnimation.AddFrame(new Rectangle(33, 0, 33, 32), TimeSpan.FromSeconds(.15));
            BoomAnimation.AddFrame(new Rectangle(66, 0, 33, 32), TimeSpan.FromSeconds(.15));
            BoomAnimation.AddFrame(new Rectangle(99, 0, 33, 32), TimeSpan.FromSeconds(.15));
            BoomAnimation.AddFrame(new Rectangle(132, 0, 33, 32), TimeSpan.FromSeconds(.15));

            _currentAnimation = BoomAnimation;
        }



        #region Method

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!IsFinshed)
            {
                spritebatch.Draw(Texture, _position, _currentAnimation.CurrentRectangle, Color.White, 0.0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
            }
        }

        public override void Updata(GameTime gametime)
        {
            _currentAnimation = BoomAnimation;
            _currentAnimation.Update(gametime);
            if (_currentAnimation.IsFinshed)
            {
                IsFinshed = true;
            }
        }
        #endregion
    }
}
