using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_ycssd.General;
using monogame_ycssd.Manager;

namespace monogame_ycssd.Object.EnemyBoosObject
{
    public enum BossState
    {
        ATTACK,MOVE,RUSH,SUPERATTACK,
    }

    public class Boss1 : EnemyBoss
    {
        #region Variables
        private BossState _state;
        private Texture2D _texture;
        private Animation _idleAniamtion;
        private Animation _currentAnimation;
        private int _bulletspeed;
        private int _speed;
        private bool isTurnright;

        private Ray2D _upray, _downray, _rightray, _leftray;
        private bool _canUp, _canDown, _canRight, _canLeft;

        private Random rnd = new Random();


        private bool changedir = false;
        private Vector2 dir = Vector2.Zero;


        private double movetime;
        private double movetimer;
        private double rushtime;
        private double rushtimer;
        private double attacktime;
        private double attacktimer;
        private double supattacktime;
        private double supattacktimer;
        private double firetimer;
        private double delay;
        #endregion

        

        #region Method
        public Boss1(Vector2 position, int width, int height, Vector2 velocity, float rotation, Vector2 origin, Vector2 scale, Color color, bool islive)
        {
            _texture = MyContentManager.GetInstance().LoadContent<Texture2D>("enemyBoss");
            _idleAniamtion = new Animation();
            _idleAniamtion.AddFrame(new Rectangle(0, 0, 163, 154), TimeSpan.FromSeconds(.25));
            _currentAnimation = _idleAniamtion;

            isTurnright = true;
            _bulletspeed = 10;
            _speed = 2;
            Health = 100;
            EnemySprite = new Sprite(position, width, height, velocity, rotation, origin, scale, color, islive);

            _upray = new Ray2D(new Vector2(position.X + width / 2, position.Y), new Vector2(0, -0.01f));
            _downray = new Ray2D(new Vector2(position.X + width / 2, position.Y + height), new Vector2(0, 0.01f));
            _rightray = new Ray2D(new Vector2(position.X + width, position.Y + height / 2), new Vector2(0.01f, 0));
            _leftray = new Ray2D(new Vector2(position.X, position.Y + height / 2), new Vector2(-0.01f, 0));

            _canUp = true;
            _canDown = true;
            _canRight = true;
            _canLeft = true;

            movetime = 3.0f;
            rushtime = 1.5f;
            attacktime = 2.0f;
            supattacktime = 2.0f;
            delay = 0.2f;

            movetimer = 0.0f;
            rushtimer = 0.0f;
            attacktimer = 0.0f;
            supattacktimer = 0.0f;
            firetimer = 0.0f;



            _state = BossState.MOVE;

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

        private void UpdateRay(Vector2 dir)
        {
            _upray.Position += dir;
            _downray.Position += dir;
            _rightray.Position += dir;
            _leftray.Position += dir;
        }
        #endregion


        #region MoveMethod
        private void Attack(GameTime gametime)
        {
            firetimer += gametime.ElapsedGameTime.TotalSeconds;
            if (firetimer >= delay)
            {
                firetimer = 0.0f;
                var temp = GameManager.GetInstance().Player.Position;
                Vector2 heroposition = new Vector2(temp.X, temp.Y);
                Vector2 direction = heroposition - EnemySprite.Position;
                direction.Normalize();
                float rotation = (float)(Math.Atan2((direction.Y), (direction.X)));
                BulletObject.bossbullet1 bullet1 = new BulletObject.bossbullet1(EnemySprite.Position, 34, 27,
                                                            new Vector2(direction.X * _bulletspeed, direction.Y * _bulletspeed),
                                                            rotation, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);

                GameManager.GetInstance().AddEnemyBullet(bullet1);
            }
                                                                
        }
        private void SuperAttack(GameTime gametime)
        {
            firetimer += gametime.ElapsedGameTime.TotalSeconds;
            if (firetimer >= delay)
            {
                firetimer = 0.0f;
                var temp = GameManager.GetInstance().Player.Position;
                Vector2 heroposition = new Vector2(temp.X, temp.Y);
                Vector2 direction = heroposition - EnemySprite.Position;
                direction.Normalize();
                float rotation = (float)(Math.Atan2((direction.Y), (direction.X)));
                BulletObject.bossbullet1 bullet1 = new BulletObject.bossbullet1(EnemySprite.Position, 34, 27,
                                                            new Vector2(direction.X * _bulletspeed, direction.Y * _bulletspeed),
                                                            rotation, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);

                GameManager.GetInstance().AddEnemyBullet(bullet1);
            }
        }
        private void Rush()
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



            EnemySprite.X += _speed * 5 * dir.X;
            EnemySprite.Y += _speed * 5 * dir.Y;
            UpdateRay(dir * 5 * _speed);
        }

        #endregion


        #region overrideMethod
        public override void Appear()
        {
            
        }

        public override void Boom()
        {
            BoomEffectObject.EnemyBoom1 boom = new BoomEffectObject.EnemyBoom1(EnemySprite.Position);
            GameManager.GetInstance().AddBoomEffect(boom);
        }

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

        public override void Update(GameTime gametime)
        {
            CheckCanMove();
            if(_state == BossState.MOVE)
            {
                movetimer += gametime.ElapsedGameTime.TotalSeconds;
                if (movetimer >= movetime)
                {
                    movetimer = 0.0f;
                    int rndnum = rnd.Next(0, 3);
                    switch (rndnum)
                    {
                        case 0:
                            _state = BossState.ATTACK;
                            break;
                        case 1:
                            changedir = true;
                            _state = BossState.RUSH;
                            break;
                        case 2:
                            _state = BossState.SUPERATTACK;
                            break;
                    }
                }
            }
            if(_state== BossState.ATTACK)
            {
                attacktimer += gametime.ElapsedGameTime.TotalSeconds;
                if (attacktimer >= attacktime)
                {
                    attacktimer = 0.0f;
                    _state = BossState.MOVE;
                }
            }
            else if(_state == BossState.RUSH)
            {
                rushtimer += gametime.ElapsedGameTime.TotalSeconds;
                if (rushtimer >= rushtime)
                {
                    rushtimer = 0.0f;
                    _state = BossState.MOVE;
                }
            }
            else if(_state == BossState.SUPERATTACK)
            {
                supattacktimer += gametime.ElapsedGameTime.TotalSeconds;
                if(supattacktimer >= supattacktime)
                {
                    supattacktimer = 0.0f;
                    _state = BossState.MOVE;
                }
            }

            switch (_state)
            {
                case BossState.MOVE:
                    Move();
                    break;
                case BossState.ATTACK:
                    Attack(gametime);
                    break;
                case BossState.RUSH:
                    Rush();
                    break;
                case BossState.SUPERATTACK:
                    SuperAttack(gametime);
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
