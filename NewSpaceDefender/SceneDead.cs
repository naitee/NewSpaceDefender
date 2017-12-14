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
    class SceneDead
    
    {
        ContentManager Content;
        Vector2 ScreenSize;

        Texture2D dead;
        Texture2D DeadBg;

        SpriteFont GameOver;
        SpriteFont Score;

        CharecterLasergun lasergun;

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
            GameOver = Content.Load<SpriteFont>("DeadGameOver");
            Score = Content.Load<SpriteFont>("DeadScore");
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DeadBg, new Rectangle(0, 0, 1024, 768), Color.White);
            spriteBatch.Draw(dead, new Rectangle(0, 170, dead.Width, dead.Height), Color.White);
            spriteBatch.DrawString(GameOver, "         ~ GAME OVER ~\n(Your EX died.. You NOOB!)", new Vector2(450, 300), Color.WhiteSmoke);
            spriteBatch.DrawString(Score, "Score: " + lasergun.Score , new Vector2(450, 440), Color.WhiteSmoke);
        }
    }
}