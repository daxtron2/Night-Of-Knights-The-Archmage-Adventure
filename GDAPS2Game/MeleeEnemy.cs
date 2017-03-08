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

        // Properties

        // Constructor
        /// <summary>
        /// Instatiate a new MeleeEnemy
        /// </summary>
        /// <param name="player">The Player</param>
        public MeleeEnemy(Player player, Rectangle enemyPosBox, Texture2D charSprite) : base (player, enemyPosBox, charSprite)
        {
            
        }
        //Do not use, use other method
        public override void Attack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public void Attack(Player user)
        {
            Rectangle eHitBox = new Rectangle(characterBox.X + 5, characterBox.Y + 5, 10, 10);
            Rectangle eHitBoxL = new Rectangle(characterBox.X - 10, characterBox.Y - 10, 10, 10);

            if (eHitBox.Intersects(user.CharacterBox))
            {
                user.TakeDamage(5);
            }

            if (eHitBoxL.Intersects(user.CharacterBox))
            {
                user.TakeDamage(5);
            }
        }
    }
}
