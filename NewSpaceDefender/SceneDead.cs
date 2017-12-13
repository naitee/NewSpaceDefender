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

        public bool restart = false;

        public SceneDead(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }

        public void LoadContent()
        {
            dead = Content.Load<Texture2D>("DeadLose");
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dead, new Rectangle(0, 0, dead.Width, dead.Height), Color.White);
        }
    }
}