using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
namespace GDAPS2Game
{
    public abstract class Character
    {
        // Purpose: Common code that all Characters will share, including player
        // Limitations: None

        // Fields
        protected int health;
        protected Rectangle characterBox;
        protected bool isActive;
        protected Texture2D characterSprite;
        protected Player playerL;
        private int downAccel;
        private int gravity;
        private int jumpHeight;
        private int score;
        protected bool addScore = false;
        //Integer for the level, initally set to 1.
        public static int level = 1;
        protected Texture2D[] debugTexture;

        protected int floorHeight = 750; //never change this in the program, only through editor, treat it as a constant
        // couldn't get external editor to run, so manually changed floorheight from 750 to 850 as sprite is large
        KeyboardState kState;
        KeyboardState lastKState;
        BinaryReader read;
        BinaryWriter write;
        protected Heart heart;

        // Properties
        public Rectangle CharacterBox { get { return characterBox; } }
        public bool IsActive { get { return isActive; } }
        public int FloorHeight { get { return floorHeight; } } // Needed for foreground positioning

        //Boolean used to determine if score should be added. Used when the enemy dies.
        public bool scoreAdd { get { return addScore; } set { addScore = value; } }
        // Constructor
        /// <summary>
        /// Instantiate a new Character
        /// </summary>
        public Character(int x, int y, int width, int height, Texture2D charSprite, Texture2D[] debugs)
        {
            debugTexture = debugs;
            characterBox = new Rectangle(x, y, width, height);
            characterSprite = charSprite;
            //Spawn(); redundant?
            BinaryReader read;
            Stream attribFilePath;
            if(this is Player)
            {
                try
                {
                    attribFilePath = File.Open("..\\..\\..\\..\\content\\attributes.dat", FileMode.Open);//format: screenWidth, screenHeight, gravity, floorheight, jumpheight
                    read = new BinaryReader(attribFilePath);
                    read.ReadInt32();//width, don't store
                    read.ReadInt32();//height, don't store
                    gravity = read.ReadInt32();
                    floorHeight = read.ReadInt32();
                    jumpHeight = read.ReadInt32();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    gravity = 1;
                    jumpHeight = -25;
                }
            }
            isActive = true;
            health = 5;
        }

        //method to add the score, used when enemies die.
        public void AddScore(int add)
        {
            score += add;
            Console.WriteLine("Score is: " + score);
        }

        //returns the score value
        public int Score
        {
            get { return score; }
        }

        /// <summary>
        /// Create the enemy or player, set default health value, Add to screen for draw
        /// </summary>
        public void Spawn()//redundant?
        {
            isActive = true;
            health = 5;//testing value

        }

        /// <summary>
        /// When enemy/playerHealth is less than or equal to 0 remove enemy from screen
        /// Death animation?
        /// give a chance to drop a health potion
        /// </summary>
        public bool TryDestroy()
        {
            if (health <= 0)
            {
                //sets addScore to true so that the score can be added, set to false after score is incremented.
                addScore = true;
                
                isActive = false;
                return true;
            }
            return false;

        }

        /// <summary>
        /// Do a Physics
        /// </summary>
        public void Physics()
        {
            
            // Called every frame
            // Somehow pull player and or enemy towards the floor
            // Not 100% sure on the best way to do this
            characterBox.Y += downAccel; 
            if(characterBox.Y >= floorHeight)//will need to be changed from FLOORHEIGHT depending on sprite height
            {
                downAccel = 0;
            }
            kState = Keyboard.GetState();
            if ((kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up)) && (lastKState.IsKeyUp(Keys.W) || (lastKState.IsKeyUp(Keys.Up))) && characterBox.Y >= floorHeight)//FLOORHEIGHT is based on sprite height
            {
                downAccel = jumpHeight;
            }
            if (characterBox.Y <= floorHeight)
            {
                downAccel += gravity;
            }



            lastKState = kState;
        }

        //Whenever this method is called, adds health to the player's current health pool.
        public void AddHealth(int healthPlus)
        {
            health += healthPlus;
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract void Attack();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (heart != null)
            {
                heart.Draw(spriteBatch);
                /*if(heart.Player == null)
                {
                    heart = null;
                }*/
            }
            //spriteBatch.Draw(characterSprite, characterBox, Color.White);
        }

        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public abstract void TakeDamage(int dmg);
    }
}