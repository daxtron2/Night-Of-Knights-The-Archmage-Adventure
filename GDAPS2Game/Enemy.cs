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
        public Enemy(Player player, Rectangle initPositionBox) : base(initPositionBox)
        {
            playerPos = player.CharacterBox;
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract override void Attack();
    }
}
