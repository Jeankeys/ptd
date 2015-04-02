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
    class ToolbarHorizontal
    {
        private Texture2D texture;
        // A class to access the font we created
        private SpriteFont font;

        // The position of the toolbar
        private Vector2 position;

        WaveManager waveManager;

        public ToolbarHorizontal(Texture2D texture, SpriteFont font, Vector2 position, WaveManager waveManager)
        {
            this.texture = texture;
            this.font = font;
            this.waveManager = waveManager;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, position, Color.Red);

            string text = string.Format("Money: {0} Lives: {1} Wave Numer: {2} out of {3}", player.Money, player.Lives, WaveManager.currentwave, waveManager.numberOfWaves - 2);
            spriteBatch.DrawString(font, text, new Vector2(10, position.Y), Color.Red);
        }

    }
}
