﻿using System;
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
    public class RangedEnemy : Enemy
    {
        // Purpose: Hold code for Ranged Enemies
        // Limitations: None

        // Fields

        // animation attributes
        private int frame = 0; // default frame of 0
        private Point currentFrame; // where current frame is on spritesheet
        private Point frameSize = new Point(14, 21); // size of each sprite
        private int damage = 5;

        Boolean archer = true;
        Random rangedSelector = new Random();
        int isRanged = 0;
        
        // shooting projectile stuff
        private Vector2 projectilePos;
        private Rectangle projectileRect;
        bool projectileActive = true;
        Color projectileColor = Color.White;
        Player pl;

        // Properties

        // Constructor
        /// <summary>
        /// Instantiate a new RangedEnemy
        /// </summary>
        public RangedEnemy(Player player, int x, Texture2D charSprite,Texture2D[] debug) : base (player, x, charSprite, debug)
        {
            projectilePos = new Vector2((characterBox.X - characterBox.Width), (characterBox.Y + 47)); // create projectile position in center of bow, dependant on position of enemy
            projectileRect = new Rectangle((int)projectilePos.X, (int)projectilePos.Y, 15, 7); // create projectile position in rectangle form for intersecting
            health += level;
            pl = player;
            isRanged = rangedSelector.Next(0, 10);
            if (isRanged == 0)
            {
                archer = false;
                base.MageScore = 50; //sets the magescore to 50 to indicate its an ArchMage (who awards 50 points upon being slain because he hurts a lot
                damage = 25;
            }

        }
        public Boolean IsArcher { get { return archer; } } // returns the boolean if the ranged archer is an archer or not
        /// <summary>
        /// Character Attack
        /// </summary>
        public override void Attack()
        {
            if ((playerPos.X + playerPos.Width) - (characterBox.X + characterBox.Width) < 500)
            {
                if (playerPos.X < characterBox.X + 500)
                {
                    frame = 1; // set frame to prep shooting
                    projectilePos.X -= 6;

                    if (projectilePos.X < (characterBox.X - characterBox.Width) - 900)
                    {
                        projectilePos.X = characterBox.X - characterBox.Width;
                        projectileActive = true;
                    }

                    // if the arrow is a certain point from the player
                    if (projectilePos.X < (characterBox.X - characterBox.Width) - 100)
                    {
                        frame = 0;
                    }

                    if (characterBox.X < playerPos.X)
                    {
                        frame = 0; // set frame to standing
                        if (projectilePos.X < 350)
                        {
                            projectilePos.X += 6; // stop arrow if it's at an x less than 350
                        }
                        else
                        {
                            frame = 1; // otherwise stay shooting
                        }
                        
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Scoring();
            if (isActive == true)
            {
                projectileRect.X = (int)projectilePos.X;
                playerPos = pl.CharacterBox;
                if (projectileRect.Intersects(playerPos))
                {
                    projectileColor = Color.Red;
                    if (projectileActive == true)
                    {
                        //the projectile for the archer does damage (set to 1), plus the level divided by 2, this adds scaling.
                        playerL.TakeDamage(damage + level / 2);
                    }
                    projectileActive = false;
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
                        currentFrame.Y = 39;
                        break;
                    case 1:
                        currentFrame.X = 104;
                        currentFrame.Y = 39;
                        break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                if (archer == false)
                {
                    // draw projectile 37, 18, 15, 7
                    spriteBatch.Draw(sprite, projectilePos, new Rectangle(47, 18, 35, 15), projectileColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);

                    // draw enemy
                    spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y - 30), new Rectangle(currentFrame.X, currentFrame.Y - 37, frameSize.X + 4, frameSize.Y + 20), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(sprite, projectilePos, new Rectangle(47, 48, 15, 7), projectileColor, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                    spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
