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
    class Player
    {
        //Total money the player starts the game with
        private int money = 1000;
        //Number of lives the player starts with
        public static int lives = 250;

        public List<Tower> towers = new List<Tower>();

        public List<Button> buttons = new List<Button>();

        WaveManager waveManager;

        ContentManager contentManager;

        SpriteFont font;

        Button towerButton = null;

        private static MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        public int Money
        { get { return money; } set { money = value; } }

        public int Lives
        { get { return lives; } set { lives = value; } }

        private Level level;

        // The textures used to draw our tower.
        private Texture2D[] towerTextures;
        private Texture2D[] badTowerTextures;
        private Texture2D[] bulletTextures;
        private Texture2D[] musicNoteTextures;

        public Player(Level level) { this.level = level; }

        /// <summary>
        /// Construct a new player.
        /// </summary>
        public Player(Level level, Texture2D[] towerTextures, Texture2D[] badTowerTextures, Texture2D[] bulletTextures, 
                            Texture2D[] musicNoteTextures, WaveManager waveManager, ContentManager contentManager, SpriteFont font)
        {
            this.level = level;

            this.towerTextures = towerTextures;
            this.badTowerTextures = badTowerTextures;
            this.bulletTextures = bulletTextures;
            this.musicNoteTextures = musicNoteTextures;
            this.waveManager = waveManager;
            this.contentManager = contentManager;
            this.font = font;

            towers.Clear();
            buttons.Clear();
            infoList.Clear();

        }

        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        private void towerButton_OnPress(object sender, EventArgs e)    
        {
            Vector2 location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
            //Find the type and index of the tower
            for (int i = 0; i < infoList.Count(); i++)
            {
                if (infoList[i].location.Equals(location))
                { 
                    Main.type = infoList[i].type;
                    Main.index = i;
                }
            }
            Main.show = true;
        }

        public List<Main.TowerInfo> infoList = new List<Main.TowerInfo>();

        public void AddTower()
        {
            Tower towerToAdd = null;
            Main.TowerInfo info = new Main.TowerInfo();


            Texture2D buttonTexture = contentManager.Load<Texture2D>(@"Images\GUI\button_clipped_rev_1");

            //Checks to see which tower is meant to be added
            switch (newTowerType)
            {

                case "Charmander":
                    {
                        //Add a small button so the towers can be clicked on
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        //Add the tower to the list 
                        towerToAdd = new Charmander(towerTextures[0],
                            bulletTextures, new Vector2(tileX, tileY));

                        //Store the info for each tower
                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Charmander";

                        break;
                    }
                case "Squirtle":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Squirtle(towerTextures[1],
                            bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Squirtle";

                        break;
                    }
                case "Bulbasaur":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Bulbasaur(towerTextures[2],
                            bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Bulbasaur";

                        break;
                    }
                case "Pikachu":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Pikachu(towerTextures[3],
                            bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Pikachu";

                        break;
                    }
                case "Jigglypuff":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Jigglypuff(towerTextures[4],
                             bulletTextures, musicNoteTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Jigglypuff";

                        break;
                    }
                case "Dratini":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Dratini(towerTextures[5],
                             bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Dratini";

                        break;
                    }
                case "Tangela":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Tangela(towerTextures[6],
                             bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Tangela";

                        ++Tangela.tangelaCounter;
                        break;
                    }
                case "Mew":
                    {
                        towerButton = new Button(buttonTexture, buttonTexture, new Vector2(tileX, tileY), new Player(level));
                        towerButton.OnPress += towerButton_OnPress;

                        towerToAdd = new Mew(towerTextures[7],
                             bulletTextures, new Vector2(tileX, tileY));

                        info.location = new Vector2((int)(mouseState.X / 32), (int)(mouseState.Y / 32));
                        info.index = towers.Count();
                        info.type = "Mew";

                        break;
                    }
                    
            }
            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= money)
            {
                towers.Add(towerToAdd);
                buttons.Add(towerButton);
                infoList.Add(info);
                money -= towerToAdd.Cost;

                // Reset the newTowerType field.
                newTowerType = string.Empty;
            }

            else
            {
                newTowerType = string.Empty;
            }
        }

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towers) // Check that there is no tower here
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)

                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space

            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space

            

            if (mouseState.LeftButton == ButtonState.Released
                && oldState.LeftButton == ButtonState.Pressed)
            {

                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }
            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.

            foreach (Tower tower in towers)
            {
                // Make sure the tower has no targets.
                if (tower.HasTarget == false)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
                Main.show = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }

        // The type of tower to add.
        private string newTowerType;

        public string NewTowerType
        { set { newTowerType = value; } }

        // The index of the new towers texture.
        private int newTowerIndex;

        public int NewTowerIndex
        {  set { newTowerIndex = value; } }

        //Draw the sprite while its being dragged onto the map
        public void DrawPreview(SpriteBatch spriteBatch)
        {
            // Draw the tower preview.
            if (string.IsNullOrEmpty(newTowerType) == false)
            {
                int cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
                int cellY = (int)(mouseState.Y / 32); // from array space to level space

                int tileX = cellX * 32; // Convert from array space to level space
                int tileY = cellY * 32; // Convert from array space to level space

                if (IsCellClear())
                {
                    Texture2D previewTexture = towerTextures[newTowerIndex];
                    spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY,
                        previewTexture.Width, previewTexture.Height), Color.White);
                }
                else
                {
                    Texture2D previewTexture = badTowerTextures[newTowerIndex];
                    spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY,
                        previewTexture.Width, previewTexture.Height), Color.White);
                }
            }
        }

    }
}
