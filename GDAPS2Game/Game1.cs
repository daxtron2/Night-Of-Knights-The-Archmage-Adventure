using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        bool paused;//true = paused, false = in game
        bool firstMenu;//true = start menu false = pause menu
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;//width of window
            graphics.PreferredBackBufferHeight = 900;//height of window
            menu = new Menu();
            paused = true;//whether or not the game is in the menu state
            IsMouseVisible = true;//mouse is visible
            firstMenu = true;//whether or not it's the start menu or pause menu


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
            if (paused == false)//if in game
            {
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//escape is pressed
                {
                    paused = true;// go to pause menu
                }
            }
            else if(paused == true)//if in pause menu
            {
                menu.Input();//check for menu input
                if (kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))//if escape is pressed
                {
                    paused = false;//exit the menu
                }
            }
            if (kState.IsKeyDown(Keys.Enter))//if enter is pressed
            {
                switch (menu.SelectionIndex)//check what is currently selected
                {
                    case 0://if the top button, play/resume game, is selected
                        paused = false;//unpause game
                        firstMenu = false;//no longer first menu, if not already
                        break;
                    case 1://if exit game is selected
                        Exit();//close the game
                        break;
                    default://if something other than the two we currently have is selected
                        paused = true;//leave it paused
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
            if(paused == true)//if in menu, i.e. paused
            {
                if (firstMenu == true)//if this is the first menu,
                {
                    spriteBatch.DrawString(mainFont, "Start Menu", new Vector2(800 - (mainFont.MeasureString("Start Menu").Length() / 2), 50), Color.Black); //centers text at 200 = y
                    //Display "Start Menu" instead of Pause menu
                }
                else if(firstMenu == false)//if not first menu
                {
                    spriteBatch.DrawString(mainFont, "Pause Menu", new Vector2(800 - (mainFont.MeasureString("Pause Menu").Length() / 2), 50), Color.Black); //centers text at 200 = y
                    //display "Pause Menu" instead of start menu
                }
                //spriteBatch.DrawString(mainFont, menu.SelectionIndex.ToString(), new Vector2(200, 100), Color.Black);//debug print needed when no graphical indication of selected object is present

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
            else if(paused == false)//if in game, i.e. not paused
            {
                //the code to draw the game
                //no ingame drawing code should be outside of this
            }


            spriteBatch.End();//Draw before this
            base.Draw(gameTime);
        }
    }
}
