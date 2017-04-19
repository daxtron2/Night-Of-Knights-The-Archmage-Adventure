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
    class Chunk
    {
        // Fields
        private Random rng;
        
        // Graphics
        private Texture2D background;
        private Dictionary<Texture2D, int> foregroundSet;
        private int sumOdds;
        private Rectangle location;
        private List<Enemy> chunkEnemies;
        private int chunkNum;
        private Game1 game;

        // tree
        private Texture2D tree;
        private Dictionary<int, Texture2D> forelements;
        private Player player;


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
        public Chunk(Random rng, Texture2D background, Dictionary<Texture2D, int> foregroundSet, int chunkNum, int numEnemies, int x, Game1 game, Player player)
        {
            this.rng = rng;
            this.background = background;
            this.foregroundSet = foregroundSet;
            this.chunkNum = chunkNum;
            location = new Rectangle(x, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            chunkEnemies = new List<Enemy>();
            Populate(numEnemies);
            this.player = player;
        }

        // Methods
        /// <summary>
        /// Populate the chunk with enemies
        /// </summary>
        /// <param name="numEnemies">Number of enemies to add</param>
        private void Populate(int numEnemies)
        {
            foreach (KeyValuePair<Texture2D, int> foreground in foregroundSet)
            {
                sumOdds = foreground.Value;
            }
            for (int i = 0; i < 10; i++)
            {
                FindForeground(rng.Next(0, sumOdds));
            }

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
            for (int i = 0; i < treePosList.Count; i++)
            {
                sb.Draw(tree, treePosList[i], new Rectangle(0, 0, 33, 58), Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0);
                
            }
        }
        

        /// <summary>
        /// Remove all enemies left if any
        /// </summary>
        public void Despawn()
        {

        }

        private Texture2D FindForeground(int odd)
        {

        }
    }
}   