﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
namespace GDAPS2Game
{
    public class Player : Character
    {
        // Purpose: The Player controlled character in the game
        // Limitations: None

        // Fields
        private int playerAttack;
        private Texture2D hit;

        Color characterColor = Color.White;

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
        private int moveSpeed = 7;
        private int blockHeldTime = 0;
        private bool attackAnim = false;
        private bool moveThisFrame = false;
        //integer that sets the leveling up goal. Starts out at 100
        private int newGoal;

        /// <summary>
        /// Integers to hold the maximum amount the player can move left
        /// </summary>
        private int maxMovement = 0;

        public Dictionary<int, List<Enemy>> worldEnemies = new Dictionary<int, List<Enemy>>();

        // Properties

        /// <summary>
        /// Integer that tracks the player's score. Increases as the player levels up
        /// </summary>
        public int Health { get { return health; } }
        public int BlockHeldTime { get { return blockHeldTime; } }

        // Constructor
        /// <summary>
        /// Instatiate a new Player
        /// </summary>
        public Player(int x, Texture2D charSprite, Texture2D hitbox, Texture2D[] debugs) : base(x, 750, 55, 130, charSprite, debugs)
        {
            level = 1;
            newGoal = 100;
            health = 50;//testing value
            faceRight = true;
            pHitBox = new Rectangle(characterBox.X, characterBox.Y, 40, 70);
            pHitBoxL = new Rectangle(characterBox.X, characterBox.Y, 40, 70);
            hit = hitbox;
            intersects = false;
            playerAttack = 1;//deals 1 damage per click
            //currentFrame = new Point(23, 36);//-2 debug
            //currentFrame = new Point(1, 66);//-1 debug

        }

        //accessor for movement speed;
        public int MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }


        public int MaxMove {  get { return maxMovement; } set { maxMovement = value; } }

        /// <summary>
        /// Script that handles the movement of the player, updats x and y values
        /// </summary>
        bool onCooldown = false;
        bool blocking = false;
        public void Movement(GameTime gameTime) // added gameTime parameter for movement animation
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump
            //if the player is blocking, reduces the movespeed.
            

            if ((Keyboard.GetState().IsKeyDown(Keys.Q) || Mouse.GetState().RightButton == ButtonState.Pressed) && blockHeldTime < 100 && onCooldown == false)//if holding down B, hasn't been holding for past 20 incrementations
            {
                blockHeldTime+=5;//increase the held amount by 5, giving the amount of time blocking around 1 sec maximum, dependant on frame rate.
                //Console.WriteLine("BHT: " + blockHeldTime);
                if (blockHeldTime > 0)
                {
                    blocking = true;
                }
                MoveSpeed = 3;//slow down the player whilst blocking
            }
            else//if they're on cooldown or not blocking
            {
                //mStateLast = mState;
                blocking = false;
                if (blockHeldTime > 0)//if the held time is greater than 0, ie they're recovering from cooldown/stopped holding down b before max
                {
                    if(blockHeldTime > 100)//if they're on cooldown,ie they've reached over the maximum allowed
                    {
                        onCooldown = true;
                    }
                    blockHeldTime--;//decrement the cooldown timer
                    //Console.WriteLine("BHT: " + blockHeldTime);

                }
                if (blockHeldTime <= 0)//if we decremented the timer too far, ie the cooldown is over
                {
                    blockHeldTime = 0;//set the timer back to 0

                    onCooldown = false;//no longer on cooldown
                }
                MoveSpeed = 7;//reset movement speed to normal
            }
            

            //If the player's health is above 0, he can move
            if (health > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    moveThisFrame = true;
                    if (characterBox.X > maxMovement)
                    {
                        characterBox.X -= moveSpeed;
                    }
                    faceRight = false;
                    //Update(gameTime); // for movement animation
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    moveThisFrame = true;
                    if (characterBox.X >= 500)
                    {
                        if (maxMovement + 500 <= characterBox.X)
                        {
                            maxMovement += moveSpeed;
                        }
                    }
                    
                    base.characterBox.X += moveSpeed;
                    //Console.WriteLine("MaxMove: " + maxMovement);
                    //Console.WriteLine("CharacterX: " + characterBox.X);
                    faceRight = true;
                    //Update(gameTime);
                }

