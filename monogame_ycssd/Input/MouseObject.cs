using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Input
{
    public class MouseObject
    {
        #region Variables
        private static MouseState _previousMouseState, _currentMouseState;
        private static Vector3 _position;
        private static Texture2D _texture;
        private static Rectangle _rectangle;
        #endregion

        #region Properties
        public static Texture2D Texture { get; set; }
        public static Vector2 Position
        {
            get { return new Vector2(_position.X, _position.Y); }
            set { var temp = value;
                _position = new Vector3(temp.X, temp.Y, 0);
            }
        }
        #endregion

        #region Left button
        public static bool LeftClick
        {
            get { return _currentMouseState.LeftButton == ButtonState.Pressed; }
        }
        public static bool NewLeftClik
        {
            get { return LeftClick && _previousMouseState.LeftButton == ButtonState.Released; }
        }

        public  static bool HoldLeft
        {
            get { return LeftClick && _previousMouseState.LeftButton == ButtonState.Pressed; }
        }

        public static bool LeftRelease
        {
            get { return !LeftClick && _previousMouseState.LeftButton == ButtonState.Pressed; }
        }
        #endregion

        #region Right button
        public static bool RightClick
        {
            get { return _currentMouseState.RightButton == ButtonState.Pressed; }
        }

        public static bool RightRelease
        {
            get { return !RightClick && _previousMouseState.RightButton == ButtonState.Pressed; }
        }
        #endregion

        #region

        public static void ResetMouse()
        {
            _position = new Vector3(0, 0, 0);
        }

        public static void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("cursor");
        }

        public static void Update()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            _position = new Vector3(_currentMouseState.X, _currentMouseState.Y, 0);
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
        public static void Draw(SpriteBatch spritebatch)
        {
            if(_texture != null)
            {
                spritebatch.Begin();
                spritebatch.Draw(_texture, _rectangle, Color.White);
                spritebatch.End();
            }
        }
        #endregion

    }

}
