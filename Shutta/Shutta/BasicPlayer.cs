using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class BasicPlayer :Player
    {
        public BasicPlayer(int name) : base(name)
        {
        }

        public override int CaculateScore()
        {
            //3개의 숫자중족보를 토대로 가장 좋은 조건인 두개의 패를 조합해서 만듬.
                if (Cards[0].IsKwang && Cards[1].IsKwang)
                    return Cards[0].Number + 20;
                else if (Cards[0].Number == Cards[1].Number)
                    return Cards[0].Number + 10; //11 ~20;
                else
                    return (Cards[0].Number + Cards[1].Number) % 10;//3~9            
        }
    }
}
