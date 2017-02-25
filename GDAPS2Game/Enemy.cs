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
        Vector2 playerPos;

        // Properties

        // Constructor
        public Enemy()
        {

        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public abstract override void Attack();
    }
}
