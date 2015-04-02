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
    class Mew : Tower
    {
        public Mew(Texture2D texture, Texture2D[] bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            // Set the damage
            this.damage = (150f + level) * evolution;

            // Set the initial cost
            this.cost = 3500;

            // Set the radius
            this.radius = 200 + (level / 2);

            this.evolutionOneCost = cost * 2;
            this.evolutionTwoCost = 0;

        }

        string lastBullet = "red";

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.damage = (150f + level) * evolution;

            this.radius = 200 + (level / 2);

            if (bulletTimer >= 0.1f / evolution - (level / 100) && target != null)
            {
                switch (lastBullet)
                {
                    case "red":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(bulletTexture[0], Vector2.Subtract(center,
                                new Vector2(bulletTexture[0].Width / 2)), rotation, 10, damage);
                            lastBullet = "blue";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "blue":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(bulletTexture[1], Vector2.Subtract(center,
                                new Vector2(bulletTexture[1].Width / 2)), rotation, 10, damage);
                            lastBullet = "green";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "green":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(bulletTexture[2], Vector2.Subtract(center,
                                new Vector2(bulletTexture[2].Width / 2)), rotation, 10, damage);
                            lastBullet = "yellow";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "yellow":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(bulletTexture[3], Vector2.Subtract(center,
                                new Vector2(bulletTexture[3].Width / 2)), rotation, 10, damage);
                            lastBullet = "purple";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "purple":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(bulletTexture[4], Vector2.Subtract(center,
                                new Vector2(bulletTexture[4].Width / 2)), rotation, 10, damage);
                            lastBullet = "red";
                            bulletList.Add(bullet);
                            break;
                        }
                }
                //if (!Main.mute)
                    //Main.fireball.Play();
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
