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

        public void ShoottheSpaceship()
        {
        }

        /* public void isHit()
         {
             if(laserguninvader.shot == true )//&& SpritebatchCrosshair intersecwith(SpritebatchInvader))
             {
                 InvaderHp -= laserguninvader.Atk;
             }
         }

         public void isInvaderDead()
         {
             if (InvaderHp == 0)
             {

             }
         }*/


    }
}