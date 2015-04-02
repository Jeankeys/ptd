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
    class Squirtle : Tower
    {
        // A list of directions that the tower can shoot in.
        private Vector2[] directions = new Vector2[8];
        // All the enimes that are in range of the tower.
        private List<Enemy> targets = new List<Enemy>();

        /// <summary>
        /// Constructs a new Spike Tower object.
        /// </summary>
        public Squirtle(Texture2D texture, Texture2D[] bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            this.damage = (2.5f + level) * evolution; // Set the damage.
            this.cost = 80;   // Set the initial cost.

            this.radius = 48 + (level / 2); // Set the radius.

            this.evolutionOneCost = cost * 2;
            this.evolutionTwoCost = cost * 4;

            // Store a list of all the directions the tower can shoot.
            directions = new Vector2[]
            {
                new Vector2(-1, -1), // North West
                new Vector2( 0, -1), // North
                new Vector2( 1, -1), // North East
                new Vector2(-1,  0), // West
                new Vector2( 1,  0), // East
                new Vector2(-1,  1), // South West
                new Vector2( 0,  1), // South
                new Vector2( 1,  1), // South East
                new Vector2(-1, -0.5f),
                new Vector2(-0.5f, -0.5f),
                new Vector2(-0.5f, -1),
                new Vector2(0, -0.5f),
                new Vector2(-0.5f, 0),
                new Vector2(1, -0.5f),
                new Vector2(-0.5f, 1),
                new Vector2(0.5f, 0),
                new Vector2(0, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(1, 0.5f),
                new Vector2(0.5f, 1),
                new Vector2(0.5f, -1),
                new Vector2(-1, 0.5f),
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.damage = (10f + level) * evolution;

            this.radius = 48 + (level / 2);

            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Decide if it is time to shoot.
            if (bulletTimer >= .5f  / evolution - (level / 100) && targets.Count != 0)
            {
                // For every direction the tower can shoot,
                for (int i = 0; i < directions.Length; i++)
                {
                    // create a new bullet that moves in that direction.
                    Bullet bullet = new Bullet(bulletTexture[0], Vector2.Subtract(center,
                        new Vector2(bulletTexture[0].Width / 2)), directions[i], 3, damage);
                    
                    bulletList.Add(bullet);
                }
                if(!Main.mute)
                    Main.waterGun.Play();
                bulletTimer = 0;
            }

            // Loop through all the bullets.
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update(gameTime);

                // Kill the bullet when it is out of range.
                if (!IsInRange(bullet.Center))
                {
                    bullet.Kill();
                }

                // Loop through all the possible targets
                for (int t = 0; t < targets.Count; t++)
                {
                    // If this bullet hits a target and is in range,
                    if (targets[t] != null && Vector2.Distance(bullet.Center, targets[t].Center) < 12)
                    {
                        // hurt the enemy.
                        targets[t].CurrentHealth -= bullet.Damage;
                        experience += target.BountyGiven / 5;
                        bullet.Kill();
                        if (targets[t].CurrentHealth <= 0)
                        {
                            experience += targets[t].BountyGiven * 10;
                            killCount++;
                        }

                        // This bullet can't kill anyone else.
                        break;
                    }
                }

                // Remove the bullet if it is dead.
                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }

        }

        public override bool HasTarget
        {
            // The tower will never have just one target.
            get { return false; }
        }

        public override void GetClosestEnemy(List<Enemy> enemies)
        {
            // Do a fresh search for targets.
            targets.Clear();

            // Loop over all the enemies.
            foreach (Enemy enemy in enemies)
            {
                // Check wether this enemy is in shooting distance.
                if (IsInRange(enemy.Center))
                {
                    // Make it a target.
                    targets.Add(enemy);
                }
            }
        }

    }
}
