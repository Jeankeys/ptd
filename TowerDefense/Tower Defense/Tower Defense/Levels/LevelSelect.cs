using System;
using System.IO;
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
    class LevelSelect
    {
        ContentManager contentManager; SpriteFont font; Player player; 

        //The map in game
        Texture2D DarkPokemonCity; Texture2D LightPokemonCity;
        Texture2D ViridianCity; Texture2D AzaleaTown; Texture2D Route8; 

        //Start screen buttons
        Button DarkCitySelect; Button LightCitySelect;
        Button ViridianCitySelect; Button AzaleaTownSelect; Button Route8Select; 

        //The start screen image
        Texture2D DarkCityTexture; Texture2D LightCityTexture; Texture2D ViridianTexture;
        Texture2D AzaleaTexture; Texture2D Route8Texture; 

        //Start screen image to show which one is selected
        Texture2D DarkCityPressedTexture; Texture2D LightCityPressedTexture;
        Texture2D ViridianPressedTexture; Texture2D AzaleaPressedTexture; Texture2D Route8PressedTexture;

        //Paths for each map
        Texture2D DarkCityPath; Texture2D LightCityPath; 
        Texture2D ViridianPath; Texture2D AzaleaPath; Texture2D Route8Path;

        Button startButton; Texture2D startTexture;

        public LevelSelect(ContentManager contentManager, SpriteFont font, Player player)
        {
            this.contentManager = contentManager;
            this.font = font;
            this.player = player;

            LoadLevelSelect();
        }

        public void LoadLevelSelect()
        {
            DarkPokemonCity = contentManager.Load<Texture2D>(@"Images\Levels\DarkPokemonCity\DarkPokemonCity");
            LightPokemonCity = contentManager.Load<Texture2D>(@"Images\Levels\LightPokemonCity\LightPokemonCity");
            ViridianCity = contentManager.Load<Texture2D>(@"Images\Levels\ViridianCity\ViridianCity");
            AzaleaTown = contentManager.Load<Texture2D>(@"Images\Levels\AzaleaTown\AzaleaTown");
            Route8 = contentManager.Load<Texture2D>(@"Images\Levels\Route8\Route8");

            DarkCityTexture = contentManager.Load<Texture2D>(@"Images\Levels\DarkPokemonCity\DarkCityStart");
            DarkCityPressedTexture = contentManager.Load<Texture2D>(@"Images\Levels\DarkPokemonCity\DarkCityStartPressed");
            LightCityTexture = contentManager.Load<Texture2D>(@"Images\Levels\LightPokemonCity\LightCityStart");
            LightCityPressedTexture = contentManager.Load<Texture2D>(@"Images\Levels\LightPokemonCity\LightCityStartPressed");
            ViridianTexture = contentManager.Load<Texture2D>(@"Images\Levels\ViridianCity\ViridianCityStart");
            ViridianPressedTexture = contentManager.Load<Texture2D>(@"Images\Levels\ViridianCity\ViridianCityStartPressed");
            AzaleaTexture = contentManager.Load<Texture2D>(@"Images\Levels\AzaleaTown\AzaleaTownStart");
            AzaleaPressedTexture = contentManager.Load<Texture2D>(@"Images\Levels\AzaleaTown\AzaleaTownStartPressed");
            Route8Texture = contentManager.Load<Texture2D>(@"Images\Levels\Route8\Route8Start");
            Route8PressedTexture = contentManager.Load<Texture2D>(@"Images\Levels\Route8\Route8StartPressed");

            DarkCityPath = contentManager.Load<Texture2D>(@"Images\Levels\DarkPokemonCity\DarkCityPath");
            LightCityPath = contentManager.Load<Texture2D>(@"Images\Levels\LightPokemonCity\LightCityPath");
            ViridianPath = contentManager.Load<Texture2D>(@"Images\Levels\ViridianCity\ViridianCityPath");
            AzaleaPath = contentManager.Load<Texture2D>(@"Images\Levels\AzaleaTown\AzaleaPath");
            Route8Path = contentManager.Load<Texture2D>(@"Images\Levels\Route8\Route8Path");

            startTexture = contentManager.Load<Texture2D>(@"Images\Levels\StartButton");

            DarkCitySelect = new Button(DarkCityTexture, DarkCityPressedTexture, new Vector2(40, 450), player);
            DarkCitySelect.OnPress += new EventHandler(levelSelectButton_OnPress);
            LightCitySelect = new Button(LightCityTexture, LightCityPressedTexture, new Vector2(210, 450), player);
            LightCitySelect.OnPress += new EventHandler(levelSelectButton_OnPress);
            ViridianCitySelect = new Button(ViridianTexture, ViridianPressedTexture, new Vector2(380, 450), player);
            ViridianCitySelect.OnPress += new EventHandler(levelSelectButton_OnPress);
            AzaleaTownSelect = new Button(AzaleaTexture, AzaleaPressedTexture, new Vector2(550, 450), player);
            AzaleaTownSelect.OnPress += new EventHandler(levelSelectButton_OnPress);
            Route8Select = new Button(Route8Texture, Route8PressedTexture, new Vector2(720, 450), player);
            Route8Select.OnPress += new EventHandler(levelSelectButton_OnPress);

            startButton = new Button(startTexture, startTexture, new Vector2(400, 600), player);
            startButton.OnPress += new EventHandler(startButton_OnPress);
            
            Main.level.AddTexture(DarkCityPath);
            Main.level.AddTexture(LightCityPath);
            Main.level.AddTexture(ViridianPath);
            Main.level.AddTexture(AzaleaPath);
            Main.level.AddTexture(Route8Path);

        }

        private void levelSelectButton_OnPress(object sender, EventArgs e)
        {
            if (sender == DarkCitySelect)
                Main.levelSelect = "Level 1"; 
            if (sender == LightCitySelect)
                Main.levelSelect = "Level 2"; 
            if (sender == ViridianCitySelect)
                Main.levelSelect = "Level 3"; 
            if (sender == AzaleaTownSelect)
                Main.levelSelect = "Level 4"; 
            if (sender == Route8Select)
                Main.levelSelect = "Level 5"; 
        }

        private void startButton_OnPress(object sender, EventArgs e)
        {
            if (Main.levelSelect != null)
                Main.setGameState(); Main.level.LoadLevel();
        }

        public void Update(GameTime gameTime)
        {
            DarkCitySelect.Update(gameTime); LightCitySelect.Update(gameTime); ViridianCitySelect.Update(gameTime);
            AzaleaTownSelect.Update(gameTime); Route8Select.Update(gameTime);
            startButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DarkCitySelect.Draw(spriteBatch); LightCitySelect.Draw(spriteBatch); ViridianCitySelect.Draw(spriteBatch);
            AzaleaTownSelect.Draw(spriteBatch); Route8Select.Draw(spriteBatch);
            startButton.Draw(spriteBatch);

            if (Main.levelSelect != null)
            {
                if (Main.levelSelect.Equals("Level 1"))
                { spriteBatch.Draw(DarkCityPressedTexture, new Rectangle(40, 450, 156, 120), Color.White); Main.map = DarkPokemonCity; }
                if (Main.levelSelect.Equals("Level 2"))
                { spriteBatch.Draw(LightCityPressedTexture, new Rectangle(210, 450, 156, 120), Color.White); Main.map = LightPokemonCity; }
                if (Main.levelSelect.Equals("Level 3"))
                { spriteBatch.Draw(ViridianPressedTexture, new Rectangle(380, 450, 156, 120), Color.White); Main.map = ViridianCity; }
                if (Main.levelSelect.Equals("Level 4"))
                { spriteBatch.Draw(AzaleaPressedTexture, new Rectangle(550, 450, 156, 120), Color.White); Main.map = AzaleaTown; }
                if (Main.levelSelect.Equals("Level 5"))
                { spriteBatch.Draw(Route8PressedTexture, new Rectangle(720, 450, 156, 120), Color.White); Main.map = Route8; }
            }
        }
    }
}
