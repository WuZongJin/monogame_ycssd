using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using monogame_ycssd.General;
using monogame_ycssd.Manager;
using Microsoft.Xna.Framework;

namespace monogame_ycssd.Object.StageObject
{
    public class Stage1
    {
        #region Variables
        private int _width = 21;
        private int _height = 21;
        private Texture2D _wallTexture;
        private TileObject.tile1 _tile;
        private MyXMLData.StageData.Stage1Data stagedata;

        private List<Enemy> _enemyList = new List<Enemy>();
        private EnemyBoosObject.Boss1 boss;

        private int enemynum = 10;
        private int enemycount = 0;
        private bool enemyallappear = false;
        private double delay = 5.0f;
        private double enemyupdatetime;
        private double enemyupdatetimer;
        private bool addboss = false;
        #endregion

        #region Method  
        public Stage1()
        {
            _wallTexture = MyContentManager.GetInstance().LoadContent<Texture2D>("wall1");
            stagedata = new MyXMLData.StageData.Stage1Data();
            stagedata = MyContentManager.GetInstance().LoadContent<MyXMLData.StageData.Stage1Data>("stage1data");

            _tile = new TileObject.tile1();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (stagedata.tilemap[i * _width + j] == 3)
                    {
                        WallObject.wall1 wall = new WallObject.wall1(_wallTexture, new Vector2(i * 100, j * 100));
                        GameManager.GetInstance().AddWall(wall);
                    }
                }
            }

            enemyupdatetime = delay;
            enemyupdatetimer = 0.0f;

            boss = new EnemyBoosObject.Boss1(new Vector2(1500, 1500), 163, 154, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);

            _enemyList.Add(new EnemyObject.enemy1(new Vector2(100, 100), 115, 85, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemygGBL(new Vector2(500, 100), 100, 130, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemy1(new Vector2(800, 100), 115, 85, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemygGBL(new Vector2(100, 500), 100, 130, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemy1(new Vector2(100, 800), 115, 85, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemygGBL(new Vector2(500, 500), 100, 130, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemy1(new Vector2(700, 700), 115, 85, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemygGBL(new Vector2(900, 900), 100, 130, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemy1(new Vector2(1000, 1000), 115, 85, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));
            _enemyList.Add(new EnemyObject.enemygGBL(new Vector2(1200, 1100), 100, 130, new Vector2(5, 5), 0, new Vector2(0, 0), new Vector2(1, 1), Color.White, true));

            SoundManager.Getinstance().PlayBGM();
        }

        public void Update(GameTime gametime)
        {
            if (!enemyallappear)
            {
                enemyupdatetimer += gametime.ElapsedGameTime.TotalSeconds;
                if (enemyupdatetimer >= enemyupdatetime)
                {
                    enemyupdatetimer = 0;
                    GameManager.GetInstance().AddEnemy(_enemyList[enemycount]);
                    enemycount++;
                    if (enemycount >= enemynum)
                    {
                        enemyallappear = true;
                    }
                }
            }
            if (enemyallappear && GameManager.GetInstance().EnemyNum == 0 && !addboss)
            {
                addboss = true;
                GameManager.GetInstance().AddBoss(boss);
            
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            for(int i = 0; i < _width; i++)
            {
                for(int j = 0; j < _height; j++)
                {
                    if (stagedata.tilemap[i *_width+ j] != 3)
                    {
                        _tile.Draw(spriteBatch, new Vector2(i * 100, j * 100), stagedata.tilemap[i * _width + j]);
                    }
                }
            }
        }
        #endregion
    }
}
