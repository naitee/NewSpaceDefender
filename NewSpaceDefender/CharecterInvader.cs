using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NewSpaceDefender
{
    class CharecterInvader
    {
        public int InvaderHp;
        public int InvaderAtk;


        public CharecterInvader(int invaderhp, int invaderatk)
        {
            InvaderHp = invaderhp;
            InvaderAtk = invaderatk;
        }

        public void Destroy(CharecterLasergun laserGun)
        {
            laserGun.Money += 200;
        }
    }
}