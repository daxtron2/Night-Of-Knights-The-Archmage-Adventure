using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
namespace GDAPS2Game
{
    //Heart Class. A Heart is drawn when an enemy dies, and when the player collides with it, it calls the addhealth method and then disappears.
    class Heart
    {
        private Vector2 enemyPosBox;
        private Rectangle heartPosition;
        private Rectangle heartSpriteBox;
        private Texture2D sheet;
        private Player player;
        public Player Player
        {
            get { return player; }
        }
        public Heart(Texture2D spriteSheet, Rectangle location, Player playerObj)
        {
            player = playerObj;
            sheet = spriteSheet;
            enemyPosBox.X = location.X + location.Width/2;
            enemyPosBox.Y = location.Y + location.Height/2;
            heartPosition = new Rectangle((int)enemyPosBox.X, (int)enemyPosBox.Y, 16 * 5, 15 * 5);
            heartSpriteBox = new Rectangle(123, 71, 16, 15);
        }

        public void Pickup()
        {
            if (player != null)
            {
                if (player.CharacterBox.Intersects(heartPosition))
                {

                    player.AddHealth(5);
                    player = null;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (player != null)
            {
                Pickup();
                spriteBatch.Draw(sheet, enemyPosBox, heartSpriteBox, Color.White, 0f, new Vector2(0, 0), 5f, SpriteEffects.None, 0f);
            }
        }
    }
}
