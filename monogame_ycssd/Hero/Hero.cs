using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using monogame_ycssd.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using monogame_ycssd.Manager;
using monogame_ycssd;

namespace monogame_ycssd.Hero
{
    public enum HeroState
    {
        NORMAL,ISBLINK, ISRUN,
    }



    public class Hero:IFocusable
    {

        #region Variables
        public int health;
        private bool _isMoveRight;
        public HeroState state;

        private double _blinktime;


        private Texture2D _texture;
        private Animation _idleAnimation;
        private Animation _isBlinkAnimation;
        private Animation _currentAnimation;


        private Sprite _sprite;
        private Ray2D _upray1, _downray1, _rightray1, _leftray1;
        private int _offset = 60;
        private bool _canUp, _canDown, _canRight, _canLeft;


        private Weapon _weapon;
        private Vector2 _weaponPosition;
        private static MyXMLData.HeroData _data;
        #endregion

        #region Propreties
        public Sprite HeroSprite
        {
            get { return _sprite; }
        }
        public Weapon PlayerWeapon
        {
            get { return _weapon; }
        }

        public Vector2 Position
        {
            get
            {
                return _sprite.Position;
            }

            set
            {
                var tem = value;
                _sprite.Position = new Vector2(tem.X, tem.Y);
            }
        }

        #endregion

        #region Method
        public Hero(Vector2 position, int width, int height, Vector2 velocity, float rotation,Vector2 origin, Vector2 scale,Color color,bool islive)
        {
            _data = MyContentManager.GetInstance().LoadContent<MyXMLData.HeroData>("herodata");
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>(_data.spritePath);
            _sprite = new Sprite(position, width, height, velocity, rotation,origin,scale,color, islive);
            
            _weaponPosition = new Vector2(80, 100);

            health = 10;
            _isMoveRight = true;
            state = HeroState.NORMAL;
            _blinktime = 0.0f;

            _idleAnimation = new Animation();
            _idleAnimation.AddFrame(new Rectangle(0, 0, 120, 140), TimeSpan.FromSeconds(.25));
            _idleAnimation.AddFrame(new Rectangle(120, 0, 120, 140), TimeSpan.FromSeconds(.25));
            _idleAnimation.AddFrame(new Rectangle(240, 0, 120, 140), TimeSpan.FromSeconds(.25));
            _idleAnimation.AddFrame(new Rectangle(360, 0, 120, 140), TimeSpan.FromSeconds(.25));

            _isBlinkAnimation = new Animation();
            _isBlinkAnimation.AddFrame(new Rectangle(480, 0, 120, 140), TimeSpan.FromSeconds(.1));
            _isBlinkAnimation.AddFrame(new Rectangle(0, 0, 120, 140), TimeSpan.FromSeconds(.1));

            _upray1 = new Ray2D(new Vector2(position.X + _offset, position.Y), new Vector2(0, -0.01f));
            //_upray1 = new Ray2D(new Vector2(position.X + width - _offset, position.Y), new Vector2(0, -0.01f));
            _downray1 = new Ray2D(new Vector2(position.X + _offset, position.Y+height), new Vector2(0, +0.01f));
            //_downray1 = new Ray2D(new Vector2(position.X + width - _offset, position.Y + height), new Vector2(0, +0.01f));
            _rightray1 = new Ray2D(new Vector2(position.X + width, position.Y + _offset), new Vector2(0.01f, 0));
            //_rightray1 = new Ray2D(new Vector2(position.X + width, position.Y + height - _offset), new Vector2(0.01f, 0));
            _leftray1 = new Ray2D(new Vector2(position.X, position.Y + _offset), new Vector2(-0.01f, 0));
            //_leftray1 = new Ray2D(new Vector2(position.X, position.Y + height - _offset), new Vector2(-0.01f, 0));

            _canUp = true;
            _canDown = true;
            _canRight = true;
            _canLeft = true;

        }
       
        public void InitWeapon()
        {
            _weapon = new Object.WeaponObject.smallGun(new Vector2(200, 200), 70, 50, new Vector2(10, 10), 0.0f,new Vector2(25,40),new Vector2(1,1),Color.White, true);
            _weapon.Equip(this);
        }

        private void UpdateRay(Vector2 dir)
        {
            _upray1.Position += dir;
            _downray1.Position += dir;
            _rightray1.Position += dir;
            _leftray1.Position += dir;
        }

