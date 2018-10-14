using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.General;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.EnemyObject
{
    public class enemygGBL : Enemy
    {

        public enum GBLSate
        {
            XL,FINDENEMY,ATTACK,SUPATTACK,
        }

        #region Variables
        private GBLSate _state;
        private Texture2D _texture;
        private Animation _idleAniamtion;
        private Animation _currentAnimation;
        private bool isTurnright;
        private int _bulletspeed;
        private int _speed;


        private Ray2D _upray, _downray, _rightray, _leftray;
        private bool _canUp, _canDown, _canRight, _canLeft;


        private Random rnd;
        private int rndnum;
        private bool _isfindHero;


        private double changetime;
        private double changetimer;
        private double stoptime;
        private double stoptimer;
        private double attacktime;
        private double attacktimer;
        private double firetime;
        private double firetimer;
        #endregion



        #region Method
        public enemygGBL(Vector2 position, int width, int height, Vector2 velocity, float rotation, Vector2 origin, Vector2 scale, Color color, bool islive)
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("enemyGBL");
            _idleAniamtion = new Animation();
            _idleAniamtion.AddFrame(new Rectangle(0, 0, 100, 130), TimeSpan.FromSeconds(.25));
            _idleAniamtion.AddFrame(new Rectangle(100, 0, 100, 130), TimeSpan.FromSeconds(.25));
            _currentAnimation = _idleAniamtion;

            isTurnright = true;
            _bulletspeed = 10;
            _speed = 2;
            Health = 10;
            EnemySprite = new Sprite(position, width, height, velocity, rotation, origin, scale, color, islive);


            _upray = new Ray2D(new Vector2(position.X + width / 2, position.Y), new Vector2(0, -0.01f));
            _downray = new Ray2D(new Vector2(position.X + width / 2, position.Y + height), new Vector2(0, 0.01f));
            _rightray = new Ray2D(new Vector2(position.X + width, position.Y + height / 2), new Vector2(0.01f, 0));
            _leftray = new Ray2D(new Vector2(position.X, position.Y + height / 2), new Vector2(-0.01f, 0));

            _canUp = true;
            _canDown = true;
            _canRight = true;
            _canLeft = true;

            rnd = new Random();
            _state = GBLSate.XL;
            rndnum = rnd.Next(0, 4);

            changetime = 2.0f;
            changetimer = 0.0f;
            stoptime = 2.0f;
            stoptimer = 0.0f;
            attacktime = 2.0f;
            attacktimer = 0.0f;
            firetime = 0.5f;
            firetimer = 0.0f;
            
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

        private void Attack(GameTime gametime)
        {
            firetimer += gametime.ElapsedGameTime.TotalSeconds;
            if (firetimer >= firetime)
            {
                firetimer = 0.0f;
                Hero.Hero hero = GameManager.GetInstance().Player;
                var temp = FindHero(hero);

                Vector2 heroposition = new Vector2(temp.X, temp.Y);
                Vector2 direction = heroposition - EnemySprite.Position;
                direction.Normalize();
                EnemyBulletObject.EnemyBullet1 bullet = new EnemyBulletObject.EnemyBullet1(EnemySprite.Position, 10, 10, new Vector2(direction.X * _bulletspeed, direction.Y * _bulletspeed), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);
                GameManager.GetInstance().AddEnemyBullet(bullet);
            }
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
        #endregion

        

        #region Override
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!EnemySprite.IsLive) return;
            if (isTurnright)
                spriteBatch.Draw(_texture, EnemySprite.Position, _currentAnimation.CurrentRectangle, Color.White, EnemySprite.Rotation, EnemySprite.Origin, EnemySprite.Scale, SpriteEffects.None, 0.0f);
            else
            {
                spriteBatch.Draw(_texture, EnemySprite.Position, _currentAnimation.CurrentRectangle, Color.White, EnemySprite.Rotation, EnemySprite.Origin, EnemySprite.Scale, SpriteEffects.FlipHorizontally, 0.0f);
            }
        }

        public override void Boom()
        {
            BoomEffectObject.EnemyBoom1 boom = new BoomEffectObject.EnemyBoom1(EnemySprite.Position);
            GameManager.GetInstance().AddBoomEffect(boom);
        }

        public override void Update(GameTime gametime)
        {
            CheckCanMove();
            if (_state == GBLSate.XL)
            {
                changetimer += gametime.ElapsedGameTime.TotalSeconds;
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
                    _state =  GBLSate.FINDENEMY;
                }
            }
            else
            {
                if (_state == GBLSate.FINDENEMY)
                {
                    stoptimer += gametime.ElapsedGameTime.TotalSeconds;
                    if (stoptimer >= stoptime)
                    {
                        _state = GBLSate.ATTACK;
                    }
                }
                else if(_state == GBLSate.ATTACK)
                {
                    attacktimer += gametime.ElapsedGameTime.TotalSeconds;
                    if (attacktimer >= attacktime)
                    {
                        _state = GBLSate.FINDENEMY;
                    }
                }
            }


            


            switch (_state)
            {
                case GBLSate.XL:
                    MoveXL();
                    break;
                case GBLSate.FINDENEMY:
                    Move();
                    break;
                case GBLSate.ATTACK:
                    Attack(gametime);
                    break;
                
            }

            if (Health <= 0) EnemySprite.IsLive = false;

            _currentAnimation = _idleAniamtion;
            _currentAnimation.Update(gametime);
            EnemySprite.Update();
        }
        #endregion
    }
}
