using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.EnemyObject
{
    public enum EnemyPigState
    {
        XL,NOTHING,FINDENEMY,RUSH,
    }


    public class enemy1:Enemy
    {
        #region Variables
        private Texture2D _texture;
        private Animation _idleAnimation;
        private Animation _currentAnimation;


        private Ray2D _upray, _downray, _rightray, _leftray;
        private bool _canUp, _canDown, _canRight, _canLeft; 


        bool isTurnright;
        private float _speed;

        private EnemyPigState _state;
        private bool _isfindHero;

        private double changetime;
        private double changetimer;
        private double rushtime;
        private double rushtimer;
        private double stoptime;
        private double stoptimer;


        private Random rnd;
        private int rndnum;

        private bool changedir = false;
        private Vector2 dir = Vector2.Zero;

        #endregion

        #region Properties

        #endregion

        



        #region Method
        public enemy1(Vector2 position, int width, int height, Vector2 velocity, float rotation, Vector2 origin, Vector2 scale, Color color, bool islive)
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("pig1_230x85");
            _idleAnimation = new Animation();
            _idleAnimation.AddFrame(new Rectangle(0, 0, 115, 85), TimeSpan.FromSeconds(.25));
            _idleAnimation.AddFrame(new Rectangle(115, 0, 115, 85), TimeSpan.FromSeconds(.25));
            isTurnright = true;

            _upray = new Ray2D(new Vector2(position.X + width / 2, position.Y), new Vector2(0, -0.01f));
            _downray = new Ray2D(new Vector2(position.X + width / 2, position.Y + height), new Vector2(0, 0.01f));
            _rightray = new Ray2D(new Vector2(position.X + width, position.Y + height / 2), new Vector2(0.01f, 0));
            _leftray = new Ray2D(new Vector2(position.X, position.Y + height / 2), new Vector2(-0.01f, 0));

            _canUp = true;
            _canDown = true;
            _canRight = true;
            _canLeft = true;

            _speed = 2;
            Health = 10;
            _isfindHero = false;
            _state = EnemyPigState.XL;

            changetime = 2.0f;
            changetimer = 0.0f;
            rushtime = 0.7f;
            rushtimer = 0.0f;
            stoptime = 2.0f;
            stoptimer = 0.0f;

            rnd = new Random();
            rndnum = 0;

            EnemySprite = new Sprite(position, width, height, velocity, rotation, origin, scale, color, islive);

        }

        private void CheckCanMove()
        {
            bool isUpcollision = false;
            bool isDowncollision = false;
            bool isRightcollision = false;
            bool isLeftcollision = false;
            foreach (Wall wall in GameManager.GetInstance().WallList)
            {
                if (_upray.Collision(wall.SourceRectangle))
                    isUpcollision = true;
                if (_downray.Collision(wall.SourceRectangle))
                    isDowncollision = true;
                if (_rightray.Collision(wall.SourceRectangle))
                    isRightcollision = true;
                if (_leftray.Collision(wall.SourceRectangle))
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

        public override void Boom()
        {
            SoundManager.Getinstance().PlaySoundEffect("pigDeath");
            BoomEffectObject.EnemyBoom1 boom = new BoomEffectObject.EnemyBoom1(EnemySprite.Position);
            GameManager.GetInstance().AddBoomEffect(boom);
        }

        public override void Update(GameTime gameTime)
        {
            CheckCanMove();

            if (_state == EnemyPigState.XL) {
                changetimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (changetimer >= changetime)
                {
                    changetimer = 0.0f;
                    if (!_isfindHero)
                    {
                        rndnum = rnd.Next(0, 4);

                    }
                }
                if ((GameManager.GetInstance().Player.Position - EnemySprite.Position).Length() <= 400)
                {
                    _isfindHero = true;
                    _state = EnemyPigState.FINDENEMY;
                }
            }
            else
            {
                if (_state == EnemyPigState.FINDENEMY)
                {
                    stoptimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (stoptimer >= stoptime)
                    {
                        stoptimer = 0.0f;
                        changedir = true;
                        _state = EnemyPigState.RUSH;
                    }
                }
                else if (_state == EnemyPigState.RUSH)
                {
                    rushtimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (rushtimer >= rushtime)
                    {
                        rushtimer = 0;
                        _state = EnemyPigState.FINDENEMY;
                    }
                }
            }
            
            switch (_state)
            {
                case EnemyPigState.XL:
                    MoveXL();
                    break;
                case EnemyPigState.FINDENEMY:
                    Move();
                    break;
                case EnemyPigState.RUSH:
                    //SoundManager.Getinstance().PlaySoundEffect("pigAttack");
                    MoveRush();
                    break;
            }
            


            if (Health <= 0) EnemySprite.IsLive = false;
            _currentAnimation = _idleAnimation;
            _currentAnimation.Update(gameTime);
            EnemySprite.Update();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!EnemySprite.IsLive) return;
            if(isTurnright)
                spriteBatch.Draw(_texture, EnemySprite.Position, _currentAnimation.CurrentRectangle, Color.White, EnemySprite.Rotation,EnemySprite.Origin, EnemySprite.Scale, SpriteEffects.None, 0.0f);
            else
            {
                spriteBatch.Draw(_texture, EnemySprite.Position, _currentAnimation.CurrentRectangle, Color.White, EnemySprite.Rotation, EnemySprite.Origin, EnemySprite.Scale, SpriteEffects.FlipHorizontally, 0.0f);
            }
            //spriteBatch.Draw(_texture, EnemySprite.Rectangle, Color.White);
        }
        #endregion
        #region MoveMethod

        private void UpdateRay(Vector2 dir)
        {
            _upray.Position += dir;
            _downray.Position += dir;
            _rightray.Position += dir;
            _leftray.Position += dir;
        }

        public override void Move()
        {
            Hero.Hero hero = GameManager.GetInstance().Player;
            var temp = FindHero(hero);

            Vector2 heroposition = new Vector2(temp.X, temp.Y);
            Vector2 direction = heroposition - EnemySprite.Position;
            if (direction.X > 0) { isTurnright = true; }
            else { isTurnright = false; }

            direction.Normalize();
            if (!_canUp)
            {
                if (direction.Y < 0) direction.Y = 0;
            }
            if (!_canDown)
            {
                if (direction.Y > 0) direction.Y = 0;
            }
            if (!_canLeft)
            {
                if (direction.X < 0) direction.X = 0;
            }
            if (!_canRight)
            {
                if (direction.X > 0) direction.X = 0;
            }

            EnemySprite.X += _speed * direction.X;
            EnemySprite.Y += _speed * direction.Y;

            UpdateRay(direction * _speed);
        }

        private void Move(Vector2 dir)
        {
            EnemySprite.X += dir.X * _speed;
            EnemySprite.Y += dir.Y * _speed;
            UpdateRay(dir * _speed);
        }

        private void MoveXL()
        {
            switch (rndnum)
            {
                case 0:
                    if (_canRight)
                    {
                        EnemySprite.X += _speed;
                        UpdateRay(new Vector2(_speed, 0));
                    }
                    break;
                case 1:
                    if (_canLeft)
                    {
                        EnemySprite.X -= _speed;
                        UpdateRay(new Vector2(-_speed, 0));
                    }
                    break;
                case 2:
                    if (_canDown)
                    {
                        EnemySprite.Y += _speed;
                        UpdateRay(new Vector2(0, _speed));
                    }
                    break;
                case 3:
                    if (_canUp)
                    {
                        EnemySprite.Y -= _speed;
                        UpdateRay(new Vector2(0, -_speed));
                    }
                    break;
                default:
                    break;
            }

        }

        private void MoveRush()
        {
            if (changedir)
            {
                Hero.Hero hero = GameManager.GetInstance().Player;
                var temp = FindHero(hero);
                Vector2 heroposition = new Vector2(temp.X, temp.Y);
                dir = heroposition - EnemySprite.Position;
                dir.Normalize();
                if (dir.X > 0) { isTurnright = true; }
                else { isTurnright = false; }

                changedir = false;
            }

            if (!_canUp)
            {
                if (dir.Y < 0) dir.Y = 0;
            }
            if (!_canDown)
            {
                if (dir.Y > 0) dir.Y = 0;
            }
            if (!_canLeft)
            {
                if (dir.X < 0) dir.X = 0;
            }
            if (!_canRight)
            {
                if (dir.X > 0) dir.X = 0;
            }



            EnemySprite.X += _speed * 7 * dir.X;
            EnemySprite.Y += _speed * 7 * dir.Y;
            UpdateRay(dir * 7 * _speed);
        }
        #endregion
    }
}

