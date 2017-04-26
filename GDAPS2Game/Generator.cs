using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Monogame
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*GDAPS 2 Game Project - Group 2
* Ben Fairlamb  - Group Lead
* TJ Wolschon   - Architect
* Zack Dunham   - UI/Art
* Michael Schek - Game Design
*/
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
        private Texture2D fog;
        private Vector2 fogLoc1;
        private Vector2 fogLoc2;
        private Vector2 fogLoc3;

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
        public Generator(Random rng, KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]>[] world, Player player, Game1 game, Texture2D[] debugs, Texture2D fog)
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

            // fog stuff
            this.fog = fog;
            fogLoc1 = new Vector2(0, 0);
            fogLoc2 = new Vector2(1600, 0);
            fogLoc3 = new Vector2(3200, 0);
        }

        // Methods
        /// <summary>
        /// Draw the Terrain
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            // draw 3 backgrounds
            sb.Draw(fog, fogLoc1, Color.White);
            sb.Draw(fog, fogLoc2, Color.White);
            sb.Draw(fog, fogLoc3, Color.White);

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
            // fog location boxes
            if (player.CharacterBox.X > fogLoc1.X + 2200)
            {
                fogLoc1.X += 1600;
            }

            if (player.CharacterBox.X > fogLoc2.X + 2200)
            {
                fogLoc2.X += 1600;
            }

            if (player.CharacterBox.X > fogLoc3.X + 2200)
            {
                fogLoc3.X += 1600;
            }

            // Add more chunks if nessesary
            if (chunkOrder.Count < ChunksRight + ChunksLeft + 1)
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
            if ((chunkOrder.Peek().ChunkNum + ChunksLeft) * ChunkSize < player.CharacterBox.X)
            {
                chunks.Remove(chunkOrder.Dequeue());
            }
        }
    }
}