using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.General
{
    public class Text
    {
        #region Variables
        private static GraphicsDevice _graphicsDevice;
        private static ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _spriteFont;
        private static Color _fontColor;
        #endregion

        #region Method
        public static void LoadContent(GraphicsDevice graphicsdevice, ContentManager contentmanager)
        {
            _graphicsDevice = graphicsdevice;
            _contentManager = contentmanager;

            _spriteFont = _contentManager.Load<SpriteFont>("DefaultFont");
            _fontColor = new Color(255, 192, 0);
        }
        public static void Draw(string text, Vector2 position)
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_spriteFont, text, position, _fontColor);
            _spriteBatch.End();
        }
        #endregion
    }
}
