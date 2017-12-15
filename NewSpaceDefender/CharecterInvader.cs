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
        public int InvaderHpMax;
        public int InvaderAtk;


        public CharecterInvader(int invaderhp,int invaderhpmax, int invaderatk)
        {
            InvaderHp = invaderhp;
            InvaderHpMax = invaderhpmax;
            InvaderAtk = invaderatk;
        }

        public void Destroy(CharecterLasergun laserGun)
        {
            laserGun.Money += 300;
        }
    }
}