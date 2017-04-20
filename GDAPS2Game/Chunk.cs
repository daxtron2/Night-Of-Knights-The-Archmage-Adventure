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
        private Dictionary<Texture2D, int> foregroundSet;
        private KeyValuePair<Texture2D, Vector2>[] foregrounds;
        private int sumOdds;
        private Rectangle location;
        private List<Enemy> chunkEnemies;
        private int chunkNum;
        private Game1 game;
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
            sumOdds = 0;
            foregrounds = new KeyValuePair<Texture2D, Vector2>[10];
            this.chunkNum = chunkNum;
            location = new Rectangle(x, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            chunkEnemies = new List<Enemy>();
            Populate(numEnemies);
            this.player = player;
        }

        // Methods
        /// <summary>
        /// Populate the chunk with enemies and foregrounds
        /// </summary>
        /// <param name="numEnemies">Number of enemies to add</param>
        private void Populate(int numEnemies)
        {
            foreach (KeyValuePair<Texture2D, int> foreground in foregroundSet)
            {
                sumOdds += foreground.Value;
            }
            for (int i = 0; i < 10; i++)
            {
                Texture2D foreground = FindForeground(rng.Next(0, sumOdds));
                foregrounds[i] = new KeyValuePair<Texture2D, Vector2>(foreground,
                    new Vector2(location.X + i * location.Width * 10 + (location.Width / 10 - foreground.Width / 2), player.FloorHeight - foreground.Height));
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
            for (int i = 0; i < foregrounds.Length; i++)
            {
                sb.Draw(foregrounds[i].Key, foregrounds[i].Value, Color.White);
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
            int min = 0;
            int max = 0;
            foreach (KeyValuePair<Texture2D, int> foreground in foregroundSet)
            {
                min = max;
                max += foreground.Value;
                if (odd >= min && odd < max)
                {
                    return foreground.Key;
                }
            }
            throw new Exception("Ya fucked up da odds, matey");
        }
    }
}   