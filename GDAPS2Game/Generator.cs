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
        private SpriteBatch sb;
        private List<Texture2D> backgrounds;
        private const int ChunkSize = 1600;

        // Generation
        private const int ChunksRight = 2;
        private const int ChunksLeft = 1;

        // Where the phuq am I?
        private int it;
        private int currentIt;
        private Player player;
        private List<Chunk> chunks;
        private Queue<Chunk> chunkOrder;

        // Properties

        // Constructor
        public Generator(SpriteBatch sb, Random rng, List<Texture2D> backgrounds, Player player)
        {
            this.rng = rng;
            this.sb = sb;
            this.backgrounds = backgrounds;
            it = 0;
            currentIt = 0;
            this.player = player;
        }

        // Methods
        /// <summary>
        /// Draw the Terrain
        /// </summary>
        public void Draw()
        {
            foreach (Chunk chunk in chunks)
            {
                chunk.Draw(sb);
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
                chunkOrder.Enqueue(new Chunk(rng, backgrounds[rng.Next(0, backgrounds.Count)], it, 1, it * ChunkSize));
                it++;
            }

            // Update player chunk location
            currentIt = player.CharacterBox.X / ChunkSize;

            // Remove previous chunks long passed
            while ((chunkOrder.Peek().ChunkNum + ChunksLeft) * ChunkSize < player.CharacterBox.X)
            {
                chunkOrder.Dequeue();
            }
        }
    }
}