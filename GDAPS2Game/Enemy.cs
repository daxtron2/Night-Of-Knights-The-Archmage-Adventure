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
        private Random rng = new Random();
        // Properties

        // Constructor
        public Enemy(Player player, Rectangle initPositionBox, Texture2D charSprite) : base(initPositionBox, charSprite)
        {
            playerPos = player.CharacterBox;
            playerL = player;
            sprite = charSprite;
            posBox = initPositionBox;
            posBox.X = initPositionBox.X - 15;
            
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
                health = 0;
            }
        }
        //method to add score, uses the addScore boolean in Character.
        public void Scoring()
        {
            if (isActive == false)
            {
                //if addscore is equal to true adds to the score then sets it to false so it only runs once.
                if (addScore == true)
                {
                    Console.WriteLine("Is this even working?");
                    playerL.AddScore(10);
                    Console.WriteLine("Score: " + playerL.Score);
                    addScore = false;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        internal void Update(GameTime gameTime)
        {
            playerPos = playerL.CharacterBox;
        }
    }
}
