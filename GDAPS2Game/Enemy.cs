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

        // animation attributes
        private int frame = 0; // default frame of 0
        private int numFrames = 2; // total number of frames is 4
        private Point currentFrame; // where current frame is on spritesheet
        private Point frameSize = new Point(14, 20); // size of each sprite

        // Properties

        // Constructor
        public Enemy(Player player, Rectangle initPositionBox, Texture2D charSprite) : base(initPositionBox, charSprite)
        {
            playerPos = player.CharacterBox;
            sprite = charSprite;
            posBox = initPositionBox;
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
            ///Edited to make it so health cannot be negative.
            if (health - dmg >= 0)
            {
                health -= dmg;
            }
            else
                health = 0;
        }

        // Update method is used for movement animation
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
            spriteBatch.Draw(sprite, new Vector2(posBox.X, posBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
        }
    }
}
