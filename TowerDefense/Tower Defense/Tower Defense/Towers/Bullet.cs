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
    class Bullet : Sprite
    {
        private float damage;
        private int age;

        private int speed;

        public float Damage { get { return damage; } }

        public bool IsDead() { return age > 100; }

        public Bullet(Texture2D texture, Vector2 position, float rotation,
            int speed, float damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;

            this.speed = speed;
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, int speed, float damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;

            this.speed = speed;

            this.velocity = velocity * speed;
        }

        public void Kill() { this.age = 200; }

        //Helps the bullet to actually hit the target in case the enemy is moving too fast
        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            position += velocity;

            base.Update(gameTime);
        }

    }
}
