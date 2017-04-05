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

        // shooting arrow stuff
        private Vector2 arrowPos;
        private Rectangle arrowRect;
        Boolean intersecting = false;

        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new RangedEnemy
        /// </summary>
        public RangedEnemy(Player player, Rectangle enemyPosBox, Texture2D charSprite) : base (player, enemyPosBox, charSprite)
        {
            arrowPos = new Vector2((posBox.X - posBox.Width), (posBox.Y + 47)); // create arrow position in center of bow, dependant on position of enemy
            arrowRect = new Rectangle((int)arrowPos.X, (int)arrowPos.Y, 15, 7); // create arrow position in rectangle form for intersecting
        }

        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            if ((playerPos.X + playerPos.Width) - (posBox.X + posBox.Width) < 500)
            {
                if (playerPos.X < posBox.X)
                {
                    arrowPos.X -= 6;
                    if (arrowPos.X < (posBox.X - posBox.Width) - 900)
                    {
                        arrowPos.X = posBox.X - posBox.Width;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            arrowRect.X = (int)arrowPos.X;
            if (arrowRect.Intersects(playerPos))
            {
                arrowPos.Y += 12;
                intersecting = true;
            }
            Console.WriteLine("ArrowRect X: {0}\nArrowRect Y: {1}",arrowRect.X,arrowRect.Y);
            if (intersecting == true)
            {
                Console.WriteLine("Huston, we have intersection!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
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
            if (isActive)
            {
                // draw arrow
                spriteBatch.Draw(sprite, arrowPos, new Rectangle(47, 48, 15, 7), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);

                // draw enemy
                spriteBatch.Draw(sprite, new Vector2(posBox.X, posBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            }
        }
    }
}
