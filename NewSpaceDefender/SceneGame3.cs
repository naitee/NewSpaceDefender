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
    class SceneGame3
    {
        ContentManager Content;
        Vector2 ScreenSize;

        SpriteFont Clip;
        SpriteFont ShipHP;
        SpriteFont GameTime;
        SpriteFont Bomb;
        SpriteFont ReloadAnnouce;
        SpriteFont GameStart;
        SpriteFont GameClear;

        Texture2D Spaceship;
        Texture2D Spaceship2;
        Texture2D Spaceship3;
        Texture2D Spaceship4;
        Texture2D Crosshair1;
        Texture2D Shot1;
        Texture2D Shot2;
        Texture2D[] Meteorite = new Texture2D[6];
        Texture2D[] MeteoriteDestroyed = new Texture2D[6];
        Texture2D[] Bomb2D = new Texture2D[10];
        Texture2D[] Bomb2DShoot = new Texture2D[10];
        Texture2D[] InvaderDead = new Texture2D[10];
        Texture2D Background;

        Texture2D StartBlog;

        Rectangle Shot1Obj;
        Rectangle Shot2Obj;
        Rectangle CrosshairObj;
        Rectangle SpacshipObj;
        Rectangle[] MeteoriteObj = new Rectangle[6];
        Rectangle[] InvaderBombObj = new Rectangle[10];

        CharecterLasergun lasergun;
        CharecterSpaceship spaceship;
        List<Meteorite> meteoriteClass = new List<Meteorite>();
        List<CharecterInvader> invaderBombClass = new List<CharecterInvader>();

        Vector2 ClipPos = new Vector2(700, 20);
        Vector2 HpPos = new Vector2(20, 20);
        Vector2 TimePos = new Vector2(700, 80);
        Vector2 BombPos = new Vector2(700, 50);


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
        bool[] CheckBomb = new bool[20];

        bool[] CheckMeteorite = new bool[5];
        bool[] CheckInvader = new bool[10];
        public bool Stage3Pass = false;
        public bool Stage3fail = false;
        bool checkBullet = false;
        public SceneGame3(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();

            //Meteorite
            for (int i = 0; i < 5; i++)
            {
                MeteoriteObj[i] = new Rectangle(-1000, 500 + 20 * i, Meteorite[i].Width / 15, Meteorite[i].Height / 15);
                check[i] = false;
            }

            //Bomb
            for (int i = 0; i < 6; i++)
            {
                InvaderBombObj[i] = new Rectangle(3000, 100 + 50 * i, Bomb2D[i].Width / 3, Bomb2D[i].Height / 3);
                CheckBomb[i] = false;
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

            StartBlog = Content.Load<Texture2D>("3");

            //MeteoriteTexture2D
            for (int i = 0; i < 5; i++)
            {
                Meteorite[i] = Content.Load<Texture2D>("Game1Meteorite");
            }
            for (int i = 0; i < 5; i++)
            {
                MeteoriteDestroyed[i] = Content.Load<Texture2D>("Game1MeteoriteDestroy");
            }
            //InvaderBombTexture2D
            for (int i = 0; i < 10; i++)
            {
                Bomb2D[i] = Content.Load<Texture2D>("BombSS");
            }
            for (int i = 0; i < 10; i++)
            {
                Bomb2DShoot[i] = Content.Load<Texture2D>("BombCC");
            }
            for (int i = 0; i < 10; i++)
            {
                InvaderDead[i] = Content.Load<Texture2D>("skull");
            }

            GameStart = Content.Load<SpriteFont>("StageStart");
            GameClear = Content.Load<SpriteFont>("StageClear");
            ReloadAnnouce = Content.Load<SpriteFont>("ReloadAnnouce");
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
        public void updateInvaderBomb(List<CharecterInvader> InvaderBomb)
        {
            invaderBombClass = InvaderBomb;
        }

        public void Update(GameTime gametime)
        {
            timer = timer + 1;
            clip = lasergun.clipleft;
            clipmax = lasergun.ClipMax;
            shiphp = spaceship.Hp;
            shiphpmax = spaceship.Maxhp;

            if (clip == 0)
            {
                checkBullet = true;
            }
            else
            {
                checkBullet = false;
            }

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
            Shot1Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width / 2, Shot1.Height / 2);
            Shot2Obj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Shot1.Width / 2, Shot1.Height / 2);
            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair1.Width / 2, Crosshair1.Height / 2);
            SpacshipObj = new Rectangle(50, 200, Spaceship.Width, Spaceship.Width);

            //MeteoriteAction
            for (int i = 0; i < 5; i++)
            {
                MeteoriteObj[i] = new Rectangle(MeteoriteObj[i].X + (i + 1), MeteoriteObj[i].Y, Meteorite[i].Width / 3, Meteorite[i].Height / 3);
                //Respawn
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


            //BombAction
            for (int i = 0; i < 6; i++)
            {
                //Speed
                switch (i % 3)
                {
                    case 0:
                        speed = 2;
                        break;
                    case 1:
                        speed = 3;
                        break;
                    case 2:
                        speed = 4;
                        break;
                }
                InvaderBombObj[i] = new Rectangle((InvaderBombObj[i].X - speed), InvaderBombObj[i].Y, Bomb2D[i].Width / 3, Bomb2D[i].Height / 3);
                
                //InvaderAttack!
                if (InvaderBombObj[i].X <= 400)
                {
                    InvaderBombObj[i].X = 400;

                    if (timer % 60 == 1)
                    {
                        spaceship.Hp -= invaderBombClass[i].InvaderAtk;
                    }
                }
                //ShootAtInvader
                if (CrosshairObj.Intersects(InvaderBombObj[i]) && clip != 0 && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
                {
                    invaderBombClass[i].InvaderHp -= lasergun.Atk;
                }
                //InvaderDead
                if (invaderBombClass[i].InvaderHp <= 0)
                {
                    lasergun.Money += 300;
                    CheckBomb[i] = true;
                    invaderBombClass[i].InvaderHp = invaderBombClass[i].InvaderHpMax;
                }
            }


            //Bombbing
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Bombing)
            {
                for (int i = 0; i < 10; i++)
                {
                    //InvaderDead
                    invaderBombClass[i].Destroy(lasergun);
                    CheckBomb[i] = true;
                    invaderBombClass[i].InvaderHp = 10;

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
            if (timer % 200 == 1 && lasergun.Bomb != 0)
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Stage3Pass = true;
            if (timer / 60 > 122)
            {
                lasergun.Score += 1000 * spaceship.Hp;
                Stage3Pass = true;
            }
            //Failed
            if (spaceship.Hp <= 0) Stage3fail = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            if (Bombing == true && lasergun.Bomb > 0)
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
            spriteBatch.DrawString(ShipHP, "Health : " + shiphp + " / " + shiphpmax, HpPos, Color.White);
            spriteBatch.DrawString(GameTime, "Time : " + timer / 60, TimePos, Color.White);

            //DrawMeteorite
            for (int i = 0; i < 5; i++)
            {
                //DIE ?
                if (check[i] == true)
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
                if (CheckMeteorite[i] == true)
                {
                    lasergun.Score += 50;
                    MeteoriteObj[i].X = -1000;
                    CheckMeteorite[i] = false;
                }

            }
            //DrawBomb
            for (int i = 0; i < 6; i++)
            {
                //Die?
                if (CheckBomb[i] == true)
                {
                    spriteBatch.Draw(InvaderDead[i], InvaderBombObj[i], Color.White);
                    InvaderBombObj[i].X += 5;
                    if (timer % 60 <= 4)
                    {
                        CheckInvader[i] = true;
                        CheckBomb[i] = false;
                    }
                }
                //NotDie
                else
                {
                    spriteBatch.Draw(Bomb2D[i], InvaderBombObj[i], Color.White);
                }
                //If die change position
                if (CheckInvader[i] == true)
                {
                    lasergun.Score += 300;
                    InvaderBombObj[i].X = 3000;
                    CheckInvader[i] = false;
                }
                //Position to ataack
                if (InvaderBombObj[i].X == 400)
                {
                    if (timer % 60 <= 4)
                    {
                        spriteBatch.Draw(Bomb2DShoot[i], InvaderBombObj[i], Color.White);
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

           

            if (checkBullet == true)
            {
                if ((timer / 30) % 2 == 1 / 2)
                    spriteBatch.DrawString(ReloadAnnouce, " Quick Pressed to RELOADING!", new Vector2(10, 80), Color.Red);
            }

            //ShootingAnimation
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && clip != 0)
            {
                spriteBatch.Draw(Shot2, Shot2Obj, Color.White);
            }
            else
            {
                spriteBatch.Draw(Shot1, Shot1Obj, Color.White);
            }


            if (timer / 60 < 3)
            {
                spriteBatch.Draw(StartBlog, new Rectangle(110, 200, StartBlog.Width, StartBlog.Height), Color.White);
                spriteBatch.DrawString(GameStart, "  STAGE 3 \n ~START~", new Vector2(375, 300), Color.White);
            }
            if (timer / 60 <= 122 && timer / 60 > 120)
            {
                spriteBatch.Draw(StartBlog, new Rectangle(110, 200, StartBlog.Width, StartBlog.Height), Color.White);
                spriteBatch.DrawString(GameStart, "  STAGE 3 \n ~CLEAR~", new Vector2(375, 300), Color.White);
                for (int i = 0; i < 10; i++)
                {
                    //InvaderDead
                    CheckBomb[i] = true;
                }
                for (int i = 0; i < 5; i++)
                {
                    //MeteoriteDestroyed
                    check[i] = true;
                }
            }
        }
    }

}
