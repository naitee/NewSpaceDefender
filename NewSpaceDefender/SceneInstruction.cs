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
    class SceneInstruction
    {
        ContentManager Content;
        Vector2 ScreenSize;

        SpriteFont Instuctor;
        Texture2D Crosshair;


        public bool Change = false;


        public SceneInstruction(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }

        public void LoadContent()
        {
            Instuctor = Content.Load<SpriteFont>("InstructionPressAnyKey");
            Crosshair = Content.Load<Texture2D>("InstructionCrosshair");

        }

        public void Update(GameTime gametime)
        {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                System.Threading.Thread.Sleep(1000);
                Change = true;
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Crosshair, destinationRectangle: new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Crosshair.Width / 2, Crosshair.Height / 2),
                            origin: new Vector2(Crosshair.Width / 2, Crosshair.Height / 2), color: Color.Red);
            spriteBatch.DrawString(Instuctor, "Think it's INSTRUCTOR", new Vector2(1024 / 2, 768 / 2), Color.White);


        }
    }
}