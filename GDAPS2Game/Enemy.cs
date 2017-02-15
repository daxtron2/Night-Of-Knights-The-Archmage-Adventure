using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPS2Game
{
    interface Enemy
    {

        void Spawn();// Create the enemy, set default health value, Add to screen for draw
        void Attack();// Similar to player's attack method. Instead of on command, attack every X seconds or milliseconds
        void Destroy();// When enemyHealth <= 0, remove enemy from screen, Death animation?, give a chance to drop a health potion

    }
}
