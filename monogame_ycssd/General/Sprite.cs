using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using monogame_ycssd;

namespace monogame_ycssd.General
{
    public class Sprite
    {

        public Sprite() { }
        public Sprite(Vector2 position,int width, int height, Vector2 velocity, float rotation,Vector2 origin, Vector2 scale,Color color, bool islive)
        {
            _position = position;
            _velocity = velocity;
            _rotation = rotation;
            _width = width;
            _height = height;
            var x = _position.X + width / 2;
            var y = _position.Y + height / 2;
            _origin = origin;
            _center = new Vector2(x, y);
            _scale = scale;
            _color = color;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
            _islive = islive;
        }

        #region Variables
        private Vector2 _position;
        private Vector2 _velocity;
        private int _width, _height;
        private float _rotation;
        private Vector2 _origin;
        private Vector2 _center;
        private Vector2 _scale;
        private Color _color;
        private Rectangle _rectangle;
        private bool _islive;
        #endregion


        #region  Poperties
        public Vector2 Position                         //位置
        {
            get { return _position; }
            set
            {
                var temp = value;
                _position = new Vector2(temp.X, temp.Y);
            }
        }

        public Color SpriteColor
        {
            get;
            set;
        }
        public Vector2 Scale
        {
            get {return _scale; }
            set
            {
                var temp = value;
                _scale = new Vector2(temp.X, temp.Y);
            }
        } 
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Hetight
        {
            get { return _height; }
            set { _height = value; }
        }
        public Vector2 Velocity          //速度
        {
            get { return _velocity; }
            set
            {
                var temp = value;
                _velocity = new Vector2(temp.X, temp.Y);
            }
        }
        public float Rotation            //旋转角度
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        public Vector2 Center
        {
            get { return _center; }
        }
        public Vector2 Origin          
        {
            get
            {
                return _origin;
            }
            set
            {
                var temp = value;
                _origin = new Vector2(temp.X, temp.Y);
            }    
        }  
        public Rectangle Rectangle
        {
            get { return _rectangle; }
        }
                  
        public float X              //坐标X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        public float Y              //坐标Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        public float SpeedX         //速度X
        {
            get { return _velocity.X; }
            set { _velocity.X = value; }
        }
        public float SpeedY         //速度Y
        {
            get { return _velocity.Y; }
            set { _velocity.Y = value; }
        }

        public bool IsLive          //是否存活
        {
            get { return _islive; }
            set { _islive = value; }
        }

        #endregion

        #region GetMethod
        public Vector2 GetPosition()
        {
            float tempx = Position.X;
            float tempy = Position.Y;
            return new Vector2(tempx, tempy);
        }
        public Vector2 GetVelocity()
        {
            float tempx = Velocity.X;
            float tempy = Velocity.Y;
            return new Vector2(tempx, tempy);
        }
        #endregion


        #region Method

        public void Update()
        {
            if (!_islive) return;
            _center.X = _position.X + _width / 2;
            _center.Y = _position.Y + _height / 2;
            _rectangle.X = (int)_position.X;
            _rectangle.Y = (int)_position.Y;
        }
        #endregion

    }
}
