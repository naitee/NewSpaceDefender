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
    class Shop4
    {
        ContentManager Content;
        Vector2 ScreenSize;

        Texture2D MechanicBoy;
        Texture2D BackGround;
        Texture2D Crosshair;
        Texture2D ShopSpeech;

        Texture2D UpgradeAttack;
        Texture2D UpgradeClip;
        Texture2D UpgradeReload;
        Texture2D UpgradeBomb;
        Texture2D UpgradeFix;
        Texture2D UpgradeShip1;
        Texture2D UpgradeShip2;
        Texture2D UpgradeShip3;
        Texture2D UpgradeShip4;
        Texture2D UpgradeOutofStock;

        SpriteFont AttackPrize;
        SpriteFont ClipPrize;
        SpriteFont ReloadPrize;
        SpriteFont BombPrize;
        SpriteFont FixPrize;
        SpriteFont ShipPrize;
        SpriteFont seeAtk;
        SpriteFont seeClip;
        SpriteFont seeReload;
        SpriteFont seeBomb;
        SpriteFont seeMoney;
        SpriteFont seeHp;
        SpriteFont ShopSpeech1;


        Rectangle MechanicBoyObj;
        Rectangle CrosshairObj;
        Rectangle ShopSpeechObj;
        Rectangle UpgradeClipObj;
        Rectangle UpgradeAttackObj;
        Rectangle UpgradeReloadObj;
        Rectangle UpgradeBombObj;
        Rectangle UpgradeFixObj;
        Rectangle UpgradeShip1Obj;
        Rectangle UpgradeShip2Obj;
        Rectangle UpgradeShip3Obj;
        Rectangle UpgradeShip4Obj;
        Rectangle UpgradeOutofStockObj;

        CharecterLasergun lasergun;
        CharecterSpaceship spaceship;

        int LvShip = 1;
        public bool ChangeStageToFinal = false;
        bool Pruet = false;
        bool CanClick = true;


        public Shop4(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();

        }

        public void LoadContent()
        {
            MechanicBoy = Content.Load<Texture2D>("ShopMechanicBoy");
            BackGround = Content.Load<Texture2D>("ShopBackground");
            Crosshair = Content.Load<Texture2D>("ShopCrosshair");
            ShopSpeech = Content.Load<Texture2D>("ShopSpeech");

            AttackPrize = Content.Load<SpriteFont>("ShopPrizeAttack");
            ClipPrize = Content.Load<SpriteFont>("ShopPrizeClip");
            ReloadPrize = Content.Load<SpriteFont>("ShopPrizeReload");
            BombPrize = Content.Load<SpriteFont>("ShopPrizeBomb");
            FixPrize = Content.Load<SpriteFont>("ShopPrizeFix");
            ShipPrize = Content.Load<SpriteFont>("ShopPrizeShip");

            seeAtk = Content.Load<SpriteFont>("ShopSeeAtk");
            seeClip = Content.Load<SpriteFont>("ShopSeeClip");
            seeReload = Content.Load<SpriteFont>("ShopSeeReload");
            seeBomb = Content.Load<SpriteFont>("ShopseeBomb");
            seeMoney = Content.Load<SpriteFont>("ShopseeMoney");
            seeHp = Content.Load<SpriteFont>("ShopseeHp");
            ShopSpeech1 = Content.Load<SpriteFont>("ShopSpeech1");

            UpgradeAttack = Content.Load<Texture2D>("ShopUpgradeAttack");
            UpgradeClip = Content.Load<Texture2D>("ShopUpgradeClip");
            UpgradeReload = Content.Load<Texture2D>("ShopUpgradeReload");
            UpgradeBomb = Content.Load<Texture2D>("ShopUpgradeBomb");
            UpgradeFix = Content.Load<Texture2D>("ShopUpgradeFixed");
            UpgradeShip1 = Content.Load<Texture2D>("ShopUpgradeShip1");
            UpgradeShip2 = Content.Load<Texture2D>("ShopUpgradeShip2");
            UpgradeShip3 = Content.Load<Texture2D>("ShopUpgradeShip3");
            UpgradeShip4 = Content.Load<Texture2D>("ShopUpgradeShip4");
            UpgradeOutofStock = Content.Load<Texture2D>("ShopUpgradeOutofStock");

        }

        public void updateLasergun(CharecterLasergun lasergun)
        {
            this.lasergun = lasergun;
        }
        public void updateSpaceship(CharecterSpaceship spaceship)
        {
            this.spaceship = spaceship;
        }

        public void Update(GameTime gametime)
        {
            //Object
            MechanicBoyObj = new Rectangle(-10, 400, MechanicBoy.Width / 3 + 70, MechanicBoy.Height / 2);
            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair.Width / 2, Crosshair.Height / 2);
            ShopSpeechObj = new Rectangle(280, 500, ShopSpeech.Width / 4, ShopSpeech.Height / 12);
            UpgradeAttackObj = new Rectangle(130, 60, UpgradeAttack.Width, UpgradeAttack.Height);
            UpgradeClipObj = new Rectangle(330, 60, UpgradeClip.Width, UpgradeClip.Height);
            UpgradeReloadObj = new Rectangle(530, 60, UpgradeReload.Width, UpgradeReload.Height);
            UpgradeBombObj = new Rectangle(730, 60, UpgradeBomb.Width, UpgradeBomb.Height);
            UpgradeFixObj = new Rectangle(530, 360, UpgradeFix.Width, UpgradeFix.Height);
            UpgradeShip1Obj = new Rectangle(730, 360, UpgradeShip2.Width, UpgradeShip2.Height);
            UpgradeShip2Obj = new Rectangle(730, 360, UpgradeShip2.Width, UpgradeShip2.Height);
            UpgradeShip3Obj = new Rectangle(730, 360, UpgradeShip2.Width, UpgradeShip2.Height);
            UpgradeShip4Obj = new Rectangle(730, 360, UpgradeShip2.Width, UpgradeShip2.Height);
            UpgradeOutofStockObj = new Rectangle(730, 360, UpgradeOutofStock.Width, UpgradeOutofStock.Height);



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



            //Atk
            if (CrosshairObj.Intersects(UpgradeAttackObj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int atk = 2;
                if (lasergun.Money >= 500)
                {
                    lasergun.Money -= 500;
                    lasergun.Atk += atk;

                }
            }
            //Clip
            if (CrosshairObj.Intersects(UpgradeClipObj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int clipshop = 5;
                if (lasergun.Money >= 1000)
                {
                    lasergun.Money -= 1000;
                    lasergun.ClipMax += clipshop;

                }
            }
            //Reload
            if (CrosshairObj.Intersects(UpgradeReloadObj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                double reloadshop = 0.2;
                if (lasergun.Money >= 2000)
                {
                    lasergun.Money -= 2000;
                    lasergun.Reload -= reloadshop;

                }

            }
            //Bomb
            if (CrosshairObj.Intersects(UpgradeBombObj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int Bomb = 1;
                if (lasergun.Money >= 200)
                {
                    lasergun.Money -= 200;
                    lasergun.Bomb += Bomb;

                }

            }

            //Eater Egg
            if (CrosshairObj.Intersects(MechanicBoyObj) && CanClick)
            {
                Pruet = true;
            }
            else
            {
                Pruet = false;
            }


            //Fixed
            if (CrosshairObj.Intersects(UpgradeFixObj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int Hpfix = 30;
                if (lasergun.Money >= 500)
                {
                    lasergun.Money -= 500;
                    spaceship.Hp += Hpfix;
                    if (spaceship.Hp > spaceship.Maxhp)
                    {
                        spaceship.Hp = spaceship.Maxhp;
                        lasergun.Money += 500;
                    }

                }

            }
            //Upgrade1
            if (CrosshairObj.Intersects(UpgradeShip1Obj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int HpMax = 100;

                if (lasergun.Money >= 1000 && LvShip == 1)
                {
                    lasergun.Money -= 1000;
                    spaceship.Maxhp += HpMax;
                    spaceship.Hp = spaceship.Maxhp;
                }
            }
            //Upgrade2
            if (CrosshairObj.Intersects(UpgradeShip2Obj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int HpMax = 100;

                if (lasergun.Money >= 1000 && LvShip == 2)
                {
                    lasergun.Money -= 1000;
                    spaceship.Maxhp += HpMax;
                    spaceship.Hp = spaceship.Maxhp;
                }
            }
            //Upgrade3
            if (CrosshairObj.Intersects(UpgradeShip3Obj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int HpMax = 100;

                if (lasergun.Money >= 1000 && LvShip == 3)
                {
                    lasergun.Money -= 1000;
                    spaceship.Maxhp += HpMax;
                    spaceship.Hp = spaceship.Maxhp;
                }
            }
            //Upgrade4
            if (CrosshairObj.Intersects(UpgradeShip4Obj) && Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                int HpMax = 100;

                if (lasergun.Money >= 1000 && LvShip == 4)
                {
                    lasergun.Money -= 1000;
                    spaceship.Maxhp += HpMax;
                    spaceship.Hp = spaceship.Maxhp;
                }
            }


            ///Dont SPAM CLICK
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && CanClick)
            {
                CanClick = false;
            }
            if (!CanClick && Mouse.GetState().LeftButton != ButtonState.Pressed)
            {
                CanClick = true;
            }

            //Finished Shopping
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                ChangeStageToFinal = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(BackGround, new Rectangle(0, 0, MechanicBoy.Width + 224, MechanicBoy.Height + 20), Color.White);
            spriteBatch.Draw(MechanicBoy, MechanicBoyObj, Color.White);

            spriteBatch.DrawString(seeAtk, "Atk : " + lasergun.Atk, new Vector2(420, 670), Color.White);
            spriteBatch.DrawString(seeClip, "Clip: " + lasergun.ClipMax, new Vector2(420, 720), Color.White);
            spriteBatch.DrawString(seeReload, "Reload: " + lasergun.Reload, new Vector2(550, 670), Color.White);
            spriteBatch.DrawString(seeBomb, "Bomb  : " + lasergun.Bomb, new Vector2(550, 720), Color.White);
            spriteBatch.DrawString(seeMoney, "Money: " + lasergun.Money, new Vector2(730, 720), Color.White);
            spriteBatch.DrawString(seeHp, "Hp : " + spaceship.Hp + "/" + spaceship.Maxhp, new Vector2(730, 670), Color.White);

            spriteBatch.Draw(UpgradeAttack, UpgradeAttackObj, Color.White);
            spriteBatch.Draw(UpgradeClip, UpgradeClipObj, Color.White);
            spriteBatch.Draw(UpgradeReload, UpgradeReloadObj, Color.White);
            spriteBatch.Draw(UpgradeBomb, UpgradeBombObj, Color.White);


            spriteBatch.Draw(UpgradeFix, UpgradeFixObj, Color.White);

            switch (LvShip)
            {
                case 1:
                    spriteBatch.Draw(UpgradeShip1, UpgradeShip1Obj, Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(UpgradeShip2, UpgradeShip2Obj, Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(UpgradeShip3, UpgradeShip3Obj, Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(UpgradeShip4, UpgradeShip4Obj, Color.White);
                    break;
                case 5:
                    spriteBatch.Draw(UpgradeOutofStock, UpgradeOutofStockObj, Color.White);
                    break;
            }

            spriteBatch.DrawString(AttackPrize, "500", new Vector2(200, 245), Color.White);
            spriteBatch.DrawString(ClipPrize, "1000", new Vector2(400, 245), Color.White);
            spriteBatch.DrawString(ReloadPrize, "2000", new Vector2(600, 245), Color.White);
            spriteBatch.DrawString(BombPrize, "200", new Vector2(800, 245), Color.White);
            spriteBatch.DrawString(FixPrize, "500", new Vector2(600, 545), Color.White);
            spriteBatch.DrawString(ShipPrize, "1000", new Vector2(800, 545), Color.White);
            if (Pruet == true)
            {
                spriteBatch.Draw(ShopSpeech, ShopSpeechObj, Color.White);
                spriteBatch.DrawString(ShopSpeech1, "It's SHOPPING TIME \nFor Final STAGE!!", new Vector2(400, 524), Color.Red);
            }
            spriteBatch.Draw(Crosshair, CrosshairObj, Color.Red);
        }
    }
}