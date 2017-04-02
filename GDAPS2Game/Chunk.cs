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
        private Random rng;
        
        // Graphics
        private Texture2D background;
        private Vector2 location;
        private List<Enemy> chunkEnemies;
        private int chunkNum;


        // Properties
        /// <summary>
        /// Number of Chunk in order
        /// </summary>
        public int ChunkNum
        {
            get { return chunkNum; }
        }

        // Constructor
        /// <summary>
        /// Instantiate a new Chunk
        /// </summary>
        /// <param name="rng">Random Number Generator</param>
        /// <param name="background">Background of chunk</param>
        /// <param name="chunkNum">Number of Chunk in order</param>
        /// <param name="numEnemies">Number of enemies to create in chunk</param>
        /// <param name="x">X start position of chunk</param>
        public Chunk(Random rng, Texture2D background, int chunkNum, int numEnemies, int x)
        {
            this.rng = rng;
            this.background = background;
            this.chunkNum = chunkNum;
            location = new Vector2(x, 0);
        }

        // Methods
        /// <summary>
        /// Populate the chunk with enemies
        /// </summary>
        /// <param name="numEnemies">Number of enemies to add</param>
        private void Populate(int numEnemies)
        {
            for (int i = 0; i < numEnemies; i++)
            {
                // Create enemies
            }
        }

        /// <summary>
        /// Draw Chunk
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(background, location, Color.White);
        }

        /// <summary>
        /// Remove all enemies left if any
        /// </summary>
        public void Despawn()
        {

        }
    }
}   