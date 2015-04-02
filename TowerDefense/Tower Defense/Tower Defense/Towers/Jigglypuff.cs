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
    class Jigglypuff : Tower
    {
        Texture2D[] musicNoteTextures;

        public Jigglypuff(Texture2D texture, Texture2D[] bulletTexture, Texture2D[] musicNoteTextures, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            // Set the damage
            this.damage = (75f * evolution) + level;

            // Set the initial cost
            this.cost = 300;

            // Set the radius
            this.radius = 125 + (level / 2);

            this.evolutionOneCost = cost * 2;
            this.evolutionTwoCost = 0;

            this.musicNoteTextures = musicNoteTextures;
        }

        String lastBullet = "red";

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.damage = (75f * evolution) + level;

            this.radius = 125 + (level / 2);

            if (bulletTimer >= 0.5f / evolution - (level / 90) && target != null)
            {
                switch (lastBullet)
                {
                    case "red":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(musicNoteTextures[0], Vector2.Subtract(center,
                                new Vector2(musicNoteTextures[0].Width / 2)), rotation, 10, damage);
                            lastBullet = "blue";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "blue":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(musicNoteTextures[1], Vector2.Subtract(center,
                                new Vector2(musicNoteTextures[1].Width / 2)), rotation, 10, damage);
                            lastBullet = "green";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "green":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(musicNoteTextures[2], Vector2.Subtract(center,
                                new Vector2(musicNoteTextures[2].Width / 2)), rotation, 10, damage);
                            lastBullet = "orange";
                            bulletList.Add(bullet);
                            break;
                        }
                    case "orange":
                        {
                            FaceTarget();
                            Bullet bullet = new Bullet(musicNoteTextures[3], Vector2.Subtract(center,
                                new Vector2(musicNoteTextures[3].Width / 2)), rotation, 10, damage);
                            lastBullet = "red";
                            bulletList.Add(bullet);
                            break;
                        }
                }
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
