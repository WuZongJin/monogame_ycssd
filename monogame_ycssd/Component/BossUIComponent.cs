using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.Manager;
using monogame_ycssd.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Component
{
    class BossUIComponent : DrawableGameComponent
    {

        #region Variables
        private SpriteBatch _spriteBatch;
        private Texture2D _bloodbar;
        private Texture2D _blood;
        private EnemyBoss _boss;
        private bool hasboss = false;
        #endregion

        public BossUIComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
        }

        #region OverrideMethod
        public override void Initialize()
        {
            _bloodbar = MyContentManager.GetInstance().LoadContent<Texture2D>("bossBoledBar");
            _blood = MyContentManager.GetInstance().LoadContent<Texture2D>("bossblood");
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameManager.GetInstance().BossList.Count != 0 && !hasboss)
            {
                _boss = GameManager.GetInstance().BossList.First();
                hasboss = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!hasboss) return;
            if (!_boss.EnemySprite.IsLive)
            {
                hasboss = false;
                return;
            }
            _spriteBatch.Begin();
            for (int i = 0; i < _boss.Health; i++)
            {
                _spriteBatch.Draw(_blood, new Vector2(500 + i * 4, 800), Color.White);
            }
            _spriteBatch.Draw(_bloodbar, new Vector2(500, 800), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
