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
        protected Texture2D sprite;
        protected Rectangle posBox;
        protected Player playerL;

        // Properties

        // Constructor
        public Enemy(Player player, Rectangle initPositionBox, Texture2D charSprite) : base(initPositionBox, charSprite)
        {
            playerPos = player.CharacterBox;
            playerL = player;
            sprite = charSprite;
            posBox = initPositionBox;
        }

        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public override void TakeDamage(int dmg)
        {
            ///Edited to make it so health cannot be negative.
            if(health >= dmg)
            {
                health -= dmg;
            }
            else
            {
               TryDestroy();
            }
        }

        internal void Update(GameTime gameTime)
        {
            playerPos = playerL.CharacterBox;
        }
    }
}
