using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using monogame_ycssd.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.Object.TileObject
{
    public class tile1:IDisposable
    {
        #region Variables
        private static Texture2D _texture;
        private List<Rectangle> _tileList;
        #endregion


        #region Method
        public tile1()
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("tile1");
            _tileList = new List<Rectangle>();
            _tileList.Add(new Rectangle(0, 0, 100, 100));
            _tileList.Add(new Rectangle(100, 0, 100, 100));
            _tileList.Add(new Rectangle(200, 0, 100, 100));
        }

        public Rectangle SouceRectangle(int i)
        {
            if (i < 0 || i > 2) i = 0;
            return _tileList[i];
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 position,int i)
        {
            spriteBatch.Draw(_texture, position, SouceRectangle(i), Color.White);
        }
        public void Dispose()
        {
            
        }
        #endregion
    }
}
