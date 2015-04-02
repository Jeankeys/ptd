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
    class Charmander : Tower
    {
        public Charmander(Texture2D texture, Texture2D[] bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            // Set the damage
            this.damage = (100f + level) * evolution;

            // Set the initial cost
            this.cost = 30;

            // Set the radius
            this.radius = 80 + level;

            this.evolutionOneCost = cost * 2;
            this.evolutionTwoCost = cost * 4;
        }

        Vector2[] directions = new Vector2[]
            {
                new Vector2(-1, -1), // North West
                new Vector2( 0, -1), // North
                new Vector2( 1, -1), // North East
                new Vector2(-1,  0), // West
                new Vector2( 1,  0), // East
                new Vector2(-1,  1), // South West
                new Vector2( 0,  1), // South
                new Vector2( 1,  1), // South East
            };

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.damage = (75f + level) * evolution;

            this.radius = 80 + level;

            if (bulletTimer >= 0.5f / evolution - (level / 100) && target != null)
            {
                FaceTarget();
                Bullet bullet = new Bullet(bulletTexture[1], Vector2.Subtract(center,
                    new Vector2(bulletTexture[1].Width / 2)), rotation, 15, damage);
                if(!Main.mute)
                    Main.fireball.Play();
                bulletList.Add(bullet);
                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                //If the bullet hits the target, the target loses health
                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
                    target.CurrentHealth -= bullet.Damage;

                    experience += target.BountyGiven;

                    bullet.Kill();
                    if (target.CurrentHealth <= 0)
                    {
                        experience += target.BountyGiven * 10;
                        killCount++;
                    }
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
