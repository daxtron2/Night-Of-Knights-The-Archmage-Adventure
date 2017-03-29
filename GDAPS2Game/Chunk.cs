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
    class Chunk
    {
        // Fields
        private int chunkNum;
        private Texture2D background;
        private List<Enemy> chunkEnemies;
        private Random rng;
        private Vector2 location;

        // Properties

        // Constructor

        public Chunk(Random rng, Texture2D background, int chunkNum, int numEnemies, int x)
        {
            this.rng = rng;
            this.background = background;
            this.chunkNum = chunkNum;
            location = new Vector2(x, 0);
        }

        // Methods
        private void Populate(int numEnemies)
        {
            for (int i = 0; i < numEnemies; i++)
            {
                // Create enemies
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(background, location, Color.White);
        }
    }
}
