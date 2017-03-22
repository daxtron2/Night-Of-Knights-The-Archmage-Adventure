using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Monogame
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS2Game
{
    class Generator
    {
        // Fields
        private SpriteBatch sb;
        Texture2D floor;
        int it;
        int currentIt;
        Player player;

        // Properties

        // Constructor
        public Generator(SpriteBatch sb, Texture2D floor, Player player)
        {
            this.sb = sb;
            this.floor = floor;
            it = 0;
            currentIt = 0;
            this.player = player;
        }

        // Methods
        public void Draw()
        {
            for (int i = currentIt - 1; i < currentIt + 2; i++)
            {
                sb.Draw(floor, new Rectangle(i * 1600, 0, 1600, 900), Color.White);
            }

        }

        public void Update()
        {
            currentIt = player.CharacterBox.X / 1600;
        }
    }
}
