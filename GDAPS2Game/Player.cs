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
        //Integer that tracks the player's score. Increases as the player levels up.
        private int score;

        //returns the health value;
        public int Health { get { return health; } }

        //Main contructor for the player, contains all the values necessary to start
        public Player()
        {
            health = 5;//testing value
            score = 0;//score starts out at zero, obviously
        }
        //Script that handles the movement of the player, updats x and y values.
        public void Movement()
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump



            throw new NotImplementedException();

        }

        //Script that handles picking up health items that then increase or refill the player's health
        public void HealthPickup()
        {
            // Enemies occasionally drop health pickup

            throw new NotImplementedException();

        }

        //Main collision detection script.
        public void Collision()
        {
            // If character is within a piece of terrain move them out
            // Might be handled by monogame?

            throw new NotImplementedException();

        }

        //Main attack script, damages enemies infront of the player character.
        public void Attack()
        {
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
            throw new NotImplementedException();

        }
    }
}
