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
        int menuState;//0 = menu, 1 = game

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;//width of window
            graphics.PreferredBackBufferHeight = 900;//height of window
            menu = new Menu(kState, oldKState);
            menuState = 0;
            mainFont = 


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
            
            if(kState.IsKeyDown(Keys.Escape) && oldKState.IsKeyUp(Keys.Escape))
            {
                menuState = 0;
            }
            if(menuState == 0)
            {
                menu.Input();
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
            if(menuState == 0)
            {
                spriteBatch.DrawString()
            }
            else if(menuState == 1)
            {
                //the code to draw the game
            }
            spriteBatch.End();//Draw before this
            base.Draw(gameTime);
        }
    }
}
