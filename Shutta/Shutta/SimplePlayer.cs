using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class SimplePlayer :Player
    {
        public override int CaculateScore()
        {
            return Cards[0].Number+ Cards[1].Number;   
        }
    }
}
