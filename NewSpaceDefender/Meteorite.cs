using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpaceDefender
{
    class Meteorite
    {
        public int Hp;


        public Meteorite(int hp)
        {
            Hp = hp;

        }

        public void isShoot()
        {
            Hp -= 1;
        }

        public void Destroy(CharecterLasergun laserGun)
        {
            laserGun.Money += 20;
        }
    }
}