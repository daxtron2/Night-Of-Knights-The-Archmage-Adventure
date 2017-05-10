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

        Rectangle eHitBox;
        Rectangle eHitBoxL;
        Texture2D hitter; //test sprite
        // Properties

        // Constructor
        /// <summary>
        /// Instatiate a new MeleeEnemy
        /// </summary>
        /// <param name="player">The Player</param>
        public MeleeEnemy(Player player, int x, Texture2D charSprite, Texture2D[] debugs) : base (player, x, charSprite, debugs)
        {
            user = player;

            //hitter = test sprite
            hitter = charSprite;
        }

        // use the parameter pls


        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            //setup the hit boxes so there is one on the right side and left side of the enemy
            eHitBox = new Rectangle(characterBox.X + 100, characterBox.Y, 10, 10);
            eHitBoxL = new Rectangle(characterBox.X - 25, characterBox.Y, 10, 10);


            if (eHitBox.Intersects(user.CharacterBox) || eHitBoxL.Intersects(user.CharacterBox))
            {
             //   if (eHitBoxL.Intersects(user.CharacterBox))
             //   {
             //       Console.WriteLine("Left intersect for enemy");
             //   }
             //   if (eHitBox.Intersects(user.CharacterBox))
             //   {
             //       Console.WriteLine("Right intersect for enemy");
             //   }
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
            Scoring(30);
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

                //test draw for hit box 
                //spriteBatch.Draw(sprite, eHitBox, Color.Purple);

                //this one works
                //   spriteBatch.Draw(hitter, eHitBoxL, Color.Purple);


                //if the character is to the left of the enemy, the enemy will face left, if not the enemy will face right #playertracking yo
                if (user.CharacterBox.X - 10 < characterBox.X)
                {
                    spriteBatch.Draw(sprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(sprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.FlipHorizontally, 0);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
