using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
namespace GDAPS2Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        ScoreTracker scoreTracker = new ScoreTracker();

        //creates a list for the player scores, and two booleans for recording adn reading score
        List<int> playerScores;
        Boolean recordScore = true;
        Boolean readScore = true;

        // Fields
        // Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainFont;
        public Texture2D spriteSheet;
        Texture2D hitSprite;
        public Texture2D[] debugs;
        public Texture2D fog;
        public Texture2D healthBar;
        public Texture2D logo;
        
        KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>[] world;
        string[] backgroundPaths = {
            "background_new",
            "background_new"
        };
        string[] foregroundPaths = {
            "tree.png",
            "tree_2.png"
        };
        int[][] odds = {
            new int[] {10000, 1000, 1 },  // Plains Foregrounds (HARDCODE)
            new int[] {1000, 100000, 1 }    // Forest Foregrounds (HARDCODE)
        };

        // Debug textures to test boxy shaped things
        string[] debugPaths = {
            "Debug1.png",
            "Debug2.png",
            "Debug3.png"
        };

        // UI
        Menu menu;

        // Map Generation
        Generator gen;

        // Camera
        Vector3 cameraPos;
        int playerXCamera = 500;

        // Controls
        KeyboardState kState;
        KeyboardState oldKState;
        MouseState mState;

        // Calculations
        Random rng;
        public Player player;
        public Dictionary<int, List<Enemy>> worldEnemies;

        // IO
        BinaryReader attribRead;
        Stream attribFilePath;

        // debug colors bool (turn debug colors on/off, does not turn off hitboxes)
        bool debugColors = false;

        //added scorescreen to the state list
        enum GameState { Menu, Pause, Game, GameOver, ScoreScreen }
        GameState currentState;
        private void FileResolution()
        {
            int screenWidth = 0;
            int screenHeight = 0;
            try
            {
                Console.WriteLine("About to open file");
                attribFilePath = File.Open("..\\..\\..\\..\\content\\attributes.dat", FileMode.Open);

                Console.WriteLine("About to initialize attribRead obj");
                attribRead = new BinaryReader(attribFilePath);

                Console.WriteLine("About to try to read file");
                screenWidth = attribRead.ReadInt32();
                screenHeight = attribRead.ReadInt32();
                Console.WriteLine("Got through try block.");
                attribRead.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (screenWidth != 0)
            {
                graphics.PreferredBackBufferWidth = screenWidth;//width of window
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1600;
            }
            if (screenHeight != 0)
            {
                graphics.PreferredBackBufferHeight = screenHeight;//height of window
            }
            else
            {
                graphics.PreferredBackBufferHeight = 900;
            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            FileResolution();
            //IsFixedTimeStep = false;
            menu = new Menu();//new menu object
            currentState = GameState.Menu;//start in the menu
            IsMouseVisible = true;//mouse is visible


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rng = new Random();
            worldEnemies = new Dictionary<int, List<Enemy>>();
            base.Initialize();

            // Test Codes
            player = new Player(50, spriteSheet, hitSprite, debugs); // spawns player right where they will be for rest of game

            //rangedEnemy = new RangedEnemy(player, 800, spriteSheet, debugs);
            //meleeEnemy = new MeleeEnemy(player, 950, spriteSheet, debugs);
            //rangedEnemies.Add(rangedEnemy);
            //meleeEnemies.Add(meleeEnemy);

            //rangedEnemies.Add(new RangedEnemy(player, 900, spriteSheet, debugs));

            gen = new Generator(rng, world, this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content. 
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load THE ENTIRE GAME here
            LoadWorld();

            // Load Debug textures
            debugs = new Texture2D[3];
            debugs[0] = Content.Load<Texture2D>(debugPaths[0]);
            debugs[1] = Content.Load<Texture2D>(debugPaths[1]);
            debugs[2] = Content.Load<Texture2D>(debugPaths[2]);

            // Load text font
            mainFont = Content.Load<SpriteFont>("mainFont");//font used in the menus

            // Load Player sprites
            spriteSheet = Content.Load<Texture2D>("spritesheet_transparent"); // now loads entire spritesheet instead of one test sprite
            hitSprite = Content.Load<Texture2D>("playerSpriteTesting");

            // Load fog bg
            fog = Content.Load<Texture2D>("fog");

            // load health bar
            healthBar = Content.Load<Texture2D>("health");

            // load title screen logo
            logo = Content.Load<Texture2D>("logo");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }
        Heart heart;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kState = Keyboard.GetState();//first thing
            mState = Mouse.GetState();//second thing

            player.worldEnemies = worldEnemies;

            if (currentState == GameState.Game)//if in game
            {
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//escape is pressed
                {
                    currentState = GameState.Pause;// go to pause menu
                }
                player.Physics();
                player.Movement(gameTime); // threw in gametime for animation
                player.Attack();
                Tuple<bool, Rectangle> test = player.Test();
                if (test.Item1)
                {
                    heart = new Heart(spriteSheet, test.Item2, player);
                }
                gen.Update(gameTime);

                //rangedEnemy.Update(gameTime);
                //rangedEnemy.Attack();
                //meleeEnemy.Update(gameTime);
                //meleeEnemy.Attack();

                // Update enemies
                foreach (List<Enemy> enemies in worldEnemies.Values)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Update(gameTime);
                        enemies[i].Attack();
                    }
                }

                if(player.IsActive == false)
                {
                    currentState = GameState.GameOver;
                }
            }
            else if (currentState == GameState.Pause || currentState == GameState.Menu || currentState == GameState.GameOver)//if in pause menu/start menu
            {
                menu.Input();//check for menu input
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//if escape is pressed
                {
                    currentState = GameState.Game;//exit the menu
                }
            }
            //added the ability to exit the score screen with esc or backspace
            if (currentState == GameState.ScoreScreen)
            {
                if (kState.IsKeyDown(Keys.Escape) || kState.IsKeyDown(Keys.Back))
                {
                    currentState = GameState.Menu;
                }
            }

            if (kState.IsKeyDown(Keys.Enter))//if enter is pressed
            {
                if (currentState == GameState.Pause) // If pause menu
                {
                    switch (menu.SelectionIndex)//check what is currently selected
                    {
                        case 0://if the top button, play/resume game, is selected
                            currentState = GameState.Game;//unpause game
                            //firstMenu = false;//no longer first menu, if not already
                            break;
                        case 1://if restart is selected
                            Initialize();
                            currentState = GameState.Game;
                            break;
                        case 2://if exit game is selected
                            Exit();//close the game
                            break;//do nothing
                    }
                }
                else if (currentState == GameState.Menu) // If main menu
                {
                    switch (menu.SelectionIndex)//check what is currently selected
                    {
                        case 0://if the top button, play game is selected
                            currentState = GameState.Game;//unpause game
                            break;
                        case 1://selects the score menu
                            currentState = GameState.ScoreScreen;
                            Console.WriteLine("Setting to pause menu");
                            break;//do nothing
                        case 2://if exit game is selected
                            Exit();//close the game
                            break;
                    }
                }
                else if (currentState == GameState.GameOver) // If GameOver screen
                {
                    switch (menu.SelectionIndex)//check what is currently selected
                    {
                        case 0://selects restart
                            Initialize();
                            currentState = GameState.Game;
                            break;
                        case 1://selects the score menu
                            currentState = GameState.ScoreScreen;
                            Console.WriteLine("Setting to pause menu");
                            break;//do nothing
                        case 2:
                            //if exit game is selected
                            Exit();//close the game
                            break;
                    }
                }
            }


            cameraPos = new Vector3((player.CharacterBox.X * -1) + playerXCamera, 0, 0f);
            //last thing
            oldKState = kState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Console.WriteLine(player.IsActive);
            if (player.CharacterBox.X > playerXCamera && currentState == GameState.Game)//if the player is past 200px on the screen
            {
                if (player.CharacterBox.X - 500 <= player.MaxMove)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(new Vector3((player.MaxMove * -1) -5, 0, 0f)));//Draw after this
                }
                else
                {
                    //the camera will stick with the player along the x coordinate
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(cameraPos));//Draw after this
                }
            }
            if (player.CharacterBox.X <= playerXCamera || currentState != GameState.Game)//if they're at or before 200, or in a menu
            { 
                if (currentState != GameState.Game && currentState != GameState.Menu)//if not in the first menu or in game
                {   //this bit of code makes the menu render in the center of the screen again, instead of off to the left.
                    spriteBatch.End();//end the spritebatch otherwise an exception is thrown on next line
                }
                if (player.MaxMove != 0)
                {
                    if (currentState == GameState.Game)
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateTranslation(new Vector3((player.MaxMove * -1) - 5, 0, 0f)));//Draw after this
                    }
                    else
                    {
                        spriteBatch.Begin();
                    }
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);//draw normally with topleft being (0,0)
                }
            }


            switch (currentState)
            {
                case GameState.Game:
                    DrawGame(spriteBatch, gameTime);//Draws in game
                    break;
                case GameState.Menu:
                    DrawMenu(spriteBatch);//Draws the main menu
                    break;
                case GameState.Pause:
                    DrawMenu(spriteBatch);//Draws the pause menu
                    break;
                case GameState.GameOver:
                    DrawMenu(spriteBatch);//Draws the Game Over screen
                    break;
                case GameState.ScoreScreen://draws the score screen
                    DrawMenu(spriteBatch); //Draws the score screen;
                    break;
            }
            //spriteBatch.DrawString(mainFont, "X: " + mState.X + " Y: " + mState.Y, new Vector2(25, 25), Color.White);
            //debug, show mouse coords

            //spriteBatch.DrawString(mainFont, "X: " + player.CharacterBox.X + " Y: " + player.CharacterBox.Y, new Vector2(25, 50), Color.White);
            //debug, show player coords

            //spriteBatch.Draw(debugs[0], new Rectangle(player.MaxMove, 600, 10, 10), Color.White);

            spriteBatch.End();//Draw before this
            base.Draw(gameTime);
        }

        protected void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            gen.Draw(spriteBatch);
            //rangedEnemy.Draw(spriteBatch);
            //meleeEnemy.Draw(spriteBatch);

            // Draw all enemies
            foreach (List<Enemy> enemies in worldEnemies.Values)
            {
                foreach (Enemy enemy in enemies)
                {
                    // check for debugColors bool
                    if (debugColors == true)
                    {
                        enemy.DrawDebug(spriteBatch);
                    }
                    enemy.Draw(spriteBatch);
                }
            }

            // check for debugColors bool
            if (debugColors == true)
            {
                player.DrawDebug(spriteBatch);
            }
            player.Draw(spriteBatch);

            int screenMiddle = GraphicsDevice.Viewport.Width / 2;//gets the midpoint of the current x resolution

            spriteBatch.DrawString(mainFont, "Score: " + player.Score, new Vector2(player.MaxMove + screenMiddle - 195, 10), Color.Black);

            // health bar conditions and drawing

            // draw full bar if health is above fifty
            if (player.Health >= 50)
            {
                spriteBatch.Draw(debugs[0], new Rectangle(15 + player.MaxMove, 10, 300, 16), Color.White);

            }
            // draw red if low
            else if (player.Health <= 15)
            {
                spriteBatch.Draw(debugs[1], new Rectangle(15 + player.MaxMove, 10, player.Health * 6, 16), Color.White);

            }
            // otherwise draw health * 6
            else
            {
                spriteBatch.Draw(debugs[0], new Rectangle(15 + player.MaxMove, 10, player.Health * 6, 16), Color.White);
            }
            // draw health bar on top of debug colors
            spriteBatch.Draw(healthBar, new Vector2(15 + player.MaxMove, 10), Color.White);

            // block bar

            if (player.BlockHeldTime == 0)
            {
                spriteBatch.Draw(debugs[2], new Rectangle(15 + player.MaxMove, 30, 300, 16), Color.White);
            }
            else
            {
                spriteBatch.Draw(debugs[2], new Rectangle(15 + player.MaxMove, 30, (300 - (player.BlockHeldTime * 3)), 16), Color.White);
            }

            spriteBatch.Draw(healthBar, new Vector2(15 + player.MaxMove, 30), Color.White);


            spriteBatch.DrawString(mainFont, "Level: " + Character.level, new Vector2(player.MaxMove+505 + screenMiddle, 10), Color.Black);
            if (heart != null)
            {
                heart.Draw(spriteBatch);
            }

        }
        protected void DrawMenu(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(mainFont.MeasureString("GAME OVER"));
            int screenMiddle = GraphicsDevice.Viewport.Width / 2;//gets the midpoint of the current x resolution
            if (currentState == GameState.Menu)//if in menu, i.e. paused
            {
                //resets the readscore boolean so its not infinitely running in score menu and can be read again
                if (readScore == false)
                {
                    readScore = true;
                }
                spriteBatch.Draw(logo, Vector2.Zero, Color.White);
                spriteBatch.DrawString(mainFont, "Start Menu", new Vector2(screenMiddle - 147, 300), Color.Black);//centers text at 50 = y
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(screenMiddle - 133, 350), Color.Black);//draws the play game "button", centered
                spriteBatch.DrawString(mainFont, "High Scores", new Vector2(screenMiddle - 161, 400), Color.Black);//draws the score game "button", centered
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 450), Color.Black);//draws the exit game "button", centered

                int y = menu.SelectionIndex * 50 + 355;// mark y of * mathematicaly
                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, y), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, y), Color.Black);//next to play game
                                                                                                               //no matter the resolution
                        break;
                    case 1://places the Asterisks with "High Scores"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 190, y), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 155, y), Color.Black);//next to scores
                                                                                                               //no matter the resolution
                        break;
                    case 2://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, y), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, y), Color.Black);//next to exit game
                                                                                                               //no matter the resolution
                        break;
                }
            }
            if (currentState == GameState.Pause)
            {
                spriteBatch.DrawString(mainFont, "Pause Menu", new Vector2(screenMiddle - 147, 50), Color.Black); //centers text at 50 = y
                spriteBatch.DrawString(mainFont, "Continue", new Vector2(screenMiddle - 119, 100), Color.Black);//draws the play game "button"
                spriteBatch.DrawString(mainFont, "Restart", new Vector2(screenMiddle - 95, 150), Color.Black);//draws the restart "button"
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 200), Color.Black);//draws the exit game "button"

                int y = menu.SelectionIndex * 50 + 105;// mark y of * mathematicaly
                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Continue"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 145, y), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 110, y), Color.Black);//no matter the resolution

                        break;
                    case 1://places the asterisks with "Restart"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 125, y), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 105, y), Color.Black);//no matter the resolution

                        break;
                    case 2://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, y), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, y), Color.Black);//no matter the resolution

                        break;
                }
            }
            if (currentState == GameState.GameOver)
            {
                spriteBatch.DrawString(mainFont, "GAME OVER", new Vector2(screenMiddle - 130, 50), Color.Black);
                //spriteBatch.DrawString(mainFont, "Play Game", new Vector2(screenMiddle - 133, 100), Color.Black);//draws the play game "button"
                float scoreSize = mainFont.MeasureString("Score: " + player.Score).X / 2;

                //records the players score then sets the boolean to false so its not constantly writing on this screen
                if (recordScore == true)
                {
                    scoreTracker.WriteScore(player.Score);
                    recordScore = false;
                }
                spriteBatch.DrawString(mainFont, "Score: " + player.Score, new Vector2(screenMiddle - scoreSize, 100), Color.Black);
                spriteBatch.DrawString(mainFont, "Restart", new Vector2(screenMiddle - 95, 150), Color.Black);//draws the restart "button"
                spriteBatch.DrawString(mainFont, "High Scores", new Vector2(screenMiddle - 161, 200), Color.Black);//draws the score game "button", centered
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 250), Color.Black);//draws the exit game "button"

                int y = menu.SelectionIndex * 50 + 155;// mark y of * mathematicaly
                switch (menu.SelectionIndex)
                {
                    case 0:
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 125, y), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 105, y), Color.Black);//no matter the resolution

                        break;
                    case 1:
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 190, y), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 155, y), Color.Black);//next to scores
                                                                                                               //no matter the resolution
                        break;
                    case 2:
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, y), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, y), Color.Black);//no matter the resolution

                        break;
                }
            }
            if (currentState == GameState.ScoreScreen)
            {
                //reads the screen once and displays the scores
                if (readScore == true)
                {
                    playerScores = scoreTracker.ReadScores();
                    readScore = false;
                }
                spriteBatch.Draw(logo, Vector2.Zero, Color.White);
                spriteBatch.DrawString(mainFont, "High Scores", new Vector2(screenMiddle - 147, 300), Color.Black);//centers text at 50 = y
                spriteBatch.DrawString(mainFont, "1st Score: " + playerScores[0], new Vector2(screenMiddle - 147, 350), Color.Black);
                spriteBatch.DrawString(mainFont, "2nd Score: " + playerScores[1], new Vector2(screenMiddle - 147, 400), Color.Black);
                spriteBatch.DrawString(mainFont, "3rd Score: " + playerScores[2], new Vector2(screenMiddle - 147, 450), Color.Black);
            }
        }

        /// <summary>
        /// Load the Maps (Currently HARDCODED)
        /// </summary>
        protected void LoadWorld()
        {
            //throw new NotImplementedException("WE MUST CONSTRUCT MORE BIOMES");

            world = new KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>[odds.Length];

            // For each Biome
            for (int i = 0; i < odds.Length; i++)
            {
                // Create pair to hold biome's foregrounds assets and odds
                KeyValuePair<Texture2D, int>[] foregroundSet = new KeyValuePair<Texture2D, int>[odds[i].Length];

                // For each Foreground
                for (int j = 0; j < odds[i].Length; j++)
                {
                    // First foreground is always null
                    if (j == 0)
                    {
                        foregroundSet[j] = new KeyValuePair<Texture2D, int>(null, odds[i][j]);
                    }
                    // Load Texture2D and it's coresponding odds in the biome
                    else
                    {
                        foregroundSet[j] = new KeyValuePair<Texture2D, int>(Content.Load<Texture2D>(foregroundPaths[j - 1]), odds[i][j]);
                    }
                }
                
                // Add biome to the world
                world[i] = new KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>(Content.Load<Texture2D>(backgroundPaths[i]), foregroundSet);
            }
        }
    }
}
