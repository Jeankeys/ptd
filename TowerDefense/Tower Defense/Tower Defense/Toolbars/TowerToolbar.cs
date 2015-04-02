using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tower_Defense
{
    class TowerToolbar
    {
        private Texture2D texture;
        private SpriteFont font;
        private Vector2 position;

        private ContentManager contentManager;
        private Level level = new Level();
        private Player player;
        private WaveManager waveManager;

        public int index; public string type;
        Texture2D removeTexture; Texture2D levelupTexture; Texture2D minimizeTexture;

        Button removeButton; Button levelupButton;

        Button charmeleonButton; Button charizardButton;
        Button wartortleButton; Button blastoiseButton;
        Button ivysaurButton; Button venusaurButton;
        Button raichuButton;
        Button dragonairButton; Button dragoniteButton;
        Button mewtwoButton;
        Button wigglytuffButton;

        Texture2D charmeleonTexture; Texture2D charizardTexture;
        Texture2D wartortleTexture; Texture2D blastoiseTexture;
        Texture2D ivysaurTexture; Texture2D venusaurTexture;
        Texture2D raichuTexture;
        Texture2D dragonairTexture; Texture2D dragoniteTexture;
        Texture2D mewtwoTexture;
        Texture2D wigglytuffTexture;

        Texture2D charmeleonTexture2; Texture2D charizardTexture2;
        Texture2D wartortleTexture2; Texture2D blastoiseTexture2;
        Texture2D ivysaurTexture2; Texture2D venusaurTexture2;
        Texture2D raichuTexture2;
        Texture2D dragonairTexture2; Texture2D dragoniteTexture2;
        Texture2D mewtwoTexture2;
        Texture2D wigglytuffTexture2;

        //Height for all the textures in the first row of the toolbar
        private int rowOneHeight = 93;
        //Height for all the textures in the second row of the toolbar
        private int rowTwoHeight = 60;
        //Height for all the textures in the third row of the toolbar
        private int rowThreeHeight = 32;

        public TowerToolbar(Texture2D texture, SpriteFont font, Vector2 position, 
                               Player player, WaveManager waveManager, ContentManager contentManager)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.player = player;
            this.waveManager = waveManager;
            this.contentManager = contentManager;

            LoadButtons();
        }

        public void LoadButtons()
        {
            removeTexture = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\removeButton");
            levelupTexture = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\levelup");
            minimizeTexture = contentManager.Load<Texture2D>(@"Images\GUI\Toolbars\removeButton");

            charmeleonTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\charmeleonButton");
            charizardTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\charizardButton");
            wartortleTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\wartortleButton");
            blastoiseTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\blastoiseButton");
            ivysaurTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\ivysaurButton");
            venusaurTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\venusaurButton");
            raichuTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\raichuButton");
            dragonairTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\dragonairButton");
            dragoniteTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\dragoniteButton");
            mewtwoTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\mewtwoButton");
            wigglytuffTexture = contentManager.Load<Texture2D>(@"Images\GUI\TowerToolbarButtons\wigglytuffButton");

            //transparent towers
            charmeleonTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\charmeleonTower");
            charizardTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\charizardTower");
            wartortleTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\wartortleTower");
            blastoiseTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\blastoiseTower");
            ivysaurTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\ivysaurTower");
            venusaurTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\venusaurTower");
            raichuTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\raichuTower");
            dragonairTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\dragonairTower");
            dragoniteTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\dragoniteTower");
            mewtwoTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\mewtwoTower");
            wigglytuffTexture2 = contentManager.Load<Texture2D>(@"Images\Towers\wigglytuffTower");

            removeButton = new Button(removeTexture, removeTexture, new Vector2(level.Width * 32 - 30, level.Height * 32 - rowOneHeight + 3), player);
            removeButton.OnPress += new EventHandler(removeButton_OnPress);
            levelupButton = new Button(levelupTexture, levelupTexture, new Vector2(level.Width * 32 - 580, level.Height * 32 - rowOneHeight), player);
            levelupButton.OnPress += new EventHandler(levelupButton_OnPress);

            charmeleonButton = new Button(charmeleonTexture, charmeleonTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            charmeleonButton.OnPress += new EventHandler(evolve);
            charizardButton = new Button(charizardTexture, charizardTexture, new Vector2(level.Width * 32 - 340, level.Height * 32 - rowOneHeight), player);
            charizardButton.OnPress += new EventHandler(evolve);

            wartortleButton = new Button(wartortleTexture, wartortleTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            wartortleButton.OnPress += new EventHandler(evolve);
            blastoiseButton = new Button(blastoiseTexture, blastoiseTexture, new Vector2(level.Width * 32 - 340, level.Height * 32 - rowOneHeight), player);
            blastoiseButton.OnPress += new EventHandler(evolve);

            ivysaurButton = new Button(ivysaurTexture, ivysaurTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            ivysaurButton.OnPress += new EventHandler(evolve);
            venusaurButton = new Button(venusaurTexture, venusaurTexture, new Vector2(level.Width * 32 - 340, level.Height * 32 - rowOneHeight), player);
            venusaurButton.OnPress += new EventHandler(evolve);

            raichuButton = new Button(raichuTexture, raichuTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            raichuButton.OnPress += new EventHandler(evolve);

            dragonairButton = new Button(dragonairTexture, dragonairTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            dragonairButton.OnPress += new EventHandler(evolve);
            dragoniteButton = new Button(dragoniteTexture, dragoniteTexture, new Vector2(level.Width * 32 - 340, level.Height * 32 - rowOneHeight), player);
            dragoniteButton.OnPress += new EventHandler(evolve);

            mewtwoButton = new Button(mewtwoTexture, mewtwoTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            mewtwoButton.OnPress += new EventHandler(evolve);

            wigglytuffButton = new Button(wigglytuffTexture, wigglytuffTexture, new Vector2(level.Width * 32 - 390, level.Height * 32 - rowOneHeight), player);
            wigglytuffButton.OnPress += new EventHandler(evolve);

        }

        private void removeButton_OnPress(object sender, EventArgs e)
        {
            player.Money += (int)(player.towers[index].Cost / 2) * player.towers[index].evolution + player.towers[index].level; 
            player.towers.RemoveAt(index);
            player.buttons.RemoveAt(index);
            player.infoList.RemoveAt(index);
            Main.show = false;
        }

        private void levelupButton_OnPress(object sender, EventArgs e)
        {
            if (player.towers[index].level < 100)
            {
                ++player.towers[index].level;
                //Main.levelup.Play();
            }
        }

        private void evolve(object sender, EventArgs e)
        {
            if (sender == charmeleonButton) //Charmander
            {
                if (player.towers[index].Evolution == 1 && player.towers[index].level >= 16 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = charmeleonTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }
            if (sender == charizardButton) 
            {
                if (player.towers[index].Evolution == 2 && player.towers[index].level >= 36 && player.Money >= player.towers[index].EvolutionTwoCost)
                { player.towers[index].texture = charizardTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionTwoCost; }
            }
            if (sender == wartortleButton) //Squirtle
            {
                if (player.towers[index].Evolution == 1 && player.towers[index].level >= 16 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = wartortleTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }
            if (sender == blastoiseButton) 
            {
                if (player.towers[index].Evolution == 2 && player.towers[index].level >= 36 && player.Money >= player.towers[index].EvolutionTwoCost)
                { player.towers[index].texture = blastoiseTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionTwoCost; }
            }
            if (sender == ivysaurButton) //Bulbasaur
            {
                if (player.towers[index].Evolution == 1 && player.towers[index].level >= 16 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = ivysaurTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }
            if (sender == venusaurButton) 
            {
                if (player.towers[index].Evolution == 2 && player.towers[index].level >= 32 && player.Money >= player.towers[index].EvolutionTwoCost)
                { player.towers[index].texture = venusaurTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionTwoCost; }
            }
            if (sender == raichuButton) //Pikachu
            {
                if (player.towers[index].Evolution == 1 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = raichuTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }

            if (sender == wigglytuffButton) //Jigglypuff
            {
                if (player.towers[index].Evolution == 1 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = wigglytuffTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }

            if (sender == dragonairButton) //Dratini
            {
                if (player.towers[index].Evolution == 1 && player.towers[index].level >= 30 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = dragonairTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }
            if (sender == dragoniteButton) 
            {
                if (player.towers[index].Evolution == 2 && player.towers[index].level >= 55 && player.Money >= player.towers[index].EvolutionTwoCost)
                { player.towers[index].texture = dragoniteTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionTwoCost; }
            }

            if (sender == mewtwoButton) //Mew
            {
                if (player.towers[index].Evolution == 1 && player.towers[index].level >= 50 && player.Money >= player.towers[index].EvolutionOneCost)
                { player.towers[index].texture = mewtwoTexture2; ++player.towers[index].evolution; player.Money -= player.towers[index].EvolutionOneCost; }
            }
        }

        public void Update(GameTime gameTime)
        {
            removeButton.Update(gameTime);
            //levelupButton.Update(gameTime);
            if (Main.show == true)
            {
                //Update the buttons based on which tower is clicked
                switch (type)
                {
                    case "Charmander": case "Charmeleon": case "Charizard":
                        {
                            if (player.towers[index].evolution == 2) { type = "Charmeleon"; }
                            if (player.towers[index].evolution == 3) { type = "Charizard"; }
                            charmeleonButton.Update(gameTime); charizardButton.Update(gameTime);
                            break;
                        }
                    case "Squirtle": case "Wartortle": case "Blastoise":
                        {
                            if (player.towers[index].evolution == 2) { type = "Wartortle"; }
                            if (player.towers[index].evolution == 3) { type = "Blastoise"; }
                            wartortleButton.Update(gameTime); blastoiseButton.Update(gameTime);
                            break;
                        }
                    case "Bulbasaur": case "Ivysaur": case "Venusaur":
                        {
                            if (player.towers[index].evolution == 2) { type = "Ivysaur"; }
                            if (player.towers[index].evolution == 3) { type = "Venusaur"; }
                            ivysaurButton.Update(gameTime); venusaurButton.Update(gameTime);
                            break;
                        }
                    case "Pikachu": case "Raichu":
                        {
                            if (player.towers[index].evolution == 2) { type = "Raichu"; }
                            raichuButton.Update(gameTime);
                            break;
                        }
                    case "Dratini": case "Dragonair": case "Dragonite":
                        {
                            if (player.towers[index].evolution == 2) { type = "Dragonair"; }
                            if (player.towers[index].evolution == 3) { type = "Dragonite"; }
                            dragonairButton.Update(gameTime); dragoniteButton.Update(gameTime);
                            break;
                        }
                    case "Mew": case "Mewtwo":
                        {
                            if (player.towers[index].evolution == 2) { type = "Mewtwo"; }
                            mewtwoButton.Update(gameTime);
                            break;
                        }
                    case "Jigglypuff": case "Wigglytuff":
                        {
                            if (player.towers[index].evolution == 2) { type = "Wigglytuff"; }
                            wigglytuffButton.Update(gameTime);
                            break;
                        }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Red);
            removeButton.Draw(spriteBatch);
            //levelupButton.Draw(spriteBatch);

            //Draw the buttons based on which tower is clicked
            switch (type)
            {
                case "Charmander": case "Charmeleon": case "Charizard":
                    { charmeleonButton.Draw(spriteBatch); charizardButton.Draw(spriteBatch); break; }

                case "Squirtle": case "Wartortle": case "Blastoise":
                    { wartortleButton.Draw(spriteBatch); blastoiseButton.Draw(spriteBatch); break; }

                case "Bulbasaur": case "Ivysaur": case "Venusaur":
                    { ivysaurButton.Draw(spriteBatch); venusaurButton.Draw(spriteBatch); break; }

                case "Pikachu": case "Raichu":
                    { raichuButton.Draw(spriteBatch); break; }

                case "Dratini": case "Dragonair": case "Dragonite":
                    { dragonairButton.Draw(spriteBatch); dragoniteButton.Draw(spriteBatch); break; }

                case "Mew": case "Mewtwo":
                    { mewtwoButton.Draw(spriteBatch); break; }

                case "Jigglypuff": case "Wigglytuff":
                    { wigglytuffButton.Draw(spriteBatch); break; }

            }

            spriteBatch.DrawString(font, String.Format("[{0}]", type),
                                   new Vector2(10, level.Height * 32 - rowOneHeight), Color.Red);

            spriteBatch.DrawString(font, String.Format("Level: {0}", player.towers[index].level),
                                   new Vector2(165, level.Height * 32 - rowOneHeight), Color.Red);

            if (!type.Equals("Tangela"))
            {
                spriteBatch.DrawString(font, "Evolutions:", new Vector2(335, level.Height * 32 - rowOneHeight), Color.Red);
                spriteBatch.DrawString(font, String.Format("${0}", player.towers[index].EvolutionOneCost), new Vector2(level.Width * 32 - 415, level.Height * 32 - rowTwoHeight), Color.Red);
                if(player.towers[index].EvolutionTwoCost != 0)
                    spriteBatch.DrawString(font, String.Format("${0}", player.towers[index].EvolutionTwoCost), new Vector2(level.Width * 32 - 345, level.Height * 32 - rowTwoHeight), Color.Red);
            }

            spriteBatch.DrawString(font, "Remove Tower:", new Vector2(690, level.Height * 32 - rowOneHeight), Color.Red);

            spriteBatch.DrawString(font, String.Format("Location:{0}", player.towers[index].Position),
                                   new Vector2(10, level.Height * 32 - rowTwoHeight), Color.Red);

            spriteBatch.DrawString(font, String.Format("Index:{0}", index), new Vector2(10, level.Height * 32 - rowThreeHeight), Color.Red);

            /*spriteBatch.DrawString(font, String.Format("Exp:{0}       Next Level:{1}       Kill Count:{2}", 
                                                        player.towers[index].experience, (int)player.towers[index].toNextLevel, (int)player.towers[index].killCount), 
                                                        new Vector2(310, level.Height * 32 - 32), Color.Red);*/

            spriteBatch.DrawString(font, String.Format("Exp:{0}", player.towers[index].experience), new Vector2(290, level.Height * 32 - rowThreeHeight), Color.Red);
            spriteBatch.DrawString(font, String.Format("Next Level:{0}", (int)player.towers[index].toNextLevel), new Vector2(460, level.Height * 32 - rowThreeHeight), Color.Red);
            spriteBatch.DrawString(font, String.Format("Kill Count:{0}", (int)player.towers[index].killCount), new Vector2(670, level.Height * 32 - rowThreeHeight), Color.Red);
        }
    }
}
