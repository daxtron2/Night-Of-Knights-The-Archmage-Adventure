using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS2Game
{
    abstract class Character
    {
        // Purpose: Common Character that all characters will share, including player
        // Limitations: None

        // Fields
        protected int health;
        protected Rectangle characterBox;
        protected bool isActive;
        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new Character
        /// </summary>
        public Character(Rectangle initialPosition)
        {
            characterBox = initialPosition;


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
            // Not 100% sure on the best way to do this - majic

        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract void Attack();
    }
}

