using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monogame_ycssd.General;
using monogame_ycssd.Hero;
using monogame_ycssd.Input;
using monogame_ycssd.Manager;
using monogame_ycssd.Object;

namespace monogame_ycssd
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        

         Hero.Hero hero;
        MyXMLData.xmlData leveldata;

        Object.StageObject.Stage1 stage;


        Enemy enemy;
        Enemy enemy1;

        MyContentManager MyContent;
        SpriteFont defaultFont;

        Camera2D camera;
        Component.HeroUIDrawComponent uiComponent;
        Component.BossUIComponent bossuiComponent;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WindowsManager.GetInstance().ScreenWidth;
            graphics.PreferredBackBufferHeight = WindowsManager.GetInstance().ScreenHeight;

            Content.RootDirectory = "Content";
            MyContent = new MyContentManager(Content);
            MyContent.Instance = MyContent;

           


            KeyboardObject.Init();
            GameManager.GetInstance();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SoundManager.Getinstance().Initialize();

            hero = new Hero.Hero(new Vector2(WindowsManager.GetInstance().ScreenWidth / 2, WindowsManager.GetInstance().ScreenHeight / 2), 120, 140, new Vector2(5, 5), 0.0f, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);
            hero.InitWeapon();


            camera = new Camera2D(this,hero);
            Components.Add(camera);

            uiComponent = new Component.HeroUIDrawComponent(this, spriteBatch);
            Components.Add(uiComponent);
            bossuiComponent = new Component.BossUIComponent(this, spriteBatch);
            Components.Add(bossuiComponent);
            //debugmanager = new DebugManager(this, spriteBatch);
            //Components.Add(debugmanager);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            MouseObject.Load(Content);
            
            defaultFont = Content.Load<SpriteFont>("DefaultFomt");
            MyContentManager.instance.defaultFont = defaultFont;
            
            enemy = new Object.EnemyObject.enemy1(new Vector2(200, 200), 115, 85, new Vector2(5, 5), 0.0f, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);
            enemy1 = new Object.EnemyObject.enemygGBL(new Vector2(400, 200), 115, 85, new Vector2(5, 5), 0.0f, new Vector2(0, 0), new Vector2(1, 1), Color.White, true);

            stage = new Object.StageObject.Stage1();

            Object.BoomEffectObject.boomEffect1 boom = new Object.BoomEffectObject.boomEffect1(new Vector2(100, 100));
            GameManager.GetInstance().AddBoomEffect(boom);

            GameManager.GetInstance().AddPlayer(hero);
            GameManager.GetInstance().AddEnemy(enemy);
            GameManager.GetInstance().AddEnemy(enemy1);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            stage.Update(gameTime);
            GameManager.GetInstance().Updata(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            string heroposition = hero.PlayerWeapon.WeaponSprite.Position.ToString();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                            null,null,null,null,null,
                            camera.Transform);
            stage.Draw(spriteBatch);
            GameManager.GetInstance().Draw(spriteBatch);

            spriteBatch.DrawString(defaultFont, hero.health.ToString(), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(defaultFont, GameManager.GetInstance().WallList[0].SourceRectangle.ToString(), new Vector2(10, 50), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
