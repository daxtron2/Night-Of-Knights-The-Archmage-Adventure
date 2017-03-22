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
    class Player : Character
    {
        // Purpose: The Player controlled character in the game
        // Limitations: None

        // Fields
        private int score;
        //Creates the two rectangles for the attack hitboxes
        public Rectangle pHitBox;
        public Rectangle pHitBoxL;
        //creates a boolean for the direction in which the player is facing;
        Boolean faceRight;

        //List of enemies that are spawned
        List<Enemy> enemies = new List<Enemy>();

        // Properties
        /// <summary>
        /// Integer that tracks the player's score. Increases as the player levels up
        /// </summary>
        public int Health { get { return health; } }

        //Properties
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        // Constructor
        /// <summary>
        /// Instatiate a new Player
        /// </summary>
        public Player(Rectangle initPositionBox, Texture2D charSprite) : base(initPositionBox, charSprite)
        {
            health = 5;//testing value
            score = 0;//score starts out at zero, obviously
            faceRight = true;
            pHitBox = new Rectangle(characterBox.X, characterBox.Y, 10, 10);
            pHitBoxL = new Rectangle(characterBox.X, characterBox.Y, 10, characterBox.Height);
        }

        /// <summary>
        /// Script that handles the movement of the player, updats x and y values
        /// </summary>
        public void Movement()
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump
            
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                characterBox.X -= 7;
                faceRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                characterBox.X += 7;
                faceRight = true;
            }
            pHitBox = new Rectangle(characterBox.X + 70, characterBox.Y + characterBox.Height/2, 60, 40);
            pHitBoxL = new Rectangle(characterBox.X - 70, characterBox.Y + characterBox.Height/2, 60, 40);

        }

        /// <summary>
        /// Script that handles picking up health items that then increase or refill the player's health
        /// </summary>
        public void HealthPickup()
        {
            // Enemies occasionally drop health pickup

            throw new NotImplementedException();

        }

        /// <summary>
        /// Main collision detection script
        /// </summary>
        public void Collision()
        {
            // If character is within a piece of terrain move them out
            // Might be handled by monogame?

            //first handle ground collision
            if (characterBox.Y + characterBox.Height >= FLOORHEIGHT)
            {
                characterBox.Y = FLOORHEIGHT - characterBox.Height;
                pHitBoxL.Y = characterBox.Y + characterBox.Height / 2;
                pHitBox.Y = characterBox.Y + characterBox.Height / 2;
            }
            if (characterBox.X <= 0)
            {
                characterBox.X = 0;
            }

        }


        /// <summary>
        /// Don't Use? use the constructor version
        /// </summary>

        /// <summary>
        /// Main attack script, damages enemies infront of the player character
        /// </summary>
        public override void Attack()
        {
            // When user presses the attack key
            // Do attack animation
            // If enemy within range, kill/deal damage to enemy
            foreach (Enemy enm in enemies)
            {
                if (faceRight == true)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (pHitBox.Intersects(enm.CharacterBox))
                        {
                            enm.TakeDamage(5);
                        }
                    }
                }
                if (faceRight == false)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (pHitBoxL.Intersects(enm.CharacterBox))
                        {
                            enm.TakeDamage(5);
                        }
                    }
                }
                if (enm.IsActive == false)
                {
                    enemies.Remove(enm);
                }
            }

        }


        /// <summary>
        /// Have character Take Damage
        /// </summary>
        /// <param name="dmg">Damage to take</param>
        public override void TakeDamage(int dmg)
        {
            //Subtracts from the health value any damage that is taken if it results in 0 or above, otherwise sets the health to 0 in the interest of not having negative health.
            if (health - dmg >= 0)
            {
                health -= dmg;
            }
            else
            {
                health = 0;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            if (faceRight == true)
            {
                spritebatch.Draw(base.characterSprite, pHitBox, Color.Green);
                spritebatch.Draw(base.characterSprite, pHitBoxL, Color.Red);
            }
            else
            {
                spritebatch.Draw(base.characterSprite, pHitBox, Color.Red);
                spritebatch.Draw(base.characterSprite, pHitBoxL, Color.Green);
            }
        }
    }
}