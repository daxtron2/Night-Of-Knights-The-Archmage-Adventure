using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS2Game
{
    class Player : Character
    {
        // Purpose: The Player controlled character in the game
        // Limitations: None

        // Fields
        private int score;
        private int playerAttack;
        private Texture2D hit;
        //Creates the two rectangles for the attack hitboxes
        public Rectangle pHitBox;
        public Rectangle pHitBoxL;
        //creates a boolean for the direction in which the player is facing;
        Boolean faceRight;
        Boolean intersects;
        // animation attributes
        private int frame = 0; // default frame of 0
        private int numFrames = 4; // total number of frames is 4
        private int timeSinceLastFrame; // for counting milliseconds
        private Point currentFrame; // where current frame is on spritesheet
        private Point frameSize = new Point(17, 26); // size of each sprite

        public List<Enemy> enemies = new List<Enemy>();

        // Properties

        /// <summary>
        /// Integer that tracks the player's score. Increases as the player levels up
        /// </summary>
        public int Health { get { return health; } }

        //Properties

        // Constructor
        /// <summary>
        /// Instatiate a new Player
        /// </summary>
        public Player(Rectangle initPositionBox, Texture2D charSprite, Texture2D hitbox, List<Enemy> enemies) : base(initPositionBox, charSprite)
        {
            health = 50;//testing value
            score = 0;//score starts out at zero, obviously
            faceRight = true;
            pHitBox = new Rectangle(characterBox.X, characterBox.Y + 55, 10, 10);
            pHitBoxL = new Rectangle(characterBox.X, characterBox.Y + 55, 10, characterBox.Height);
            hit = hitbox;
            intersects = false;
            playerAttack = 1;//deals 1 damage per click

        }

        /// <summary>
        /// Script that handles the movement of the player, updats x and y values
        /// </summary>
        public void Movement(GameTime gameTime) // added gameTime parameter for movement animation
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump

            //If the player's health is above 0, he can move
            if (health > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    base.characterBox.X -= 7;
                    faceRight = false;
                    Update(gameTime); // for movement animation
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    base.characterBox.X += 7;
                    faceRight = true;
                    Update(gameTime);
                }
                pHitBox = new Rectangle(characterBox.X + 50, characterBox.Y + 105, 40, 70);
                pHitBoxL = new Rectangle(characterBox.X - 35, characterBox.Y + 105, 40, 70);

            }

        }

        /// <summary>
        /// Script that handles picking up health items that then increase or refill the player's health
        /// </summary>
        public void HealthPickup()
        {
            // Enemies occasionally drop health pickup

            throw new NotImplementedException();

        }

        /// <summary>
        /// Main collision detection script
        /// </summary>
        public void Collision()
        {
            // If character is within a piece of terrain move them out
            // Might be handled by monogame?
            
            //first handle ground collision
            if (characterBox.Y + characterBox.Height >= FLOORHEIGHT)
            {
                //characterBox.Y = FLOORHEIGHT - characterBox.Height;
                pHitBoxL.Y = characterBox.Y + 30; // + characterBox.Height - 50;
               
                pHitBox.Y = characterBox.Y + 30;  //+ characterBox.Height - 50;
            }
            
            if (characterBox.X <= 0)
            {
                characterBox.X = 0;
            }

        }


        /// <summary>
        /// Don't Use? use the constructor version
        /// </summary>

        /// <summary>
        /// Main attack script, damages enemies infront of the player character
        /// </summary>

        MouseState mState;
        MouseState mStateLast;
        public override void Attack()
        {
            mState = Mouse.GetState();
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
            if (enemies.Count == 0)//if the enemy list is empty, ie no enemies
            {
                intersects = false;//cant intersect because there is no enemies
            }
            foreach (Enemy enm in enemies.ToList())//for some reason needs a tolist, otherwise it throws exceptions when changed
            {
                if (faceRight == true)//if facing right
                {
                    if (pHitBox.Intersects(enm.CharacterBox))//if the right hit box intersects the current enemy's hitbox
                    {
                        intersects = true;//currently intersecting
                        if (mState.LeftButton == ButtonState.Pressed && mStateLast.LeftButton == ButtonState.Released)//if LMB just pressed
                        {
                            //Console.WriteLine("CLICK EVENT");//debug output
                            enm.TakeDamage(playerAttack);//take an amount of damage
                        }
                    }
                    else//if not intersecting
                    {
                        intersects = false;//false obv
                    }
                }
                if (faceRight == false)//if facing left
                {
                    if (pHitBoxL.Intersects(enm.CharacterBox))//if the left hitbox intersects w/ enemy
                    {
                        intersects = true;
                        if (mState.LeftButton == ButtonState.Pressed && mStateLast.LeftButton == ButtonState.Released)
                        {//if LMB just pressed
                            //Console.WriteLine("CLICK EVENT");//debug console output
                            enm.TakeDamage(playerAttack);//take an amount of damage
                        }
                    }
                    else//if hitboxes don't intersect
                    {
                        intersects = false;
                    }
                }
                enm.TryDestroy();//check if the enemy's health<=0, if it is set IsActive=false
                if (enm.IsActive == false)//if enemy is "dead"
                {
                    //Console.WriteLine("Removing enemy from list.");//debug output
                    enemies.Remove(enm);//remove the enemy from the list
                }
            }

            mStateLast = mState;//put the mouse state we just used into last state for use next runthrough
        }


        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public override void TakeDamage(int dmg)
        {
            //Subtracts from the health value any damage that is taken if it results in 0 or above, otherwise sets the health to 0 in the interest of not having negative health.
            if (health - dmg >= 0)
            {
                health -= dmg;
            }
            else
            {
                health = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch) // also changed spritebatch to spriteBatch because it was aggravating me lmao
        {
            //While the player's health is greater than 0, it continues to draw him
            if (health > 0)
            {
                if (faceRight == true)
                {
                    if (intersects)
                    {
                        spriteBatch.Draw(hit, pHitBox, Color.Purple);
                        spriteBatch.Draw(hit, pHitBoxL, Color.Purple);
                    }
                    else
                    {
                        spriteBatch.Draw(hit, pHitBox, Color.Green);
                        spriteBatch.Draw(hit, pHitBoxL, Color.Red);
                    }
                    // player is now drawn here and base.Draw is no longer called
                    if (
                        (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.D))
                        ||
                        (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right))
                        )
                    {
                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        if (
                                (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                                ||
                                (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                            )
                        {
                            spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                        }
                        if (
                                (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.D))
                                ||
                                (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right))
                                ||
                                (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.D))
                                ||
                                (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.Right))
                            )
                        {
                            spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                        }
                    }
                }
                else
                {
                    spriteBatch.Draw(hit, pHitBox, Color.Red);
                    spriteBatch.Draw(hit, pHitBoxL, Color.Green);
                    // same thing as above but flipped 
                    if (
                            (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
                            ||
                            (Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Left))
                       )
                    {
                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, new Vector2(6, 0), 5f, SpriteEffects.FlipHorizontally, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), Color.White, 0, new Vector2(6, 0), 5f, SpriteEffects.FlipHorizontally, 0);
                    }
                }
            }
        }

        // Update method is used for movement animation
        bool firstRun = true;
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (firstRun)
            {
                currentFrame.X = 1;
                currentFrame.Y = 6;
            }
            // every 80 ms while player is holding left/right it will change frame
            if(timeSinceLastFrame > 80)
            {
                timeSinceLastFrame = 0;
                frame++;
                if (frame >= numFrames)
                {
                    frame = 0;
                }

                // switch case for loading different frames of animation
                switch(frame)
                {
                    case 0:
                        currentFrame.X = 1;
                        currentFrame.Y = 6;
                        break;
                    case 1:
                        currentFrame.X = 23;
                        currentFrame.Y = 6;
                        break;
                    case 2:
                        currentFrame.X = 1;
                        currentFrame.Y = 6;
                        break;
                    case 3:
                        currentFrame.X = 1;
                        currentFrame.Y = 36; // on the spritesheet this sprite's location is actually 1,40 but for some reason monogame decided to bring it up 4 pixels
                        break;
                }
            }
            firstRun = false;
        }
    }
}