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
    class PauseMenu
    {
        private Texture2D texture;
        private Vector2 position;
        private Player player;
        private Level level;
        private ContentManager contentManager;

        Button mainMenuButton; Texture2D mainMenuTexture;
        Button continueButton; Texture2D continueTexture;
        Button optionsButton; Texture2D optionsTexture; Texture2D optionsMenuTexture;
        Button muteButton; Texture2D muteTexture;
        Button mutePressedButton; Texture2D mutePressedTexture;
        Button backButton; Texture2D backTexture;

        private bool options = false;

        public PauseMenu(Texture2D texture, Vector2 position, Player player, Level level, ContentManager contentManager)
        {
            this.texture = texture;
            this.position = position;
            this.player = player;
            this.level = level;
            this.contentManager = contentManager;

            LoadButtons();
        }

        public void LoadButtons()
        {
            mainMenuTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\mainMenuTexture");
            continueTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\continueTexture");
            optionsTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\optionsTexture");
            optionsMenuTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\pauseMenuTexture");

            muteTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\muteTexture");
            mutePressedTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\mutePressedTexture");
            backTexture = contentManager.Load<Texture2D>(@"Images\GUI\PauseMenu\backTexture");


            mainMenuButton = new Button(mainMenuTexture, mainMenuTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 140), player);
            mainMenuButton.OnPress += new EventHandler(mainMenu_OnPress);

            optionsButton = new Button(optionsTexture, optionsTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 90), player);
            optionsButton.OnPress += new EventHandler(optionsButton_OnPress);

            continueButton = new Button(continueTexture, continueTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 + 110), player);
            continueButton.OnPress += new EventHandler(continueButton_OnPress);

            muteButton = new Button(muteTexture, muteTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 40), player);
            muteButton.OnPress += new EventHandler(muteButton_OnPress);

            mutePressedButton = new Button(mutePressedTexture, mutePressedTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 40), player);

            backButton = new Button(backTexture, backTexture, new Vector2((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 140), player);
            backButton.OnPress += new EventHandler(backButton_OnPress);
        }

        private void mainMenu_OnPress(object sender, EventArgs e)
        { Main.returnToMainMenu(); }

        private void continueButton_OnPress(object sender, EventArgs e)
        { Main.setGameState(); }

        private void optionsButton_OnPress(object sender, EventArgs e)
        { options = true; }

        private void muteButton_OnPress(object sender, EventArgs e)
        { 
            if(Main.mute == false)
                Main.mute = true;
            else if (Main.mute == true)
                Main.mute = false;

            //Console.WriteLine(Main.mute);
        }

        private void backButton_OnPress(object sender, EventArgs e)
        { options = false; }

        public void Update(GameTime gameTime)
        {
            if (!options)
            {
                mainMenuButton.Update(gameTime);
                continueButton.Update(gameTime);
                //optionsButton.Update(gameTime);
                muteButton.Update(gameTime);
            }
            //else if (options) { muteButton.Update(gameTime); backButton.Update(gameTime); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!options)
            {
                spriteBatch.Draw(texture, position, Color.Red);
                mainMenuButton.Draw(spriteBatch);
                continueButton.Draw(spriteBatch);
                optionsButton.Draw(spriteBatch);
                muteButton.Draw(spriteBatch);
                if (Main.mute) { mutePressedButton.Draw(spriteBatch); }
            }
            /*else if (options) 
            { 
                spriteBatch.Draw(optionsMenuTexture, new Rectangle((level.Width * 32) / 2 - 110, (level.Height * 32) / 2 - 165, 220, 300), Color.Red);
                muteButton.Draw(spriteBatch); backButton.Draw(spriteBatch);
                if (Main.mute) { spriteBatch.Draw(mutePressedTexture, new Rectangle((level.Width * 32) / 2 - 100, (level.Height * 32) / 2 - 90, 200, 32), Color.White); }
            }*/
        }
    }
}
