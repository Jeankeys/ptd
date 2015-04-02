using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.CodeDom.Compiler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tower_Defense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        //Struct to hold all of the towers important information
        public struct TowerInfo
        {
            public Vector2 location;
            public int index;
            public string type;
        }

        //Game States
        enum GameState { Intro, StartScreen, LevelSelect, InGame, Pause, GameOver }
        static GameState currentGameState = GameState.StartScreen;

        private GraphicsDeviceManager graphics;
        //Create a new static ContentManager so it can be passed in static functions
        new private static ContentManager Content;
        private static SpriteBatch spriteBatch;
        private static SpriteFont font;
        public static Level level;
        private static LevelSelect levelSelectScreen;
        private static WaveManager waveManager;
        private static Player player;
        private static ToolbarHorizontal toolbarHorizontal; 
        private static ToolbarVertical toolbarVertical; 
        private static TowerToolbar toolbar;
        private static PauseMenu pauseMenu;

        //Start screen that shows when the game is run
        Texture2D StartScreenOne; Texture2D StartScreenTwo;
        private float screenTimer;

        //Booleans to tell when the game is paused or when the sound is muted
        public static bool pause = false;
        public static bool mute = false;

        //Background image that changes depending on which level is selected
        public static Texture2D map;

        //Textures for the toolbars
        static Texture2D bottomBar; static Texture2D sideBar;
        static Texture2D toolbarTexture; static Texture2D pauseTexture;

        //Arrays to hold all the textures for the towers, bullets, and enemies
        static Texture2D[] enemyTextures = new Texture2D[5]; 
        static Texture2D[] bulletTextures = new Texture2D[5];
        static Texture2D[] musicNoteTextures = new Texture2D[4];
        static Texture2D[] towerTextures = new Texture2D[8];
        static Texture2D[] badTowerTextures = new Texture2D[8];

        //Sounds for each towers attack, and poping balloons
        public static SoundEffect levelup;
        public static SoundEffect fireball;
        public static SoundEffect vineWhip;
        public static SoundEffect waterGun;
        public static SoundEffect thunderShock;
        public static SoundEffect pop;
        
        MouseState mouseState; 

        //Texture2D introTexture;
        Point frameSize = new Point(156, 144);
        Point currentFrame = new Point(0,0);
        Point sheetSize = new Point(10, 10);

        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 100;

        //Information for the TowerInfo struct
        public static bool show = false;
        public static int index;
        public static string type;

        //Determines which level has been selected
        public static string levelSelect;


        //for background sound
        public static float songTime;
        public static float songLength;
        public static SoundEffectInstance battleMusicInstance;
        public static SoundEffect battleMusic;
        //public static SoundEffect battleMusic2;
        //public static SoundEffectInstance battleMusicInstance2;
        //public static bool start;

        public Main()
        {
            Content = new ContentManager(Services); 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Size of the window, taking into account the toolbars on the ride side and the bottom of the screen
            graphics.PreferredBackBufferWidth = 930;
            graphics.PreferredBackBufferHeight = 672;
            graphics.ApplyChanges();
            IsMouseVisible = true;

        }

        //Load all the textures and sounds used in Main, Player, and Wave classes
        //Load texture arrays of the towers, bullets and enemies
        protected override void Initialize()
        {
            //Start screens
            StartScreenOne = Content.Load<Texture2D>(@"Images\Levels\StartScreenStart");
            StartScreenTwo = Content.Load<Texture2D>(@"Images\Levels\StartScreen");

            font = Content.Load<SpriteFont>(@"Font\Fonts");

            //Audio for each towr, and background music
            levelup = Content.Load<SoundEffect>(@"Audio\LevelUp");
            fireball = Content.Load<SoundEffect>(@"Audio\fireBall");
            vineWhip = Content.Load<SoundEffect>(@"Audio\\vineWhip");
            waterGun = Content.Load<SoundEffect>(@"Audio\waterGun");
            thunderShock = Content.Load<SoundEffect>(@"Audio\thunderShock");
            pop = Content.Load<SoundEffect>(@"Audio\pop");
            battleMusic = Content.Load<SoundEffect>(@"Audio\BattleMusic");
            //battleMusic2 = Content.Load<SoundEffect>(@"Audio\BattleMusic2");

            //Textures for all of the toolbars
            bottomBar = Content.Load<Texture2D>(@"Images\GUI\Toolbars\tool bar_horizontal");
            sideBar = Content.Load<Texture2D>(@"Images\GUI\Toolbars\tool bar_vertical");
            toolbarTexture = Content.Load<Texture2D>(@"Images\GUI\Toolbars\towerToolbar");
            pauseTexture = Content.Load<Texture2D>(@"Images\GUI\PauseMenu\pauseMenuTexture");

            //Textures for each enemy that will spawn
            enemyTextures[0] = Content.Load<Texture2D>(@"Images\Enemies\sized_trans_pokeballs\pokeball");
            enemyTextures[1] = Content.Load<Texture2D>(@"Images\Enemies\sized_trans_pokeballs\timerball");
            enemyTextures[2] = Content.Load<Texture2D>(@"Images\Enemies\sized_trans_pokeballs\greatball");
            enemyTextures[3] = Content.Load<Texture2D>(@"Images\Enemies\sized_trans_pokeballs\ultraball");
            enemyTextures[4] = Content.Load<Texture2D>(@"Images\Enemies\sized_trans_pokeballs\masterball");

            //Textures for each bullet the towers can shoot
            bulletTextures[0] = Content.Load<Texture2D>(@"Images\Bullets\blueBullet_clipped_rev_1");
            bulletTextures[1] = Content.Load<Texture2D>(@"Images\Bullets\redBullet_clipped_rev_1");
            bulletTextures[2] = Content.Load<Texture2D>(@"Images\Bullets\greenBullet_clipped_rev_1");
            bulletTextures[3] = Content.Load<Texture2D>(@"Images\Bullets\yellowBullet_clipped_rev_1");
            bulletTextures[4] = Content.Load<Texture2D>(@"Images\Bullets\purpleBullet_clipped_rev_1");

            //Textures for each tower that will appear when towers are placed
            towerTextures[0] = Content.Load<Texture2D>(@"Images\Towers\charmanderTower");
            towerTextures[1] = Content.Load<Texture2D>(@"Images\Towers\squirtleTower");
            towerTextures[2] = Content.Load<Texture2D>(@"Images\Towers\bulbasaurTower");
            towerTextures[3] = Content.Load<Texture2D>(@"Images\Towers\pikachuTower");
            towerTextures[4] = Content.Load<Texture2D>(@"Images\Towers\jigglypuffTower");
            towerTextures[5] = Content.Load<Texture2D>(@"Images\Towers\dratiniTower");
            towerTextures[6] = Content.Load<Texture2D>(@"Images\Towers\tangelaTower");
            towerTextures[7] = Content.Load<Texture2D>(@"Images\Towers\mewTower");

            //Textures for each tower that will appear if the player tries to place a tower on the path
            badTowerTextures[0] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badCharmanderTower");
            badTowerTextures[1] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badSquirtleTower");
            badTowerTextures[2] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badBulbasaurTower");
            badTowerTextures[3] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badPikachuTower");
            badTowerTextures[4] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badJigglypuffTower");
            badTowerTextures[5] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badDratiniTower");
            badTowerTextures[6] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badTangelaTower");
            badTowerTextures[7] = Content.Load<Texture2D>(@"Images\Towers\BadTowers\badMewTower");

            //Textures of music notes, which are bullets specifically for Jigglypuff and Wigglytuff
            musicNoteTextures[0] = Content.Load<Texture2D>(@"Images\Bullets\musicNotes_left\note1");
            musicNoteTextures[1] = Content.Load<Texture2D>(@"Images\Bullets\musicNotes_left\note2");
            musicNoteTextures[2] = Content.Load<Texture2D>(@"Images\Bullets\musicNotes_left\note3");
            musicNoteTextures[3] = Content.Load<Texture2D>(@"Images\Bullets\musicNotes_left\note4");
            
            //introTexture = Content.Load<Texture2D>(@"Images\GUI\introSheet");

            //initialize background sound variables
            songTime = 0f;
            songLength = 249.0f;
            battleMusicInstance = battleMusic.CreateInstance();
            //battleMusicInstance2 = battleMusic2.CreateInstance();
            

            base.Initialize(); 
        }

        //Function to initialize all important objects
        public static void LoadAll()
        {
            levelSelect = null;
            show = false; mute = false;
            level = new Level();
            levelSelectScreen = new LevelSelect(Content, font, player);
            player = new Player(level, towerTextures, badTowerTextures, bulletTextures, musicNoteTextures, waveManager, Content, font);

            //Call the wave manager, with current level, number of waves, and the enemies
            waveManager = new WaveManager(player, level, 100, enemyTextures);

            //Initialize the toolbars
            toolbarHorizontal = new ToolbarHorizontal(bottomBar, font, new Vector2(0, level.Height * 32), waveManager);
            toolbarVertical = new ToolbarVertical(sideBar, font, new Vector2(level.Width * 32, 0), player, waveManager, Content);
            toolbar = new TowerToolbar(toolbarTexture, font, new Vector2(0, level.Height * 32 - 96), player, waveManager, Content);
            pauseMenu = new PauseMenu(pauseTexture, new Vector2((level.Width * 32) / 2 - 110, (level.Height * 32) / 2 - 150), player, level, Content);

            battleMusicInstance.Stop();
            //battleMusicInstance2.Stop();
            songTime = 0f;
            //start = true;
        }

        // Create a new SpriteBatch, which can be used to draw textures.
        //Load all the content to be used in the game
        protected override void LoadContent()
        { spriteBatch = new SpriteBatch(GraphicsDevice); LoadAll(); }

        //Set the game state to 'InGame', used with various functions
        public static void setGameState()
        { currentGameState = GameState.InGame;  }

        //Reset the game by returning to the main menu
        //Reinitilizes all important objects
        public static void returnToMainMenu()
        { LoadAll(); currentGameState = GameState.StartScreen;   }

        //Pause the game while in the 'InGame' gamestate
        public static void pauseGame()
        { currentGameState = GameState.Pause; }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            switch (currentGameState)
            {
                case GameState.Intro:
                    {
                        timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                        if (timeSinceLastFrame > millisecondsPerFrame)
                        {
                            timeSinceLastFrame -= millisecondsPerFrame;
                            ++currentFrame.X;
                            if (currentFrame.X >= sheetSize.X)
                            {
                                currentFrame.X = 0;
                                ++currentFrame.Y;
                                if (currentFrame.Y >= sheetSize.Y)
                                    currentGameState = GameState.StartScreen;
                            }
                        }
                        break;
                    }
                case GameState.StartScreen:
                    {
                        screenTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (screenTimer > 2) { screenTimer = 0; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        { currentGameState = GameState.LevelSelect; }
                        break;
                    }
                case GameState.LevelSelect:
                    { levelSelectScreen.Update(gameTime); break; }
                case GameState.InGame:
                    {
                        //background sound stuffz
                        if (songTime == 0 && mute == false)
                            battleMusicInstance.Play();
                        //if (songTime == 0 && mute == false && start == false)
                            //battleMusicInstance2.Play();
                        
                        songTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (songTime >= songLength)
                        {
                            songTime = 0;
                            //start = false;
                            battleMusicInstance.Stop();
                            //battleMusicInstance2.Stop();
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            currentGameState = GameState.Pause;

                        waveManager.Update(gameTime);
                        player.Update(gameTime, waveManager.Enemies);
                        toolbarVertical.Update(gameTime);

                        //Update the index and type when the tower toolbar is shown
                        if (show)
                        {
                            toolbar.index = index;
                            toolbar.type = type;
                            toolbar.Update(gameTime);
                        }

                        //End the game is the player runs out of lives, or the max wave is reached
                        if (player.Lives <= 0)
                            currentGameState = GameState.GameOver;
                        if (WaveManager.currentwave == 100)
                            currentGameState = GameState.GameOver;   
                        break;
                    }
                case GameState.Pause:
                    {
                        //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            //currentGameState = GameState.InGame;
                        toolbarVertical.pauseButton.Update(gameTime);
                        pauseMenu.Update(gameTime);
                        
                        if (mute == true)
                        {
                            battleMusicInstance.Stop();
                            //battleMusicInstance2.Stop();
                            songTime = 0;
                        }

                        break;
                    }
                case GameState.GameOver:
                    {  if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { Exit(); }  break;  }
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Intro:
                    {
                        spriteBatch.Begin();

                        /*spriteBatch.Draw(introTexture, Vector2.Zero,
                        new Rectangle(currentFrame.X * frameSize.X,
                            currentFrame.Y * frameSize.Y,
                            frameSize.X, frameSize.Y),
                            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);*/

                        spriteBatch.End();
                        break;
                    }
                case GameState.StartScreen:
                    {
                        spriteBatch.Begin();

                        if(screenTimer < 1)
                        { spriteBatch.Draw(StartScreenOne, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null,
                                    Color.White, 0, Vector2.Zero, SpriteEffects.None, 0); }
                        if(screenTimer > 1)
                        { spriteBatch.Draw(StartScreenTwo, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null,
                                    Color.White, 0, Vector2.Zero, SpriteEffects.None, 0); }

                        spriteBatch.End();
                        break;
                    }
                case GameState.LevelSelect:
                    { GraphicsDevice.Clear(Color.DarkRed); spriteBatch.Begin();

                    spriteBatch.Draw(Content.Load<Texture2D>(@"Images\Levels\StartScreenLevelSelect"), 
                                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null,
                                Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                     levelSelectScreen.Draw(spriteBatch); spriteBatch.End(); break; }
                case GameState.InGame:
                    {
                        GraphicsDevice.Clear(Color.Snow); spriteBatch.Begin();

                        spriteBatch.Draw(map,
                                    new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null,
                                    Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                        level.Draw(spriteBatch);
                        waveManager.Draw(spriteBatch);
                        player.Draw(spriteBatch); player.DrawPreview(spriteBatch);
                        toolbarHorizontal.Draw(spriteBatch, player); toolbarVertical.Draw(spriteBatch, player);
                        if (show) { toolbar.Draw(spriteBatch); }

                        spriteBatch.End();
                        break;
                    }
                case GameState.Pause:
                    {
                        spriteBatch.Begin();
                        
                        //Draw everything that would have been drawn in 'InGame', but don't Update them to pause the game and save the positions of enemies and towers
                        spriteBatch.Draw(map,
                                    new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null,
                                    Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                        level.Draw(spriteBatch);
                        waveManager.Draw(spriteBatch);
                        player.Draw(spriteBatch); player.DrawPreview(spriteBatch);
                        toolbarHorizontal.Draw(spriteBatch, player); toolbarVertical.Draw(spriteBatch, player);
                        if (show) { toolbar.Draw(spriteBatch); }

                        pauseMenu.Draw(spriteBatch);

                        spriteBatch.End();
                        break;
                    }
                case GameState.GameOver:
                    {
                        GraphicsDevice.Clear(Color.Snow); spriteBatch.Begin();

                        string text = "Game Over";
                        spriteBatch.DrawString(font, text,
                            new Vector2(340, 304), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        spriteBatch.End();
                        break;
                    }
            }

            base.Draw(gameTime);
        }

    }
}
