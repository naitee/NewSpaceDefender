using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace NewSpaceDefender
{
    class SceneGame2
    {
        ContentManager Content;
        Vector2 ScreenSize;

        SpriteFont Clip;
        SpriteFont ShipHP;
        SpriteFont GameTime;

        Texture2D Spaceship;
        Texture2D Crosshair1;
        Texture2D[] Meteorite = new Texture2D[6];



        Rectangle CrosshairObj;
        Rectangle SpacshipObj;
        Rectangle[] MeteoriteObj = new Rectangle[6];

        CharecterLasergun lasergun;
        CharecterSpaceship spaceship;
        List<Meteorite> meteoriteClass = new List<Meteorite>();


        Vector2 ClipPos = new Vector2(680, 20);
        Vector2 HpPos = new Vector2(200, 20);
        Vector2 TimePos = new Vector2(680, 50);

        int[] MetroX = new int[10];
        int[] MetoY = new int[10];

        int timer = 0;
        int clip;
        int clipmax;
        int shiphp;
        int shiphpmax;
        bool CanClick = false;
        bool[] check = new bool[10];

        public bool Stage2Pass = false;
        public bool Stage2fail = false;

        public SceneGame2(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();

            for (int i = 0; i < 5; i++)
            {
                MeteoriteObj[i] = new Rectangle(-1000, 500 + 20 * i, Meteorite[i].Width / 15, Meteorite[i].Height / 15);
                check[i] = false;
            }
        }


        public void LoadContent()
        {
            Spaceship = Content.Load<Texture2D>("Game1Blade");
            Crosshair1 = Content.Load<Texture2D>("Game1Crosshair");



            for (int i = 0; i <= 5; i++)
            {
                Meteorite[i] = Content.Load<Texture2D>("Game1Meteorite");
            }



            GameTime = Content.Load<SpriteFont>("Game1Time");
            Clip = Content.Load<SpriteFont>("Game1Clip");
            ShipHP = Content.Load<SpriteFont>("Game1SpaceshipHP");
        }


        /// <summary>
        /// Update Value
        /// </summary>
        /// <param name="lasergun"></param>
        public void updateLasergun(CharecterLasergun lasergun)
        {
            this.lasergun = lasergun;
        }
        public void updateSpaceship(CharecterSpaceship spaceship)
        {
            this.spaceship = spaceship;
        }

        public void updateMeteorite(List<Meteorite> meteorites)
        {

            meteoriteClass = meteorites;

        }





        public void Update(GameTime gametime)
        {
            timer = timer + 1;
            clip = lasergun.clipleft;
            clipmax = lasergun.ClipMax;
            shiphp = spaceship.Hp;
            shiphpmax = spaceship.Maxhp;


            //Obj
            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair1.Width / 2, Crosshair1.Height / 2);
            SpacshipObj = new Rectangle(50, 200, Spaceship.Width, Spaceship.Width);





            for (int i = 0; i < 5; i++)
            {
                MeteoriteObj[i] = new Rectangle(MeteoriteObj[i].X + (i + 1), (MeteoriteObj[i].Y), Meteorite[i].Width / 3, Meteorite[i].Height / 3);
                if (MeteoriteObj[i].X > ScreenSize.X)
                {
                    switch (i % 4)
                    {
                        case 1:
                            MeteoriteObj[i].X = -1000;
                            break;
                        case 2:
                            MeteoriteObj[i].X = -800;
                            break;
                        case 3:
                            MeteoriteObj[i].X = -600;
                            break;
                        case 4:
                            MeteoriteObj[i].X = -400;
                            break;
                    }
                }

            }

            for (int i = 0; i < 5; i++)
            {
                if (CrosshairObj.Intersects(MeteoriteObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    meteoriteClass[i].Hp -= lasergun.Atk;
                }
                if (meteoriteClass[i].Hp <= 0)
                {
                    meteoriteClass[i].Destroy(lasergun);
                    check[i] = true;
                    meteoriteClass[i].Hp = 5;

                }
            }


            ///Dont SPAM CLICK
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                CanClick = false;
                lasergun.isShoot();

            }
            if (!CanClick && Mouse.GetState().LeftButton != ButtonState.Pressed)
            {
                CanClick = true;
            }
            ///Pass
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Stage2Pass = true;
            if (timer / 60 > 10) Stage2Pass = true;
            ///Failed
            if (spaceship.Hp == 0) Stage2fail = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Clip, "Magazine : " + meteoriteClass[0].Hp + " / " + meteoriteClass[1].Hp, ClipPos, Color.White);
            spriteBatch.DrawString(ShipHP, "Health : " + meteoriteClass[2].Hp + " / " + meteoriteClass[3].Hp, HpPos, Color.White);
            spriteBatch.DrawString(GameTime, "Time : " + meteoriteClass[4].Hp, TimePos, Color.White);


            for (int i = 0; i < 5; i++)
            {
                spriteBatch.Draw(Meteorite[i], MeteoriteObj[i], Color.White);
                if (check[i] == true)
                {
                    MeteoriteObj[i].X = -1000;
                    check[i] = false;
                }

            }




            spriteBatch.Draw(Spaceship, SpacshipObj, Color.White);
            spriteBatch.Draw(Crosshair1, CrosshairObj, Color.Red);
        }
    }
}