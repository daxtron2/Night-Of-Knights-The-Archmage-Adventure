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
        private int score;
        public int Health { get { return health; } }
        public Player()
        {
            health = 5;//testing value
            score = 0;//score starts out at zero, obviously
        }
        public void Movement()
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump



            throw new NotImplementedException();

        }

        public void HealthPickup()
        {
            // Enemies occasionally drop health pickup

            throw new NotImplementedException();

        }

        public void Collision()
        {
            // If character is within a piece of terrain move them out
            // Might be handled by monogame?

            throw new NotImplementedException();

        }

        public void Attack()
        {
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
            throw new NotImplementedException();

        }
    }
}
