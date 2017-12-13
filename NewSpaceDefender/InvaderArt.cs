using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpaceDefender
{
    class InvaderArt : CharecterInvader
    {
        public InvaderArt(int ArtHp, int ArtAtk) : base(ArtHp, ArtAtk )
        {
            ArtAtk = 1;
            ArtHp = 20;
        }
    }
}
