using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tower_Defense
{
    class WaveManager
    {

        public int numberOfWaves; // How many waves the game will have

        public Queue<Wave> waves = new Queue<Wave>(); // A queue of all our waves

        private Texture2D[] enemyTexture; // The texture used to draw the enemies

        public bool waveFinished = true; // Is the current wave over?

        private Level level; // A reference to our level class

        // Get the wave at the front of the queue
        public Wave CurrentWave  { get { return waves.Peek(); } }

        public static int currentwave = 0;

        public int getWave() { return currentwave; }

        public int getNumberOfWaves() { return numberOfWaves; } 

        // Get a list of the current enemeies
        public List<Enemy> Enemies  { get { return CurrentWave.Enemies; } }

        // Returns the wave number
        public int Round  { get { return CurrentWave.RoundNumber + 1; } }

        public WaveManager(Player player, Level level, int numberOfWaves, Texture2D[] enemyTexture)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemyTexture = enemyTexture;
            this.numberOfWaves = numberOfWaves;
            this.level = level;

            for (int i = 0; i < numberOfWaves; i++)
            {
                Wave wave = new Wave(i, player, level, enemyTexture, numberOfWaves);

                waves.Enqueue(wave);
            }

            //StartNextWave(); //Use if using timer to start each wave instead of button
        }

        public void StartNextWave()
        {
            if (waves.Count() > 0)
            {
                waves.Peek().Start(); // Start the next one
                ++currentwave;
                waveFinished = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime); // Update the wave

            if (CurrentWave.RoundOver) // Check if it has finished
            {
                waveFinished = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
