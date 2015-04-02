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
    class Tower : Sprite
    {
        // How much will the tower cost to make
        protected int cost;

        // The damage done to enemy's
        protected float damage;

        public static float damage2;

        // How far the tower can shoot
        protected float radius;

        public int evolution = 1;
        public int experience = 0;
        public int level = 1;

        protected int evolutionOneCost;
        protected int evolutionTwoCost;

        public double totalForLevel;
        public double toNextLevel;

        public int killCount = 0;
        
        protected Texture2D[] bulletTexture;

        protected Enemy target;

        protected float bulletTimer; // How long ago was a bullet fired
        protected List<Bullet> bulletList = new List<Bullet>();

        public int Cost { get { return cost; } }

        public float Damage { get { return damage; } }

        public float Radius { get { return radius; } }

        public Enemy Target { get { return target; } }

        public int Evolution { get { return evolution; } }

        public int EvolutionOneCost { get { return evolutionOneCost; } }

        public int EvolutionTwoCost { get { return evolutionTwoCost; } }

        public Tower(Texture2D texture, Texture2D[] bulletTexture, Vector2 position)
            : base(texture, position)
        {
            this.bulletTexture = bulletTexture;

            damage2 = damage;

            evolutionOneCost = cost * 2;
            evolutionTwoCost = cost * 3;
        }

        //Check if enemy is in range
        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= radius)
                return true;

            return false;
        }

        //Finds the closest enemy to the tower
        public virtual void GetClosestEnemy(List<Enemy> enemies)
        {
            target = null;
            float smallestRange = radius;

            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
            }
        }

        //Rotates the tower to face at its target
        protected void FaceTarget()
        {
            Vector2 direction = center - target.Center;
            direction.Normalize();

            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            totalForLevel = Math.Pow(level+1, 3);
            toNextLevel = totalForLevel - experience;

            //Level up the tower if it has enough xp
            if (toNextLevel <= 0 && level < 100)
            {
                ++level;
                if (!Main.mute)
                   Main.levelup.Play();
                if (damage == 0)
                    ++Player.lives;
            }

            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target != null)
            {

                if (!IsInRange(target.Center) || target.IsDead)
                {
                    target = null;
                    bulletTimer = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bulletList)
                bullet.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public virtual bool HasTarget
        {
            // Check if the tower has a target.
            get { return target != null; }
        }
    }
}
