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
        SpriteFont Bomb;

        Texture2D Spaceship;
        Texture2D Spaceship2;
        Texture2D Spaceship3;
        Texture2D Spaceship4;
        Texture2D Crosshair1;
        Texture2D Shot1;
        Texture2D Shot2;
        Texture2D[] Meteorite = new Texture2D[6];
        Texture2D[] MeteoriteDestroyed = new Texture2D[6];
        Texture2D[] Tang2D = new Texture2D[10];
        Texture2D[] Tang2DShoot = new Texture2D[10];
        Texture2D[] InvaderDead = new Texture2D[10];
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
        Vector2 HpPos = new Vector2(0, 20);
        Vector2 TimePos = new Vector2(680, 80);
        Vector2 BombPos = new Vector2(680, 50);


        int z;
        int speed;
        int timer = 0;
        int LvShip;
        int clip;
        int clipmax;
        int shiphp;
        int shiphpmax;

        bool Bombing = true;
        bool CanClick = false;
        bool[] check = new bool[10];
        bool[] CheckTang = new bool[20];

        bool[] CheckMeteorite = new bool[5];
        bool[] CheckInvader = new bool[10];
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
            {                                      
                InvaderTangObj[i] = new Rectangle(3000, 100+50*i, Tang2D[i].Width/3, Tang2D[i].Height/3);
                CheckTang[i] = false;
            }
        }


        public void LoadContent()
        {
            Spaceship = Content.Load<Texture2D>("ship");
            Spaceship2 = Content.Load<Texture2D>("ship2");
            Spaceship3 = Content.Load<Texture2D>("ship3");
            Spaceship4 = Content.Load<Texture2D>("ship4");
            Crosshair1 = Content.Load<Texture2D>("Game1Crosshair");
            Shot1 = Content.Load<Texture2D>("Game1Shot1");
            Shot2 = Content.Load<Texture2D>("Game1Shot2");
            Background = Content.Load<Texture2D>("War/1");
            
            //MeteoriteTexture2D
            for (int i = 0; i<5; i++)
            {
                Meteorite[i] = Content.Load<Texture2D>("Game1Meteorite");
            }
            for (int i = 0; i<5; i++)
            {
                MeteoriteDestroyed[i] = Content.Load<Texture2D>("Game1MeteoriteDestroy");
            }
            //InvaderTangTexture2D
            for(int i = 0; i < 10; i++)
            {
                Tang2D[i] = Content.Load<Texture2D>("TangSS");
            }
            for(int i = 0; i < 10; i++)
            {
                Tang2DShoot[i] = Content.Load<Texture2D>("TangCC");
            }
            for(int i = 0; i < 10; i++)
            {
                InvaderDead[i] = Content.Load<Texture2D>("skull");
            }

            Bomb = Content.Load<SpriteFont>("Game1Bomb");
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
           
            //MovingBackground
            z += timer / 3600 * 60;
            z = z % 40 + 1;
            Background = Content.Load<Texture2D>("War/" + z.ToString());

            //Level Ship
            if (spaceship.Maxhp >= 200 && spaceship.Maxhp <= 300)
            {
                LvShip = 2;
            }
            else if (spaceship.Maxhp > 300 && spaceship.Maxhp <= 400)
            {
                LvShip = 3;
            }
            else if (spaceship.Maxhp > 400 && spaceship.Maxhp <= 500)
            {
                LvShip = 4;
            }
            else if (spaceship.Maxhp > 500 && spaceship.Maxhp <= 600)
            {
                LvShip = 5;
            }

            //Obj
            Shot1Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width/2, Shot1.Height/2);
            Shot2Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width / 2, Shot1.Height / 2);
            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair1.Width / 2, Crosshair1.Height / 2);
            SpacshipObj = new Rectangle(50, 200, Spaceship.Width, Spaceship.Width);
            
            //MeteoriteAction
            for (int i = 0; i < 5; i++)
            { 
                MeteoriteObj[i] = new Rectangle(MeteoriteObj[i].X + (i+1), MeteoriteObj[i].Y , Meteorite[i].Width / 3, Meteorite[i].Height / 3);
                //Respawn
                if (MeteoriteObj[i].X > ScreenSize.X)
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
                //ShootAtMeteorite
                if (CrosshairObj.Intersects(MeteoriteObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
                {
                    meteoriteClass[i].Hp -= lasergun.Atk;
                }
                //MeteoriteDestroyed
                if (meteoriteClass[i].Hp <= 0)
                {
                    meteoriteClass[i].Destroy(lasergun);
                    check[i] = true;
                    meteoriteClass[i].Hp = 3;

                }
            }


            //TangAction
            for (int i = 0; i < 6; i++)
            {
                //Speed
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
                //InvaderAttack!
                if (InvaderTangObj[i].X <= 400)
                {
                    InvaderTangObj[i].X = 400;

                    if (timer % 60 == 1)
                    {
                        spaceship.Hp -= invaderTangClass[i].InvaderAtk;
                    }
                }
                //ShootAtInvader
                if(CrosshairObj.Intersects(InvaderTangObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
                {
                    invaderTangClass[i].InvaderHp -= lasergun.Atk;
                }
                //InvaderDead
                if(invaderTangClass[i].InvaderHp <= 0)
                {
                    invaderTangClass[i].Destroy(lasergun);
                    CheckTang[i] = true;
                    invaderTangClass[i].InvaderHp = 10;
                }
            }
            //Bombbing
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && Bombing)
            {
                for (int i = 0; i < 10; i++)
                {
                    //InvaderDead
                    invaderTangClass[i].Destroy(lasergun);
                    CheckTang[i] = true;
                    invaderTangClass[i].InvaderHp = 10;
                    
                }
                for (int i = 0; i < 5; i++)
                {
                    //MeteoriteDestroyed
                    meteoriteClass[i].Destroy(lasergun);
                    check[i] = true;
                    meteoriteClass[i].Hp = 3;
                }
                Bombing = false;
                lasergun.Bomb -= 1;
                if (lasergun.Bomb <= 0) lasergun.Bomb = 0;
            }
            if (timer % 200 == 1)
            {
                Bombing = true;

            }


            //Dont SPAM CLICK
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                CanClick = false;
                lasergun.isShoot();

            }
            if (!CanClick && Mouse.GetState().LeftButton != ButtonState.Pressed)
            {
                CanClick = true;
            }
            //Pass
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Stage1Pass = true;
            if (timer / 60 > 60)
            {
                lasergun.Score += 1000;
                Stage1Pass = true;
            }
            //Failed
            if (spaceship.Hp <= 0) Stage1fail = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            if(Bombing == true && lasergun.Bomb >0)
            {
                spriteBatch.DrawString(Bomb, "Bomb: " + lasergun.Bomb + "  ||| BOMB now!!", BombPos, Color.White);
            }
            else
            {
                
                if (lasergun.Bomb <= 0)
                {
                    spriteBatch.DrawString(Bomb, "Bomb: " + lasergun.Bomb + "  ||| Empty!", BombPos, Color.White);


                }
                else
                {
                    spriteBatch.DrawString(Bomb, "Bomb: " + lasergun.Bomb + "  ||| Reload Bomb!", BombPos, Color.White);
                }
            }
            spriteBatch.DrawString(Clip, "Magazine : " + clip + " / " + clipmax, ClipPos, Color.White);
            spriteBatch.DrawString(ShipHP, "Health : " + lasergun.Score + " / " + shiphpmax, HpPos, Color.White);
            spriteBatch.DrawString(GameTime, "Time : " + timer/60, TimePos, Color.White);
            
            //DrawMeteorite
            for (int i = 0;i < 5; i++)
            {
                 //DIE ?
                if(check[i] == true)
                {
                    spriteBatch.Draw(MeteoriteDestroyed[i], MeteoriteObj[i], Color.White);
                    if (timer % 60 <= 4)
                    {
                        CheckMeteorite[i] = true;
                         check[i] = false;
                    }

                }
                else
                {
                    spriteBatch.Draw(Meteorite[i], MeteoriteObj[i], Color.White);
                }
                //Die ANd Change Position
                if(CheckMeteorite[i] == true)
                {
                        lasergun.Score += 50;
                        MeteoriteObj[i].X = -1000;
                        CheckMeteorite[i] = false;
                }
                    
             }
            //DrawTang
            for (int i = 0; i < 6; i++)
            {
                //Die?
                if (CheckTang[i] == true)
                {
                    spriteBatch.Draw(InvaderDead[i], InvaderTangObj[i], Color.White);
                    InvaderTangObj[i].X += 5;
                    if (timer % 60 <= 4)
                    {
                        CheckInvader[i] = true;
                        CheckTang[i] = false;
                    }
                }
                //NotDie
                else
                {
                    spriteBatch.Draw(Tang2D[i], InvaderTangObj[i], Color.White);
                }
                //If die change position
                if (CheckInvader[i] == true) {
                    lasergun.Score += 20;
                    InvaderTangObj[i].X = 3000;
                    CheckInvader[i] = false;
                }
                //Position to ataack
                if (InvaderTangObj[i].X == 400)
                {
                    if (timer % 60 <= 4)
                    {
                        spriteBatch.Draw(Tang2DShoot[i], InvaderTangObj[i], Color.White);
                    }
                }









                
            }

            spriteBatch.Draw(Spaceship, SpacshipObj, Color.White);


            switch (LvShip)
            {
                case 1:
                    spriteBatch.Draw(Spaceship, SpacshipObj, Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(Spaceship2, SpacshipObj, Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(Spaceship3, SpacshipObj, Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(Spaceship4, SpacshipObj, Color.White);
                    break;
                case 5:
                    spriteBatch.Draw(Spaceship4, SpacshipObj, Color.White);
                    break;

            }
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
