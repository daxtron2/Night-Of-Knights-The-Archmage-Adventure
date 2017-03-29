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
        private Texture2D floor;
        private int it;
        private int currentIt;
        private Player player;
        private List<Chunk> chunks;

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
            foreach (Chunk chunk in chunks)
            {
                chunk.Draw(sb);
            }

        }

        public void Update()
        {
            currentIt = player.CharacterBox.X / 1600;
        }
    }
}
