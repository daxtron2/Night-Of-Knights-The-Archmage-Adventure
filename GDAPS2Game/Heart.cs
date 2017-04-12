using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;

namespace GDAPS2Game
{
    //Heart Class. A Heart is drawn when an enemy dies, and when the player collides with it, it calls the addhealth method and then disappears.
    class Heart : Enemy
    {
        private static Rectangle enemyPosBox;
        protected Rectangle heartBox;
        public Heart(Player player, Rectangle initialPosition, Texture2D charSprite) : base (player, initialPosition, charSprite)
        {
            heartBox = initialPosition;
        }
        public Rectangle HeartBox { get { return heartBox; } }

        public override void Attack()
        {
            if (isActive == true)
            {
                if (heartBox.Intersects(playerL.CharacterBox))
                {
                    playerL.AddHealth(5);
                    isActive = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            { 
                spriteBatch.Draw(sprite, new Vector2(posBox.X, posBox.Y), heartBox, Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
            }
        }
    }
}
