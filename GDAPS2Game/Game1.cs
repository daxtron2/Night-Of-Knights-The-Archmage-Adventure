﻿using Microsoft.Xna.Framework;
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
        // Fields
        // Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainFont;
        public Texture2D spriteSheet;
        Texture2D hitSprite;
        public Texture2D[] debugs;
        public Texture2D fog;
        
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
            "Debug2.png"
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
        RangedEnemy rangedEnemy;
        MeleeEnemy meleeEnemy;
        List<RangedEnemy> rangedEnemies = new List<RangedEnemy>();
        List<MeleeEnemy> meleeEnemies = new List<MeleeEnemy>();

        // IO
        BinaryReader attribRead;
        Stream attribFilePath;


        enum GameState { Menu, Pause, Game, GameOver }
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
            debugs = new Texture2D[2];
            debugs[0] = Content.Load<Texture2D>(debugPaths[0]);
            debugs[1] = Content.Load<Texture2D>(debugPaths[1]);

            // Load text font
            mainFont = Content.Load<SpriteFont>("mainFont");//font used in the menus

            // Load Player sprites
            spriteSheet = Content.Load<Texture2D>("spritesheet_transparent"); // now loads entire spritesheet instead of one test sprite
            hitSprite = Content.Load<Texture2D>("playerSpriteTesting");

            // Load fog bg
            fog = Content.Load<Texture2D>("fog");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kState = Keyboard.GetState();//first thing
            mState = Mouse.GetState();//second thing

            player.rangedEnemies = rangedEnemies;
            player.meleeEnemies = meleeEnemies;

            if (currentState == GameState.Game)//if in game
            {
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//escape is pressed
                {
                    currentState = GameState.Pause;// go to pause menu
                }
                player.Physics();
                player.Movement(gameTime); // threw in gametime for animation
                player.Collision();
                player.Attack();
                gen.Update(gameTime);

                //rangedEnemy.Update(gameTime);
                //rangedEnemy.Attack();
                //meleeEnemy.Update(gameTime);
                //meleeEnemy.Attack();

                // Update enemies
                for (int i = 0; i < rangedEnemies.Count; i++)
                {
                    rangedEnemies[i].Update(gameTime);
                    rangedEnemies[i].Attack();
                }
                for (int i = 0; i < meleeEnemies.Count; i++)
                {
                    meleeEnemies[i].Update(gameTime);
                    meleeEnemies[i].Attack();
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
            if (kState.IsKeyDown(Keys.Enter))//if enter is pressed
            {
                switch (menu.SelectionIndex)//check what is currently selected
                {
                    case 0://if the top button, play/resume game, is selected
                        if(currentState == GameState.GameOver)
                        {
                            currentState = GameState.Menu;
                        }
                        currentState = GameState.Game;//unpause game
                        //firstMenu = false;//no longer first menu, if not already
                        break;
                    case 1://if exit game is selected
                        Exit();//close the game
                        break;
                    default://if something other than the two we currently have is selected
                        currentState = GameState.Pause;//leave it paused
                        break;//do nothing
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
            if (player.CharacterBox.X > playerXCamera)//if the player is past 200px on the screen
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
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);//draw normally with topleft being (0,0)
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

            for (int i = 0; i < rangedEnemies.Count; i++)
            {
                rangedEnemies[i].Draw(spriteBatch);
            }
            for (int i = 0; i < meleeEnemies.Count; i++)
            {
                meleeEnemy.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            int screenMiddle = GraphicsDevice.Viewport.Width / 2;//gets the midpoint of the current x resolution

            //Draws the player score and health depending on his position, made such that it follows the player when its x position is below 200, else it appears in the top left
            if (player.CharacterBox.X < playerXCamera)
            {
                spriteBatch.DrawString(mainFont, "Score: " + player.Score, new Vector2(screenMiddle - 200, 10), Color.Black);
                spriteBatch.DrawString(mainFont, "Health: " + player.Health, new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(mainFont, "Level: " + Character.level, new Vector2(screenMiddle + 500, 10), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(mainFont, "Score: " + player.Score, new Vector2(player.MaxMove + screenMiddle - 195, 10), Color.Black);
                spriteBatch.DrawString(mainFont, "Health: " + player.Health, new Vector2(15 + player.MaxMove, 10), Color.Black);
                spriteBatch.DrawString(mainFont, "Level: " + Character.level, new Vector2(player.MaxMove+505 + screenMiddle, 10), Color.Black);
            }

        }
        protected void DrawMenu(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(mainFont.MeasureString("GAME OVER"));
            int screenMiddle = GraphicsDevice.Viewport.Width / 2;//gets the midpoint of the current x resolution
            if (currentState == GameState.Menu)//if in menu, i.e. paused
            {
                spriteBatch.DrawString(mainFont, "Start Menu", new Vector2(screenMiddle - 147, 50), Color.Black);//centers text at 50 = y
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(screenMiddle - 133, 100), Color.Black);//draws the play game "button", centered
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 150), Color.Black);//draws the exit game "button", centered

                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 105), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 105), Color.Black);//next to play game
                                                                                                                 //no matter the resolution
                        break;
                    case 1://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 155), Color.Black);//draws the asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 155), Color.Black);//next to exit game
                                                                                                                 //no matter the resolution
                        break;
                }
            }
            if (currentState == GameState.Pause)
            {
                spriteBatch.DrawString(mainFont, "Pause Menu", new Vector2(screenMiddle - 147, 50), Color.Black); //centers text at 50 = y
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(screenMiddle - 133, 100), Color.Black);//draws the play game "button"
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 150), Color.Black);//draws the exit game "button"

                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 105), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 105), Color.Black);//no matter the resolution

                        break;
                    case 1://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 155), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 155), Color.Black);//no matter the resolution

                        break;
                }
            }
            if (currentState == GameState.GameOver)
            {
                spriteBatch.DrawString(mainFont, "GAME OVER", new Vector2(screenMiddle - 130, 50), Color.Black);
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(screenMiddle - 133, 100), Color.Black);//draws the play game "button"
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(screenMiddle - 133, 150), Color.Black);//draws the exit game "button"

                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 105), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 105), Color.Black);//no matter the resolution

                        break;
                    case 1://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle - 160, 155), Color.Black);//centers asterisk
                        spriteBatch.DrawString(mainFont, "*", new Vector2(screenMiddle + 125, 155), Color.Black);//no matter the resolution

                        break;
                }
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
