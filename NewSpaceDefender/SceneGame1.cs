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
    class SceneGame1
    {
        ContentManager Content;
        Vector2 ScreenSize;

        SpriteFont Clip;
        SpriteFont ShipHP;
        SpriteFont GameTime;

        Texture2D Spaceship;
        Texture2D Crosshair1;
        Texture2D Shot1;
        Texture2D Shot2;
        Texture2D[] Meteorite = new Texture2D[6];
        Texture2D[] Tang2D = new Texture2D[10];
        Texture2D[] Tang2DShoot = new Texture2D[10];
        Texture2D Background;

        Rectangle Shot1Obj;
        Rectangle Shot2Obj;
        Rectangle CrosshairObj;
        Rectangle SpacshipObj;
        Rectangle[] MeteoriteObj= new Rectangle[6];
        Rectangle[] InvaderTangObj = new Rectangle[10];

        CharecterLasergun lasergun;
        CharecterSpaceship spaceship;
        List<Meteorite> meteoriteClass = new List<Meteorite>();
        List<CharecterInvader> invaderTangClass = new List<CharecterInvader>();

        Vector2 ClipPos = new Vector2(680, 20);
        Vector2 HpPos = new Vector2(200, 20);
        Vector2 TimePos = new Vector2(680, 50);


        int speed;
        int timer = 0;
        int clip;
        int clipmax;
        int shiphp;
        int shiphpmax;
        int z;
        bool CanClick = false;
        bool[] check = new bool[10];
        bool[] CheckTang = new bool[20];

        public bool Stage1Pass = false;
        public bool Stage1fail = false;


        public SceneGame1(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();

            //Meteorite
            for (int i = 0; i < 5; i++)
            {
                MeteoriteObj[i] = new Rectangle(-1000, 500+20*i, Meteorite[i].Width / 15, Meteorite[i].Height / 15);
                check[i] = false;
            }

            //Tang
            for(int i =0;i < 6; i++)
            {                                       //400
                InvaderTangObj[i] = new Rectangle(3000, 100+50*i, Tang2D[i].Width/3, Tang2D[i].Height/3);
                CheckTang[i] = false;
            }
        }


        public void LoadContent()
        {
            Spaceship = Content.Load<Texture2D>("Game1Blade");
            Crosshair1 = Content.Load<Texture2D>("Game1Crosshair");
            Shot1 = Content.Load<Texture2D>("Game1Shot1");
            Shot2 = Content.Load<Texture2D>("Game1Shot2");
            Background = Content.Load<Texture2D>("War/1");
            //MeteoriteTexture2D
            for (int i =0; i<5; i++)
            {
                Meteorite[i] = Content.Load<Texture2D>("Game1Meteorite");
            }
            //InvaderTangTexture2D
            for(int i = 0; i < 10; i++)
            {
                Tang2D[i] = Content.Load<Texture2D>("TangSS");
            }
            for(int i =0; i < 10; i++)
            {
                Tang2DShoot[i] = Content.Load<Texture2D>("TangCC");
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
        public void updateInvaderTang(List<CharecterInvader> InvaderTang)
        {
            invaderTangClass = InvaderTang;
        }

        public void Update(GameTime gametime)
        {
            timer = timer + 1;
            clip = lasergun.clipleft;
            clipmax = lasergun.ClipMax;
            shiphp = spaceship.Hp;
            shiphpmax = spaceship.Maxhp;
            
                z+=timer/3600*60;
                z = z % 40 + 1;
                Background = Content.Load<Texture2D>("War/" + z.ToString());


            //Obj
            Shot1Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width/2, Shot1.Height/2);
            Shot2Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width / 2, Shot1.Height / 2);
            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair1.Width / 2, Crosshair1.Height / 2);
            SpacshipObj = new Rectangle(50, 200, Spaceship.Width, Spaceship.Width);
            
            //MeteoriteMoving
            for (int i = 0; i < 5; i++)
            { 
                MeteoriteObj[i] = new Rectangle(MeteoriteObj[i].X + (i+1), MeteoriteObj[i].Y , Meteorite[i].Width / 3, Meteorite[i].Height / 3);
                if(MeteoriteObj[i].X > ScreenSize.X)
                {
                    switch ( i % 4)
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
                    meteoriteClass[i].Hp = 3;
                }
                if (CrosshairObj.Intersects(MeteoriteObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
                {
                    meteoriteClass[i].Hp -= lasergun.Atk;
                }
                if (meteoriteClass[i].Hp <= 0)
                {
                    meteoriteClass[i].Destroy(lasergun);
                    check[i] = true;
                    meteoriteClass[i].Hp = 3;

                }
            }


            //TangMoving
            for (int i = 0; i < 6; i++)
            {

                switch (i % 3)
                {
                    case 0:
                        speed = 1;
                        break;
                    case 1:
                        speed = 2;
                        break;
                    case 2:
                        speed = 3;
                        break;
                }
                InvaderTangObj[i] = new Rectangle((InvaderTangObj[i].X-speed) , InvaderTangObj[i].Y, Tang2D[i].Width / 3, Tang2D[i].Height / 3);
                if(InvaderTangObj[i].X <= 400)
                {
                    InvaderTangObj[i].X = 400;

                    if (timer % 60 == 1)
                    {
                        spaceship.Hp -= invaderTangClass[i].InvaderAtk;
                    }
                    
                    
                }
                if(CrosshairObj.Intersects(InvaderTangObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
                {
                    invaderTangClass[i].InvaderHp -= lasergun.Atk;
                }
                if(invaderTangClass[i].InvaderHp <= 0)
                {
                    invaderTangClass[i].Destroy(lasergun);
                    CheckTang[i] = true;
                    invaderTangClass[i].InvaderHp = 10;
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Stage1Pass = true;
            if (timer/60 > 60) Stage1Pass = true;
            ///Failed
            if (spaceship.Hp <= 0) Stage1fail = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(Clip, "Magazine : " + clip+ " / " + clipmax, ClipPos, Color.White);
            spriteBatch.DrawString(ShipHP, "Health : " + shiphp + " / " + shiphpmax, HpPos, Color.White);
            spriteBatch.DrawString(GameTime, "Time : " + timer/60, TimePos, Color.White);
            
            //Meteorite
            for (int i = 0;i < 5; i++)
                {
                    spriteBatch.Draw(Meteorite[i], MeteoriteObj[i], Color.White);
                    if(check[i] == true)
                    {
                        MeteoriteObj[i].X = -1000;
                        check[i] = false;
                    }
                    
                }
            //Tang
            for(int i = 0; i < 6; i++)
            {
                spriteBatch.Draw(Tang2D[i], InvaderTangObj[i], Color.White);
                if(CheckTang[i]== true)
                {
                    InvaderTangObj[i].X = 3000;
                    CheckTang[i] = false;
                }
                if(InvaderTangObj[i].X == 400)
                {
                        if (timer%60<=4)
                        {
                            spriteBatch.Draw(Tang2DShoot[i], InvaderTangObj[i], Color.White);
                        }
                    
                }
            }

            spriteBatch.Draw(Spaceship, SpacshipObj, Color.White);
            spriteBatch.Draw(Crosshair1, CrosshairObj, Color.Red);

            //ShootingAnimation
            if(Mouse.GetState().LeftButton == ButtonState.Pressed&&clip !=0)
            {
                spriteBatch.Draw(Shot2, Shot2Obj, Color.White);
            }
            else
            {
                spriteBatch.Draw(Shot1, Shot1Obj, Color.White);
            }
  
        }
    }

}
