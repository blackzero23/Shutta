using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class SimplePlayer :Player
    {
        public SimplePlayer(int name) : base(name)
        {
        }

        public override void CaculateScore()
        {
            //return Cards[0].Number+ Cards[1].Number;   
        }
    }
}
