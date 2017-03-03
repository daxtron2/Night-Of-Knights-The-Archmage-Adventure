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
        protected Vector2 position;

        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new Character
        /// </summary>
        public Character(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        /// <summary>
        /// Create the enemy or player, set default health value, Add to screen for draw
        /// </summary>
        public void Spawn()
        {
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// When enemy/playerHealth is less than or equal to 0 remove enemy from screen
        /// Death animation?
        /// give a chance to drop a health potion
        /// </summary>
        public void Destroy()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Do a Physics
        /// </summary>
        public void Physics()
        {
            // Called every frame
            // Somehow pull player and or enemy towards the floor
            // Not 100% sure on the best way to do this - majic

            throw new NotImplementedException();
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract void Attack();
    }
}

