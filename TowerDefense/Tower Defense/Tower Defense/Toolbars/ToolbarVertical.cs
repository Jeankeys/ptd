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
    class ToolbarVertical 
    {

        private Texture2D texture;
        private Vector2 position;
        private SpriteFont font;
        private ContentManager contentManager;
        private Level level = new Level();
        private Player player;
        private WaveManager waveManager;

        Button charmanderButton; Button squirtleButton;
        Button jigglypuffButton; Button bulbasaurButton;
        Button pikachuButton; Button tangelaButton;
        Button dratiniButton; Button mewButton;

        Button startButton; 
        public Button pauseButton;

        float startButtonTimer;

        Charmander charmander; Squirtle squirtle;
        Bulbasaur bulbasaur; Pikachu pikachu; Jigglypuff jigglypuff;
        Dratini dratini; Tangela tangela; Mew mew;

        public ToolbarVertical(Texture2D texture, SpriteFont font, Vector2 position, 
                               Player player, WaveManager waveManager, ContentManager contentManager)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.player = player;
            this.waveManager = waveManager;
            this.contentManager = contentManager;

            LoadButtons(); //Initialize all of the buttons
        }

        public void LoadButtons()
        {
            Texture2D charmanderTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\charmanderButton");
            Texture2D squirtleTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\squirtleButton");
            Texture2D jigglypuffTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\jigglypuffButton");
            Texture2D bulbasaurTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\bulbasaurButton");
            Texture2D pikachuTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\pikachuButton");
            Texture2D tangelaTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\tangelaButton");
            Texture2D dratiniTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\dratiniButton");
            Texture2D mewTexture = contentManager.Load<Texture2D>(@"Images\GUI\ToolbarVerticalButtons\mewButton");

            Texture2D start = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\startButton");
            Texture2D startPressed = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\startButtonPressed");

            Texture2D pauseTexture = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\pauseButton");
            Texture2D pauseTexturePressed = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\pauseButtonPressed");

            bulbasaurButton = new Button(bulbasaurTexture, bulbasaurTexture, new Vector2(level.Width * 32 + 16.5f, 96), player);
            bulbasaurButton.OnPress += new EventHandler(bulbasaurButton_OnPress);
            bulbasaur = new Bulbasaur(bulbasaurTexture, null, new Vector2(0, 0));

            charmanderButton = new Button(charmanderTexture, charmanderTexture, new Vector2(level.Width * 32 + 16.5f, 32), player);
            charmanderButton.OnPress += new EventHandler(charmanderButton_OnPress);
            charmander = new Charmander(charmanderTexture, null, new Vector2(0, 0));

            squirtleButton = new Button(squirtleTexture, squirtleTexture, new Vector2(level.Width * 32 + 16.5f, 160), player);
            squirtleButton.OnPress += new EventHandler(squirtleButton_OnPress);
            squirtle = new Squirtle(squirtleTexture, null, new Vector2(0, 0));

            pikachuButton = new Button(pikachuTexture, pikachuTexture, new Vector2(level.Width * 32 + 16.5f, 224), player);
            pikachuButton.OnPress += new EventHandler(pikachuButton_OnPress);
            pikachu = new Pikachu(pikachuTexture, null, new Vector2(0, 0));

            jigglypuffButton = new Button(jigglypuffTexture, jigglypuffTexture, new Vector2(level.Width * 32 + 16.5f, 288), player);
            jigglypuffButton.OnPress += new EventHandler(jigglypuffButton_OnPress);
            jigglypuff = new Jigglypuff(jigglypuffTexture, null, null, new Vector2(0, 0));

            tangelaButton = new Button(tangelaTexture, tangelaTexture, new Vector2(level.Width * 32 + 16.5f, 345), player);
            tangelaButton.OnPress += new EventHandler(tangelaButton_OnPress);
            tangela = new Tangela(tangelaTexture, null, new Vector2(0,0));
            
            dratiniButton = new Button(dratiniTexture, dratiniTexture, new Vector2(level.Width * 32 + 16.5f, 407), player);
            dratiniButton.OnPress += new EventHandler(dratiniButton_OnPress);
            dratini = new Dratini(dratiniTexture, null, new Vector2(0, 0));

            mewButton = new Button(mewTexture, mewTexture, new Vector2(level.Width * 32 + 16.5f, 470), player);
            mewButton.OnPress += new EventHandler(mewButton_OnPress);
            mew = new Mew(mewTexture, null, new Vector2(0, 0));

            startButton = new Button(start, startPressed, new Vector2(level.Width * 32 + 16, level.Height * 32 - 16), player);
            startButton.OnPress += new EventHandler(startButton_OnPress);

            pauseButton = new Button(pauseTexture, pauseTexturePressed, new Vector2(level.Width * 32 + 16, level.Height * 32 - 55), player);
            pauseButton.OnPress += new EventHandler(pauseButton_OnPress);

        }

        //Methods for each button to make them work
        private void charmanderButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Charmander"; player.NewTowerIndex = 0; } //Tell the player class which tower to create

        private void squirtleButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Squirtle"; player.NewTowerIndex = 1; }

        private void bulbasaurButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Bulbasaur"; player.NewTowerIndex = 2; }

        private void pikachuButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Pikachu"; player.NewTowerIndex = 3; }

        private void jigglypuffButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Jigglypuff"; player.NewTowerIndex = 4; }

        private void tangelaButton_OnPress(object sender, EventArgs e)
        { 
            if(Tangela.tangelaCounter < 6)
                player.NewTowerType = "Tangela"; player.NewTowerIndex = 6; 
        }

        private void dratiniButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Dratini"; player.NewTowerIndex = 5; }

        private void mewButton_OnPress(object sender, EventArgs e)
        { player.NewTowerType = "Mew"; player.NewTowerIndex = 7; }

        private void pauseButton_OnPress(object sender, EventArgs e)
        { Main.pauseGame();  }

        private void startButton_OnPress(object sender, EventArgs e)
        {
            if (startButtonTimer > 1f) //Delay so the button cannot be clicked repeatedly
            {
                if (waveManager.CurrentWave.enemies.Count == 0) 
                { waveManager.waves.Dequeue(); waveManager.StartNextWave(); startButtonTimer = 0; }
            }
        }

        public void Update(GameTime gameTime)
        {
            startButtonTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; //Increment the delay timer
            //Draw all of the buttons
            charmanderButton.Update(gameTime); squirtleButton.Update(gameTime); jigglypuffButton.Update(gameTime);
            bulbasaurButton.Update(gameTime); pikachuButton.Update(gameTime);
            tangelaButton.Update(gameTime);
            dratiniButton.Update(gameTime);
            mewButton.Update(gameTime);

            startButton.Update(gameTime); pauseButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, position, Color.Red);
            //Update all of the buttons
            charmanderButton.Draw(spriteBatch); squirtleButton.Draw(spriteBatch); jigglypuffButton.Draw(spriteBatch);
            bulbasaurButton.Draw(spriteBatch); pikachuButton.Draw(spriteBatch);
            tangelaButton.Draw(spriteBatch);
            dratiniButton.Draw(spriteBatch);
            mewButton.Draw(spriteBatch);

            startButton.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Towers", new Vector2(level.Width * 32, 0), Color.Red);

            spriteBatch.DrawString(font, String.Format("${0}", bulbasaur.Cost), new Vector2(level.Width * 32 + 10, 125), Color.Red);//Bulbasaur
            spriteBatch.DrawString(font, String.Format("${0}", charmander.Cost), new Vector2(level.Width * 32 + 10, 60), Color.Red);//Charmander
            spriteBatch.DrawString(font, String.Format("${0}", squirtle.Cost), new Vector2(level.Width * 32 + 10, 190), Color.Red);//Squirtle
            spriteBatch.DrawString(font, String.Format("${0}", pikachu.Cost), new Vector2(level.Width * 32 + 10, 255), Color.Red);//Pikachu
            spriteBatch.DrawString(font, String.Format("${0}", jigglypuff.Cost), new Vector2(level.Width * 32 + 10, 315), Color.Red);//Jigglypuff
            spriteBatch.DrawString(font, String.Format("${0}", tangela.Cost), new Vector2(level.Width * 32 + 10, 375), Color.Red);//Tangela
            spriteBatch.DrawString(font, String.Format("${0}", dratini.Cost), new Vector2(level.Width * 32 + 10, 435), Color.Red);//Dratini
            spriteBatch.DrawString(font, String.Format("${0}", mew.Cost), new Vector2(level.Width * 32 + 10, 500), Color.Red);//Mew

            //spriteBatch.DrawString(font, "Start\nWave:", new Vector2(level.Width * 32 + 5, level.Height * 32 - 67), Color.Red);

            pauseButton.Draw(spriteBatch);
        }
    }
}
