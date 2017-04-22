﻿using System;
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
        private KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>[] world;
        private const int ChunkSize = 1600;
        private Game1 game;
        private Texture2D[] debugs;

        // Generation
        private KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]> prevBiome;
        private const int PrevBiomeOdds = 10;
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
        public Generator(Random rng, KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>[] world, Player player, Game1 game, Texture2D[] debugs)
        {
            // Save required perameters
            this.rng = rng;
            this.world = world;
            this.player = player;
            this.game = game;
            this.debugs = debugs;

            // Instatiate necessary fields
            it = 0;
            currentIt = 0;
            prevBiome = world[0];
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
                // Calculate biome of new chunk
                int biome = rng.Next(0, world.Length + PrevBiomeOdds);

                // If the biome is in worlds, change biome, else don't change
                if (biome < world.Length)
                {
                    prevBiome = world[biome];
                }
                
                // Create chunk, and register it in the tracker variables
                Chunk chunk = new Chunk(rng, prevBiome, it, 1, it * ChunkSize, game, player, debugs);
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