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
        public Vector2 Position { get { return position; } }
        // Properties
        /// <summary>
        /// Integer that tracks the player's score. Increases as the player levels up
        /// </summary>
        public int Health { get { return health; } }

        // Constructor
        /// <summary>
        /// Instatiate a new Player
        /// </summary>
        public Player(Vector2 initPosition) : base(initPosition)
        {
            health = 5;//testing value
            score = 0;//score starts out at zero, obviously
        }

        /// <summary>
        /// Script that handles the movement of the player, updats x and y values
        /// </summary>
        public void Movement()
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump

            throw new NotImplementedException();

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

            throw new NotImplementedException();

        }

        /// <summary>
        /// Main attack script, damages enemies infront of the player character
        /// </summary>
        public override void Attack()
        {
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
            throw new NotImplementedException();

        }
    }
}
