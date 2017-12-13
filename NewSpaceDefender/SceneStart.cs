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
    class SceneStart
    {
        ContentManager Content;
        Vector2 ScreenSize;

        Texture2D BulletHoles;
        Texture2D StartBackground;
        Texture2D Crosshair;

        SpriteFont PressAnyKeyToStart;
        SpriteFont Title;

        Vector2 TitlePos;
        Vector2 PressPos;

        String timeStamp;
        String currentTime;

        Timer Swap;
        bool SwapVa = true;



        public bool Gamestart = false;
        public bool ChangeMap = false;


        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public SceneStart(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }

        public void LoadContent()
        {
            //Load
            StartBackground = Content.Load<Texture2D>("SceneStartBackground");
            PressAnyKeyToStart = Content.Load<SpriteFont>("SceneStartPressAnyKey");
            Title = Content.Load<SpriteFont>("SceneStartTitle");
            BulletHoles = Content.Load<Texture2D>("SceneStartHole");
            Crosshair = Content.Load<Texture2D>("SceneStartCrosshair");

            //Set Position
            TitlePos = new Vector2(0, 150);
            PressPos = new Vector2(700, 150);

            //Swap Medthod
            Swap = new Timer(500);
            Swap.Elapsed += SwapMethod;
            Swap.Enabled = false;

        }

        public void Update(GameTime gametime)
        {

            if (!(TitlePos.X == 380) && !(PressPos.Y == 230))
            {
                TitlePos.X += 5;
                PressPos.Y += 1;
                Swap.Enabled = true;
            }

            //Gamestart
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                timeStamp = GetTimestamp(DateTime.Now);
                TitlePos.X = 380;
                PressPos.Y = 230;
                Gamestart = true;
            }
            currentTime = GetTimestamp(DateTime.Now);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(StartBackground, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.DrawString(Title, "SPACE DEFENDER", TitlePos, Color.White);
            spriteBatch.Draw(Crosshair, destinationRectangle: new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Crosshair.Width / 2, Crosshair.Height / 2)
                            , origin: new Vector2(Crosshair.Width / 2, Crosshair.Height / 2), color: Color.Red);
            if (SwapVa)
            {
                spriteBatch.DrawString(PressAnyKeyToStart, "Shoot Anywhere to start!", (PressPos), Color.White);
            }
            if (Gamestart == true)
            {
                //BulletHoles
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp))
                    spriteBatch.Draw(BulletHoles, new Rectangle(354, 532, BulletHoles.Width, BulletHoles.Height), Color.White);
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + 400)
                    spriteBatch.Draw(BulletHoles, new Rectangle(425, 321, BulletHoles.Width, BulletHoles.Height), Color.White);
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + 800)
                    spriteBatch.Draw(BulletHoles, new Rectangle(163, 351, BulletHoles.Width, BulletHoles.Height), Color.White);
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + 1200)
                    spriteBatch.Draw(BulletHoles, new Rectangle(277, 245, BulletHoles.Width, BulletHoles.Height), Color.White);
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + 1600)
                    spriteBatch.Draw(BulletHoles, new Rectangle(564, 185, BulletHoles.Width, BulletHoles.Height), Color.White);
                if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + 10000)
                    ChangeMap = true;

            }


        }

        private void SwapMethod(Object source, ElapsedEventArgs e)
        {
            if (SwapVa == true && Gamestart == false) SwapVa = false;
            else SwapVa = true;
        }
    }
}