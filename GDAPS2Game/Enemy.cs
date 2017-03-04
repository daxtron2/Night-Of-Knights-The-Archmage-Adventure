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
    abstract class Enemy : Character
    {
        // Purpose: Class to hold all the Enemy code
        // Limitations: None

        // Fields
        protected Rectangle playerPos;

        // Properties

        // Constructor
        public Enemy(Player player, Rectangle initPositionBox, Texture2D charSprite) : base(initPositionBox, charSprite)
        {
            playerPos = player.CharacterBox;
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract override void Attack();

        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public override void TakeDamage(int dmg)
        {
            throw new NotImplementedException();
        }
    }
}
