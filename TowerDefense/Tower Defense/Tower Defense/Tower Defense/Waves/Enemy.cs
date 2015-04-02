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
    class Enemy : Sprite
    {
        //Enemy's total health
        protected float startHealth;
        protected float currentHealth;

        //Check whether the enemy has been killed
        protected bool alive = true;

        //Speed of the enemy
        protected float speed;

        protected float tempSpeed;

        protected float stunTimer;

        public int stun;

        protected int bountyGiven;

        //Waypoints for the enemies to follow
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        //Access the enemy's current health
        public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

        public float StartHealth { get { return startHealth; } set { startHealth = value; } }

        //Check is the enemy is dead
        public bool IsDead { get { return !alive; } }

        public int BountyGiven { get { return bountyGiven; } }

        public float DistanceToDestination { get { return Vector2.Distance(position, waypoints.Peek()); } }


        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            tempSpeed = speed;

            if(stun == 2)
            {
                stunTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                tempSpeed *= 0.25f;
                if (stunTimer > 5f)
                {
                    stunTimer = 0;
                    stun = 1;
                }
            }

            if (currentHealth <= 0)
            {
                if(!Main.mute)
                    Main.pop.Play();
                alive = false;
            }

            if (waypoints.Count > 0 && alive)
            {
                if (DistanceToDestination < speed)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();
                    
                    velocity = Vector2.Multiply(direction, tempSpeed);

                    position += velocity;
                }
            }

            else
                alive = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
