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
    class SceneDead
    
    {
        ContentManager Content;
        Vector2 ScreenSize;

        Song DeadSongLose;

        Texture2D dead;
        Texture2D DeadBg;
        Texture2D Restart;
        Texture2D Exit;
        Texture2D Cursor;

        SpriteFont RestartFont;
        SpriteFont ExitFont;
        SpriteFont GameOver;
        SpriteFont Score;

        Rectangle RestartObj;
        Rectangle ExitObj;
        Rectangle CursorObj;

        CharecterLasergun lasergun;

        public bool restart = false;
        public bool exit = false;

        public void updateLasergun(CharecterLasergun lasergun)
        {
            this.lasergun = lasergun;
        }

        public SceneDead(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }

        public void LoadContent()
        {
            dead = Content.Load<Texture2D>("DeadLose2");
            DeadBg = Content.Load<Texture2D>("graveyard");
            Restart = Content.Load<Texture2D>("RestartPicture");
            Exit = Content.Load<Texture2D>("ExitPicture");
            Cursor = Content.Load<Texture2D>("cursor");

            GameOver = Content.Load<SpriteFont>("DeadGameOver");
            Score = Content.Load<SpriteFont>("DeadScore");
            RestartFont = Content.Load<SpriteFont>("Restart");
            ExitFont = Content.Load<SpriteFont>("Exit");

            DeadSongLose = Content.Load<Song>("DeadSong");
            MediaPlayer.Play(DeadSongLose);
        }

        public void Update(GameTime gametime)
        {
            RestartObj = new Rectangle(700, 500, Restart.Width, Restart.Height/2);
            ExitObj = new Rectangle(690,600, Exit.Width, Exit.Height/2);
            CursorObj = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Cursor.Width/6, Cursor.Height/6);

            if(CursorObj.Intersects(RestartObj)&&Mouse.GetState().LeftButton == ButtonState.Pressed)
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
            spriteBatch.Draw(DeadBg, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.Draw(dead, new Rectangle(0, 170, dead.Width, dead.Height), Color.White);
            spriteBatch.DrawString(GameOver, "         ~ GAME OVER ~\n(Your EX died.. You NOOB!)", new Vector2(450, 300), Color.WhiteSmoke);
            spriteBatch.DrawString(Score, "Score: " + lasergun.Score , new Vector2(450, 440), Color.WhiteSmoke);

            spriteBatch.Draw(Restart, RestartObj, Color.White);
            spriteBatch.Draw(Exit, ExitObj, Color.White);
            spriteBatch.DrawString(RestartFont, "Play Again",new Vector2(745,530),Color.White);
            spriteBatch.DrawString(ExitFont, "Exit", new Vector2(800, 650), Color.White);
            spriteBatch.Draw(Cursor, CursorObj, Color.White);
        }
    }
}