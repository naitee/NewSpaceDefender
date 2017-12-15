using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Timers;

namespace NewSpaceDefender
{
    class VictoryScene
    {
        ContentManager Content;
        Vector2 ScreenSize;

        Song VictorySong;

        Texture2D Alive;
        Texture2D AliveBg;
        Texture2D Cursor;
        Texture2D Restart;
        Texture2D Exit;

        SpriteFont RestartFont;
        SpriteFont ExitFont;
        SpriteFont victory;
        SpriteFont Score;

        Rectangle RestartObj;
        Rectangle ExitObj;
        Rectangle CursorObj;


        public bool restart = false;
        public bool exit = false;
        CharecterLasergun lasergun;

        public void updateLasergun(CharecterLasergun lasergun)
        {
            this.lasergun = lasergun;
        }

        public VictoryScene(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }

        public void LoadContent()
        {
            MediaPlayer.Play(VictorySong);
            Alive = Content.Load<Texture2D>("Win");
            AliveBg = Content.Load<Texture2D>("VictoryBg");
            victory = Content.Load<SpriteFont>("Victory");
            Score = Content.Load<SpriteFont>("VictoryScore");
            VictorySong = Content.Load<Song>("VictorySong");

            Restart = Content.Load<Texture2D>("Restart1");
            Exit = Content.Load<Texture2D>("Exit1");
            Cursor = Content.Load<Texture2D>("cursor");
            RestartFont = Content.Load<SpriteFont>("Restart");
            ExitFont = Content.Load<SpriteFont>("Exit");
        }

        public void Update(GameTime gametime)
        {
            RestartObj = new Rectangle(100, 500, Restart.Width, Restart.Height / 2);
            ExitObj = new Rectangle(90, 600, Exit.Width, Exit.Height / 2);
            CursorObj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Cursor.Width / 6, Cursor.Height / 6);

            if (CursorObj.Intersects(RestartObj) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                restart = true;
            }

            if (CursorObj.Intersects(ExitObj) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                exit = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AliveBg, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.Draw(Alive, new Rectangle(300, 170, Alive.Width, Alive.Height), Color.White);
            spriteBatch.DrawString(victory, "~  Victory  ~ \n(Your EX Alive.. Congrat!)", new Vector2(100, 320), Color.WhiteSmoke);
            spriteBatch.DrawString(Score, "Score: " + lasergun.Score, new Vector2(100, 440), Color.WhiteSmoke);

            spriteBatch.Draw(Restart, RestartObj, Color.White);
            spriteBatch.Draw(Exit, ExitObj, Color.White);
            spriteBatch.DrawString(RestartFont, "Play Again", new Vector2(145, 530), Color.White);
            spriteBatch.DrawString(ExitFont, "Exit", new Vector2(200, 650), Color.White);
            spriteBatch.Draw(Cursor, CursorObj, Color.White);

        }
    }
}
