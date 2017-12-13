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

        Texture2D Spaceship;
        Texture2D Crosshair1;



        Rectangle CrosshairObj;
        Rectangle SpacshipObj;

        CharecterLasergun lasergun;
        CharecterSpaceship spaceship;




        Vector2 ClipPos = new Vector2(680, 20);
        Vector2 HpPos = new Vector2(200, 20);

        int timer = 0;
        int clip;
        int clipmax;
        int shiphp;
        int shiphpmax;
        bool CanClick = false;

        public bool Stage1Pass = false;
        public bool Stage1fail = false;




        public SceneGame1(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
            
        }


        public void LoadContent()
        {
            Spaceship = Content.Load<Texture2D>("Game1Blade");
            Crosshair1 = Content.Load<Texture2D>("Game1Crosshair");


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

   

        public void Update(GameTime gametime)
        {
            timer = timer + 1;


            CrosshairObj = new Rectangle(Mouse.GetState().X - 50, Mouse.GetState().Y - 50, Crosshair1.Width / 2, Crosshair1.Height / 2);
            SpacshipObj = new Rectangle(50, 200, Spaceship.Width, Spaceship.Width);

            clip = lasergun.clipleft;
            clipmax = lasergun.ClipMax;

            shiphp = spaceship.Hp;
            shiphpmax = spaceship.Maxhp;

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
            if (timer/60 > 5) Stage1Pass = true;

            ///Failed
            if (spaceship.Hp == 0) Stage1fail = true;

            
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(Clip, "Magazine : " + timer/60+ " / " + clipmax, ClipPos, Color.White);
            spriteBatch.DrawString(ShipHP, "Health : " + shiphp + " / " + shiphpmax, HpPos, Color.White);

            spriteBatch.Draw(Spaceship, SpacshipObj, Color.White);
            spriteBatch.Draw(Crosshair1, CrosshairObj, Color.Red);

        }


    }
}
