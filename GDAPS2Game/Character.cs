using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;

namespace GDAPS2Game
{
    abstract class Character
    {
        // Purpose: Common code that all Characters will share, including player
        // Limitations: None

        // Fields
        protected int health;
        protected Rectangle characterBox;
        protected bool isActive;
        protected Texture2D characterSprite;
        private int downAccel;
        private int jumpHeight;
        protected int FLOORHEIGHT = 750; //never change this in the program, only through editor, treat it as a constant
        // couldn't get external editor to run, so manually changed floorheight from 750 to 850 as sprite is large
        KeyboardState kState;
        KeyboardState lastKState;
        BinaryReader read;
        BinaryWriter write;
        // Properties
        public Rectangle CharacterBox { get { return characterBox; } }
        public bool IsActive { get { return isActive; } }

        // Constructor
        /// <summary>
        /// Instantiate a new Character
        /// </summary>
        public Character(Rectangle initialPosition, Texture2D charSprite)
        {

            characterBox = initialPosition;
            characterSprite = charSprite;
            Spawn();
        }

        /// <summary>
        /// Create the enemy or player, set default health value, Add to screen for draw
        /// </summary>
        public void Spawn()
        {
            isActive = true;
            health = 5;//testing value

        }

        /// <summary>
        /// When enemy/playerHealth is less than or equal to 0 remove enemy from screen
        /// Death animation?
        /// give a chance to drop a health potion
        /// </summary>
        public void Destroy()
        {
            if (health <= 0)
            {
                isActive = false;
            }

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
            if(characterBox.Y >= FLOORHEIGHT)//will need to be changed from FLOORHEIGHT depending on sprite height
            {
                downAccel = 0;
            }
            kState = Keyboard.GetState();
            if ((kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up)) && (lastKState.IsKeyUp(Keys.W) || (lastKState.IsKeyUp(Keys.Up))) && characterBox.Y >= FLOORHEIGHT - characterBox.Height)//FLOORHEIGHT is based on sprite height
            {
                downAccel = -25;
            }
            if (characterBox.Y + characterBox.Height <= FLOORHEIGHT)
            {
                downAccel++;
            }



            lastKState = kState;
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract void Attack();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterSprite, characterBox, Color.White);
        }

        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public abstract void TakeDamage(int dmg);
    }
}

