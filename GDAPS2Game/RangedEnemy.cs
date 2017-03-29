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

        // animation attributes
        private int frame = 0; // default frame of 0
        private int numFrames = 2; // total number of frames is 2
        private Point currentFrame; // where current frame is on spritesheet
        private Point frameSize = new Point(14, 20); // size of each sprite

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

        public void Update(GameTime gameTime)
        {
            // switch case for loading different frames of animation
            switch (frame)
            {
                case 0:
                    currentFrame.X = 82;
                    currentFrame.Y = 40;
                    break;
                case 1:
                    currentFrame.X = 104;
                    currentFrame.Y = 40;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (health > 0)
            {
                spriteBatch.Draw(sprite, new Vector2(posBox.X, posBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            }
        }
    }
}
