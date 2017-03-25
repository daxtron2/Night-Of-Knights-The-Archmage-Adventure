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
        private Texture2D hit;
        //Creates the two rectangles for the attack hitboxes
        public Rectangle pHitBox;
        public Rectangle pHitBoxL;
        //creates a boolean for the direction in which the player is facing;
        Boolean faceRight;

        // animation attributes
        private int frame = 0; // default frame of 0
        private int numFrames = 3; // total number of frames is 3
        private int timeSinceLastFrame; // for counting milliseconds
        private Point currentFrame; // where current frame is on spritesheet
        private Point frameSize = new Point(17, 26); // size of each sprite

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
        public Player(Rectangle initPositionBox, Texture2D charSprite, Texture2D hitbox) : base(initPositionBox, charSprite)
        {
            health = 5;//testing value
            score = 0;//score starts out at zero, obviously
            faceRight = true;
            pHitBox = new Rectangle(characterBox.X, characterBox.Y, 10, 10);
            pHitBoxL = new Rectangle(characterBox.X, characterBox.Y, 10, characterBox.Height);
            hit = hitbox;
        }

        /// <summary>
        /// Script that handles the movement of the player, updats x and y values
        /// </summary>
        public void Movement(GameTime gameTime) // added gameTime parameter for movement animation
        {
            // Will use Arrow Keys and WASD for movement
            // W or Up to jump
            
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                base.characterBox.X -= 7;
                faceRight = false;
                Update(gameTime); // for movement animation
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                base.characterBox.X += 7;
                faceRight = true;
                Update(gameTime);
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
                //characterBox.Y = FLOORHEIGHT - characterBox.Height;
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
        public override void Draw(SpriteBatch spriteBatch) // also changed spritebatch to spriteBatch because it was aggravating me lmao
        {
            if (faceRight == true)
            {
                spriteBatch.Draw(hit, pHitBox, Color.Green);
                spriteBatch.Draw(hit, pHitBoxL, Color.Red);
                // player is now drawn here and base.Draw is no longer called
                spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);

            }
            else
            {
                spriteBatch.Draw(hit, pHitBox, Color.Red);
                spriteBatch.Draw(hit, pHitBoxL, Color.Green);
                // same thing as above but flipped 
                spriteBatch.Draw(characterSprite, new Vector2(characterBox.X, characterBox.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, new Vector2(6,0), 5f, SpriteEffects.FlipHorizontally, 0);
            }
        }

        // Update method is used for movement animation
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            // every 80 ms while player is holding left/right it will change frame
            if(timeSinceLastFrame > 80)
            {
                timeSinceLastFrame = 0;
                frame++;
                if (frame >= numFrames)
                {
                    frame = 0;
                }

                // switch case for loading different frames of animation
                switch(frame)
                {
                    case 0:
                        currentFrame.X = 1;
                        currentFrame.Y = 6;
                        break;
                    case 1:
                        currentFrame.X = 23;
                        currentFrame.Y = 6;
                        break;
                    case 2:
                        currentFrame.X = 1;
                        currentFrame.Y = 36; // on the spritesheet this sprite's location is actually 1,40 but for some reason monogame decided to bring it up 4 pixels
                        break;
                }
            }
        }
    }
}