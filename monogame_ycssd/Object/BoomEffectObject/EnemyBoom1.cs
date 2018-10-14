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
    public class EnemyBoom1 : BoomEffect
    {
        #region Variables
        private Vector2 _position;
        private Animation _currentAnimation;
        #endregion

        #region Method
        public EnemyBoom1(Vector2 position)
        {
            _position = position;
            BoomAnimation = new Animation();
            BoomAnimation.IsLoop = false;
            BoomAnimation.FrameNum = 6;
            Texture = MyContentManager.GetInstance().LoadContent<Texture2D>("EnemyBoom1");
            BoomAnimation.AddFrame(new Rectangle(0, 0, 100, 100), TimeSpan.FromSeconds(.1));
            BoomAnimation.AddFrame(new Rectangle(100, 0, 100, 100), TimeSpan.FromSeconds(.1));
            BoomAnimation.AddFrame(new Rectangle(200, 0, 100, 100), TimeSpan.FromSeconds(.1));
            BoomAnimation.AddFrame(new Rectangle(300, 0, 100, 100), TimeSpan.FromSeconds(.1));
            BoomAnimation.AddFrame(new Rectangle(400, 0, 100, 100), TimeSpan.FromSeconds(.1));
            BoomAnimation.AddFrame(new Rectangle(500, 0, 100, 100), TimeSpan.FromSeconds(.1));

            _currentAnimation = BoomAnimation;

        }
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