                //if no movement keys are pressed
                if(Keyboard.GetState().IsKeyUp(Keys.A)&& Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                {
                    moveThisFrame = false;//we're not moving
                }
                pHitBox = new Rectangle(characterBox.X + 45, characterBox.Y, 40, 70);
                pHitBoxL = new Rectangle(characterBox.X - 30, characterBox.Y, 40, 70);

            }
            else
            {
                isActive = false;
            }
            Update(gameTime);
        }

        /// <summary>
        /// Script that handles picking up health items that then increase or refill the player's health
        /// </summary>

        //method for leveling up. If the level starts out at 0 it sets it to 1. Each time the score reaches the 'newGoal', the new goal gets 100 added to it and level goes up
        public void levelUp()
        {
            if (level == 0)
            {
                level = 1;
            }
            if (Score >= newGoal)
            {
                //the player gains a level and some health when the level up, and sets a new score goal to reach in order to level up.
                newGoal += 50;
                level += 1;
                health += 10;
            }
        }
        
        /// <summary>
        /// Main attack script, damages enemies infront of the player character
        /// </summary>

        MouseState mState;
        MouseState mStateLast;
        KeyboardState kState;
        KeyboardState kStateLast;
        bool test = false;
        Rectangle enmHeart;
        public override void Attack()
        {
            pHitBoxL.Y = characterBox.Y + 35;
            pHitBox.Y = characterBox.Y + 35;
            intersects = false;
            mState = Mouse.GetState();
            kState = Keyboard.GetState();
            // When user presses the attack key
            if((mState.LeftButton == ButtonState.Pressed || kState.IsKeyDown(Keys.E)) && (mStateLast.LeftButton == ButtonState.Released && kStateLast.IsKeyUp(Keys.E)))
            {
                attackAnim = true;
                //attackIt++;
            }

            // Do attack animation
            
            // If enemy within range, kill/deal damage to enemy
            if (worldEnemies.Count == 0)//if the enemy list is empty, ie no enemies
            {
                intersects = false; //can't intersect because there is no enemies
            }
            else
            {
                foreach (List<Enemy> enemies in worldEnemies.Values)
                {
                    if (enemies.Count > 0)
                    {
                        foreach (Enemy enm in enemies.ToList())//for some reason needs a tolist, otherwise it throws exceptions when changed
                        {
                            if (enm.TryDestroy())
                            {
                                Random rng = new Random();
                                if (rng.Next(0, 2) == 1)//1 in 2 chance of spawning a heart
                                {
                                    test = true;
                                    enmHeart = enm.CharacterBox;
                                }
                            }
                            if (faceRight == true)//if facing right
                            {
                                if (pHitBox.Intersects(enm.CharacterBox))//if the right hit box intersects the current enemy's hitbox
                                {
                                    intersects = true;//currently intersecting
                                    if ((mState.LeftButton == ButtonState.Pressed && mStateLast.LeftButton == ButtonState.Released) || (kState.IsKeyDown(Keys.E) && kStateLast.IsKeyUp(Keys.E)))//if LMB just pressed
                                    {
                                        //Console.WriteLine("CLICK EVENT");//debug output
                                        //The player's damage scales with the level such that it does damage (Set to 5) plus the level / 5, it scales but not quickly.
                                        enm.TakeDamage(playerAttack + level / 5);//take an amount of damage
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
                                    if ((mState.LeftButton == ButtonState.Pressed && mStateLast.LeftButton == ButtonState.Released) || (kState.IsKeyDown(Keys.E) && kStateLast.IsKeyUp(Keys.E)))
                                    {//if LMB just pressed
                                     //Console.WriteLine("CLICK EVENT");//debug console output
                                     //The player's damage scales with the level such that it does damage (Set to 5) plus the level / 5, it scales but not quickly.
                                        enm.TakeDamage(playerAttack + level / 5);//take an amount of damage
                                        //Console.WriteLine("I just hit left yo!");

                                    }
                                }
                                else//if hitboxes don't intersect
                                {
                                    intersects = false;
                                }
                            }
                            if (enm.IsActive == false)//if enemy is "dead"
                            {
                                //Console.WriteLine("Removing enemy from list.");//debug output
                                if (enm is MeleeEnemy)
                                {
                                    AddScore(20);
                                }
                                else
                                {
                                    if (enm is RangedEnemy)
                                    {
                                        if (enm.MageScore > 0) //checks the mageScore integer, if its greater than 0 then its an ArchMage enemy and not an archer, thus it awards more points.
                                        {
                                            AddScore(50);
                                        }
                                        else
                                            AddScore(10);
                                    }
                                }
                                enemies.Remove(enm);//remove the enemy from the list
                            }
                        }
                    }
                }
            }
            kStateLast = kState;
            mStateLast = mState;//put the mouse state we just used into last state for use next runthrough
        }


        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public override void TakeDamage(int dmg)
        {
            if (blocking == false)
            {
                //Subtracts from the health value any damage that is taken if it results in 0 or above, otherwise sets the health to 0 in the interest of not having negative health.
                if (health - dmg >= 0)
                {
                    health -= dmg;
                    characterColor = Color.Red;
                }

                else
                {

                    health = 0;
                }
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
                        //spriteBatch.Draw(hit, pHitBox, Color.Purple);
                        //spriteBatch.Draw(hit, pHitBoxL, Color.Purple);
                    }
                    else
                    {
                        //spriteBatch.Draw(hit, pHitBox, Color.Green);
                        //spriteBatch.Draw(hit, pHitBoxL, Color.Red);
                    }
                    // player is now drawn here and base.Draw is no longer called
                    if (
                        (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.D))
                        ||
                        (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right))
                        ||
                        attackAnim
                        )
                    {
                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), characterColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        if (
                                (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                                ||
                                (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                            )
                        {

                            //frame = 0;

                            spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), characterColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);

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

                            //frame = 0;

                            spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), characterColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                        }
                    }
                }
                else
                {
                    //spriteBatch.Draw(hit, pHitBox, Color.Red);
                    //spriteBatch.Draw(hit, pHitBoxL, Color.Green);
                    // same thing as above but flipped 
                    if (intersects)
                    {
                        //spriteBatch.Draw(hit, pHitBox, Color.PeachPuff);
                        //spriteBatch.Draw(hit, pHitBoxL, Color.PeachPuff);
                    }
                    if (
                            (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
                            ||
                            (Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Left))
                            ||
                            attackAnim
                       )
                    {

                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), characterColor, 0, new Vector2(6, 0), 5f, SpriteEffects.FlipHorizontally, 0);

                     
                    }
                    else
                    {

                        //frame = 0;

                        spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(1, 6, frameSize.X, frameSize.Y), characterColor, 0, new Vector2(6, 0), 5f, SpriteEffects.FlipHorizontally, 0);
                    }
                }
            }
        }

        // Update method is used for movement animation
        bool firstRun = true;
        //bool attackLastFrame = false;
        // bool
        int attackIt = 0;
        public void Update(GameTime gameTime)
        {
            //Console.WriteLine(attackIt);
            //calls the level up method.
            levelUp();
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

                characterColor = Color.White;
                
                frame++;

                if (frame >= numFrames)
                {
                    frame = 0;
                }

                
                if (attackAnim)
                {
                    attackIt++;

                    if (attackIt <= 1)
                    {
                        frame = -2;
                    }
                    else if(attackIt <= 2)
                    {
                        frame = -1;
                    }
                    else
                    {
                        frame = 0;
                        attackIt = 0;
                        attackAnim = false;
                    }
                    

                    //Console.WriteLine(frame);
                    switch (frame)
                    {
                        case -2:
                            currentFrame.X = 23;
                            currentFrame.Y = 36;
                            break;
                        case -1:
                            currentFrame.X = 1;
                            currentFrame.Y = 66;
                            break;
                    }
                }

                // switch case for loading different frames of animation
                if (moveThisFrame)
                {
                    switch (frame)
                    {
                        case -2:
                            currentFrame.X = 23;
                            currentFrame.Y = 36;
                            frame++;
                            break;
                        case -1:
                            currentFrame.X = 1;
                            currentFrame.Y = 66;
                            //frame++;
                            break;
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
                

            }
            firstRun = false;
        }
        
        public Tuple<bool,Rectangle> Test()
        {
            if (test)
            {
                test = false;
                return new Tuple<bool, Rectangle>(true, enmHeart);
            }
            else { return new Tuple<bool, Rectangle>(false,Rectangle.Empty); }
        }
    }
}