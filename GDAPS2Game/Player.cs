using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPS2Game
{
    class Player
    {
        private int playerHealth;
        private int score;
        

        public void Movement()
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump 
        }

        public void HealthPickup()
        {
            // Enemies occasionally drop health pickup
        }

        public void Collision()
        {
            // If character is within a piece of terrain move them out
            // Might be handled by monogame?
        }

        public void Attack()
        {
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
        }

        public void Physics()
        {
            // Called every frame
            // Somehow pull player towards the floor
            // Not 100% sure on the best way to do this
        }

    }
}
