using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace NewSpaceDefender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Vector2 ScreenSize = new Vector2(1024, 768);

        //Include Scene && scene setting
        SceneStart SceneStart;
        SceneInstruction sceneInstruction;
        SceneGame1 sceneGame1;
        SceneGame2 sceneGame2;
        SceneShop sceneShop;
        SceneDead sceneDead;

        
        public int GameSence = 1;




        CharecterLasergun laserGun = new CharecterLasergun(1, 10, 2, 40000, 3);
        CharecterSpaceship spaceship = new CharecterSpaceship(100, 100);
        List<Meteorite> meteorites = new List<Meteorite>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            for(int i = 0; i < 5; i++)
            {
                meteorites.Add(new Meteorite(3));
            }
            

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()

        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.ApplyChanges();
            ScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //Scene Setting
            SceneStart = new SceneStart(Content, ScreenSize);
            sceneInstruction = new SceneInstruction(Content, ScreenSize);
            sceneGame1 = new SceneGame1(Content, ScreenSize);
            sceneGame2 = new SceneGame2(Content, ScreenSize);
            sceneShop = new SceneShop(Content, ScreenSize);
            sceneDead = new SceneDead(Content, ScreenSize);



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ///Quick quit
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4))
                Exit();

            //Start To Instruction
            if (SceneStart.Gamestart && SceneStart.ChangeMap) GameSence = 2;
            //Instuction To Game1
            if (sceneInstruction.Change) GameSence = 3;
            //Game To Shop
            if (sceneGame1.Stage1Pass) GameSence += 1 ;
            //Shop To Game 2
            if (sceneShop.ChangeStageTo2) GameSence = 5;
            //Game2 To Shop
            if (sceneGame2.Stage2Pass) GameSence = 6;
            //Shop To Game3
            if (sceneShop.ChangeStageTo3) GameSence = 7;


            //Died at stage 1
            if (sceneGame1.Stage1fail||sceneGame2.Stage2fail) GameSence = 101;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (GameSence)
            {
                case 1:
                    SceneStart.Draw(spriteBatch);
                    SceneStart.Update(gameTime);
                    break;
                case 2:
                    sceneInstruction.Draw(spriteBatch);
                    sceneInstruction.Update(gameTime);
                    break;
                case 3:
                    sceneGame1.updateLasergun(laserGun);
                    sceneGame1.updateSpaceship(spaceship);
                    sceneGame1.updateMeteorite(meteorites);
                    sceneGame1.Draw(spriteBatch);
                    sceneGame1.Update(gameTime);
                    break;
                case 4:
                    sceneShop.updateLasergun(laserGun);
                    sceneShop.updateSpaceship(spaceship);
                    sceneShop.Draw(spriteBatch);
                    sceneShop.Update(gameTime);
                    break;
                case 5:
                    sceneGame2.updateMeteorite(meteorites);
                    sceneGame2.updateLasergun(laserGun);
                    sceneGame2.updateSpaceship(spaceship);
                    sceneGame2.Draw(spriteBatch);
                    sceneGame2.Update(gameTime);
                    break;
                case 6:
                    sceneShop.updateLasergun(laserGun);
                    sceneShop.updateSpaceship(spaceship);
                    sceneShop.Draw(spriteBatch);
                    sceneShop.Update(gameTime);
                    break;
                case 7:

                    

                ///Lost Case
                case 101:
                    sceneDead.Draw(spriteBatch);
                    sceneDead.Update(gameTime);
                    break;


            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}