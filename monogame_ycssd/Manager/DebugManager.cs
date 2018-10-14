using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Manager
{
    public class DebugManager:DrawableGameComponent
    {
        #region Variables
        private SpriteBatch _spribatch;
        private SpriteFont _spriteFont;
        private Vector2 _heroPosition;
        private int _enemyNum;
        private int _heroBulletNum;
        #endregion

        public DebugManager(Game game,SpriteBatch spriteBatch)
            : base(game)
        {
            _spribatch = spriteBatch;
           
            _heroPosition = new Vector2(0, 0);
            _enemyNum = 0;
            _heroBulletNum = 0;
        }

        public override void Initialize()
        {
            _spriteFont = MyContentManager.instance.defaultFont;
            base.Initialize();
        }

        
        public override void Update(GameTime gameTime)
        {

            _heroPosition = GameManager.GetInstance().Player.Position;
            _enemyNum = GameManager.GetInstance().EnemyNum;
            _heroBulletNum = GameManager.GetInstance().PlayBulletNum;


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            string heroMesage = "英雄目前位置：" + _heroPosition.ToString();
            string PlayerBulletNum = "英雄子弹目前数量：" + _heroBulletNum.ToString();
            string EnemyNum = "敌人目前数量：" + _enemyNum.ToString();

            _spribatch.Begin();
            _spribatch.DrawString(_spriteFont, heroMesage, new Vector2(0, 0), Color.White);
            _spribatch.DrawString(_spriteFont, heroMesage, new Vector2(0, 20), Color.White);
            _spribatch.DrawString(_spriteFont, heroMesage, new Vector2(0, 40), Color.White);

            _spribatch.End();
            base.Draw(gameTime);
        }


    }
}
