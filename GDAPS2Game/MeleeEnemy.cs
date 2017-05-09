using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
namespace GDAPS2Game
{
    public class MeleeEnemy : Enemy
    {
        // Purpose: Hold code for Melee Enemies
        // Limitations: None

        // Fields
        private int frame;//currently unused because no animation has been added
        private Point currentFrame;
        private Point frameSize = new Point(15,21);

        private int lastAttack = 0;
        private bool hasAttacked = false;
        private Player user;

        // Properties

        // Constructor
        /// <summary>
        /// Instatiate a new MeleeEnemy
        /// </summary>
        /// <param name="player">The Player</param>
        public MeleeEnemy(Player player, int x, Texture2D charSprite, Texture2D[] debugs) : base (player, x, charSprite, debugs)
        {
            user = player;
        }

        // use the parameter pls


        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            Rectangle eHitBox = new Rectangle(characterBox.X + 20, characterBox.Y, 10, 10);
            Rectangle eHitBoxL = new Rectangle(characterBox.X - 25, characterBox.Y, 10, 10);


            if (eHitBox.Intersects(user.CharacterBox) || eHitBoxL.Intersects(user.CharacterBox))
            {
                if (hasAttacked == false && isActive == true)
                {
                    user.TakeDamage(5);
                    hasAttacked = true;
                    frame = 1;
                }
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(characterBox);
            Scoring();
            if (lastAttack + 1 < gameTime.TotalGameTime.Seconds)
            {
                Attack();
                hasAttacked = false;
                lastAttack = gameTime.TotalGameTime.Seconds;
                frame = 0;
            }   

            switch (frame)
            {
                case 0:
                    currentFrame.X = 82;
                    currentFrame.Y = 66;
                    break;
                case 1:
                    currentFrame.X = 104;
                    currentFrame.Y = 66;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                // draw enemy
                spriteBatch.Draw(sprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            }
            base.Draw(spriteBatch);
        }
    }
}
