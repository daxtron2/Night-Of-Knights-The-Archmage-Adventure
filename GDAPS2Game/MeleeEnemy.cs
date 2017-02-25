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
    class MeleeEnemy : Enemy
    {
        // Purpose: Hold code for Melee Enemies
        // Limitations: None

        // Fields
        public int enemyHealth;

        // Properties

        // Constructor
        /// <summary>
        /// Instatiate a new MeleeEnemy
        /// </summary>
        /// <param name="player">The Player</param>
        public MeleeEnemy(Player player)
        {
            enemyHealth = 0;
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
