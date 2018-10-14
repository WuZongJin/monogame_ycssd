using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.General;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.WallObject
{
    public class wall1 : Wall
    {
        #region Varibales
        private Texture2D _texture;
        #endregion


        #region Method
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public wall1(Texture2D texture,Vector2 position)
        {
            _texture = texture;
            Position = position;
        }
        public wall1()
        {
            Position = Vector2.Zero;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, new Rectangle(0,0,100,100), Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch,Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 100, 100), Color.White);
        }

        public override void Update(GameTime gametime)
        {
            
        }
        #endregion
    }
}
