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
        public int enemyHealth;

        public MeleeEnemy(Player playerObj)
        {
            enemyHealth = 0;
        }
        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
