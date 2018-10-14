using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Component
{
    class HeroUIDrawComponent : DrawableGameComponent
    {
        #region Variables
        private SpriteBatch _spritebatch;
        private Texture2D _heartTexture;
        private Texture2D _weaponTexture;
        private Texture2D _coinTexture;
        private Texture2D _bulletNumTexture;
        private SpriteFont _font;
        #endregion
        public HeroUIDrawComponent(Game game,SpriteBatch spriteBatch)
            : base(game)
        {
            _spritebatch = spriteBatch;
        }

        private void DrawHeroHealth()
        {
            int health = GameManager.GetInstance().Player.health;
            for(int i = 0; i < health; i++)
            {
                _spritebatch.DrawString(_font, "HP:", new Vector2(10, 20), Color.Black);
                _spritebatch.Draw(_heartTexture, new Vector2(i * 36 + 50, 20), Color.White);
            }
        }


        public override void Initialize()
        {
            _heartTexture = MyContentManager.GetInstance().LoadContent<Texture2D>("herohealth");
            _font = MyContentManager.GetInstance().LoadContent<SpriteFont>("DefaultFomt");
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            _spritebatch.Begin();
            DrawHeroHealth();
            _spritebatch.End();

            base.Draw(gameTime);
        }
    }
}