        public void GetHert(int damage)
        {
            if (state != HeroState.ISBLINK)
            {
                health--;
                state = HeroState.ISBLINK;
            }
        }

        private void UpdateKyeboardAndMouce(ref Vector2 movement)
        {
            Vector2 camera = Camera2D.GetInstance().Position - new Vector2(600, 450);
           
            if (_canUp)
            {
                if (KeyboardObject.KeyUpPressed())
                {
                    movement.Y -= _sprite.SpeedY;
                    _weapon.WeaponSprite.Y -= _sprite.SpeedY;


                }
            }
            if (_canDown)
            {
                if (KeyboardObject.KeyDownPressed())
                {
                    movement.Y += _sprite.SpeedY;
                    _weapon.WeaponSprite.Y += _sprite.SpeedY;

                }
            }
            if (_canRight)
            {
                if (KeyboardObject.KeyRightPressed())
                {
                    _isMoveRight = true;
                    movement.X += _sprite.SpeedX;
                    //_sprite.X += _sprite.SpeedX;
                    _weapon.WeaponSprite.X += _sprite.SpeedX;
                }
            }
            if (_canLeft)
            {
                if (KeyboardObject.KeyLeftPressed())
                {
                    _isMoveRight = false;
                    movement.X -= _sprite.SpeedX;
                    _weapon.WeaponSprite.X -= _sprite.SpeedX;
                }
            }
            if (MouseObject.LeftRelease)
            {
                _weapon.Attack(MouseObject.Position + camera);
            }
        }

        private void CheckCanMove()
        {
            bool isUpcollision = false;
            bool isDowncollision = false;
            bool isRightcollision = false;
            bool isLeftcollision = false;
            foreach (Wall wall in GameManager.GetInstance().WallList)
            {
                if (_upray1.Collision(wall.SourceRectangle) )
                    isUpcollision = true;
                if (_downray1.Collision(wall.SourceRectangle))
                    isDowncollision = true;
                if (_rightray1.Collision(wall.SourceRectangle))
                    isRightcollision = true;
                if (_leftray1.Collision(wall.SourceRectangle))
                    isLeftcollision = true;
            }
            if (isUpcollision) _canUp = false;
            else _canUp = true;
            if (isDowncollision) _canDown = false;
            else _canDown = true;
            if (isRightcollision) _canRight = false;
            else _canRight = true;
            if (isLeftcollision) _canLeft = false;
            else _canLeft = true;
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            switch (state)
            {
                case HeroState.NORMAL:
                    _currentAnimation = _idleAnimation;
                    break;
                case HeroState.ISBLINK:
                    _currentAnimation = _isBlinkAnimation;
                    break;
            }
            _currentAnimation.Update(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardObject.Update();
            MouseObject.Update();
            Vector2 movement = Vector2.Zero;

            if (state == HeroState.ISBLINK)
            {
                _blinktime += gameTime.ElapsedGameTime.TotalSeconds;
                if(_blinktime >= 1.0f)
                {
                    state = HeroState.NORMAL;
                    _blinktime = 0.0f;
                }
            }

            UpdateKyeboardAndMouce(ref movement);
            CheckCanMove();

            _sprite.Position += movement;
            UpdateRay(movement);

            UpdateAnimation(gameTime);

            _weapon.SetRotation(MouseObject.Position + (Camera2D.GetInstance().Position - new Vector2(600, 450)));
            _weapon.Updata();
            _sprite.Update();
            
        }

        public void Draw(SpriteBatch spritebatch)
        {
            
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth
            if(_isMoveRight)
                spritebatch.Draw(_texture, _sprite.Position,_currentAnimation.CurrentRectangle, Color.White,_sprite.Rotation,_sprite.Origin,_sprite.Scale,SpriteEffects.None,0);
            else
                spritebatch.Draw(_texture, _sprite.Position, _currentAnimation.CurrentRectangle, Color.White, _sprite.Rotation, _sprite.Origin, _sprite.Scale, SpriteEffects.FlipHorizontally, 0);
            //spritebatch.Draw(_texture, new Vector2(10, 10), null, Color.White, 0.0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 1);
            _weapon.Draw(spritebatch);
        }
        #endregion
    }
}
