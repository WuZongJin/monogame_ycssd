using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.Manager
{
    public class GameManager
    {
        public GameManager()
        {

        }


        #region Variable
        public static GameManager instance;


        protected Hero.Hero _player;
        protected List<Bullet> _playerBulletList = new List<Bullet>();
        protected List<Enemy> _enemyList = new List<Enemy>();
        protected List<BoomEffect> _boomEffectList = new List<BoomEffect>();
        protected List<Wall> _wallList = new List<Wall>();
        protected List<Bullet> _enemyBulletList = new List<Bullet>();
        protected List<EnemyBoss> _bossList = new List<EnemyBoss>();
        #endregion

        #region Properties
        public Hero.Hero Player
        {
            get { return _player; }
        }
        public int EnemyNum
        {
            get
            {
                return _enemyList.Count;
            }
        }
        public int PlayBulletNum
        {
            get
            {
                return _playerBulletList.Count;
            }
        }
        public List<EnemyBoss> BossList
        {
            get { return _bossList; }
        }
        public List<Wall> WallList
        {
            get { return _wallList; }
        }

        #endregion

        #region AddMethod
        public void AddPlayerBullet(Bullet bullet)
        {
            _playerBulletList.Add(bullet);
        }
        public void AddPlayer(Hero.Hero player)
        {
            _player = player;
        }
        public void AddEnemy(Enemy enemy)
        {
            _enemyList.Add(enemy);
        }
        public void AddBoomEffect(BoomEffect boomeffect)
        {
            _boomEffectList.Add(boomeffect);
        }
        public void AddWall(Wall wall)
        {
            _wallList.Add(wall);
        }
        public void AddEnemyBullet(Bullet bullet)
        {
            _enemyBulletList.Add(bullet);
        }
        public void AddBoss(EnemyBoss boss)
        {
            _bossList.Add(boss);
        }
        #endregion

        #region UpdateMethod
        public void UpdateEnemyBullet()
        {
            for (int i = 0; i < _enemyBulletList.Count; i++)
            {
                if (IsCollision(Player.HeroSprite.Rectangle, _enemyBulletList[i].BulletSprite.Rectangle))
                {
                    Player.GetHert(1);
                    _enemyBulletList[i].Bomb();
                }

                foreach (var wall in _wallList)
                {
                    if (IsCollision(_enemyBulletList[i].BulletSprite.Rectangle, wall.SourceRectangle))
                    {
                        _enemyBulletList[i].Bomb();
                    }
                }
                if (!_enemyBulletList[i].BulletSprite.IsLive)
                {
                    _enemyBulletList.Remove(_enemyBulletList[i]);
                }
                else
                {
                    _enemyBulletList[i].Updata();
                }
            }
        }
        private void UpdateBullet()
        {
            for(int i = 0; i < _playerBulletList.Count; i++)
            {
                foreach(var wall in _wallList)
                {
                    if (wall.SourceRectangle.Intersects(_playerBulletList[i].BulletSprite.Rectangle))
                    {
                        _playerBulletList[i].Bomb();
                    }
                }
                foreach(var enemy in _enemyList)
                {
                    if (enemy.EnemySprite.Rectangle.Intersects(_playerBulletList[i].BulletSprite.Rectangle))
                    {
                        _playerBulletList[i].Bomb();
                        enemy.Health--;
                    }
                }
                foreach(var boss in _bossList)
                {
                    if (boss.EnemySprite.Rectangle.Intersects(_playerBulletList[i].BulletSprite.Rectangle))
                    {
                        _playerBulletList[i].Bomb();
                        boss.Health--;
                    }
                }
                if (!_playerBulletList[i].BulletSprite.IsLive)
                {
                    _playerBulletList.Remove(_playerBulletList[i]);
                }
                else
                {
                    _playerBulletList[i].Updata();
                }
            }
        }
        private void UpdateBoss(GameTime gametime)
        {
            for (int i = 0; i < _bossList.Count; i++)
            {
                if (IsCollision(_player.HeroSprite.Rectangle, _bossList[i].EnemySprite.Rectangle))
                {
                    Player.GetHert(1);
                }
                if (!_bossList[i].EnemySprite.IsLive)
                {
                    _bossList[i].Boom();
                    _bossList.Remove(_bossList[i]);

                }
                else
                {
                    _bossList[i].Update(gametime);
                }
            }
        }
        private void UpdateEnemy(GameTime gametime)
        {
            for (int i = 0; i < _enemyList.Count; i++)
            {
                if (IsCollision(_player.HeroSprite.Rectangle, _enemyList[i].EnemySprite.Rectangle))
                {
                    Player.GetHert(1);
                }
                if (!_enemyList[i].EnemySprite.IsLive)
                {
                    _enemyList[i].Boom();
                    _enemyList.Remove(_enemyList[i]);

                }
                else
                {
                    _enemyList[i].Update(gametime);
                }
            }
        }
        
        private void UpdateBoomEffect(GameTime gametime)
        {
            for (int i = 0; i < _boomEffectList.Count; i++)
            {
                if (!_boomEffectList[i].IsFinshed)
                {
                    _boomEffectList[i].Updata(gametime);
                }
                else
                {
                    _boomEffectList.Remove(_boomEffectList[i]);

                }

            }
        }

        #endregion


        #region Method
        public static GameManager GetInstance()
        {
            if(instance != null)
            {
                return instance;
            }
            return instance = new GameManager();
        }

        public bool IsCollision(Rectangle r1,Rectangle r2)
        {
            return r1.Intersects(r2);
        }

        public void Updata(GameTime gametime)
        {
            _player.Update(gametime);
            UpdateEnemyBullet();
            UpdateBullet();
            UpdateEnemy(gametime);
            UpdateBoss(gametime);
            UpdateBoomEffect(gametime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var wall in _wallList)
            {
                wall.Draw(spriteBatch);
            }
            _player.Draw(spriteBatch);
            foreach(var bullet in _playerBulletList)
            {
                bullet.Draw(spriteBatch);
            }
            foreach (var enemy in _enemyList)
            {
                enemy.Draw(spriteBatch);
            }
            foreach(var boss in _bossList)
            {
                boss.Draw(spriteBatch);
            }
            foreach (var boomeffect in _boomEffectList)
            {
                boomeffect.Draw(spriteBatch);
            }
            foreach(var bullet in _enemyBulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
