﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
/* GDAPS 2 Game Project - Group 2
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainFont;
        Menu menu;
        KeyboardState kState;
        KeyboardState oldKState;
        MouseState mState;
        Texture2D floorBG;
        Player player;
        List<Enemy> enemies;
        
        enum GameState { Menu, Pause, Game, GameOver}
        GameState currentState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;//width of window
            graphics.PreferredBackBufferHeight = 900;//height of window
            menu = new Menu();
            currentState = GameState.Menu;
            IsMouseVisible = true;//mouse is visible
            player = new Player()
            enemies.Add(new MeleeEnemy(player));

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainFont = Content.Load<SpriteFont>("mainFont");//font used in the menus
            floorBG = Content.Load<Texture2D>("Background Layer 1.png");

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
            if (currentState == GameState.Game)//if in game
            {
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//escape is pressed
                {
                    currentState = GameState.Pause;// go to pause menu
                }
            }
            else if (currentState == GameState.Pause || currentState == GameState.Menu)//if in pause menu/start menu
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
            spriteBatch.Begin();//Draw after this
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
            spriteBatch.End();//Draw before this
            base.Draw(gameTime);
        }
        
        protected void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            


        }
        protected void DrawMenu(SpriteBatch spriteBatch)
        {
            if (currentState == GameState.Menu)//if in menu, i.e. paused
            {
                spriteBatch.DrawString(mainFont, "Start Menu", new Vector2(800 - (mainFont.MeasureString("Start Menu").Length() / 2), 50), Color.Black); //centers text at 200 = y
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(667, 100), Color.Black);//draws the play game "button"
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(667, 150), Color.Black);//draws the exit game "button"

                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(640, 105), Color.Black);
                        spriteBatch.DrawString(mainFont, "*", new Vector2(925, 105), Color.Black);

                        break;
                    case 1://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(640, 155), Color.Black);
                        spriteBatch.DrawString(mainFont, "*", new Vector2(925, 155), Color.Black);

                        break;
                }
            }
            if (currentState == GameState.Pause)
            {
                spriteBatch.DrawString(mainFont, "Pause Menu", new Vector2(800 - (mainFont.MeasureString("Pause Menu").Length() / 2), 50), Color.Black); //centers text at 200 = y
                spriteBatch.DrawString(mainFont, "Play Game", new Vector2(667, 100), Color.Black);//draws the play game "button"
                spriteBatch.DrawString(mainFont, "Exit Game", new Vector2(667, 150), Color.Black);//draws the exit game "button"

                switch (menu.SelectionIndex)//draws two asterisks before and after currently selected item
                {
                    case 0://places the asterisks with "Play Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(640, 105), Color.Black);
                        spriteBatch.DrawString(mainFont, "*", new Vector2(925, 105), Color.Black);

                        break;
                    case 1://places the asterisks with "Exit Game"
                        spriteBatch.DrawString(mainFont, "*", new Vector2(640, 155), Color.Black);
                        spriteBatch.DrawString(mainFont, "*", new Vector2(925, 155), Color.Black);

                        break;
                }
            }
            if(currentState == GameState.GameOver)
            {
                //Game over screen
            }
        }
    }
}
