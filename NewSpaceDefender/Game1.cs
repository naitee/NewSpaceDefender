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
        SceneGame3 sceneGame3;
        SceneGameSemiFinal sceneGameSemi;
        SceneShop sceneShop;
        SceneShop2 sceneShop2;
        SceneShop3 sceneShop3;
        SceneDead sceneDead;
        VictoryScene victoryScene;


        public int GameSence = 1;




        CharecterLasergun laserGun = new CharecterLasergun(1, 10, 2, 500, 3, 0);
        CharecterSpaceship spaceship = new CharecterSpaceship(100, 100);
        List<Meteorite> meteorites = new List<Meteorite>();
        List<CharecterInvader> Tangs = new List<CharecterInvader>();
        List<CharecterInvader> Arts = new List<CharecterInvader>();
        List<CharecterInvader> Bombs = new List<CharecterInvader>();
        List<CharecterInvader> Pluems = new List<CharecterInvader>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //MeteoritesSetting
            for (int i = 0; i < 5; i++)
            {
                meteorites.Add(new Meteorite(3));
            }
            //TangSetting
            for (int i = 0; i < 10; i++)
            {
                Tangs.Add(new CharecterInvader(5,5,1));
            }
            //ArtSetting
            for (int i = 0; i < 10; i++)
            {
                Arts.Add(new CharecterInvader(10,10, 3));
            }
            //BombSetting
            for (int i = 0; i < 10; i++)
            {
                Bombs.Add(new CharecterInvader(20,20, 9));
            }
            //PluemSetting
            for (int i = 0; i < 10; i++)
            {
                Pluems.Add(new CharecterInvader(1,1, 50));
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
            sceneGame3 = new SceneGame3(Content, ScreenSize);
            sceneShop = new SceneShop(Content, ScreenSize);
            sceneShop2 = new SceneShop2(Content, ScreenSize);
            sceneShop3 = new SceneShop3(Content, ScreenSize);
            sceneGameSemi = new SceneGameSemiFinal(Content, ScreenSize);
            sceneDead = new SceneDead(Content, ScreenSize);
            victoryScene = new VictoryScene(Content, ScreenSize);



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
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4)) Exit();
            if (sceneDead.exit== true) Exit();
            if (victoryScene.exit == true) Exit();


            //Start To Instruction
            if (SceneStart.Gamestart && SceneStart.ChangeMap) GameSence = 2;
            //Instuction To Game1
            if (sceneInstruction.Change) GameSence = 3;
            //Game1 To Shop
            if (sceneGame1.Stage1Pass) GameSence = 4;
            //Shop To Game 2
            if (sceneShop.ChangeStageTo2) GameSence = 5;
            //Game2 To Shop
            if (sceneGame2.Stage2Pass) GameSence = 6;
            //Shop To Game3
            if (sceneShop2.ChangeStageTo3) GameSence = 7;
            //Game3 To Shop
            if (sceneGame3.Stage3Pass) GameSence = 8;
            //Shop3 To SemiGame
            if (sceneShop3.ChangeStageTo4) GameSence = 9;
            //SemiGame To Shop
            if (sceneGameSemi.Stage4Pass) GameSence = 10;


            //Died at stage 1
            if (sceneGame1.Stage1fail || sceneGame2.Stage2fail||sceneGame3.Stage3fail||sceneGameSemi.Stage4fail) GameSence = 101;
            //Alive at Last Stage
            //if () GameSence = 102;

            // Restart
            if (sceneDead.restart)
            {
                sceneInstruction.Change = false;
                sceneGame1.Stage1Pass = false;
                sceneShop.ChangeStageTo2 = false;
                sceneGame2.Stage2Pass = false;
                sceneShop2.ChangeStageTo3 = false;
                sceneGame3.Stage3Pass = false;
                sceneShop3.ChangeStageTo4 = false;
                GameSence = 2;
            }
            if (victoryScene.restart)
            {
                sceneInstruction.Change = false;
                sceneGame1.Stage1Pass = false;
                sceneShop.ChangeStageTo2 = false;
                sceneGame2.Stage2Pass = false;
                sceneShop2.ChangeStageTo3 = false;
                sceneGame3.Stage3Pass = false;
                sceneShop3.ChangeStageTo4 = false;
                GameSence = 2;

            }

            base.Update(gameTime);

            
        }


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
                    sceneDead.restart = false;
                    victoryScene.restart = false;
                    sceneInstruction.Draw(spriteBatch);
                    sceneInstruction.Update(gameTime);
                    break;
                case 3:
                    sceneGame1.updateInvaderTang(Tangs);
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
                    sceneGame2.updateInvaderArt(Arts);
                    sceneGame2.updateMeteorite(meteorites);
                    sceneGame2.updateLasergun(laserGun);
                    sceneGame2.updateSpaceship(spaceship);
                    sceneGame2.Draw(spriteBatch);
                    sceneGame2.Update(gameTime);
                    break;
                case 6:
                    sceneShop2.updateLasergun(laserGun);
                    sceneShop2.updateSpaceship(spaceship);
                    sceneShop2.Draw(spriteBatch);
                    sceneShop2.Update(gameTime);
                    break;
                case 7:
                    sceneGame3.updateInvaderBomb(Bombs);
                    sceneGame3.updateMeteorite(meteorites);
                    sceneGame3.updateLasergun(laserGun);
                    sceneGame3.updateSpaceship(spaceship);
                    sceneGame3.Draw(spriteBatch);
                    sceneGame3.Update(gameTime);
                    break;
                case 8:
                    sceneShop3.updateLasergun(laserGun);
                    sceneShop3.updateSpaceship(spaceship);
                    sceneShop3.Draw(spriteBatch);
                    sceneShop3.Update(gameTime);
                    break;
                case 9:
                    sceneGameSemi.updateInvaderPluem(Pluems);
                    sceneGameSemi.updateMeteorite(meteorites);
                    sceneGameSemi.updateLasergun(laserGun);
                    sceneGameSemi.updateSpaceship(spaceship);
                    sceneGameSemi.Draw(spriteBatch);
                    sceneGameSemi.Update(gameTime);
                    break;
                case 10:

                    break;

                ///Lost Case
                case 101:
                    sceneDead.updateLasergun(laserGun);
                    sceneDead.Draw(spriteBatch);
                    sceneDead.Update(gameTime);
                    break;
                ///Alive Case
                case 102:
                    victoryScene.updateLasergun(laserGun);
                    victoryScene.Draw(spriteBatch);
                    victoryScene.Update(gameTime);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}