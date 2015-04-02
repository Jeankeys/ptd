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
    class Tangela : Tower
    {
        public static float scale = 1;

        public static int tangelaCounter = 0;

        public Tangela(Texture2D texture, Texture2D[] bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            // Set the damage
            this.damage = 0;

            // Set the initial cost
            this.cost = 500;

            // Set the radius
            this.radius = 80 + level;

            this.evolutionOneCost = 0;
            this.evolutionTwoCost = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            ++experience;
        }
    }
}