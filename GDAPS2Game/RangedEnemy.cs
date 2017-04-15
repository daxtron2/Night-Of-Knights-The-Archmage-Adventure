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

        // shooting projectile stuff
        private Vector2 projectilePos;
        private Rectangle projectileRect;
        Boolean intersecting = false;
        Boolean projectileActive = true;
        Color projectileColor = Color.White;

        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new RangedEnemy
        /// </summary>
        public RangedEnemy(Player player, Rectangle enemyPosBox, Texture2D charSprite) : base (player, enemyPosBox, charSprite)
        {
            projectilePos = new Vector2((posBox.X - posBox.Width), (posBox.Y + 47)); // create projectile position in center of bow, dependant on position of enemy
            projectileRect = new Rectangle((int)projectilePos.X, (int)projectilePos.Y, 15, 7); // create projectile position in rectangle form for intersecting
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
                    projectilePos.X -= 6;
                    if (projectilePos.X < (posBox.X - posBox.Width) - 900)
                    {
                        projectilePos.X = posBox.X - posBox.Width;
                        projectileActive = true;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Scoring();
            if (isActive == true)
            {
                projectileRect.X = (int)projectilePos.X;
                if (projectileRect.Intersects(playerPos))
                {
                    projectileColor = Color.Red;
                    if (projectileActive == true)
                    {
                        playerL.TakeDamage(5);
                    }
                    projectileActive = false;
                    intersecting = true;
                }
                else
                {
                    projectileColor = Color.White;
                }

                //Console.WriteLine("projectileRect X: {0}\tplayerPos{2}\nprojectileRect Y: {1}",projectileRect.X,projectileRect.Y,playerPos);

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
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                // draw projectile
                spriteBatch.Draw(sprite, projectilePos, new Rectangle(47, 48, 15, 7), projectileColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);

                // draw enemy
                spriteBatch.Draw(sprite, new Vector2(posBox.X, posBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            }
            base.Draw(spriteBatch);
        }
    }
}
