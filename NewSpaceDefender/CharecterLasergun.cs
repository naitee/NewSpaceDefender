using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace NewSpaceDefender
{
    class CharecterLasergun
    {
        public bool bombing = false;
        public bool shot = false;
        public int clipleft;

        /// <summary>
        /// Get Time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        String timeStamp;

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public int Atk;
        public int ClipMax;
        public int Money;
        public double Reload;
        public int Bomb;
        public CharecterLasergun(int atk, int clipmax, double reload, int money, int bomb)
        {
            Atk = atk;
            ClipMax = clipmax;
            Reload = reload;
            Money = money;
            Bomb = bomb;
        }



        /// <summary>
        /// Check the LaserGun is shot !
        /// </summary>
        public bool isShoot()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (clipleft == 0)
                {

                    string currentTime = GetTimestamp(DateTime.Now);

                    if (Convert.ToInt64(currentTime) > Convert.ToInt64(timeStamp) + (Reload * 10000))

                        clipleft += ClipMax;
                    shot = false;

                }
                else
                {
                    timeStamp = GetTimestamp(DateTime.Now);
                    while (!(clipleft == 0))
                    {
                        clipleft -= 1;
                        break;
                    }
                    shot = true;

                }
            }
            return shot;
        }

        public void ClearMapBomb()
        {
            if (Keyboard.GetState().IsKeyDown(key: Keys.Space))
            {
                if (Bomb > 0)
                {
                    bombing = true;
                    Bomb -= 1;
                }
                else
                {
                    bombing = false;
                }

            }
        }
    }
}