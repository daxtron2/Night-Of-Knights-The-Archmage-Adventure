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
    class Chunk
    {
        // Fields
        // Functionality
        private Random rng;
        private List<Enemy> chunkEnemies;
        private Player player;
        private List<int> enemyLocations;
        private Game1 game;
        
        // Graphics
        private KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]> biome;
        private Texture2D fog;

        // Background
        private Rectangle location;
        private int chunkNum;

        // Foregrounds
        private const int NumForegrounds = 5; // number of foreground sections per chunk (must be greater than 1, Begins to lag over 100, kills itself at 1000000)
        private KeyValuePair<Texture2D, Vector2>[] foregrounds;
        private Texture2D[] debugs;
        private int sumOdds;

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
        public Chunk(Random rng, KeyValuePair<Texture2D, KeyValuePair<Texture2D, int>[]> biome, int chunkNum, int numEnemies, int x, Game1 game)
        {
            // Save required perameters
            this.rng = rng;
            this.biome = biome;
            this.chunkNum = chunkNum;
            debugs = game.debugs;
            player = game.player;
            fog = game.fog;
            this.game = game;

            // Instatiate necessary fields
            sumOdds = 0;
            foregrounds = new KeyValuePair<Texture2D, Vector2>[NumForegrounds];
            location = new Rectangle(x, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            chunkEnemies = new List<Enemy>();
            enemyLocations = new List<int>();
            for (int i = 0; i < NumForegrounds; i++)
            {
                enemyLocations.Add(i);
            }

            // Generate Chunk
            Populate(numEnemies);
        }

        // Methods
        /// <summary>
        /// Populate the chunk with enemies and foregrounds
        /// </summary>
        /// <param name="numEnemies">Number of enemies to add</param>
        private void Populate(int numEnemies)
        {
            // Calculate total odds
            foreach (KeyValuePair<Texture2D, int> foreground in biome.Value)
            {
                sumOdds += foreground.Value;
            }

            // Add foregrounds
            for (int i = 0; i < NumForegrounds; i++)
            {
                // Random a foreground for location
                Texture2D foreground = FindForeground(rng.Next(0, sumOdds));

                // If not null, create a foreground at location
                if (foreground != null)
                {
                    if (location.Width / NumForegrounds > foreground.Width)
                    {
                        foregrounds[i] = new KeyValuePair<Texture2D, Vector2>(foreground,
                            new Vector2(location.X + i * location.Width / NumForegrounds + (rng.Next(0, location.Width / NumForegrounds - foreground.Width)),
                            825 - foreground.Height));
                    }
                    else
                    {
                        foregrounds[i] = new KeyValuePair<Texture2D, Vector2>(foreground,
                            new Vector2(location.X + i * location.Width / NumForegrounds + location.Width / NumForegrounds - foreground.Width,
                            825 - foreground.Height));
                    }
                }

                // Null placeholder in array
                else
                {
                    foregrounds[i] = new KeyValuePair<Texture2D, Vector2>(null, new Vector2());
                }
            }

            // Add enemies
            for (int i = 0; i < numEnemies; i++)
            {
                int place = rng.Next(0, enemyLocations.Count);
                enemyLocations.RemoveAt(place);
                if (rng.Next(0, 2) == 1)
                {
                    chunkEnemies.Add(new RangedEnemy(player, location.X + place * location.Width / NumForegrounds + (rng.Next(0, location.Width / NumForegrounds - 70)), game.spriteSheet, debugs));
                }
                else
                {
                    chunkEnemies.Add(new MeleeEnemy(player, location.X + place * location.Width / NumForegrounds + (rng.Next(0, location.Width / NumForegrounds - 70)), game.spriteSheet, debugs));
                }
            }
        }

        /// <summary>
        /// Draw Chunk
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(fog, location, Color.White);
            sb.Draw(biome.Key, location, Color.White);
            for (int i = 0; i < foregrounds.Length; i++)
            {
                // Foreground Debuggery (Comment out if actually playtesting)
                //sb.Draw(debugs[((NumForegrounds % 2) * (chunkNum % 2) + i) % 2], new Rectangle(location.X + i * location.Width / NumForegrounds, 0, location.Width / NumForegrounds, location.Height), Color.White);

                if (foregrounds[i].Key != null)
                {
                    sb.Draw(foregrounds[i].Key, foregrounds[i].Value, Color.White);
                }
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
            foreach (KeyValuePair<Texture2D, int> foreground in biome.Value)
            {
                min = max;
                max += foreground.Value;
                if (odd >= min && odd < max)
                {
                    return foreground.Key;
                }
            }
            // Just in case I cant math (also fixes the not all paths error)
            throw new Exception("Ya screwed up da foreground odds, matey");
        }
    }
}   