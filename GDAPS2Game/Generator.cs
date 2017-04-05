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
        private Random rng;

        // Graphics
        private List<Texture2D> backgrounds;
        private const int ChunkSize = 1600;
        private Game1 game;

        // Generation
        private const int ChunksRight = 2;
        private const int ChunksLeft = 2;

        // Where the phuq am I?
        private int it;
        private int currentIt;
        private Player player;
        private List<Chunk> chunks;
        private Queue<Chunk> chunkOrder;

        // Properties

        // Constructor
        public Generator(Random rng, List<Texture2D> backgrounds, Player player, Game1 game)
        {
            this.rng = rng;
            this.backgrounds = backgrounds;
            it = 0;
            currentIt = 0;
            this.player = player;
            this.game = game;
            chunks = new List<Chunk>();
            chunkOrder = new Queue<Chunk>();
        }

        // Methods
        /// <summary>
        /// Draw the Terrain
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            if (chunks.Count != 0)
            {
                foreach (Chunk chunk in chunks)
                {
                    chunk.Draw(sb);
                }
            }

        }

        /// <summary>
        /// Update the terrain
        /// </summary>
        public void Update()
        {
            // Add more chunks if nessesary
            while (chunkOrder.Count < ChunksRight + ChunksLeft + 1)
            {
                Chunk chunk = new Chunk(rng, backgrounds[rng.Next(0, backgrounds.Count)], it, 1, it * ChunkSize, game);
                chunks.Add(chunk);
                chunkOrder.Enqueue(chunk);
                it++;
            }

            // Update player chunk location
            currentIt = player.CharacterBox.X / ChunkSize;

            // Remove previous chunks long passed
            while ((chunkOrder.Peek().ChunkNum + ChunksLeft) * ChunkSize < player.CharacterBox.X)
            {
                chunks.Remove(chunkOrder.Dequeue());
            }
        }
    }
}