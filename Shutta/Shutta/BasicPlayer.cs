using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class BasicPlayer :Player
    {
        public override int CaculateScore()
        {
            if (Cards[0].IsKwang && Cards[1].IsKwang)
                return Cards[0].Number + 20;
            else if (Cards[0].Number == Cards[1].Number)
                return Cards[0].Number + 10; //11 ~20;
            else
                return (Cards[0].Number + Cards[1].Number) % 10;
            //3~9
            
        }
    }
}
