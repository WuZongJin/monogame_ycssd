using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.General
{
    public abstract class Wall
    {

        #region Variables
        private Rectangle _rectangle;
        private Vector2 _position;

        #endregion

        #region Properties
        public Rectangle SourceRectangle
        {
            get { return _rectangle = new Rectangle((int)Position.X, (int)Position.Y, 100, 100); }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        #endregion

        #region Method
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position);
        #endregion
    }
}
