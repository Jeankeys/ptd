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
    class Wave
    {
        private int waveNumber;

        private int numberOFWaves;

        static public Random rnd = new Random();

        private float redSpawnTimer = 0; //spawn timers for each enemy
        private float blueSpawnTimer = 0;
        private float greenSpawnTimer = 0;
        private float yellowSpawnTimer = 0;
        private float blackSpawnTimer = 0;

        public int totalEnemies = 0; // How many enemies have spawned
        private int redEnemiesSpawned = 0; //How many enemies of each enemy type have spawned
        private int blueEnemiesSpawned = 0;
        private int greenEnemiesSpawned = 0;
        private int yellowEnemiesSpawned = 0;
        private int blackEnemiesSpawned = 0;

        private int enemyHealth = 75;
        private int enemyBounty;

        private bool enemyAtEnd; // Has an enemy reached the end of the path?
        public bool spawningEnemies; // Are we still spawing enemies?
        private Level level; // A reference of the level
        private Texture2D[] enemyTexture; // A texture array for each of the enemy types
        public List<Enemy> enemies = new List<Enemy>(); // List of enemies


        private Player player; // A reference to the player.

        public bool RoundOver { get { return spawningEnemies; } }

        public int RoundNumber { get { return waveNumber; } }
         
        public bool EnemyAtEnd { get { return enemyAtEnd; } set { enemyAtEnd = value; } }

        public List<Enemy> Enemies { get { return enemies; } }

        public static int[][] enemyTypeSpawned = new int[101][];

        public Wave(int waveNumber, Player player, Level level, Texture2D[] enemyTexture, int numberOfWaves)
        {
            this.waveNumber = waveNumber;
            this.player = player;
            this.level = level;
            this.numberOFWaves = numberOfWaves;

            this.enemyTexture = enemyTexture;

            enemyBounty = enemyHealth / 20;

        }

        //Functions for each Enemy type to add
        private void AddRedEnemy()
        {
            Enemy Redenemy = new Enemy(enemyTexture[0],
                                level.Waypoints.Peek(), enemyHealth, enemyBounty, 1f, 0);  //Health, Bounty, Speed
            Redenemy.SetWaypoints(level.Waypoints); //Set the enemy to follow the map by following the waypoints
            enemies.Add(Redenemy); redEnemiesSpawned++; redSpawnTimer = 0; totalEnemies++;
        }

        private void AddBlueEnemy()
        {
            Enemy Blueenemy = new Enemy(enemyTexture[1],
                                level.Waypoints.Peek(), enemyHealth * 2 * (waveNumber / 2), enemyBounty, 1.25f, 1); 
            Blueenemy.SetWaypoints(level.Waypoints);
            //Keep track of the number of each enemy is spawning, and the total amount of enemies per wave
            enemies.Add(Blueenemy); blueEnemiesSpawned++; blueSpawnTimer = 0; totalEnemies++; 
        }

        private void AddGreenEnemy()
        {
            Enemy Greenenemy = new Enemy(enemyTexture[2],
                                level.Waypoints.Peek(), enemyHealth * 3 * waveNumber, enemyBounty, 1.50f, 2); 
            Greenenemy.SetWaypoints(level.Waypoints);
            enemies.Add(Greenenemy); greenEnemiesSpawned++; greenSpawnTimer = 0; totalEnemies++;
        }

        private void AddYellowEnemy()
        {
            Enemy Yellowenemy = new Enemy(enemyTexture[3],
                                level.Waypoints.Peek(), enemyHealth * 4 * (waveNumber * 2), enemyBounty, 1.75f, 3); 
            Yellowenemy.SetWaypoints(level.Waypoints);
            enemies.Add(Yellowenemy); yellowEnemiesSpawned++; yellowSpawnTimer = 0; totalEnemies++;
        }

        private void AddBlackEnemy()
        {
            Enemy Blackenemy = new Enemy(enemyTexture[4],
                                level.Waypoints.Peek(), enemyHealth * 8 * (waveNumber * 4), enemyBounty, 2f, 4);
            Blackenemy.SetWaypoints(level.Waypoints);
            enemies.Add(Blackenemy); blackEnemiesSpawned++; blackSpawnTimer = 0; totalEnemies++;
        }

        //Start the wave
        public void Start() { spawningEnemies = true; AddEnemyCount(); }

        //Initializes the number of enemies per enemy type based on the current wave
        //Adds a new enemy every 5 waves
        public void AddEnemyCount()
        {
            enemyTypeSpawned[waveNumber] = new int[5];
            //Waves 1-4
            if (waveNumber < 5)
            { 
                enemyTypeSpawned[waveNumber][0] = rnd.Next(waveNumber * 2, waveNumber * 5); 
            }
            //Waves 5-9
            if (waveNumber < 10 && waveNumber >= 5)
            {
                enemyTypeSpawned[waveNumber][0] = rnd.Next(waveNumber * 2, waveNumber * 5);
                enemyTypeSpawned[waveNumber][1] = rnd.Next(waveNumber * 2, waveNumber * 5);
            }
            //Waves 10-14
            if (waveNumber < 15 && waveNumber >= 10)
            {
                enemyTypeSpawned[waveNumber][0] = rnd.Next(waveNumber * 5, waveNumber * 7);
                enemyTypeSpawned[waveNumber][1] = rnd.Next(waveNumber * 5, waveNumber * 7);
                enemyTypeSpawned[waveNumber][2] = rnd.Next(waveNumber * 5, waveNumber * 7);
            }
            //Waves 15-19
            if (waveNumber < 20 && waveNumber >= 15)
            {
                enemyTypeSpawned[waveNumber][0] = rnd.Next(waveNumber * 7, waveNumber * 10);
                enemyTypeSpawned[waveNumber][1] = rnd.Next(waveNumber * 7, waveNumber * 10);
                enemyTypeSpawned[waveNumber][2] = rnd.Next(waveNumber * 7, waveNumber * 10);
                enemyTypeSpawned[waveNumber][3] = rnd.Next(waveNumber * 7, waveNumber * 10);
            }
            //Waves 20-24
            if (waveNumber < 25 && waveNumber >= 20)
            {
                enemyTypeSpawned[waveNumber][0] = rnd.Next(waveNumber * 10, waveNumber * 15);
                enemyTypeSpawned[waveNumber][1] = rnd.Next(waveNumber * 10, waveNumber * 15);
                enemyTypeSpawned[waveNumber][2] = rnd.Next(waveNumber * 10, waveNumber * 15);
                enemyTypeSpawned[waveNumber][3] = rnd.Next(waveNumber * 10, waveNumber * 15);
                enemyTypeSpawned[waveNumber][4] = rnd.Next(waveNumber * 10, waveNumber * 15);
            }
            //Wave 25
            if (waveNumber == 25)
            {
                enemyTypeSpawned[waveNumber][0] = waveNumber * 30;
                enemyTypeSpawned[waveNumber][1] = waveNumber * 30;
                enemyTypeSpawned[waveNumber][2] = waveNumber * 30;
                enemyTypeSpawned[waveNumber][3] = waveNumber * 30;
                enemyTypeSpawned[waveNumber][4] = waveNumber * 30;
            }
            //Waves 26 - 100
            if (waveNumber > 25)
            {
                enemyTypeSpawned[waveNumber][0] = waveNumber * 30 + waveNumber * 2;
                enemyTypeSpawned[waveNumber][1] = waveNumber * 30 + waveNumber * 2;
                enemyTypeSpawned[waveNumber][2] = waveNumber * 30 + waveNumber * 2;
                enemyTypeSpawned[waveNumber][3] = waveNumber * 30 + waveNumber * 2;
                enemyTypeSpawned[waveNumber][4] = waveNumber * 30 + waveNumber * 2;
            }

        }

        //totals the number of enemies that will spawn to determine if the wave is over or not
        public int TotalEnemyPerWave()
        {
            int sum = 0;

            for (int i = 0; i < 5; i++)
            { sum += enemyTypeSpawned[waveNumber][i]; }

            return sum;
        }

        public void Update(GameTime gameTime)
        {
            if(waveNumber > 0)
            {
                if (totalEnemies >= TotalEnemyPerWave())
                    spawningEnemies = false; // We have spawned enough enemies
                if (spawningEnemies)
                {
                    //Add Enemies based on the Jagged Array "enemyTypeSpawned"
                    //Add red balloons first, then blue, green, yellow, and black
                    if (redEnemiesSpawned < enemyTypeSpawned[waveNumber][0])
                    {
                        redSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; // Increment timer for the red enemies
                        if (redSpawnTimer > 0.5f) // Time to add a new enemey
                            AddRedEnemy();
                    }
                    //When previous enemy type is done spawning, start with the next enemy type
                    if (blueEnemiesSpawned < enemyTypeSpawned[waveNumber][1] /*&& redEnemiesSpawned == enemyTypeSpawned[waveNumber][0]*/)
                    {
                        blueSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (blueSpawnTimer > 0.6f)
                            AddBlueEnemy();
                    }

                    if (greenEnemiesSpawned < enemyTypeSpawned[waveNumber][2] /*&& blueEnemiesSpawned == enemyTypeSpawned[waveNumber][1]*/)
                    {
                        greenSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (greenSpawnTimer > 0.7f)
                            AddGreenEnemy();
                    }

                    if (yellowEnemiesSpawned < enemyTypeSpawned[waveNumber][3] /*&& greenEnemiesSpawned == enemyTypeSpawned[waveNumber][2]*/)
                    {
                        yellowSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (yellowSpawnTimer > 0.8f)
                            AddYellowEnemy();
                    }

                    if (blackEnemiesSpawned < enemyTypeSpawned[waveNumber][4] /*&& yellowEnemiesSpawned == enemyTypeSpawned[waveNumber][3]*/)
                    {
                        blackSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (blackSpawnTimer > 0.9f)
                            AddBlackEnemy();
                    }
                }
            }

            //Remove dead enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i]; //Initialize an enemy from the array to update
                enemy.Update(gameTime); //Update the enemy

                if (enemy.IsDead) //Check if the enemy has died
                {
                    if (enemy.CurrentHealth > 0) // Enemy is at the end
                    {
                        enemyAtEnd = true;
                        //Harder enemies subtract a larger amount of lives if they reach the end
                        switch (enemy.enemyId) //Find out which enemy it is based on their starting health
                        {
                            case 0: { player.Lives -= 1; break; } 
                            case 1: { player.Lives -= 2; break; } 
                            case 2: { player.Lives -= 3; break; }
                            case 3: { player.Lives -= 4; break; }
                            case 4: { player.Lives -= 5; break; }
                        }

                    }
                    else //Enemy has been killed
                    {
                        player.Money += enemy.BountyGiven; //Increment players money based on enemy killed
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
    }
}
