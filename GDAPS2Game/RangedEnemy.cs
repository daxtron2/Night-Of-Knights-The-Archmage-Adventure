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
    class RangedEnemy : Enemy
    {
        // Purpose: Hold code for Ranged Enemies
        // Limitations: None

        // Fields
        protected Point leftAttack;
        protected Point rightAttack;

        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new RangedEnemy
        /// </summary>
        public RangedEnemy(Player player, Rectangle enemyPosBox, Texture2D charSprite) : base (player, enemyPosBox, charSprite)
        {
            
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            if (playerPos.X + playerPos.Width / 2 > characterBox.X + characterBox.Width / 2)
            {
                // Instatiate projectile at rightAttack
            }
            else
            {
                // Instantiate projectile at leftAttack
            }
        }
    }
}
