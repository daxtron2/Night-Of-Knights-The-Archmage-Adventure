using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS2Game
{
    abstract class Character
    {
        protected int health;
        protected Vector2 position;

        public Character()
        {
            position = new Vector2();
        }
        public void Spawn()// Create the enemy or player, set default health value, Add to screen for draw
        {
            
            throw new NotImplementedException();
        }
        public void Destroy()// When enemy/playerHealth <= 0, remove enemy from screen, Death animation?, give a chance to drop a health potion
        {

            throw new NotImplementedException();
        }
        public void Physics()
        {
            // Called every frame
            // Somehow pull player and or enemy towards the floor
            // Not 100% sure on the best way to do this

            throw new NotImplementedException();
        }
    }
}

